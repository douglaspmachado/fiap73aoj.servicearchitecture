using System;
using System.Collections.Generic;
using System.Text;
using App.Domain.Entity;

namespace App.Application.Interfaces
{
   public interface IFilmeRepository
    {
        IEnumerable<Filme> GetAll();

        IEnumerable<Filme> GetAllFilmesPalavraChave(string pNomeFilme, string pDiretor, string pProdutor, string pCategoria);

        IEnumerable<Filme> GetAllFilmesCategoria(int pCategoriaFilme);

        Filme Get(int pCodigoFilme);

        int Insert(Filme filme);

        int Update(Filme filme);

        int Delete(Filme filme);

    }
}
