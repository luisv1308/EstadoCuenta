using EstadoCuenta.Web.Helpers;
using EstadoCuenta.Web.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EstadoCuenta.Web.Services
{
    public class PagosService : IPagosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public PagosService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiBaseUrl = apiSettings.Value.BaseUrl;
        }

        public async Task<List<PagoViewModel>> ObtenerTransacciones()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}api/Pagos/1");
            if (!response.IsSuccessStatusCode)
            {
                return new List<PagoViewModel>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var pagos = JsonSerializer.Deserialize<List<PagoViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return pagos ?? new List<PagoViewModel>();
        }

        public async Task<ResultadoOperacion> AgregarTransaccion(PagoViewModel transaccion)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}api/Pagos", transaccion);
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
                : ResultadoOperacion.Fallo(new List<string> { "Error desconocido al agregar el pago." });
        }      
    }
}
