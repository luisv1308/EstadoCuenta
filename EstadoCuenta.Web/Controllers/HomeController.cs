using EstadoCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EstadoCuenta.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "EstadoCuenta");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult PruebaError()
        {
            throw new Exception("Esto es una prueba de error.");
        }
    }
}
