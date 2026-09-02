using Gestion.Entities.Models;

namespace Gestion.DAL.Interfaces
{
    public interface IOrganizadorRepository
    {
        Task<List<Organizador>> GetOrganizadoresAsync();
        Task<Organizador?> GetOrganizadorByIdAsync(int id);
        Task<int> InsertOrganizadorAsync(Organizador organizador);
        Task<bool> UpdateOrganizadorAsync(Organizador organizador);
        Task<bool> DeleteOrganizadorAsync(int id);
    }
}