
namespace DataAccessLibrary.Data
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadDateAsync<T, U>(string storedProcedure, U parameters, string connectionStringName = "Default");
        Task SaveDateAsync<T>(string storedProcedure, T parameters, string connectionStringName = "Default");
    }
}