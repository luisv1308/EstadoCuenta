using AutoMapper;
using EstadoCuenta.Api.Models;

namespace EstadoCuenta.Api.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaccion, TransaccionDTO>().ReverseMap();
            CreateMap<TarjetaCredito, TarjetaCreditoDTO>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
