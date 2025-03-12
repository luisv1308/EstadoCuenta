namespace EstadoCuenta.Api.DTOs
{
    public class TarjetaCreditoDTO
    {
        public int Id { get; set; }
        public string Titular { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal LimiteCredito { get ; set; }
    }
}
