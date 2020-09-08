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
                                    SELECT F.TITULO AS Titulo
                                          ,F.DIRETOR AS Diretor
                                          ,F.PRODUTOR AS Produtor
                                          ,F.DATA_LANCAMENTO AS DataLancamento
                                          ,C.NOME AS Categoria 
                                    FROM dbo.TBFILME AS F
                                    INNER JOIN dbo.TBCATEGORIA AS C
                                    ON F.CATEGORIA = C.CODIGO
                                    WHERE F.CODIGO = '{0}' ", pCodigoFilme);
                
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

        public IEnumerable<Filme> GetAllFilmesCategoria(int pCategoriaFilme)
        {
            IEnumerable<Filme> filmes;

            using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
            {
                filmes = conn.Query<Filme>(@"
                                    SELECT F.TITULO AS Titulo
                                          ,F.DIRETOR AS Diretor
                                          ,F.PRODUTOR AS Produtor
                                          ,F.DATA_LANCAMENTO AS DataLancamento
                                          ,C.NOME AS Categoria 
                                    FROM dbo.TBFILME AS F
                                    INNER JOIN dbo.TBCATEGORIA AS C
                                    ON F.CATEGORIA = C.CODIGO
                                    WHERE F.CATEGORIA = '{0}' ", pCategoriaFilme);
            }

            return filmes;
        }

        public IEnumerable<Filme> GetAllFilmesPalavraChave(string pPalavraChave)
        {
            IEnumerable<Filme> filmes = null;

         
                using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                {
                    filmes = conn.Query<Filme>(@"
                                        SELECT F.TITULO AS Titulo
                                            ,F.DIRETOR AS Diretor
                                            ,F.PRODUTOR AS Produtor
                                            ,F.DATA_LANCAMENTO AS DataLancamento
                                            ,C.NOME AS Categoria 
                                        FROM dbo.TB_FILME AS F
                                        INNER JOIN dbo.TBCATEGORIA AS C
                                        ON F.CATEGORIA = C.CODIGO
                                        WHERE F.TITULO LIKE '%{0}%' ", (pPalavraChave));
                                        
                }
                
                if(filmes == null)
                {
                    using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                    {
                        filmes = conn.Query<Filme>(@"
                                            SELECT F.TITULO AS Titulo
                                                ,F.DIRETOR AS Diretor
                                                ,F.PRODUTOR AS Produtor
                                                ,F.DATA_LANCAMENTO AS DataLancamento
                                                ,C.NOME AS Categoria 
                                            FROM dbo.TB_FILME AS F
                                            INNER JOIN dbo.TBCATEGORIA AS C
                                            ON F.CATEGORIA = C.CODIGO
                                            WHERE F.DIRETOR LIKE '%{0}%' ", (pPalavraChave));
                                            
                    }
                }

                if(filmes == null)
                {
                    using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                    {
                        filmes = conn.Query<Filme>(@"
                                            SELECT F.TITULO AS Titulo
                                                ,F.DIRETOR AS Diretor
                                                ,F.PRODUTOR AS Produtor
                                                ,F.DATA_LANCAMENTO AS DataLancamento
                                                ,C.NOME AS Categoria 
                                            FROM dbo.TB_FILME AS F
                                            INNER JOIN dbo.TBCATEGORIA AS C
                                            ON F.CATEGORIA = C.CODIGO
                                            WHERE F.PPRODUTOR LIKE '%{0}%' ", (pPalavraChave));
                                            
                    }
                }

                if(filmes == null)
                {
                    using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
                    {
                        filmes = conn.Query<Filme>(@"
                                            SELECT F.TITULO AS Titulo
                                                ,F.DIRETOR AS Diretor
                                                ,F.PRODUTOR AS Produtor
                                                ,F.DATA_LANCAMENTO AS DataLancamento
                                                ,C.NOME AS Categoria 
                                            FROM dbo.TB_FILME AS F
                                            INNER JOIN dbo.TBCATEGORIA AS C
                                            ON F.CATEGORIA = C.CODIGO
                                            WHERE C.NOME LIKE '%{0}%' ", (pPalavraChave));                       
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
                                    SELECT F.TITULO AS Titulo
                                          ,F.DIRETOR AS Diretor
                                          ,F.PRODUTOR AS Produtor
                                          ,F.DATA_NASCIMENTO AS DataLancamento
                                          ,C.NOME AS Categoria 
                                    FROM dbo.TBFILME AS F
                                    INNER JOIN dbo.TBCATEGORIA AS C
                                    ON F.CATEGORIA = C.CODIGO");
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
