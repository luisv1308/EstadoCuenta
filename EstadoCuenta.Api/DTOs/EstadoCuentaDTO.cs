﻿namespace EstadoCuenta.Api.DTOs
{
    public class EstadoCuentaDTO
    {
        public int TarjetaId { get; set; }
        public string Titular { get; set; } = string.Empty;
        public string NumeroTarjeta { get; set; } = string.Empty;
        public decimal SaldoActual { get; set; }
        public decimal SaldoDisponible { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal InteresBonificable { get; set; }
        public decimal CuotaMinimaPagar { get; set; }
        public decimal MontoTotalPagar { get; set; }
        public decimal PagoContadoConIntereses { get; set; }
        public decimal TotalMesActual { get; set; }
        public decimal TotalMesAnterior { get; set; }
        public List<TransaccionDTO> Compras { get; set; } = new List<TransaccionDTO>();
    }
}
