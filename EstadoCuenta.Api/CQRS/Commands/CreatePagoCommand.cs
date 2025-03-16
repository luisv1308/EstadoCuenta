using EstadoCuenta.Api.Models;
using EstadoCuenta.Api.Repositories;
using MediatR;
using Microsoft.Extensions.Primitives;

namespace EstadoCuenta.Api.CQRS.Commands
{
    public record CreatePagoCommand(int TarjetaCreditoId, string Descripcion, decimal Monto, DateTime Fecha, String Tipo) : IRequest<int>;

    public class CreatePagoHandler : IRequestHandler<CreatePagoCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePagoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreatePagoCommand request, CancellationToken cancellationToken)
        {
            var transaccion = new Transaccion
            {
                TarjetaCreditoId = request.TarjetaCreditoId,
                Descripcion = request.Descripcion,
                Monto = request.Monto,
                Fecha = request.Fecha,
                Tipo =  request.Tipo
            };

            await _unitOfWork.Pagos.AgregarPagoAsync(transaccion);
            await _unitOfWork.SaveChangesAsync();

            return transaccion.Id;
        }
    }
}
