using EstadoCuenta.Api.Models;
using EstadoCuenta.Api.Repositories;
using MediatR;

namespace EstadoCuenta.Api.CQRS.Queries
{
    public record GetTarjetaByIdQuery(int Id) : IRequest<TarjetaCredito?>;

    public class GetTarjetaByIdHandler : IRequestHandler<GetTarjetaByIdQuery, TarjetaCredito?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTarjetaByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TarjetaCredito?> Handle(GetTarjetaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TarjetasCredito.ObtenerPorIdAsync(request.Id);
        }
    }
}
