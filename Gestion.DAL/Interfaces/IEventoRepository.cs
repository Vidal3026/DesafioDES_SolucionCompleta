using Gestion.Entities.Models;
namespace Gestion.DAL.Interfaces
{
    public interface IEventoRepository
    {
        Task<List<Evento>> GetEventosAsync();
        Task<Evento?> GetEventoByIdAsync(int id);
        Task<int> InsertEventoAsync(Evento evento);
        Task<bool> UpdateEventoAsync(Evento evento);
        Task<bool> DeleteEventoAsync(int id);
    }
}