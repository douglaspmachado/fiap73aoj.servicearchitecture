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

        private const string QueueFavoritarFilme = "FAVORITAR_FILME";

        public FilmeController(IServiceMessage serviceBus)
        {
            this._serviceMessage = serviceBus;
        }


        [HttpPost]
        [Route("Favoritar/{id}")]
        public async Task<IActionResult> Favoritar(int id)
        {
            try
            {
                
                _serviceMessage.GetConnectionFactory();

                if (_serviceMessage.CreateConnection())
                {

                    _serviceMessage.CreateModel();

                    if (_serviceMessage.IsChannelOpen())
                    {
                        if (_serviceMessage.SendMessageQueue(QueueMessage.FAVORITAR_FILME, "Teste"))
                        {
                            return Ok();
                        }
                        else
                        {
                            return BadRequest("Ocorreu um erro ao postar a mensagem no message broker");
                        }

                    }
                    else
                    {
                        return BadRequest("Falha na conexão com o canal");
                    }

                }
                else
                {
                    return BadRequest("Falha ao conectar com o message broker");
                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu erro: {ex.Message} - Detalhes: {ex.InnerException}");
            }
            finally
            {
                _serviceMessage.CloseConnection();
            }

        }
    }
}
