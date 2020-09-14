using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity
{
    public class Usuario
    {
        public int Codigo { get; set; }

        public string  Nome { get; set; }

        public List<Filme> FilmesAssistidos { get; set; }

        public List<Filme> FilmesParaAssistir { get; set; }

        public List<Filme> FilmesParaVotar { get; set; }

    }
}