using EstadoCuenta.Api.DTOs;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EstadoCuenta.Api.Services
{
    public class PdfService
    {
        public byte[] GenerarEstadoCuentaPdf(TarjetaCreditoDTO tarjetaCredito, IEnumerable<TransaccionDTO> transacciones)
        {
            using (var memoryStream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                document.Add(new Paragraph($"Estado de cuenta -  {tarjetaCredito.Titular}"));
                document.Add(new Paragraph($"Número de tarjeta: {tarjetaCredito.NumeroTarjeta}"));
                document.Add(new Paragraph($"Saldo actual: {tarjetaCredito.SaldoActual}"));
                document.Add(new Paragraph($"Límite de crédito: {tarjetaCredito.LimiteCredito}"));
                document.Add(new Paragraph("\n"));

                // Creando tabla de transacciones
                PdfPTable table = new PdfPTable(4);
                table.AddCell("Fecha");
                table.AddCell("Descripcion");
                table.AddCell("Monto");
                table.AddCell("Tipo");

                foreach (var transaccion in transacciones)
                {
                    table.AddCell(transaccion.Fecha.ToString("dd/MM/yyyy"));
                    table.AddCell(transaccion.Descripcion);
                    table.AddCell($"${transaccion.Monto}");
                    table.AddCell(transaccion.Tipo);
                }

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }
    }
}
