using EstadoCuenta.Api.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace EstadoCuenta.Api.Hubs
{
    public class TransaccionesHub : Hub
    {
        public async Task NotificarNuevaTransaccion(TransaccionDTO transaccion)
        {
            await Clients.All.SendAsync("RecibirTransaccion", transaccion);
        }
    }
}
