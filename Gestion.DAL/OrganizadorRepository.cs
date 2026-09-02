using Gestion.DAL.Interfaces;
using Gestion.Entities.Models;

namespace Gestion.DAL
{
    public class OrganizadorRepository(IDatabaseRepository databaseRepository) : IOrganizadorRepository
    {
        private static class Queries
        {
            public const string GetAll = "SELECT * FROM Organizadores";
            public const string GetById = "SELECT * FROM Organizadores WHERE Id = @Id";
            public const string Insert = "INSERT INTO Organizadores (Nombre, Cargo, EventoId) VALUES (@Nombre, @Cargo, @EventoId); SELECT SCOPE_IDENTITY()";
            public const string Update = "UPDATE Organizadores SET Nombre = @Nombre, Cargo = @Cargo, EventoId = @EventoId WHERE Id = @Id";
            public const string Delete = "DELETE FROM Organizadores WHERE Id = @Id";
        }

        public async Task<List<Organizador>> GetOrganizadoresAsync()
            => [.. (await databaseRepository.QueryAsync<Organizador>(Queries.GetAll))];

        public async Task<Organizador?> GetOrganizadorByIdAsync(int id)
            => await databaseRepository.QueryFirstOrDefaultAsync<Organizador>(Queries.GetById, new { Id = id });

        public async Task<int> InsertOrganizadorAsync(Organizador organizador)
            => await databaseRepository.ExecuteScalarAsync<int>(Queries.Insert, new { organizador.Nombre, organizador.Cargo, organizador.EventoId });

        public async Task<bool> UpdateOrganizadorAsync(Organizador organizador)
        {
            var rows = await databaseRepository.ExecuteAsync(Queries.Update, new { organizador.Id, organizador.Nombre, organizador.Cargo, organizador.EventoId });
            return rows > 0;
        }

        public async Task<bool> DeleteOrganizadorAsync(int id)
            => await databaseRepository.ExecuteAsync(Queries.Delete, new { Id = id }) > 0;
    }
}