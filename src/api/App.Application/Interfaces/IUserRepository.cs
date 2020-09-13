using System;
using System.Collections.Generic;
using System.Text;
using App.Domain.Entity;

namespace App.Application.Interfaces
{
   public interface IUserRepository
    {
        IEnumerable<Filme> GetFilmesAssistidos(int codigoUsuario);

    }
}
