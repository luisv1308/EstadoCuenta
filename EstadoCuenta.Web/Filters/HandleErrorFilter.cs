using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EstadoCuenta.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EstadoCuenta.Web.Filters
{
    public class HandleErrorFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (resultContext.Result is ObjectResult objectResult && objectResult.Value is ResultadoOperacion resultado)
            {
                if (!resultado.Exitoso)
                {
                    var controller = context.Controller as Controller;
                    if (controller != null)
                    {
                        foreach (var error in resultado.Errores)
                        {
                            controller.ModelState.AddModelError("", error);
                        }

                        var comprasService = controller.HttpContext.RequestServices.GetService(typeof(Services.IComprasService)) as Services.IComprasService;
                        if (comprasService != null)
                        {
                            var compras = await comprasService.ObtenerCompras();
                            context.Result = controller.View("Index", compras);
                            return;
                        }

                        var pagosService = controller.HttpContext.RequestServices.GetService(typeof(Services.IPagosService)) as Services.IPagosService;
                        if (pagosService != null)
                        {
                            var pagos = await pagosService.ObtenerPagos();
                            context.Result = controller.View("Index", pagos);
                            return;
                        }
                    }
                }
            }
        }
    }
}
