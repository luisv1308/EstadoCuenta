using EstadoCuenta.Api.Models;

namespace EstadoCuenta.Api.Repositories
{
    public class ITransaccionRepository
    {
        Task<IEnumerable<Transaccion>> ObtenerPorTarjetaAsync(int idTarjeta);
        Task AgregarAsync(Transaccion transaccion);
        void Eliminar(Transaccion transaccion);
    }
}
