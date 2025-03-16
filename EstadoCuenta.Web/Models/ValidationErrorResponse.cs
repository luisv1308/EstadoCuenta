using System.Collections.Generic;

namespace EstadoCuenta.Web.Models
{
    public class ValidationErrorResponse
    {
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
