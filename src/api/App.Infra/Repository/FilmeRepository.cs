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
    public class FilmeRepository : IFilmeRepository
    {

        private readonly IConfiguration _configuration;
        private StringBuilder SQL = new StringBuilder();


        public FilmeRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public int Delete(Filme filme)
        {
            throw new NotImplementedException();
        }

        public Filme Get(int pCodigoFilme)
        {
            Filme filme = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                {
                    conn.Query<Filme>(@"
                                    SELECT TITULO AS Titulo
                                          ,DIRETOR AS Diretor
                                          ,PRODUTOR AS Produtor
                                          ,DATA_LANCAMENTO AS DataLancamento
                                          ,CATEGORIA AS Categoria 
                                    FROM TAB_FILME
                                    WHERE CODIGO = '{0}' ", pCodigoFilme);
                
                    filme = conn.QueryFirstOrDefault<Filme>(SQL.ToString());
                }
            }
            catch (Exception ex)
            {
                filme = null;
                throw new Exception(ex.Message);
            }
            
            return filme;
        }

        public IEnumerable<Filme> GetAllFilmesCategoria(string pCategoriaFilme)
        {
            IEnumerable<Filme> filmes;

            using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
            {
                filmes = conn.Query<Filme>(@"
                                    SELECT TITULO AS Titulo
                                          ,DIRETOR AS Diretor
                                          ,PRODUTOR AS Produtor
                                          ,DATA_LANCAMENTO AS DataLancamento
                                          ,CATEGORIA AS Categoria 
                                    FROM dbo.TAB_FILME
                                    WHERE CATEGORIA = '{0}' ", pCategoriaFilme);
            }

            return filmes;
        }

        public IEnumerable<Filme> GetAllFilmesPalavraChave(string pPalavraChave)
        {
            IEnumerable<Filme> filmes = null;

         
                using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                {
                    filmes = conn.Query<Filme>(@"
                                        SELECT TITULO AS Titulo
                                            ,DIRETOR AS Diretor
                                            ,PRODUTOR AS Produtor
                                            ,DATA_LANCAMENTO AS DataLancamento
                                            ,CATEGORIA AS Categoria 
                                        FROM TAB_FILME
                                        WHERE F.TITULO LIKE '%{0}%' ", (pPalavraChave));
                                        
                }
                
                if(filmes == null)
                {
                    using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                    {
                        filmes = conn.Query<Filme>(@"
                                            SELECT TITULO AS Titulo
                                                ,DIRETOR AS Diretor
                                                ,PRODUTOR AS Produtor
                                                ,DATA_LANCAMENTO AS DataLancamento
                                                ,NOME AS Categoria 
                                            FROM TAB_FILME
                                            WHERE DIRETOR LIKE '%{0}%' ", (pPalavraChave));
                                            
                    }
                }

                if(filmes == null)
                {
                    using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                    {
                        filmes = conn.Query<Filme>(@"
                                            SELECT TITULO AS Titulo
                                                ,DIRETOR AS Diretor
                                                ,PRODUTOR AS Produtor
                                                ,DATA_LANCAMENTO AS DataLancamento
                                                ,CATEGORIA AS Categoria 
                                            FROM dbo.TAB_FILME
                                            WHERE PRODUTOR LIKE '%{0}%' ", (pPalavraChave));
                                            
                    }
                }

                if(filmes == null)
                {
                    using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                    {
                        filmes = conn.Query<Filme>(@"
                                            SELECT TITULO AS Titulo
                                                ,DIRETOR AS Diretor
                                                ,PRODUTOR AS Produtor
                                                ,DATA_LANCAMENTO AS DataLancamento
                                                ,CATEGORIA AS Categoria 
                                            FROM TAB_FILME
                                            WHERE CATEGORIA LIKE '%{0}%' ", (pPalavraChave));                       
                    }
                }
                return filmes;
        }

        public IEnumerable<Filme> GetAll()
        {
            IEnumerable<Filme> filmes;

            using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
            {
                filmes = conn.Query<Filme>(@"
                                    SELECT TITULO AS Titulo
                                          ,DIRETOR AS Diretor
                                          ,PRODUTOR AS Produtor
                                          ,DATA_NASCIMENTO AS DataLancamento
                                          ,CATEGORIA AS Categoria 
                                    FROM TAB_FILME AS F");
            }

            return filmes;
        }

        public int Insert(Filme filme)
        {
            throw new NotImplementedException();
        }

        public int Update(Filme filme)
        {
            throw new NotImplementedException();
        }
    }
}
