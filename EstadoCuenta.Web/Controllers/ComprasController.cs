using EstadoCuenta.Web.Models;
using EstadoCuenta.Web.Services;


namespace EstadoCuenta.Web.Controllers
{
    public class ComprasController : TransaccionBaseController<CompraViewModel>
    {
        public ComprasController(ITransaccionService<CompraViewModel> comprasService)
            : base(comprasService)
        {
        }
    }
}
