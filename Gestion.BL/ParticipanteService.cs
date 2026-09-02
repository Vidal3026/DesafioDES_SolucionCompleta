using AutoMapper;
using Gestion.BL.Interfaces;
using Gestion.DAL.Interfaces;
using Gestion.Entities.DTO;
using Gestion.Entities.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Gestion.BL
{
    public class ParticipanteService(IParticipanteRepository participanteRepository, IMapper mapper, IDistributedCache cache) : IParticipanteService
    {
        private const string CacheKeyAll = "participantes:all";
        private static string CacheKeyById(int id) => $"participante:{id}";
        private static readonly DistributedCacheEntryOptions CacheOptions = new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) };

        public async Task<List<ParticipanteDto>> GetParticipantesAsync()
        {
            var cached = await cache.GetStringAsync(CacheKeyAll);
            if (cached != null)
                return JsonSerializer.Deserialize<List<ParticipanteDto>>(cached)!;

            var participantes = await participanteRepository.GetParticipantesAsync();
            var dto = mapper.Map<List<ParticipanteDto>>(participantes);
            await cache.SetStringAsync(CacheKeyAll, JsonSerializer.Serialize(dto), CacheOptions);
            return dto;
        }

        public async Task<ParticipanteDto?> GetParticipanteByIdAsync(int id)
        {
            var cached = await cache.GetStringAsync(CacheKeyById(id));
            if (cached != null)
                return JsonSerializer.Deserialize<ParticipanteDto>(cached);

            var participante = await participanteRepository.GetParticipanteByIdAsync(id);
            var dto = mapper.Map<ParticipanteDto?>(participante);
            if (dto != null)
                await cache.SetStringAsync(CacheKeyById(id), JsonSerializer.Serialize(dto), CacheOptions);
            return dto;
        }

        public async Task<ParticipanteDto> InsertParticipanteAsync(ParticipanteDto participante)
        {
            var entity = mapper.Map<Participante>(participante);
            var newId = await participanteRepository.InsertParticipanteAsync(entity);
            participante.Codigo = newId;
            await cache.RemoveAsync(CacheKeyAll);
            return participante;
        }

        public async Task<ParticipanteDto?> UpdateParticipanteAsync(int id, ParticipanteDto participante)
        {
            var entity = mapper.Map<Participante>(participante);
            entity.Id = id;
            var updated = await participanteRepository.UpdateParticipanteAsync(entity);
            if (!updated) return null;

            participante.Codigo = id;
            await cache.RemoveAsync(CacheKeyAll);
            await cache.RemoveAsync(CacheKeyById(id));
            return participante;
        }

        public async Task<bool> DeleteParticipanteAsync(int id)
        {
            var deleted = await participanteRepository.DeleteParticipanteAsync(id);
            if (deleted)
            {
                await cache.RemoveAsync(CacheKeyAll);
                await cache.RemoveAsync(CacheKeyById(id));
            }
            return deleted;
        }
    }
}