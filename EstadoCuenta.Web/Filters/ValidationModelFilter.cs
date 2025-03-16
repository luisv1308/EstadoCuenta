using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EstadoCuenta.Web.Services;
using EstadoCuenta.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EstadoCuenta.Web.Controllers;

namespace EstadoCuenta.Web.Filters
{
    public class ValidationModelFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as Controller;
            if (controller != null && !controller.ModelState.IsValid)
            {
                object service = null;
                if (controller is ComprasController)
                {
                    service = controller.HttpContext.RequestServices.GetService(typeof(ITransaccionService<CompraViewModel>));
                }
                else if (controller is PagosController)
                {
                    service = controller.HttpContext.RequestServices.GetService(typeof(ITransaccionService<PagoViewModel>));
                }

                if (service != null)
                {
                    var obtenerTransaccionesMethod = service.GetType().GetMethod("ObtenerTransacciones");
                    var transaccionesTask = obtenerTransaccionesMethod?.Invoke(service, null) as Task;

                    if (transaccionesTask != null)
                    {
                        await transaccionesTask;
                        var transacciones = transaccionesTask.GetType().GetProperty("Result")?.GetValue(transaccionesTask);

                        context.Result = controller.View("Index", transacciones);
                        return;
                    }
                }

                if (controller is ComprasController)
                {
                    context.Result = controller.View("Index", new List<CompraViewModel>());
                }
                else if (controller is PagosController)
                {
                    context.Result = controller.View("Index", new List<PagoViewModel>());
                }
            }
            else
            {
                await next();
            }
        }
    }
}
