using App.Application.Interfaces;
using App.Domain.Entity;
using Dapper;
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

        public string InsereChamadoDB(string message)
        {
            SQL = new StringBuilder();
            ChamadoTecnico chamado = null;

            try
            {
                if (!string.IsNullOrEmpty(message))
                {

                    var chamadoTec = JsonConvert.DeserializeObject<ChamadoTecnico>(message);

                    using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("NETFLIX")))
                    {

                        chamadoTec.CodigoChamado = GeraCodigoChamado();

                            SQL.AppendLine(string.Format(@"
                                
                                INSERT INTO TAB_CHAMADO
                               (CODIGO
                               ,TITULO
                               ,DESCRICAO
                               ,CODIGO_USUARIO
                               ,DATA_ABERTURA) 
                         VALUES
                               ('{0}'
                               ,'{1}'
                               ,'{2}'
                               , {3},NOW());"


                        , chamadoTec.CodigoChamado,
                        chamadoTec.Titulo,
                        chamadoTec.Descricao,
                        chamadoTec.CodigoUsuario));


                        conn.Execute(SQL.ToString());

                        message = JsonConvert.SerializeObject(chamadoTec);

                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return message;

        }

        public string GeraCodigoChamado()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");

            string codigo = $"{DateTime.Now.Date.Year}{DateTime.Now.Date.Month}{DateTime.Now.Date.Day}{guid.Substring(0, 10)}";

            return codigo.ToUpper();
        }
    }
}
