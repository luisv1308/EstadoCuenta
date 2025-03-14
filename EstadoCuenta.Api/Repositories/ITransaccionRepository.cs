using EstadoCuenta.Api.Models;

namespace EstadoCuenta.Api.Repositories
{
    public interface ITransaccionRepository
    {
        Task<IEnumerable<Transaccion>> ObtenerPorTarjetaAsync(int idTarjeta);
        Task<IEnumerable<Transaccion>> ObtenerPorTipoAsync(int idTarjeta, string tipo);
        Task AgregarAsync(Transaccion transaccion);
        void Eliminar(Transaccion transaccion);
    }
}
