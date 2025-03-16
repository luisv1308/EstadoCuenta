using EstadoCuenta.Api.Models;

namespace EstadoCuenta.Api.Repositories
{
    public interface IPagosRepository
    {
        Task<IEnumerable<Transaccion>> ObtenerPagosAsync(int tarjetaId);
        Task AgregarPagoAsync(Transaccion transaccion);
        void Eliminar(Transaccion transaccion);
    }
}