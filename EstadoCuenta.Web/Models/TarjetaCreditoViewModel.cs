namespace EstadoCuenta.Web.Models
{
    public class TarjetaCreditoViewModel
    {
        public int Id { get; set; }
        public string Titular { get; set; } = string.Empty;
        public string NumeroTarjeta { get; set; } = string.Empty;
        public decimal SaldoActual { get; set; }
        public decimal LimiteCredito { get; set; }
    }
}
