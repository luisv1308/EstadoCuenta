using EstadoCuenta.Api.DTOs;

namespace EstadoCuenta.Api.Services
{
    public interface IExportService
    {
        Task<byte[]> GenerarEstadoCuentaPDF(int tarjetaId);
        Task<byte[]> GenerarEstadoCuentaExcel(int tarjetaId);
    }
}