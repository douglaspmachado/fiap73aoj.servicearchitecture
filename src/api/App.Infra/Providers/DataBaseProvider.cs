using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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

            get { return new MySqlConnection(_config.GetConnectionString("")); }
        }
    }
}
