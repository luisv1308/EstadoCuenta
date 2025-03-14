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
    public class PagosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<TransaccionesHub> _hubContext;
        private readonly IValidator<TransaccionDTO> _validator;

        public PagosController(IMediator mediator, IHubContext<TransaccionesHub> hubContext, IValidator<TransaccionDTO> validator)
        {
            _mediator = mediator;
            _hubContext = hubContext;
            _validator = validator;
        }

        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerPagos(int tarjetaId)
        {
            var pagos = await _mediator.Send(new GetPagosByTarjetaQuery(tarjetaId));
            return Ok(pagos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPago([FromBody] TransaccionDTO transaccion)
        {
            var validationResult = await _validator.ValidateAsync(transaccion);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            transaccion.Tipo = "Pago";
            var command = new CreateTransaccionCommand(transaccion.TarjetaCreditoId, transaccion.Descripcion, transaccion.Monto, transaccion.Fecha, transaccion.Tipo);
            var transaccionId = await _mediator.Send(command);

            transaccion.Id = transaccionId;

            await _hubContext.Clients.All.SendAsync("RecibirTransaccion", transaccion);

            return Ok(transaccion);
        }
    }
}
