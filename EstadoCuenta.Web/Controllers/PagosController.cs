using EstadoCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace EstadoCuenta.Web.Controllers
{
    public class PagosController : Controller
    {
        private readonly HttpClient _httpClient;

        public PagosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7264/api/Pagos/1"); 
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<TransaccionViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var pagos = JsonSerializer.Deserialize<List<TransaccionViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(pagos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPago(TransaccionViewModel transaccion)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", await ObtenerPagos());
            }

            transaccion.TarjetaCreditoId = 1;
            transaccion.Tipo = "Pago"; 

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7264/api/Transaccion", transaccion);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al agregar el pago.");
                return View("Index", await ObtenerPagos());
            }

            return RedirectToAction("Index");
        }

        private async Task<List<TransaccionViewModel>> ObtenerPagos()
        {
            var response = await _httpClient.GetAsync("https://localhost:7264/api/Pagos/1");
            if (!response.IsSuccessStatusCode)
            {
                return new List<TransaccionViewModel>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var transacciones = JsonSerializer.Deserialize<List<TransaccionViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return transacciones ?? new List<TransaccionViewModel>();
        }
    }
}
