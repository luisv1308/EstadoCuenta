using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace EstadoCuenta.Web.Filters
{
    public class ApiBaseUrlFilter : IActionFilter
    {
        private readonly string _apiBaseUrl;

        public ApiBaseUrlFilter(IConfiguration configuration)
        {
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7264/";
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                controller.ViewData["ApiBaseUrl"] = _apiBaseUrl;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
