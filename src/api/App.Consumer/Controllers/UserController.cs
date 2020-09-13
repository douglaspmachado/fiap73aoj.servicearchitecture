using System;
using System.Collections.Generic;
using System.Linq;
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

        [Route("Assistidos/{idUsuario}")]
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

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
