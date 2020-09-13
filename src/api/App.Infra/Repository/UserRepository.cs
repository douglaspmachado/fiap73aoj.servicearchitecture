using App.Domain.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using App.Application.Interfaces;
using System.Data;

namespace App.Infra.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly IConfiguration _configuration;
        private StringBuilder SQL = new StringBuilder();

        public UserRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(_configuration.GetConnectionString("NETFLIX"));
            }
        }

        public IEnumerable<Filme> GetFilmesAssistidos(int codigoUsuario)
        {            
            IEnumerable<Filme> filmes = null;
            
            try
            {
                using (IDbConnection conn = Connection)
                {
                    SQL.AppendLine(string.Format(@"
                                    SELECT
                                        F.CODIGO,
                                        F.TITULO,
                                        F.DIRETOR,
                                        F.PRODUTOR,
                                        F.DATA_LANÇAMENTO,
                                        F.CATEGORIA
                                    FROM 
                                        TAB_FILME F
                                    INNER JOIN TAB_FILMES_ASSISTIDOS A ON
                                        F.CODIGO = A.CD_FILME
                                    WHERE
                                        A.CD_USUARIO = {0} ", codigoUsuario));
                
                    filmes = conn.Query<Filme>(SQL.ToString());
                    return filmes;
                }
                
            }
            catch (Exception ex)
            {
                filmes = null;
                throw new Exception(ex.Message);
            }
  
        }
    }
}
