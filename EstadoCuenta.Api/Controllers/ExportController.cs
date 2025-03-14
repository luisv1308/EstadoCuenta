using EstadoCuenta.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : Controller
    {
         private readonly IExportService _exportService;

        public ExportController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpGet("pdf/{tarjetaId}")]
        public async Task<IActionResult> ExportarEstadoCuentaPDF(int tarjetaId)
        {
            var pdfBytes = await _exportService.GenerarEstadoCuentaPDF(tarjetaId);
            if (pdfBytes == null)
                return NotFound("No se pudo generar el PDF.");

            return File(pdfBytes, "application/pdf", "EstadoCuenta.pdf");
        }

        [HttpGet("excel/{tarjetaId}")]
        public async Task<IActionResult> ExportarEstadoCuentaExcel(int tarjetaId)
        {
            var excelBytes = await _exportService.GenerarEstadoCuentaExcel(tarjetaId);
            if (excelBytes == null)
                return NotFound("No se pudo generar el Excel.");

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EstadoCuenta.xlsx");
        }
    }
}
