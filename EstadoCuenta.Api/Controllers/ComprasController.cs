using EstadoCuenta.Api.CQRS.Commands;
using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Filters;
using EstadoCuenta.Api.Hubs;
using EstadoCuenta.Api.Services;
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
        private readonly TransaccionNotificationService _notificationService;

        public ComprasController(IMediator mediator, 
            TransaccionNotificationService notificationService
        )
        {
            _mediator = mediator;
            _notificationService = notificationService;
        }

        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerCompras(int tarjetaId)
        {
            var compras = await _mediator.Send(new GetComprasByTarjetaQuery(tarjetaId));
            return Ok(compras);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter<ComprasDTO>))]
        public async Task<IActionResult> AgregarCompra([FromBody] ComprasDTO transaccion)
        {
            transaccion.Tipo = "Compra";
            var command = new CreateCompraCommand(transaccion.TarjetaCreditoId, transaccion.Descripcion, transaccion.Monto, transaccion.Fecha, transaccion.Tipo);
            var transaccionId = await _mediator.Send(command);

            transaccion.Id = transaccionId;
            
            await _notificationService.NotificarNuevaCompra(transaccion);

            return Ok(transaccion);
        }
    }
}
