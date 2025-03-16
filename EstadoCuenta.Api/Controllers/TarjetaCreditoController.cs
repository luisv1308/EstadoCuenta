using AutoMapper;
using EstadoCuenta.Api.CQRS.Commands;
using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Filters;
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
       
        public TarjetaCreditoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
        [ServiceFilter(typeof(ValidationFilter<TarjetaCreditoDTO>))]
        public async Task<IActionResult> CrearTarjeta([FromBody] TarjetaCreditoDTO tarjetaDTO)
        {
            var command = new CreateTarjetaCommand(tarjetaDTO.Titular, tarjetaDTO.NumeroTarjeta, tarjetaDTO.LimiteCredito);
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(ObtenerTarjeta), new { id }, id);
        }
    }
}
