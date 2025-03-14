using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Repositories;
using MediatR;

namespace EstadoCuenta.Api.CQRS.Queries
{
    public record GetEstadoCuentaByTarjetaQuery(int TarjetaId) : IRequest<EstadoCuentaDTO>;


    public class GetEstadoCuentaByTarjetaHandler : IRequestHandler<GetEstadoCuentaByTarjetaQuery, EstadoCuentaDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEstadoCuentaByTarjetaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<EstadoCuentaDTO> Handle(GetEstadoCuentaByTarjetaQuery request, CancellationToken cancellationToken)
        {
            var estadoCuenta = await _unitOfWork.EstadoCuenta.ObtenerEstadoCuentaAsync(request.TarjetaId);

            if (estadoCuenta == null)
            {
                throw new InvalidOperationException("No se encontró el estado de cuenta");
            }

            return _mapper.Map<EstadoCuentaDTO>(estadoCuenta);
        }
    }
}
