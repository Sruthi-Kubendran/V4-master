using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Data;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.DTO;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        IRepository repository;
        public MoviesController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Movies
        [HttpGet]
        public IActionResult GetMovies()
        {
            var movies = repository.GetMovies();
            if (!movies.Any())
            {
                return NoContent();
            }
            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Get(int id)
        {
            var movie = repository.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        // POST: api/Movies
        [HttpPost]
        public IActionResult Post([FromBody] MovieDTO movie)
        {
            if (ModelState.IsValid)
            {
                bool result = repository.AddMovie(movie);
                if (result)
                {
                    return Created("AddMovie", movie);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MovieDTO movie)
        {
            if (ModelState.IsValid && id==movie.Movie.Id)
            {
                bool result = repository.UpdateMovie(movie);
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
            var movie = repository.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            bool result = repository.DeleteMovie(movie);
            if (result)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpGet("api/movies/actor/{id}")]
        //GET api/movies/actor/1
        //GET api/movies/actor?id=1
        public IActionResult MoviesByActor(int id)
        {
            var movies = repository.GetMoviesByActor(id);
            if (!movies.Any())
            {
                return NoContent();
            }
            return Ok(movies);
        }

        public IActionResult MoviesByGenre(int id)
        {
            bool isValid = Enum.IsDefined(typeof(Genre), id);
            if (!isValid)
            {
                return BadRequest("Invalid genre");
            }
            var movies = repository.GetMoviesByGenre((Genre)id);
            if (!movies.Any())
            {
                return NoContent();
            }
            return Ok(movies);
        }
        [HttpGet("movieid={id}&name={name}")]
        public IActionResult ActorsByMovie(int id)
        {
            var actors = repository.GetActorsByMovie(id);
            if (!actors.Any())
            {
                return NoContent();
            }
            return Ok(actors);
        }
    }
}
