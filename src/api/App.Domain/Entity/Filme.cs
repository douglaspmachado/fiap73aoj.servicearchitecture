using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity
{
    public class Filme
    {
        public int Codigo { get; set; }

        public string  Titulo { get; set; }

        public string Diretor { get; set; }

        public string Produtor { get; set; }

        public DateTime DataLançamento { get; set; }

        public Categoria Categoria { get; set; }

    }
}
