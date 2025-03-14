using AutoMapper;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Models;
using EstadoCuenta.Api.Repositories;
using MediatR;

namespace EstadoCuenta.Api.CQRS.Queries
{
    public record GetTarjetaByIdQuery(int Id) : IRequest<TarjetaCreditoDTO?>;

    public class GetTarjetaByIdHandler : IRequestHandler<GetTarjetaByIdQuery, TarjetaCreditoDTO?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const decimal PorcentajeInteres = 0.25m;
        private const decimal PorcentajeSaldoMinimo = 0.05m;

        public GetTarjetaByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TarjetaCreditoDTO?> Handle(GetTarjetaByIdQuery request, CancellationToken cancellationToken)
        {
            var tarjeta = await _unitOfWork.TarjetasCredito.ObtenerPorIdAsync(request.Id);
            if (tarjeta == null)
                return null;

            var tarjetaDTO = _mapper.Map<TarjetaCreditoDTO>(tarjeta);
            if (tarjetaDTO == null)
                return null;

            return tarjetaDTO;
        }
    }
}
