using EstadoCuenta.Api.DTOs;
using OfficeOpenXml;

namespace EstadoCuenta.Api.Services
{
    public class ExcelService
    {
        public byte[] GenerarEstadoCuentaExcel(TarjetaCreditoDTO tarjetaCredito, IEnumerable<TransaccionDTO> transacciones)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Estado de Cuenta");

                worksheet.Cells["A1"].Value = "Titular";
                worksheet.Cells["B1"].Value = tarjetaCredito.Titular;
                worksheet.Cells["A2"].Value = "Numero de Tarjeta";
                worksheet.Cells["B2"].Value = tarjetaCredito.NumeroTarjeta;
                worksheet.Cells["A3"].Value = "Saldo Actual";
                worksheet.Cells["B3"].Value = Math.Round(tarjetaCredito.SaldoActual, 2);
                worksheet.Cells["A4"].Value = "Limite de Credito";
                worksheet.Cells["B4"].Value = Math.Round(tarjetaCredito.LimiteCredito, 2);

                // Calculos
                worksheet.Cells["A6"].Value = "Interés Bonificable";
                worksheet.Cells["B6"].Value = Math.Round(tarjetaCredito.InteresBonificable, 2);
                worksheet.Cells["A7"].Value = "Cuota Mínima a Pagar";
                worksheet.Cells["B7"].Value = Math.Round(tarjetaCredito.CuotaMinimaPagar, 2);
                worksheet.Cells["A8"].Value = "Monto Total a Pagar";
                worksheet.Cells["B8"].Value = Math.Round(tarjetaCredito.MontoTotalPagar, 2);
                worksheet.Cells["A9"].Value = "Pago de Contado con Intereses";
                worksheet.Cells["B9"].Value = Math.Round(tarjetaCredito.PagoContadoConIntereses, 2);

                worksheet.Cells["A11"].Value = "Fecha";
                worksheet.Cells["B11"].Value = "Descripcion";
                worksheet.Cells["C11"].Value = "Monto";
                worksheet.Cells["D11"].Value = "Tipo";

                int row = 12;
                foreach (var transaccion in transacciones)
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
