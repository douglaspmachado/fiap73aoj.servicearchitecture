using App.Application.Interfaces;
using App.Domain.Entity;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infra.Repository
{
    public class ChamadoTecnicoRepository : IChamadoTecnicoRepository
    {
        private readonly IConfiguration _configuration;
        private StringBuilder SQL = new StringBuilder();


        public ChamadoTecnicoRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void InsereChamadoDB(string message)
        {
            SQL = new StringBuilder();

            try
            {
                if (!string.IsNullOrEmpty(message))
                {

                    var chamadoTec = JsonConvert.DeserializeObject<ChamadoTecnico>(message);

                    using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                    {

                        SQL.AppendLine(string.Format(@"
                                
                            INSERT INTO TAB_CHAMADO
                           ([CODIGO_CHAMADO]
                           ,[TITULO]
                           ,[DESCRICAO]
                           ,[CODIGO_USUARIO]
                           ,[DATA_ABERTURA]) 
                     VALUES
                           ('{0}'
                           ,'{1}'
                           ,'{2}'
                           , {3},NOW());"


                    , GeraCodigoChamado(),
                    chamadoTec.Titulo,
                    chamadoTec.Descricao,
                    chamadoTec.CodigoUsuario));

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        public string GeraCodigoChamado()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");

            string codigo = $"{DateTime.Now.Date.Year}{DateTime.Now.Date.Month}{DateTime.Now.Date.Day}{guid.Substring(0, 10)}";

            return codigo.ToUpper();
        }
    }
}
