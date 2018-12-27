using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {

        private ApplicationDbContext _context;
        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: /api/Rentals
        public IHttpActionResult GetRentals()
        {
            var rentals = _context.Rentals.Include(r => r.Customer).Include(r => r.Movie).AsEnumerable();

            return Ok(rentals);
        }

        //GET /api/rentals/1
        public IHttpActionResult GetRental(int id)
        {
            return Ok();
        }

        //POST /api/rentals
        [HttpPost]
        public IHttpActionResult CreateRental(NewRentalDto newRental)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _context.Customers.FirstOrDefault(c => c.Id == newRental.CustomerId);

            if (customer == null)
                return BadRequest("CustomerId is not valid.");

            if (newRental.MovieIds.Count == 0)
            {
                return BadRequest("No Movie Ids have been given.");
            }

            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id));

            foreach (var movie in movies)
            {
                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/"), newRental);
        }

        //PUT /api/rentals/1

        //DELETE /api/rentals/1
    }
}
