using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Web.Controllers
{
    public class EstadoCuentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
