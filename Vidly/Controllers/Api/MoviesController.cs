using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Entity;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {

        private MyDbContext _myDbContext;


        public MoviesController()
        {
            _myDbContext = new MyDbContext();
        }


        //GET /api/movies

        public IHttpActionResult GetMovies()
        {
            var moviesDto = _myDbContext.Movies
                .Include(m => m.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(moviesDto);
        }


        //Get /api/movies/1

        public IHttpActionResult GetMovie(int id)
        {
            var movieInDb = _myDbContext.Movies.FirstOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return BadRequest();

            return Ok(Mapper.Map<Movie, MovieDto>(movieInDb));
        }

        //POST /api/movies

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();


            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _myDbContext.Movies.Add(movie);
            _myDbContext.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }


        //PUT /api/movies/1

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _myDbContext.Movies.FirstOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return BadRequest();

            var updatedMovie = Mapper.Map(movieDto, movieInDb);

            _myDbContext.SaveChanges();



            return Created(new Uri(Request.RequestUri + "/" + id), updatedMovie);
        }

        //DELETE /api/movies/1

        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _myDbContext.Movies.FirstOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            _myDbContext.Movies.Remove(movieInDb);
            _myDbContext.SaveChanges();

            return Ok("Movie Erased");
        }
    }
}
