using System;
using System.Collections.Generic;
using System.Text;
using App.Domain.Entity;

namespace App.Application.Interfaces
{
   public interface IFilmeRepository
    {
        IEnumerable<Filme> GetAll();

        Filme Get(int pCodigoFilme);

        int Insert(Filme filme);

        int Update(Filme filme);

        int Delete(Filme filme);

    }
}
