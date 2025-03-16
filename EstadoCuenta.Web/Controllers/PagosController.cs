﻿using EstadoCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using EstadoCuenta.Web.Helpers;
using Microsoft.Extensions.Options;

namespace EstadoCuenta.Web.Controllers
{
    public class PagosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public PagosController(HttpClient httpClient, IOptions<ApiSettings> apisettings)
        {
            _httpClient = httpClient;
            _apiBaseUrl = apisettings.Value.BaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}api/Pagos/1"); 
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<PagoViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var pagos = JsonSerializer.Deserialize<List<PagoViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(pagos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPago(PagoViewModel transaccion)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", await ObtenerPagos());
            }

            transaccion.TarjetaCreditoId = 1;

            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}api/Pagos", transaccion);
            if (!response.IsSuccessStatusCode)
            {
                await ModelState.ProcesarErroresApi(response);
                return View("Index", await ObtenerPagos());
            }

            return RedirectToAction("Index");
        }

        private async Task<List<PagoViewModel>> ObtenerPagos()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}api/Pagos/1");
            if (!response.IsSuccessStatusCode)
            {
                return new List<PagoViewModel>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var transacciones = JsonSerializer.Deserialize<List<PagoViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return transacciones ?? new List<PagoViewModel>();
        }
    }
}
