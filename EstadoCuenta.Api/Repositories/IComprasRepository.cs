using EstadoCuenta.Api.Models;

namespace EstadoCuenta.Api.Repositories
{
    public interface IComprasRepository
    {
        Task<IEnumerable<Transaccion>> ObtenerComprasAsync(int tarjetaId);
        Task AgregarCompraAsync(Transaccion transaccion);
        void Eliminar(Transaccion transaccion);
    }
}