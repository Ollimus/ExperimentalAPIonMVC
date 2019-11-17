using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Customers()
        {
            ViewBag.Message = "Display Customers";

            return View();
        }

        public ActionResult CustomerManagement(int? id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                customer = new Customer();
            }

            return View("NewCustomerForm", customer);
        }
    }
}