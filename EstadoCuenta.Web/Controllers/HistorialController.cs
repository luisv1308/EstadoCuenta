using EstadoCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

namespace EstadoCuenta.Web.Controllers
{
    public class HistorialController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly IConfiguration _configuration;

        public HistorialController(HttpClient httpClient, IOptions<ApiSettings> apisettings, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = apisettings.Value.BaseUrl;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewData["ApiBaseUrl"] = _configuration["ApiSettings:BaseUrl"];
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}api/Transaccion/1");
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
