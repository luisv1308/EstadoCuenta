using EstadoCuenta.Api.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PagosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerPagos(int tarjetaId)
        {
            var pagos = await _mediator.Send(new GetPagosByTarjetaQuery(tarjetaId));
            return Ok(pagos);
        }
    }
}
