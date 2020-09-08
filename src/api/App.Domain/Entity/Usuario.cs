using System.Collections.Generic;
using App.Domain.Entity;

namespace App.Domain
{
    public class Usuario
    {
        public int Codigo { get; set; }

        public string  Nome { get; set; }

        public List<Filme> FilmesAssistidos { get; set; }

        public List<Filme> FilmesParaAssistir { get; set; }

    }
}