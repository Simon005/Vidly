using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private MyDbContext _context;

        public CustomersController()
        {
            _context = new MyDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Customers()
        {

            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            var model = new CustomersViewModel { Customers = customers };
            return View(model);
        }

        [Route("Customers/Details/{id:regex(\\d{1}):range(1,12)}")]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.FirstOrDefault( c => c.Id == id);
            if (customer == null)
                return HttpNotFound();


            var model = new CustomerDetailsViewModel { Customer = customer };
            return View(model);
        }

        private List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer{ Id = 1, Name = "kund1"},
                new Customer{ Id = 2, Name = "kund2"}
            };
        }
    }
}