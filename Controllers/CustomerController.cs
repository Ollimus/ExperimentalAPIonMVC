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
        private ICustomerRepository _customerRepository;

        public CustomerController(IUnitOfWork context, ICustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }

        public ActionResult Customers()
        {
            ViewBag.Message = "Display Customers";

            return View();
        }

        public ActionResult CustomerManagement(int? id)
        {
            Customer customer = _customerRepository.Customers.SingleOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                customer = new Customer();
            }

            return View("NewCustomerForm", customer);
        }
    }
}