using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalController : ApiController
    {

        private ApplicationDbContext _context;
        public RentalController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: /api/Rentals
        public IHttpActionResult GetRentals()
        {
            return Ok();
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

            var customer = _context.Customers.FirstOrDefault(c => c.Id == newRental.customerId);

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

            return Ok();
        }

        //PUT /api/rentals/1

        //DELETE /api/rentals/1
    }
}