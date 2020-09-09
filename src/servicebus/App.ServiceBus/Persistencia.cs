using System;
using System.Collections.Generic;
using System.Text;
using App.Application.Interfaces;

namespace App.ServiceBus
{
   public  class Persistencia
    {
        private readonly IChamadoTecnicoRepository _chamadoTecnicoRepository;

        public Persistencia(IChamadoTecnicoRepository chamadoTecnicoRepository)
        {
            _chamadoTecnicoRepository = chamadoTecnicoRepository;
        }

        public void InsereChamado(string message)
        {
            _chamadoTecnicoRepository.InsereChamadoDB(message);

        }

    }
}
