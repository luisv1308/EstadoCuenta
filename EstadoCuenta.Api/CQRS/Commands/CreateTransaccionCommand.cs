using EstadoCuenta.Api.Models;
using EstadoCuenta.Api.Repositories;
using MediatR;
using Microsoft.Extensions.Primitives;

namespace EstadoCuenta.Api.CQRS.Commands
{
    public record CreateTransaccionCommand(int TarjetaCreditoId, string Descripcion, decimal Monto, DateTime Fecha, String Tipo) : IRequest<int>;

    public class CreateTransaccionHandler : IRequestHandler<CreateTransaccionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateTransaccionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTransaccionCommand request, CancellationToken cancellationToken)
        {
            var transaccion = new Transaccion
            {
                TarjetaCreditoId = request.TarjetaCreditoId,
                Descripcion = request.Descripcion,
                Monto = request.Monto,
                Fecha = request.Fecha,
                Tipo = request.Tipo
            };

            await _unitOfWork.Transacciones.AgregarAsync(transaccion);
            await _unitOfWork.SaveChangesAsync();

            return transaccion.Id;
        }
    }
}
