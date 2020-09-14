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
                                        F.CODIGO AS Codigo,
                                        F.TITULO AS Titulo,
                                        F.DIRETOR AS Diretor,
                                        F.PRODUTOR AS Produtor,
                                        F.DATA_LANCAMENTO AS DataLançamento,
                                        F.CATEGORIA AS Categoria
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

        public bool VotarFilme(Usuario usuario)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {

                    foreach (var item in usuario.FilmesParaVotar)
                    {
                        SQL = new StringBuilder();

                        SQL.AppendLine(string.Format(@"
                        INSERT INTO 
                            TAB_FILMES_CURTIDOS
                                (COD_USUARIO,
                                COD_FILME,
                                NOTA) 
                            VALUES
                                ('{0}'
                                ,'{1}'
                                ,'{2}');"
                                , usuario.Codigo
                                , item.Codigo
                                , item.Nota
                                ));

                        conn.Execute(SQL.ToString());
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);                
            }

        }

        public bool FavoritarFilme(Usuario usuario)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {

                    foreach (var item in usuario.FilmesParaAssistir)
                    {
                        SQL = new StringBuilder();

                        SQL.AppendLine(string.Format(@"
                        INSERT INTO 
                            TAB_FILMES_ASSISTIR
                                (CODI_USUARIO,
                                CODI_FILME) 
                            VALUES
                                ('{0}'
                                ,'{1}');"
                                , usuario.Codigo
                                , item.Codigo));

                        conn.Execute(SQL.ToString());
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);                
            }

        }
    }
}
