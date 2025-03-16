using EstadoCuenta.Web.Filters;
using EstadoCuenta.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EstadoCuenta.Web.Controllers
{
    public abstract class TransaccionBaseController<TViewModel> : Controller
    {
        protected readonly ITransaccionService<TViewModel> _transaccionService;

        protected TransaccionBaseController(ITransaccionService<TViewModel> transaccionService)
        {
            _transaccionService = transaccionService;
        }

        public async Task<IActionResult> Index()
        {
            var transacciones = await _transaccionService.ObtenerTransacciones();
            return View(transacciones);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationModelFilter))]
        [ServiceFilter(typeof(HandleApiErrorFilter))]
        public async Task<IActionResult> Agregar(TViewModel transaccion)
        {
            
            var result = await _transaccionService.AgregarTransaccion(transaccion);
            if (!result.Exitoso)
            {
                return new BadRequestObjectResult(result);
            }


            return RedirectToAction("Index");
        }
    }
}
