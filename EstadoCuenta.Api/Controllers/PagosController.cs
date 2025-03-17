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
    public class PagosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly TransaccionNotificationService _notificationService;

        public PagosController(IMediator mediator,
            TransaccionNotificationService notificationService
        )
        {
            _mediator = mediator;
            _notificationService = notificationService;
        }

        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerPagos(int tarjetaId)
        {
            var pagos = await _mediator.Send(new GetPagosByTarjetaQuery(tarjetaId));
            return Ok(pagos);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter<PagosDTO>))]
        public async Task<IActionResult> AgregarPago([FromBody] PagosDTO transaccion)
        {
            transaccion.Tipo = "Pago";
            transaccion.Descripcion = "Pago a tarjeta";
            var command = new CreatePagoCommand(transaccion.TarjetaCreditoId, transaccion.Descripcion, transaccion.Monto, transaccion.Fecha, transaccion.Tipo);
            var transaccionId = await _mediator.Send(command);

            transaccion.Id = transaccionId;            

            await _notificationService.NotificarNuevoPago(transaccion);

            return Ok(transaccion);
        }
    }
}
