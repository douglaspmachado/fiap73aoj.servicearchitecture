using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infra.Repository
{
    public class FilmeRepository 
    {

        private readonly IConfiguration _configuration;
        private StringBuilder SQL = new StringBuilder();


        public FilmeRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
    }
}
