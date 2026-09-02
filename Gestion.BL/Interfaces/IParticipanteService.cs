using Gestion.Entities.DTO;

namespace Gestion.BL.Interfaces
{
    public interface IParticipanteService
    {
        Task<List<ParticipanteDto>> GetParticipantesAsync();
        Task<ParticipanteDto?> GetParticipanteByIdAsync(int id);
        Task<ParticipanteDto> InsertParticipanteAsync(ParticipanteDto participante);
        Task<ParticipanteDto?> UpdateParticipanteAsync(int id, ParticipanteDto participante);
        Task<bool> DeleteParticipanteAsync(int id);
    }
}