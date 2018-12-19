using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public MyDbContext _myDbContext;

        public MoviesController()
        {
            _myDbContext = new MyDbContext();
        }

        // GET: Movies/random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek" };

            var customers = new List<Customer>()
            {
                new Customer{ Name = "Customer 1"},
                new Customer{ Name = "Customer 2"}
            };

            var viewmodel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewmodel);
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
                return HttpNotFound();


            var movie = _myDbContext.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);

            var model = new MovieFormViewModel
            {
                 Genres = _myDbContext.Genres,
                  Movie = movie
            };


            
            return View("MovieForm", model);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            var movies = _myDbContext.Movies.Include(m => m.Genre).ToList();
            var model = new MoviesViewModel
            {
                Movies = movies
            };

            return View(model);
        }

        [Route("Movies/Details/{id:regex(\\d{1})}")]
        public ActionResult Details(int id)
        {

            var movie = _myDbContext.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);

            var model = new MovieDetailsViewModel
            {
                Name = movie.Name,
                DateAdded = movie.DateAdded,
                GenreName = movie.Genre.Name,
                ReleaseDate = movie.ReleaseDate,
                InStock = movie.NumberInStock
            };

            return View(model);
        }

        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {


            return Content(year + "/" + month);
        }

        public ActionResult New()
        {
            var genres = _myDbContext.Genres.ToList();

            var model = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = genres
            };

            return View("MovieForm", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {

            if (!ModelState.IsValid)
            {
                var model = new MovieFormViewModel
                {
                     Genres = _myDbContext.Genres,
                      Movie = movie
                };
                return View("MovieForm", model);
            }

            if (movie.Id == 0)
                _myDbContext.Movies.Add(movie);
            else
            {
                var movieInDb = _myDbContext.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.GenreId = movie.GenreId;

            }

            _myDbContext.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}