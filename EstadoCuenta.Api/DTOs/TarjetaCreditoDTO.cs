namespace EstadoCuenta.Api.DTOs
{
    public class TarjetaCreditoDTO
    {
        public int Id { get; set; }
        public string Titular { get; set; } = string.Empty;
        public string NumeroTarjeta { get; set; } = string.Empty;
        public decimal SaldoActual { get; set; }
        public decimal LimiteCredito { get ; set; }

        // Calculos
        public decimal InteresBonificable { get; set; }
        public decimal CuotaMinimaPagar { get; set;  }
        public decimal MontoTotalPagar { get; set; }
        public decimal PagoContadoConIntereses { get; set; }

    }
}
