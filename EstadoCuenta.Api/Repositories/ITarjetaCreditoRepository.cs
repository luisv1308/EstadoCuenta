using EstadoCuenta.Api.Models;

namespace EstadoCuenta.Api.Repositories
{
    public interface ITarjetaCreditoRepository
    {
        Task<TarjetaCredito?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<TarjetaCredito>> ObtenerTodosAsync();
        Task AgregarAsync(TarjetaCredito tarjetaCredito);
        void Actualizar(TarjetaCredito tarjetaCredito);
        void Eliminar(TarjetaCredito tarjetaCredito);
    }
}
