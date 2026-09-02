using System.Data;
namespace Gestion.DAL.Interfaces
{
    public interface IDatabaseRepository
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null);
        Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null);
        Task<int> ExecuteAsync(string sql, object? parameters = null, IDbTransaction? transaction = null);
        Task<T?> ExecuteScalarAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null);
        Task<IDbTransaction> BeginTransactionAsync();
    }
}