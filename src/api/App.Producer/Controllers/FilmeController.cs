using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;
using App.Infra.Providers.Mensageria;

namespace App.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {

        private readonly IServiceMessage _serviceMessage;

        public FilmeController(IServiceMessage serviceBus)
        {
            this._serviceMessage = serviceBus;
        }

        


    }
}
