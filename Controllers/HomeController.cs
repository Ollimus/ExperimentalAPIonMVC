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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customer()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult NewCustomer()
        {
            Customer customer = new Customer();

            return View("NewCustomerForm", customer);
        }
    }
}