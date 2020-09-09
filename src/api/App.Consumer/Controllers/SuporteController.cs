using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;
using App.Infra.Providers.Mensageria;
using App.Domain.Entity;
using Newtonsoft.Json;

namespace App.Consumer.Controllers
{

    [Route("api/Consumer/[controller]")]
    [ApiController]
    public class SuporteController : Controller
    {
        private readonly IServiceMessage _serviceMessage;


        public SuporteController(IServiceMessage serviceBus)
        {
            this._serviceMessage = serviceBus;
        }

        [HttpPost]
        [Route("AtenderChamado")]
        public async Task<IActionResult> AtenderChamado()
        {
            string message = string.Empty;

            try
            {

                _serviceMessage.GetConnectionFactory();

                if (_serviceMessage.CreateConnection())
                {

                    _serviceMessage.CreateModel();

                    if (_serviceMessage.IsChannelOpen())
                    {
                        message = _serviceMessage.ReceiveMessageQueue(QueueMessage.ABERTURA_CHAMADO);
                        

                        if (!string.IsNullOrEmpty(message))
                        {
                            return Ok(JsonConvert.DeserializeObject<ChamadoTecnico>(message));
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