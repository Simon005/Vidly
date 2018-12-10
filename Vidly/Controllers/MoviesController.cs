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
        public MyDbContext myDbContext;

        public MoviesController()
        {
            myDbContext = new MyDbContext();
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
            return Content("id " + id);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            var movies = myDbContext.Movies.Include(m => m.Genre).ToList();
            var model = new MoviesViewModel
            {
                 Movies = movies
            };

            return View(model);
        }

        [Route("Movies/Details/{id:regex(\\d{1}):range(1,12)}")]
        public ActionResult Details(int id)
        {

            var movie = myDbContext.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);

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
    }
}