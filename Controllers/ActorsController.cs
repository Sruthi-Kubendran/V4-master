using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Data;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ActorsController : ControllerBase
    {
        IRepository repository;
        public ActorsController(IRepository repository)
        {
            this.repository = repository;
        }
        // GET: api/Actors
        [HttpGet]
        public IActionResult GetActors()
        {
            var actors = repository.GetActors();
            if (!actors.Any())
            {
                return NoContent();
            }
            return Ok(actors);
        }

        // GET: api/Actors/5
        [HttpGet("{id}", Name = "GetActor")]
        public IActionResult Get(int id)
        {
            var actor = repository.GetActor(id);
            if (actor==null)
            {
                return NotFound();
            }
            return Ok(actor);
        }

        // POST: api/Actors
        [HttpPost]
        public IActionResult Post([FromBody] Actor actor)
        {
            if(ModelState.IsValid)
            {
                bool result = repository.AddActor(actor);
                if (result)
                {
                    return Created("AddActor", actor.Id);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Actors/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Actor actor)
        {
            if (ModelState.IsValid && id==actor.Id)
            {
                bool result = repository.UpdateActor(actor);
                if (result)
                {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var actor = repository.GetActor(id);
            if (actor == null)
            {
                return NotFound();
            }
            bool result = repository.DeleteActor(actor);
            if (result)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
