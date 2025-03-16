using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using EstadoCuenta.Web.Models;

namespace EstadoCuenta.Web.Helpers
{
    public static class ModelStateExtensions
    {
        public static async Task ProcesarErroresApi(this ModelStateDictionary modelState, HttpResponseMessage response)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            var errorData = JsonSerializer.Deserialize<ValidationErrorResponse>(errorResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (errorData?.Errors != null)
            {
                foreach (var error in errorData.Errors)
                {
                    foreach (var message in error.Value)
                    {
                        modelState.AddModelError(error.Key, message);
                    }
                }
            }
            else
            {
                modelState.AddModelError("", "Ocurrió un error inesperado en la API.");
            }
        }
    }
}