using AutoMapper;
using Gestion.Entities.DTO;
using Gestion.Entities.Models;

namespace Gestion.BL.Profiles
{
    public class ParticipanteProfile : Profile
    {
        public ParticipanteProfile()
        {
            CreateMap<Participante, ParticipanteDto>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombreParticipante, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EventoId, opt => opt.MapFrom(src => src.EventoId))
                .ReverseMap();
        }
    }
}