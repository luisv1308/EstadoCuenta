using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Models;
using FluentValidation;

namespace EstadoCuenta.Api.Validators
{
    public class TarjetaCreditoValidator : AbstractValidator<TarjetaCreditoDTO>
    {
        public TarjetaCreditoValidator()
        {
            RuleFor(x => x.Titular)
                .NotEmpty()
                .WithMessage("El titular es requerido")
                .MaximumLength(100).WithMessage("El titular no puede tener mas de 100 caracteres");

            RuleFor(x => x.NumeroTarjeta)
                .NotEmpty()
                .WithMessage("El numero de tarjeta es requerido")
                .Length(16).WithMessage("El numero de tarjeta debe tener 16 caracteres")
                .Matches("^[0-9]+$").WithMessage("El numero de tarjeta debe ser numerico");

            RuleFor(x => x.LimiteCredito)
                .GreaterThan(0)
                .WithMessage("El limite de credito debe ser mas de 0");
        }
    }
}
