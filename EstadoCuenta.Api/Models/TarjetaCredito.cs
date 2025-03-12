using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstadoCuenta.Api.Models
{
    public class TarjetaCredito
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titular { get; set; } = string.Empty;

        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "El número de tarjeta debe tener 16 dígitos")]
        public string NumeroTarjeta { get; set; } = string.Empty;

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El código de seguridad debe tener 3 dígitos")]
        public string CodigoSeguridad { get; set; } = string.Empty;

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SaldoActual { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LimiteCredito { get; set; }

        // Relacion con Transacciones
        public ICollection<Transaccion>? Transacciones { get; set; }
    }
}
