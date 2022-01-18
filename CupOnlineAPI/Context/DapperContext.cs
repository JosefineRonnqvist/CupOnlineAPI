using Microsoft.Data.SqlClient;
using System.Data;

namespace CupOnlineAPI.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration config)
        {
            _configuration = config;
            _connectionString = _configuration.GetConnectionString("Default");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
