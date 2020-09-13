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

namespace App.Producer.Controllers
{

    [Route("api/Producer/[controller]")]
    [ApiController]
    public class SuporteController : Controller
    {
        private readonly IServiceMessage _serviceMessage;


        public SuporteController(IServiceMessage serviceBus)
        {
            this._serviceMessage = serviceBus;
        }


        /// <summary>
        /// Possibilidade de abrir um chamado técnico de algum problema que está acontecendo
        /// </summary>
        /// <param name="chamadoTecnico"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AbrirChamado")]
        public async Task<IActionResult> AbrirChamado([FromBody] ChamadoTecnico chamadoTecnico)
        {
            string message = string.Empty;

            try
            {
                
                if (chamadoTecnico != null)
                {
                    chamadoTecnico.DataAbertura = DateTime.Now;

                    message = JsonConvert.SerializeObject(chamadoTecnico);
                    

                    _serviceMessage.GetConnectionFactory();

                    if (_serviceMessage.CreateConnection())
                    {

                        _serviceMessage.CreateModel();

                        if (_serviceMessage.IsChannelOpen())
                        {
                            
                            if (_serviceMessage.SendMessageQueue(QueueMessage.ABERTURA_CHAMADO, message ))
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
                else
                {
                    return BadRequest("Requisição incorreta");
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