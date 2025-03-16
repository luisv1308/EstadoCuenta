using Microsoft.AspNetCore.Mvc;
using EstadoCuenta.Web.Models;
using EstadoCuenta.Web.Services;

namespace EstadoCuenta.Web.Helpers
{
    public static class ResponseHandler
    {
        public static IActionResult ProcesarResultado<T>(Controller controller, ResultadoOperacion resultado, ITransaccionService<T> servicio)
        {
            if (!resultado.Exitoso)
            {
                return new BadRequestObjectResult(resultado);
            }


            return controller.RedirectToAction("Index");
        }
    }
}
