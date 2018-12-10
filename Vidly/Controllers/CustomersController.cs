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
        public ActionResult Index()
        {

            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            var model = new CustomersViewModel { Customers = customers };
            return View(model);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var model = new CustomerFormViewModel
            {
                 MembershipTypes = membershipTypes
            };
     
            return View("CustomerForm", model);
        }

        [HttpPost]
        public ActionResult Save(CustomerFormViewModel model)
        {

            if (model.Customer.Id == 0)
                _context.Customers.Add(model.Customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == model.Customer.Id);
                customerInDb.Name = model.Customer.Name;
                customerInDb.BirthDate = model.Customer.BirthDate;
                customerInDb.MembershipTypeId = model.Customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = model.Customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var model = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", model);
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