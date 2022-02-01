using Microsoft.Data.SqlClient;
using System.Data;

namespace CupOnlineAPI.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        /// <summary>
        /// Uses IConfiguration to get the connectionstring from appsettings.json
        /// </summary>
        /// <param name="config">configuration</param>
        public DapperContext(IConfiguration config)
        {
            _configuration = config;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }


        /// <summary>
        /// Creates connection to database
        /// </summary>
        /// <returns>SQLConnection object</returns>
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
