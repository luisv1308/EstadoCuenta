using EstadoCuenta.Web.Helpers;
using EstadoCuenta.Web.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EstadoCuenta.Web.Services
{
    public class ComprasService : ITransaccionService<CompraViewModel>
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ComprasService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiBaseUrl = apiSettings.Value.BaseUrl;
        }

        public async Task<List<CompraViewModel>> ObtenerTransacciones()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}api/Compras/1");
            if (!response.IsSuccessStatusCode)
            {
                return new List<CompraViewModel>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var compras = JsonSerializer.Deserialize<List<CompraViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return compras ?? new List<CompraViewModel>();
        } 

        public async Task<ResultadoOperacion> AgregarTransaccion(CompraViewModel transaccion)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}api/Compras", transaccion);
            if (response.IsSuccessStatusCode)
            {
                return ResultadoOperacion.Ok();
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            var errorData = JsonSerializer.Deserialize<ValidationErrorResponse>(errorResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return errorData?.Errors != null
                ? ResultadoOperacion.Fallo(errorData.Errors.SelectMany(e => e.Value).ToList())
                : ResultadoOperacion.Fallo(new List<string> { "Error desconocido al agregar la compra." });
        }
    }
}
