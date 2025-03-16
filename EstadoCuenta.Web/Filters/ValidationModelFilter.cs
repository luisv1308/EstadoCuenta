using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using EstadoCuenta.Web.Services;

namespace EstadoCuenta.Web.Filters
{
    public class ValidationModelFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    var transaccionService = controller.HttpContext.RequestServices.GetService(typeof(ITransaccionService<>));
                    if (transaccionService != null)
                    {
                        var obtenerTransaccionesMethod = transaccionService.GetType().GetMethod("ObtenerTransacciones");
                        var transaccionesTask = obtenerTransaccionesMethod?.Invoke(transaccionService, null) as Task;
                        if (transaccionesTask != null)
                        {
                            await transaccionesTask;
                            var transacciones = transaccionesTask.GetType().GetProperty("Result")?.GetValue(transaccionesTask);
                            context.Result = controller.View("Index", transacciones);
                            return;
                        }
                    }
                }
            }
            await next();
        }
    }
}
