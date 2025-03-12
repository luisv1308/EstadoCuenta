using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstadoCuenta.Api.Models
{
    public class Transaccion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TarjetaCreditoId { get; set; }
        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(10)]
        public string Tipo { get; set; } = string.Empty;

        // Relacion con TarjetaCredito
        public TarjetaCredito? TarjetaCredito { get; set; }
    }
}
