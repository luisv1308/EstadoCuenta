using EstadoCuenta.Api.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerCompras(int tarjetaId)
        {
            var compras = await _mediator.Send(new GetComprasByTarjetaQuery(tarjetaId));
            return Ok(compras);
        }
    }
}
