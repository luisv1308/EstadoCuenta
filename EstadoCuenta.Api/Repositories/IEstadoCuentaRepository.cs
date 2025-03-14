using EstadoCuenta.Api.DTOs;

namespace EstadoCuenta.Api.Repositories
{
    public interface IEstadoCuentaRepository
    {
        Task<EstadoCuentaDTO> ObtenerEstadoCuentaAsync(int id);
    }
}
