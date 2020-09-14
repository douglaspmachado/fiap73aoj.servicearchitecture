using System;

namespace App.Domain.Entity
{
    public class Filme
    {
        public string Codigo { get; set; }

        public string  Titulo { get; set; }

        public string Diretor { get; set; }

        public string Produtor { get; set; }

        public DateTime DataLançamento { get; set; }

        public string Categoria { get; set; }

        public int? Nota { get; set; }
    }
}
