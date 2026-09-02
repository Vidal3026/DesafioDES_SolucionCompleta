using AutoMapper;
using Gestion.Entities.DTO;
using Gestion.Entities.Models;

namespace Gestion.BL.Profiles
{
    public class EventoProfile : Profile
    {
        public EventoProfile()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombreEvento, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.FechaEvento, opt => opt.MapFrom(src => src.Fecha))
                .ForMember(dest => dest.LugarEvento, opt => opt.MapFrom(src => src.Lugar))
                .ReverseMap();
        }
    }
}