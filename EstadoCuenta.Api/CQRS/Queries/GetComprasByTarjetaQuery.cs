using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Repositories;
using MediatR;

namespace EstadoCuenta.Api.CQRS.Queries
{
    public class GetComprasByTarjetaQuery : IRequest<IEnumerable<TransaccionDTO>>
    {
        public int TarjetaId { get; set; }

        public GetComprasByTarjetaQuery(int tarjetaId)
        {
            TarjetaId = tarjetaId;
        }
    }

    public class GetComprasByTarjetaHandler : IRequestHandler<GetComprasByTarjetaQuery, IEnumerable<TransaccionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetComprasByTarjetaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TransaccionDTO>> Handle(GetComprasByTarjetaQuery request, CancellationToken cancellationToken)
        {
            var compras = await _unitOfWork.Transacciones.ObtenerPorTipoAsync(request.TarjetaId, "Compra");

            return _mapper.Map<IEnumerable<TransaccionDTO>>(compras);
        }
    }
}
