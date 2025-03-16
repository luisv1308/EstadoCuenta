using EstadoCuenta.Web.Helpers;
using EstadoCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace EstadoCuenta.Web.Controllers
{
    public class ComprasController : Controller
    {
        public readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ComprasController(HttpClient httpClient, IOptions<ApiSettings> apisettings)
        {
            _httpClient = httpClient;
            _apiBaseUrl = apisettings.Value.BaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            var response = _httpClient.GetAsync($"{_apiBaseUrl}api/Compras/1");
            if (!response.Result.IsSuccessStatusCode)
            {
                return View(new List<CompraViewModel>());
            }

            var json = await response.Result.Content.ReadAsStringAsync();
            var compras = JsonSerializer.Deserialize<List<CompraViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(compras);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarCompra(CompraViewModel transaccion)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", await ObtenerCompras());
            }
            transaccion.TarjetaCreditoId = 1;

            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}api/Compras", transaccion);
            if (!response.IsSuccessStatusCode)
            {
                await ModelState.ProcesarErroresApi(response);
                return View("Index", await ObtenerCompras());
            }

            return RedirectToAction("Index");
        }

        private async Task<List<CompraViewModel>> ObtenerCompras()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}api/Compras/1");
            if (!response.IsSuccessStatusCode)
            {
                return new List<CompraViewModel>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var transacciones = JsonSerializer.Deserialize<List<CompraViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return transacciones ?? new List<CompraViewModel>();
        }
    }
}
