namespace EstadoCuenta.Api.DTOs
{
    public class TransaccionDTO
    {
        public int Id { get; set; }
        public int TarjetaCreditoId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
