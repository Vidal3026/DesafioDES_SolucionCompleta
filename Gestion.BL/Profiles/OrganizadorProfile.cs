using AutoMapper;
using Gestion.Entities.DTO;
using Gestion.Entities.Models;

namespace Gestion.BL.Profiles
{
    public class OrganizadorProfile : Profile
    {
        public OrganizadorProfile()
        {
            CreateMap<Organizador, OrganizadorDto>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombreOrganizador, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.EventoId, opt => opt.MapFrom(src => src.EventoId))
                .ReverseMap();
        }
    }
}