using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace EstadoCuenta.Api.Services
{
    public class TransaccionNotificationService
    {
        private readonly IHubContext<TransaccionesHub> _hubContext;

        public TransaccionNotificationService(IHubContext<TransaccionesHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotificarNuevaCompra(ComprasDTO transaccion)
        {
            await _hubContext.Clients.All.SendAsync("RecibirTransaccion", transaccion);
        }

        public async Task NotificarNuevoPago(PagosDTO transaccion)
        {
            await _hubContext.Clients.All.SendAsync("RecibirTransaccion", transaccion);
        }
    }
}
