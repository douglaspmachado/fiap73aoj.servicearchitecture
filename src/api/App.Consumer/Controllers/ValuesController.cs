﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Consumer.Controllers
{
    [Route("netflix/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IFilmeRepository _filmeRepository;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET netflix/filme/1
        [HttpGet("{idFilme}")]
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
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        // GET netflix/filme/categoria/1
        [HttpGet("{idCategoria}")]
        public ActionResult<string> GetFilmesPorCategoria(int idCategoria)
        {
            try
            {
                var filme = _filmeRepository.GetAllFilmesCategoria(idCategoria);

                if (filme != null)
                {
                    return Ok(filme);
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

        // GET netflix/filme/"{PalavraChave}"
        [HttpGet("{palavraChave}")]
        public ActionResult<string> GetFilmesPorCategoria(string palavraChave)
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