using System.Collections.Generic;
using System.Threading.Tasks;
using EstadoCuenta.Web.Models;

namespace EstadoCuenta.Web.Services
{
    public interface ITransaccionService<TViewModel>
    {
        Task<List<TViewModel>> ObtenerTransacciones();
        Task<ResultadoOperacion> AgregarTransaccion(TViewModel transaccion);
    }
}