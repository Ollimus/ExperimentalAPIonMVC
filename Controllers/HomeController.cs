using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAPI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Terve";
            return View();
        }

        public ActionResult ApiGuide()
        {
            return View();
        }
    }
}