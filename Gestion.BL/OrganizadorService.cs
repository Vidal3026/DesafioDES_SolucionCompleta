using AutoMapper;
using Gestion.BL.Interfaces;
using Gestion.DAL.Interfaces;
using Gestion.Entities.DTO;
using Gestion.Entities.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Gestion.BL
{
    public class OrganizadorService(IOrganizadorRepository organizadorRepository, IMapper mapper, IDistributedCache cache) : IOrganizadorService
    {
        private const string CacheKeyAll = "organizadores:all";
        private static string CacheKeyById(int id) => $"organizador:{id}";
        private static readonly DistributedCacheEntryOptions CacheOptions = new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) };

        public async Task<List<OrganizadorDto>> GetOrganizadoresAsync()
        {
            var cached = await cache.GetStringAsync(CacheKeyAll);
            if (cached != null)
                return JsonSerializer.Deserialize<List<OrganizadorDto>>(cached)!;

            var organizadores = await organizadorRepository.GetOrganizadoresAsync();
            var dto = mapper.Map<List<OrganizadorDto>>(organizadores);
            await cache.SetStringAsync(CacheKeyAll, JsonSerializer.Serialize(dto), CacheOptions);
            return dto;
        }

        public async Task<OrganizadorDto?> GetOrganizadorByIdAsync(int id)
        {
            var cached = await cache.GetStringAsync(CacheKeyById(id));
            if (cached != null)
                return JsonSerializer.Deserialize<OrganizadorDto>(cached);

            var organizador = await organizadorRepository.GetOrganizadorByIdAsync(id);
            var dto = mapper.Map<OrganizadorDto?>(organizador);
            if (dto != null)
                await cache.SetStringAsync(CacheKeyById(id), JsonSerializer.Serialize(dto), CacheOptions);
            return dto;
        }

        public async Task<OrganizadorDto> InsertOrganizadorAsync(OrganizadorDto organizador)
        {
            var entity = mapper.Map<Organizador>(organizador);
            var newId = await organizadorRepository.InsertOrganizadorAsync(entity);
            organizador.Codigo = newId;
            await cache.RemoveAsync(CacheKeyAll);
            return organizador;
        }

        public async Task<OrganizadorDto?> UpdateOrganizadorAsync(int id, OrganizadorDto organizador)
        {
            var entity = mapper.Map<Organizador>(organizador);
            entity.Id = id;
            var updated = await organizadorRepository.UpdateOrganizadorAsync(entity);
            if (!updated) return null;

            organizador.Codigo = id;
            await cache.RemoveAsync(CacheKeyAll);
            await cache.RemoveAsync(CacheKeyById(id));
            return organizador;
        }

        public async Task<bool> DeleteOrganizadorAsync(int id)
        {
            var deleted = await organizadorRepository.DeleteOrganizadorAsync(id);
            if (deleted)
            {
                await cache.RemoveAsync(CacheKeyAll);
                await cache.RemoveAsync(CacheKeyById(id));
            }
            return deleted;
        }
    }
}