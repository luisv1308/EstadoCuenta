using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Repositories;
using MediatR;

namespace EstadoCuenta.Api.CQRS.Queries
{
    public class GetPagosByTarjetaQuery : IRequest<IEnumerable<TransaccionDTO>>
    {
        public int TarjetaId { get; }

        public GetPagosByTarjetaQuery(int tarjetaId)
        {
            TarjetaId = tarjetaId;
        }
    }

    public class GetPagosByTarjetaHandler : IRequestHandler<GetPagosByTarjetaQuery, IEnumerable<TransaccionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetPagosByTarjetaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TransaccionDTO>> Handle(GetPagosByTarjetaQuery request, CancellationToken cancellationToken)
        {
            var pagos = await _unitOfWork.Pagos.ObtenerPagosAsync(request.TarjetaId);
            return _mapper.Map<IEnumerable<TransaccionDTO>>(pagos);
        }
    }
}