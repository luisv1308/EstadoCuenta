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

        [HttpGet]
        public async Task<IActionResult> ExportarPdf(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7264/api/Export/pdf/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("No se pudo generar el PDF.");
            }

            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes, "application/pdf", "EstadoCuenta.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> ExportarExcel(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7264/api/Export/excel/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("No se pudo generar el Excel.");
            }

            var excelBytes = await response.Content.ReadAsByteArrayAsync();
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EstadoCuenta.xlsx");
        }
    }
}
