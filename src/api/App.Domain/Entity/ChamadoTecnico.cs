using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity
{
   public class ChamadoTecnico
    {

        public string CodigoChamado { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime DataAbertura { get; set; }

        public int CodigoUsuario { get; set; }
    }
}
