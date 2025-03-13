using AutoMapper;
using EstadoCuenta.Api.CQRS.Commands;
using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.DTOs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IValidator<TransaccionDTO> _validator;

        public TransaccionController(IMediator mediator, IMapper mapper, IValidator<TransaccionDTO> validator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerTransacciones(int tarjetaId)
        {
            var transacciones = await _mediator.Send(new GetTransaccionesByTarjetaQuery(tarjetaId));
            if (transacciones == null)
                return NotFound();

            var transaccionesDTO = _mapper.Map<List<TransaccionDTO>>(transacciones);

            return Ok(transaccionesDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CrearTransaccion([FromBody] TransaccionDTO transaccionDTO)
        {
            var validationResult = await _validator.ValidateAsync(transaccionDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new CreateTransaccionCommand(transaccionDTO.TarjetaCreditoId, transaccionDTO.Descripcion, transaccionDTO.Monto, transaccionDTO.Fecha, transaccionDTO.Tipo);
            var transaccionId = await _mediator.Send(command);

            return CreatedAtAction(nameof(ObtenerTransacciones), new { tarjetaId = transaccionDTO.TarjetaCreditoId }, transaccionId);
        }
    }
}
