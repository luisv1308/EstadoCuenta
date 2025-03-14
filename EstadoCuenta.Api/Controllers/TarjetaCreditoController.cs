using AutoMapper;
using EstadoCuenta.Api.CQRS.Commands;
using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarjetaCreditoController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IValidator<TarjetaCreditoDTO> _validator;
       
        public TarjetaCreditoController(IMediator mediator, IMapper mapper, IValidator<TarjetaCreditoDTO> validator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTarjeta(int id)
        {
            var tarjeta = await _mediator.Send(new GetTarjetaByIdQuery(id));
            if (tarjeta == null)
                return NotFound();

            var tarjetaDTO = _mapper.Map<TarjetaCreditoDTO>(tarjeta);

            return Ok(tarjetaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CrearTarjeta([FromBody] TarjetaCreditoDTO tarjetaDTO)
        {
            var validationResult = await _validator.ValidateAsync(tarjetaDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new CreateTarjetaCommand(tarjetaDTO.Titular, tarjetaDTO.NumeroTarjeta, tarjetaDTO.LimiteCredito);
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(ObtenerTarjeta), new { id }, id);
        }

        [HttpGet("{id}/export/pdf")]
        public async Task<IActionResult> ExportarEstadoCuentaPdf(int id, [FromServices] PdfService pdfService)
        {
            var estadoCuenta = await _mediator.Send(new GetEstadoCuentaByTarjetaQuery(id));
            if (estadoCuenta == null) return NotFound();
            
            var estadoCuentaDTO = _mapper.Map<EstadoCuentaDTO>(estadoCuenta);
            if (estadoCuentaDTO == null) return NotFound();

            var transacciones = await _mediator.Send(new GetTransaccionesByTarjetaQuery(id));
            var pdfBytes = pdfService.GenerarEstadoCuentaPdf(estadoCuentaDTO, transacciones);

            return File(pdfBytes, "application/pdf", $"EstadoCuenta_{estadoCuenta.Titular}.pdf");
        }

        [HttpGet("{id}/export/excel")]
        public async Task<IActionResult> ExportarEstadoCuentaExcel(int id, [FromServices] ExcelService excelService)
        {
            var estadoCuenta = await _mediator.Send(new GetEstadoCuentaByTarjetaQuery(id));
            if (estadoCuenta == null) return NotFound();

            var estadoCuentaDTO = _mapper.Map<EstadoCuentaDTO>(estadoCuenta);
            if (estadoCuentaDTO == null) return NotFound();

            var transacciones = await _mediator.Send(new GetTransaccionesByTarjetaQuery(id));
            var excelBytes = excelService.GenerarEstadoCuentaExcel(estadoCuentaDTO, transacciones);

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"EstadoCuenta_{estadoCuenta.Titular}.xlsx");
        }
    }
}
