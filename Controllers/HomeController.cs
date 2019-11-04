using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customer()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ApiGuide()
        {
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