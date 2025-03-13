using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Web.Controllers
{
    public class ComprasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
