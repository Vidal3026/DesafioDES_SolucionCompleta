using Gestion.DAL.Interfaces;
using Gestion.Entities.Models;

namespace Gestion.DAL
{
    public class ParticipanteRepository(IDatabaseRepository databaseRepository) : IParticipanteRepository
    {
        private static class Queries
        {
            public const string GetAll = "SELECT * FROM Participantes";
            public const string GetById = "SELECT * FROM Participantes WHERE Id = @Id";
            public const string Insert = "INSERT INTO Participantes (Nombre, Email, EventoId) VALUES (@Nombre, @Email, @EventoId); SELECT SCOPE_IDENTITY()";
            public const string Update = "UPDATE Participantes SET Nombre = @Nombre, Email = @Email, EventoId = @EventoId WHERE Id = @Id";
            public const string Delete = "DELETE FROM Participantes WHERE Id = @Id";
        }

        public async Task<List<Participante>> GetParticipantesAsync()
            => [.. (await databaseRepository.QueryAsync<Participante>(Queries.GetAll))];

        public async Task<Participante?> GetParticipanteByIdAsync(int id)
            => await databaseRepository.QueryFirstOrDefaultAsync<Participante>(Queries.GetById, new { Id = id });

        public async Task<int> InsertParticipanteAsync(Participante participante)
            => await databaseRepository.ExecuteScalarAsync<int>(Queries.Insert, new { participante.Nombre, participante.Email, participante.EventoId });

        public async Task<bool> UpdateParticipanteAsync(Participante participante)
        {
            var rows = await databaseRepository.ExecuteAsync(Queries.Update, new { participante.Id, participante.Nombre, participante.Email, participante.EventoId });
            return rows > 0;
        }

        public async Task<bool> DeleteParticipanteAsync(int id)
            => await databaseRepository.ExecuteAsync(Queries.Delete, new { Id = id }) > 0;
    }
}