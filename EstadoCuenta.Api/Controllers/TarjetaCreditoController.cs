using EstadoCuenta.Api.CQRS.Commands;
using EstadoCuenta.Api.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarjetaCreditoController : ControllerBase
    {
       private readonly IMediator _mediator;
        public TarjetaCreditoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTarjeta(int id)
        {
            var tarjeta = await _mediator.Send(new GetTarjetaByIdQuery(id));
            if (tarjeta == null)
                return NotFound();

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
