using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Web.Controllers
{
    public class PagosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
