using System;
using System.ComponentModel.DataAnnotations;

namespace EstadoCuenta.Web.Validators
{
    public class FechaNoFuturaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime fecha)
            {
                if (fecha > DateTime.Today)
                {
                    return new ValidationResult("La fecha no puede ser en el futuro.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
