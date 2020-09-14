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
    public class FilmesController : ControllerBase
    {

        private readonly IFilmeRepository _filmeRepository;

        public FilmesController(IFilmeRepository filmeRepository)
        {
            this._filmeRepository = filmeRepository;
        }

        // GET api/Consumer/filmes/detalhe/1
        // Retorna 200 OK quando encontra filmes para o codigo enviado
        // Retorna 404 NotFound quando não encontra filmes para o codigo especificado

        /// <summary>
        /// Listar detalhe de um determinado filme 
        /// </summary>
        /// <param name="idFilme"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("detalhe/{idFilme}")]
        public ActionResult<string> Get(int idFilme)
        {
            try
            {
                var filme = _filmeRepository.Get(idFilme);

                if (filme != null)
                {
                    return Ok(filme);
                }
                else
                {
                    return NotFound("Filme não encontrado");
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // GET api/Consumer/filmes/categoria/Ação
        // Retorna 200 OK quando encontra filmes para a categoria
        // Retorna 404 NotFound quando não encontra filmes para a categoria especificada
        
        /// <summary>
        /// Listar filmes de acordo com a categoria 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("categoria/{categoria}")]
        public ActionResult<string> GetFilmesPorCategoria(string categoria)
        {
            try
            {
                var filmes = _filmeRepository.GetAllFilmesCategoria(categoria);

                if (filmes != null)
                {
                    return Ok(filmes);
                }
                else
                {
                    return NotFound("Filme não encontrado");
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // GET api/Consumer/filmes/PalavraChave
        // Retorna 200 OK quando encontra filmes relacionado à palavra chave
        // Retorna 404 NotFound quando não encontra nenhum filme relacionado à palavra chave
        /// <summary>
        /// Listar filmes de acordo com uma palavra chave
        /// </summary>
        /// <param name="palavraChave"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("palavraChave/{palavraChave}")]
        public ActionResult<string> GetFilmesPorPalavraChave(string palavraChave)
        {
            try
            {
                var filmes = _filmeRepository.GetAllFilmesPalavraChave(palavraChave);

                if (filmes != null)
                {
                    return Ok(filmes);
                }
                else
                {
                    return NotFound("Filme não encontrado");
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
