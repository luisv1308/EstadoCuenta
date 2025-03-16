﻿namespace EstadoCuenta.Api.DTOs
{
    public class PagosDTO
    {
        public int Id { get; set; }
        public int TarjetaCreditoId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
