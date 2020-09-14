using System;
using System.Collections.Generic;
using System.Linq;
using App.Domain.Entity;
using System.Threading.Tasks;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Consumer.Controllers
{
    [Route("api/Consumer/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _iUserRepository;

        public UserController(IUserRepository iUserRepository)
        {
            this._iUserRepository = iUserRepository;
        }

        /// <summary>
        /// Listar filmes visualizados por determinado usuário
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Assistidos/{codigoUsuario}")]
        public ActionResult<string> GetFilmesAssistidos(int codigoUsuario)
        {
            try
            {
                var filmes = _iUserRepository.GetFilmesAssistidos(codigoUsuario);

                if (filmes != null)
                {
                    return Ok(filmes);
                }
                else
                {
                    return NotFound("Filme não encontrado");
                }
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Possibilidade de atribuir notas a filmes de seu interesse
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Votar")]
       public async Task<IActionResult> VotarFilme([FromBody]Usuario usuario)
        {
            try
            {
                bool execCount = _iUserRepository.VotarFilme(usuario);

                if (execCount)
                {
                    return Ok(execCount.ToString());
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Possibilidade de favoritar filmes para assistir posteriormente 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Favoritar")]
       public async Task<IActionResult> FavoritarFilme([FromBody]Usuario usuario)
        {
            try
            {
                bool execCount = _iUserRepository.FavoritarFilme(usuario);

                if (execCount)
                {
                    return Ok(execCount.ToString());
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
