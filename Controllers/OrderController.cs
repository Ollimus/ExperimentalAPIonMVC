using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAPI.Models;
using TestAPI.ViewModel;

namespace TestAPI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _context;

        public OrderController(IUnitOfWork context)
        {
            _context = context;
        }

        public ActionResult Orders()
        {
            ViewBag.Message = "Display Orders";

            return View();
        }

        public ActionResult OrderManagement(int? id)
        {
            if (id == null)
                id = 0;

            Order order = _context.Orders.GetOrderById((int)id);
            OrderViewModel orderViewModel;

            if (order != null)
            {
                orderViewModel = new OrderViewModel()
                {
                    Id = order.Id,
                    ProductId = order.ProductId,
                    CustomerId = order.CustomerId,
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice,
                    Customers = _context.Customers.GetCustomers,
                    Products = _context.Products.GetProducts
                };
            }

            else
            {
                orderViewModel = new OrderViewModel()
                {
                    Customers = _context.Customers.GetCustomers,
                    Products = _context.Products.GetProducts
                };
            }


            return View("NewOrderForm", orderViewModel);
        }
    }
}