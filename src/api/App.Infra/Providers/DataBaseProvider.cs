using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace App.Infra.Providers
{
    public class DataBaseProvider
    {

        private readonly IConfiguration _config;

        public DataBaseProvider(IConfiguration config)
        {
            _config = config;

        }

        public IDbConnection Connection
        {

            get { return new MySqlConnection(_config.GetConnectionString("NETFLIX")); }
        }
    }
}
