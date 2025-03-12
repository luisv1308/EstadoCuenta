using EstadoCuenta.Api.Models;
using EstadoCuenta.Api.Repositories;
using MediatR;

namespace EstadoCuenta.Api.CQRS.Commands
{
   public record CreateTarjetaCommand(string Titular, string NumeroTarjeta, decimal LimiteCredito) : IRequest<int>;

   public class CreateTarjetaHandler : IRequestHandler<CreateTarjetaCommand, int>
   {
      private readonly IUnitOfWork _unitOfWork;

      public CreateTarjetaHandler(IUnitOfWork unitOfWork)
      {
         _unitOfWork = unitOfWork;
      }

      public async Task<int> Handle(CreateTarjetaCommand request, CancellationToken cancellationToken)
      {
         var tarjeta = new TarjetaCredito
         {
             Titular = request.Titular,
             NumeroTarjeta = request.NumeroTarjeta,
             LimiteCredito = request.LimiteCredito,
             SaldoActual = 0
         };

         _unitOfWork.TarjetasCredito.AgregarAsync(tarjeta);
         await _unitOfWork.SaveChangesAsync();

         return tarjeta.Id;
      }
   }
}
