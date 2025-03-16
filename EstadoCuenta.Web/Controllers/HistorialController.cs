using EstadoCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace EstadoCuenta.Web.Controllers
{
    public class HistorialController : Controller
    {
        private readonly HttpClient _httpClient;

        public HistorialController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var response = await _httpClient.GetAsync("https://localhost:7264/api/Transaccion/1");
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Error al obtener el historial" });
            }

            var json = await response.Content.ReadAsStringAsync();
            var historial = JsonSerializer.Deserialize<List<CompraViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return Json(new { success = true, data = historial });
        }
    }
}
