using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Web.Controllers
{
    public class HistorialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
