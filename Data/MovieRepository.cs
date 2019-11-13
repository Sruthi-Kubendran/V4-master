using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.DTO;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Data
{
    public class MovieRepository : IRepository
    {
        MovieContext context;
        public MovieRepository(MovieContext context)
        {
            this.context = context;
        }

        public bool AddActor(Actor actor)
        {
            try
            {
                context.Actors.Add(actor);
                int result = context.SaveChanges();
                if(result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddMovie(Movie movie)
        {
            try
            {
                context.Movies.Add(movie);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddMovie(MovieDTO movie)
        {
            
                try
                {
                    context.Movies.Add(movie.Movie);
                    foreach (var actorId in movie.Actors)
                    {
                        context.MovieActors.Add(new MovieActor
                        {
                            Movie = movie.Movie,
                            Actor = context.Actors.Find(actorId)
                        });
                        context.SaveChanges();
                    }
                    context.SaveChanges();

                    return true;

                }
                catch (Exception)
                {

                    throw;
                }
            
        }

        public bool DeleteActor(Actor actor)
        {
            try
            {
                context.Actors.Remove(actor);
                int result = context.SaveChanges();
                if(result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteMovie(Movie movie)
        {
            try
            {
                context.Movies.Remove(movie);
                var movieActors = context.MovieActors.Where(ma => ma.Movie == movie);
                context.MovieActors.RemoveRange(movieActors);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Actor GetActor(int id)
        {
            return context.Actors.Find(id);       
        }

        public IEnumerable<Actor> GetActors()
        {
            return context.Actors.ToList();
        }

        public IEnumerable<Actor> GetActorsByMovie(int movieId)
        {
            var actor = from a in context.Actors
                        join ma in context.MovieActors on a.Id equals ma.ActorId
                        where ma.MovieId == movieId
                        select a;
            return actor;
        }

        public Movie GetMovie(int id)
        {
            return context.Movies.Find(id);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return context.Movies.Include(m => m.MovieActors);
        }

        public IEnumerable<Movie> GetMoviesByActor(int actorid)
        {
            var movies = from m in context.Movies
                         join ma in context.MovieActors on m.Id equals ma.MovieId
                         where ma.ActorId == actorid 
                         select m;
            return movies;
        }

        public IEnumerable<Movie> GetMoviesByGenre(Genre genre)
        {

            return context.Movies.Where(m => m.Genre == (Genre)genre);
        }

        public bool UpdateActor(Actor actor)
        {
            try
            {
                context.Actors.Update(actor);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateMovie(MovieDTO movie)
        {
            try
            {
                context.Movies.Update(movie.Movie);
                var movieActors = context.MovieActors.Where(ma => ma.Movie == movie.Movie);
                context.MovieActors.RemoveRange(movieActors);
                //remove all relations
                
                foreach (var actorId in movie.Actors)
                {
                    context.MovieActors.Add(new MovieActor
                    {
                        Movie = movie.Movie,
                        Actor = context.Actors.Find(actorId)
                    });
                    context.SaveChanges();
                }
                context.SaveChanges();

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
