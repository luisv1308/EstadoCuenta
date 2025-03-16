using System.Collections.Generic;
using System.Threading.Tasks;
using EstadoCuenta.Web.Models;

namespace EstadoCuenta.Web.Services
{
    public interface IComprasService
    {
        Task<List<CompraViewModel>> ObtenerCompras();
        Task<ResultadoOperacion> AgregarCompra(CompraViewModel transaccion);
    }
}
