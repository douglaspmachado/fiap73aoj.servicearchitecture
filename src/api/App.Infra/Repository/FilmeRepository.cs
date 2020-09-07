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
                                          ,F.DATA_NASCIMENTO AS DataNascimento
                                          ,C.NOME AS Categoria 
                                    FROM dbo.TB_FILME AS F
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
                                          ,F.DATA_NASCIMENTO AS DataNascimento
                                          ,C.NOME AS Categoria 
                                    FROM dbo.TB_FILME AS F
                                    INNER JOIN dbo.TBCATEGORIA AS C
                                    ON F.CATEGORIA = C.CODIGO
                                    WHERE F.CATEGORIA = '{0}' ", pCategoriaFilme);
            }

            return filmes;
        }

        public IEnumerable<Filme> GetAllFilmesPalavraChave(string pNomeFilme, string pDiretor, string pProdutor, string pCategoria)
        {
            IEnumerable<Filme> filmes;

            using (MySqlConnection conn = new MySqlConnection(_configuration["NETFLIX"]))
            {
                filmes = conn.Query<Filme>(@"
                                    SELECT F.TITULO AS Titulo
                                          ,F.DIRETOR AS Diretor
                                          ,F.PRODUTOR AS Produtor
                                          ,F.DATA_NASCIMENTO AS DataNascimento
                                          ,C.NOME AS Categoria 
                                    FROM dbo.TB_FILME AS F
                                    INNER JOIN dbo.TBCATEGORIA AS C
                                    ON F.CATEGORIA = C.CODIGO
                                    WHERE F.TITULO LIKE '%{0}%' 
                                    OR F.DIRETOR LIKE '%{1}%'
                                    OR F.PRODUTOR LIKE '%{2}%'
                                    OR C.NOME LIKE '%{3}%' ", (pNomeFilme, pDiretor, pProdutor, pCategoria));
                                    
            }

            return filmes;
        }

        public IEnumerable<Filme> GetAll()
        {
            IEnumerable<Filme> filmes;

            using (MySqlConnection conn = new MySqlConnection(_configuration["netflixDB"]))
            {
                filmes = conn.Query<Filme>(@"
                                    SELECT F.TITULO AS Titulo
                                          ,F.DIRETOR AS Diretor
                                          ,F.PRODUTOR AS Produtor
                                          ,F.DATA_NASCIMENTO AS DataNascimento
                                          ,C.NOME AS Categoria 
                                    FROM dbo.TB_FILME AS F
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
