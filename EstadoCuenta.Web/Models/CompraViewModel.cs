using EstadoCuenta.Web.Validators;
using System.ComponentModel.DataAnnotations;

namespace EstadoCuenta.Web.Models
{
    public class CompraViewModel
    {
        public int Id { get; set; }
        [Required]
        public int TarjetaCreditoId { get; set; }
        [Required]
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        [Range(0.01, 10000, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }
        [Required]
        [FechaNoFutura]
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
