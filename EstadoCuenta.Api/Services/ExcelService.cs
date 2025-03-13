using EstadoCuenta.Api.DTOs;
using OfficeOpenXml;

namespace EstadoCuenta.Api.Services
{
    public class ExcelService
    {
        public byte[] GenerarEstadoCuentaExcel(TarjetaCreditoDTO tarjetaCredito, IEnumerable<TransaccionDTO> transacciones)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Estado de Cuenta");

                worksheet.Cells["A1"].Value = "Titular";
                worksheet.Cells["B1"].Value = tarjetaCredito.Titular;
                worksheet.Cells["A2"].Value = "Numero de Tarjeta";
                worksheet.Cells["B2"].Value = tarjetaCredito.NumeroTarjeta;
                worksheet.Cells["A3"].Value = "Saldo Actual";
                worksheet.Cells["B3"].Value = tarjetaCredito.SaldoActual;
                worksheet.Cells["A4"].Value = "Limite de Credito";
                worksheet.Cells["B4"].Value = tarjetaCredito.LimiteCredito;

                worksheet.Cells["A6"].Value = "Fecha";
                worksheet.Cells["B6"].Value = "Descripcion";
                worksheet.Cells["C6"].Value = "Monto";
                worksheet.Cells["D6"].Value = "Tipo";

                int row = 7;
                foreach (var transaccion in transacciones)
                {
                    worksheet.Cells[$"A{row}"].Value = transaccion.Fecha.ToString("dd/MM/yyyy");
                    worksheet.Cells[$"B{row}"].Value = transaccion.Descripcion;
                    worksheet.Cells[$"C{row}"].Value = transaccion.Monto;
                    worksheet.Cells[$"D{row}"].Value = transaccion.Tipo;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
