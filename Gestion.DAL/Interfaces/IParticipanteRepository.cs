using Gestion.Entities.Models;

namespace Gestion.DAL.Interfaces
{
    public interface IParticipanteRepository
    {
        Task<List<Participante>> GetParticipantesAsync();
        Task<Participante?> GetParticipanteByIdAsync(int id);
        Task<int> InsertParticipanteAsync(Participante participante);
        Task<bool> UpdateParticipanteAsync(Participante participante);
        Task<bool> DeleteParticipanteAsync(int id);
    }
}