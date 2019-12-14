using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;

        public ProductController(IUnitOfWork context)
        {
            _context = context;
        }

        public ActionResult Customers()
        {
            ViewBag.Message = "Display Products";

            return View();
        }

        public ActionResult ProductManagement(int? id)
        {
            if (id == null)
                id = 0;

            Product product = _context.Products.GetProductById((int)id);

            if (product == null)
            {
                product = new Product();
            }

            return View("NewProductForm", product);
        }
    }
}