using EstadoCuenta.Api.DTOs;
using FluentValidation;

namespace EstadoCuenta.Api.Validators
{
    public class ComprasValidator : AbstractValidator<ComprasDTO>
    {
        public ComprasValidator()
        {
            RuleFor(x => x.TarjetaCreditoId)
                .GreaterThan(0)
                .WithMessage("El id de la tarjeta debe ser valido");

            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage("La descripcion es requerida")
                .MaximumLength(500).WithMessage("La descripcion no puede tener mas de 500 caracteres");

            RuleFor(x => x.Monto)
                .GreaterThan(0)
                .WithMessage("El monto debe ser mayor a 0");

            RuleFor(x => x.Fecha)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La fecha no puede ser en el futuro");
        }
    }
}
