using System.Collections.Generic;
using System.Threading.Tasks;
using EstadoCuenta.Web.Models;

namespace EstadoCuenta.Web.Services
{
    public interface IPagosService
    {
        Task<List<PagoViewModel>> ObtenerPagos();
        Task<ResultadoOperacion> AgregarPago(PagoViewModel transaccion);
    }
}
