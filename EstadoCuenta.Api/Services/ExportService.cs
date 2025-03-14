using System;
using System.IO;
using System.Threading.Tasks;
using EstadoCuenta.Api.Models;
using EstadoCuenta.Api.Repositories;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;

namespace EstadoCuenta.Api.Services
{
    public class ExportService : IExportService
    {
        private readonly IEstadoCuentaRepository _estadoCuentaRepository;

        public ExportService(IEstadoCuentaRepository estadoCuentaRepository)
        {
            _estadoCuentaRepository = estadoCuentaRepository;
        }

        public async Task<byte[]> GenerarEstadoCuentaPDF(int tarjetaId)
        {
            var estadoCuenta = await _estadoCuentaRepository.ObtenerEstadoCuentaAsync(tarjetaId);
            if (estadoCuenta == null) return null;

            using (var memoryStream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                document.Add(new Paragraph($"Estado de cuenta -  {estadoCuenta.Titular}"));
                document.Add(new Paragraph($"Número de tarjeta: {estadoCuenta.NumeroTarjeta}"));
                document.Add(new Paragraph($"Saldo actual: {estadoCuenta.SaldoActual}"));
                document.Add(new Paragraph($"Límite de crédito: {estadoCuenta.LimiteCredito}"));
                document.Add(new Paragraph($"Total Mes Actual: ${estadoCuenta.TotalMesActual}"));
                document.Add(new Paragraph($"Total Mes Anterior: ${estadoCuenta.TotalMesAnterior}"));
                document.Add(new Paragraph("\n"));

                // Calculos
                document.Add(new Paragraph($"Interés Bonificable: ${estadoCuenta.InteresBonificable:F2}"));
                document.Add(new Paragraph($"Cuota Mínima a Pagar: ${estadoCuenta.CuotaMinimaPagar:F2}"));
                document.Add(new Paragraph($"Monto Total a Pagar: ${estadoCuenta.MontoTotalPagar:F2}"));
                document.Add(new Paragraph($"Pago de Contado con Intereses: ${estadoCuenta.PagoContadoConIntereses:F2}"));
                document.Add(new Paragraph("\n"));

                // Creando tabla de transacciones
                PdfPTable table = new PdfPTable(4);
                table.AddCell("Fecha");
                table.AddCell("Descripcion");
                table.AddCell("Monto");
                table.AddCell("Tipo");

                foreach (var transaccion in estadoCuenta.Compras)
                {
                    table.AddCell(transaccion.Fecha.ToString("dd/MM/yyyy"));
                    table.AddCell(transaccion.Descripcion);
                    table.AddCell($"${transaccion.Monto:F2}");
                    table.AddCell(transaccion.Tipo);
                }

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }

        public async Task<byte[]> GenerarEstadoCuentaExcel(int tarjetaId)
        {
            var estadoCuenta = await _estadoCuentaRepository.ObtenerEstadoCuentaAsync(tarjetaId);
            if (estadoCuenta == null) return null;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Estado de Cuenta");

                worksheet.Cells["A1"].Value = "Titular";
                worksheet.Cells["B1"].Value = estadoCuenta.Titular;
                worksheet.Cells["A2"].Value = "Numero de Tarjeta";
                worksheet.Cells["B2"].Value = estadoCuenta.NumeroTarjeta;
                worksheet.Cells["A3"].Value = "Saldo Actual";
                worksheet.Cells["B3"].Value = Math.Round(estadoCuenta.SaldoActual, 2);
                worksheet.Cells["A4"].Value = "Limite de Credito";
                worksheet.Cells["B4"].Value = Math.Round(estadoCuenta.LimiteCredito, 2);

                worksheet.Cells["A5"].Value = "Total Mes Actual";
                worksheet.Cells["B5"].Value = Math.Round(estadoCuenta.TotalMesActual, 2);
                worksheet.Cells["A6"].Value = "Total Mes Anterior";
                worksheet.Cells["B6"].Value = Math.Round(estadoCuenta.TotalMesAnterior, 2);

                // Calculos
                worksheet.Cells["A8"].Value = "Interés Bonificable";
                worksheet.Cells["B8"].Value = Math.Round(estadoCuenta.InteresBonificable, 2);
                worksheet.Cells["A9"].Value = "Cuota Mínima a Pagar";
                worksheet.Cells["B9"].Value = Math.Round(estadoCuenta.CuotaMinimaPagar, 2);
                worksheet.Cells["A10"].Value = "Monto Total a Pagar";
                worksheet.Cells["B10"].Value = Math.Round(estadoCuenta.MontoTotalPagar, 2);
                worksheet.Cells["A11"].Value = "Pago de Contado con Intereses";
                worksheet.Cells["B11"].Value = Math.Round(estadoCuenta.PagoContadoConIntereses, 2);

                worksheet.Cells["A13"].Value = "Fecha";
                worksheet.Cells["B13"].Value = "Descripcion";
                worksheet.Cells["C13"].Value = "Monto";
                worksheet.Cells["D13"].Value = "Tipo";

                int row = 14;
                foreach (var transaccion in estadoCuenta.Compras)
                {
                    worksheet.Cells[$"A{row}"].Value = transaccion.Fecha.ToString("dd/MM/yyyy");
                    worksheet.Cells[$"B{row}"].Value = transaccion.Descripcion;
                    worksheet.Cells[$"C{row}"].Value = Math.Round(transaccion.Monto, 2);
                    worksheet.Cells[$"D{row}"].Value = transaccion.Tipo;
                    row++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}

