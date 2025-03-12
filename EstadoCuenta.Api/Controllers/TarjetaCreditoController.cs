using AutoMapper;
using EstadoCuenta.Api.CQRS.Commands;
using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.DTOs;
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

            return Ok(tarjeta);
        }

        [HttpPost]
        public async Task<IActionResult> CrearTarjeta([FromBody] CreateTarjetaCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObtenerTarjeta), new { id }, id);
        }
    }
}
