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
        private IUnitOfWork _context;

        public CustomerController(IUnitOfWork context)
        {
            _context = context;
        }

        public ActionResult Customers()
        {
            ViewBag.Message = "Display Customers";

            return View();
        }

        public ActionResult CustomerManagement(int? id)
        {
            if (id == null)
                id = 0;

            Customer customer = _context.Customers.GetCustomerById((int)id);

            if (customer == null)
            {
                customer = new Customer();
            }

            return View("NewCustomerForm", customer);
        }
    }
}