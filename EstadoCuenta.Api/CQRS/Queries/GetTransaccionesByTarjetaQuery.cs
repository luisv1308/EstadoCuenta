using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Models;
using EstadoCuenta.Api.Repositories;
using MediatR;

namespace EstadoCuenta.Api.CQRS.Queries
{
    public record GetTransaccionesByTarjetaQuery(int TarjetaId) : IRequest<IEnumerable<TransaccionDTO>>;

    public class GetTransaccionesByTarjetaHandler : IRequestHandler<GetTransaccionesByTarjetaQuery, IEnumerable<TransaccionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTransaccionesByTarjetaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransaccionDTO>> Handle(GetTransaccionesByTarjetaQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Transaccion> transacciones = await _unitOfWork.Transacciones.ObtenerPorTarjetaAsync(request.TarjetaId);
            return _mapper.Map<IEnumerable<TransaccionDTO>>(transacciones) ?? Enumerable.Empty<TransaccionDTO>();
        }
        
    }
}
