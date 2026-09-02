using Gestion.Entities.DTO;

namespace Gestion.BL.Interfaces
{
    public interface IEventoService
    {
        Task<List<EventoDto>> GetEventosAsync();
        Task<EventoDto?> GetEventoByIdAsync(int id);
        Task<EventoDto> InsertEventoAsync(EventoDto evento);
        Task<EventoDto?> UpdateEventoAsync(int id, EventoDto evento);
        Task<bool> DeleteEventoAsync(int id);
    }
}