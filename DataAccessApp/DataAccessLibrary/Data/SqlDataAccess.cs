using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;

namespace DataAccessLibrary.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadDateAsync<T, U>(string storedProcedure, U parameters, string connectionStringName = "Default")
        {
            string ConnectionSring = _config.GetConnectionString(connectionStringName);
             
            using IDbConnection connection = new SqlConnection(ConnectionSring);

            var rows = await connection.QueryAsync<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);

            return rows;
        }

        public async Task SaveDateAsync<T>(string storedProcedure, T parameters, string connectionStringName = "Default")
        {
            string ConnectionSring = _config.GetConnectionString(connectionStringName);

            using IDbConnection connection = new SqlConnection(ConnectionSring);

            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
