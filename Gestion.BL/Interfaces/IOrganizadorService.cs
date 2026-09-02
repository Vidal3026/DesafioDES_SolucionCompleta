using Gestion.Entities.DTO;

namespace Gestion.BL.Interfaces
{
    public interface IOrganizadorService
    {
        Task<List<OrganizadorDto>> GetOrganizadoresAsync();
        Task<OrganizadorDto?> GetOrganizadorByIdAsync(int id);
        Task<OrganizadorDto> InsertOrganizadorAsync(OrganizadorDto organizador);
        Task<OrganizadorDto?> UpdateOrganizadorAsync(int id, OrganizadorDto organizador);
        Task<bool> DeleteOrganizadorAsync(int id);
    }
}