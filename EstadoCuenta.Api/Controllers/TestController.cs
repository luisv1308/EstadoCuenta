using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController
    {
        [HttpGet("prueba-error")]
        public IActionResult ForzarError()
        {
            throw new Exception("Este es un error forzado para probar GlobalExceptions.");
        }
    }
}
