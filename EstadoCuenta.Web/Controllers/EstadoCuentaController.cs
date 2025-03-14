using EstadoCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EstadoCuenta.Web.Controllers
{
    public class EstadoCuentaController : Controller
    {
        private readonly HttpClient _httpClient;

        public EstadoCuentaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = _httpClient.GetAsync("https://localhost:7264/api/EstadoCuenta/1");
            if (!response.Result.IsSuccessStatusCode)
            {
                return View(new EstadoCuentaViewModel());
            }

            var json = await response.Result.Content.ReadAsStringAsync();
            var estadoCuenta = JsonSerializer.Deserialize<EstadoCuentaViewModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(estadoCuenta);
        }
    }
}
