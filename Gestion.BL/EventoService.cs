using AutoMapper;
using Gestion.BL.Interfaces;
using Gestion.DAL.Interfaces;
using Gestion.Entities.DTO;
using Gestion.Entities.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Gestion.BL
{
    public class EventoService(IEventoRepository eventoRepository, IMapper mapper, IDistributedCache cache) : IEventoService
    {
        private const string CacheKeyAll = "eventos:all";
        private static string CacheKeyById(int id) => $"evento:{id}";
        private static readonly DistributedCacheEntryOptions CacheOptions = new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) };

        public async Task<List<EventoDto>> GetEventosAsync()
        {
            var cached = await cache.GetStringAsync(CacheKeyAll);
            if (cached != null)
                return JsonSerializer.Deserialize<List<EventoDto>>(cached)!;

            var eventos = await eventoRepository.GetEventosAsync();
            var dto = mapper.Map<List<EventoDto>>(eventos);
            await cache.SetStringAsync(CacheKeyAll, JsonSerializer.Serialize(dto), CacheOptions);
            return dto;
        }

        public async Task<EventoDto?> GetEventoByIdAsync(int id)
        {
            var cached = await cache.GetStringAsync(CacheKeyById(id));
            if (cached != null)
                return JsonSerializer.Deserialize<EventoDto>(cached);

            var evento = await eventoRepository.GetEventoByIdAsync(id);
            var dto = mapper.Map<EventoDto?>(evento);
            if (dto != null)
                await cache.SetStringAsync(CacheKeyById(id), JsonSerializer.Serialize(dto), CacheOptions);
            return dto;
        }

        public async Task<EventoDto> InsertEventoAsync(EventoDto evento)
        {
            var entity = mapper.Map<Evento>(evento);
            var newId = await eventoRepository.InsertEventoAsync(entity);
            evento.Codigo = newId;
            await cache.RemoveAsync(CacheKeyAll);
            return evento;
        }

        public async Task<EventoDto?> UpdateEventoAsync(int id, EventoDto evento)
        {
            var entity = mapper.Map<Evento>(evento);
            entity.Id = id;
            var updated = await eventoRepository.UpdateEventoAsync(entity);
            if (!updated) return null;

            evento.Codigo = id;
            await cache.RemoveAsync(CacheKeyAll);
            await cache.RemoveAsync(CacheKeyById(id));
            return evento;
        }

        public async Task<bool> DeleteEventoAsync(int id)
        {
            var deleted = await eventoRepository.DeleteEventoAsync(id);
            if (deleted)
            {
                await cache.RemoveAsync(CacheKeyAll);
                await cache.RemoveAsync(CacheKeyById(id));
            }
            return deleted;
        }
    }
}