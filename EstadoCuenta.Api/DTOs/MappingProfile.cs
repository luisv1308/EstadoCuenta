using AutoMapper;
using EstadoCuenta.Api.Models;

namespace EstadoCuenta.Api.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaccion, TransaccionDTO>().ReverseMap();
            CreateMap<TarjetaCredito, TarjetaCreditoDTO>().ReverseMap();
        }
    }
}
