using EstadoCuenta.Web.Models;
using EstadoCuenta.Web.Services;

namespace EstadoCuenta.Web.Controllers
{
    public class PagosController : TransaccionBaseController<PagoViewModel>
    {
        public PagosController(ITransaccionService<PagoViewModel> pagosService)
            : base(pagosService)
        {
        }
    }
}
