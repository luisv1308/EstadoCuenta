using EstadoCuenta.Api.CQRS.Commands;
using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Hubs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EstadoCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<TransaccionesHub> _hubContext;
        private readonly IValidator<ComprasDTO> _validator;

        public ComprasController(IMediator mediator, IHubContext<TransaccionesHub> hubContext, IValidator<ComprasDTO> validator)
        {
            _mediator = mediator;
            _hubContext = hubContext;
            _validator = validator;
        }

        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerCompras(int tarjetaId)
        {
            var compras = await _mediator.Send(new GetComprasByTarjetaQuery(tarjetaId));
            return Ok(compras);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarCompra([FromBody] ComprasDTO transaccion)
        {
            var validationResult = await _validator.ValidateAsync(transaccion);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            transaccion.Tipo = "Compra";
            var command = new CreateCompraCommand(transaccion.TarjetaCreditoId, transaccion.Descripcion, transaccion.Monto, transaccion.Fecha, transaccion.Tipo);
            var transaccionId = await _mediator.Send(command);

            transaccion.Id = transaccionId;
            
            await _hubContext.Clients.All.SendAsync("RecibirTransaccion", transaccion);

            return Ok(transaccion);
        }
    }
}
