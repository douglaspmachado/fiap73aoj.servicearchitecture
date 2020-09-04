using App.Domain.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using App.Application.Interfaces;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Filme> GetAll()
        {
            IEnumerable<Filme> filmes;

            using (MySqlConnection conn = new MySqlConnection(_configuration["netflixDB"]))
            {
                filmes = conn.Query<Filme>("SELECT * FROM TAB_FILMES");
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
