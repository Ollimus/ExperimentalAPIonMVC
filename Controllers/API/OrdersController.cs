using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TestAPI.Models;
using System.Web.Http.Results;

namespace TestAPI.Controllers.API
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private readonly IUnitOfWork _context;

        public OrdersController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            var order = _context.Orders.GetOrders.ToList();

            return Ok(order);
        }

        [HttpGet]
        public IHttpActionResult GetOrders(int id)
        {
            var order = _context.Orders.GetOrderById(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet]
        [Route("customers/{customerId}")]
        public IHttpActionResult GetOrdersByCustomerid(int customerId)
        {
            var orders = _context.Orders.GetOrdersByCustomerid(customerId).ToList();

            if (orders.Count == 0)
                return NotFound();


            return Ok(orders);
        }

        [HttpGet]
        [Route("products/{productId}")]
        public IHttpActionResult GetOrdersByProductId(int productId)
        {
            var orders = _context.Orders.GetOrdersByProductId(productId).ToList();

            if (orders.Count == 0)
                return NotFound();

            return Ok(orders);
        }

        [HttpPost]
        public IHttpActionResult CreateOrder(Order order)
        {
            if (!ModelState.IsValid || order == null)
                return BadRequest();

            var customer = _context.Customers.GetCustomerById(order.CustomerId);
            var product = _context.Products.GetProductById(order.ProductId);

            if (customer == null || product == null)
                return NotFound();

            Order createNewOrder = new Order()
            {
                CustomerId = order.CustomerId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                TotalPrice = _context.Orders.CalculateTotalOrderValue(product, order.Quantity),
                TimeAdded = DateTime.Now
            };

            _context.Orders.Add(createNewOrder);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + createNewOrder.Id), createNewOrder);
        }

        [HttpPut]
        public IHttpActionResult UpdateOrder(int Id, Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var orderFromDB = _context.Orders.GetOrderById(Id);
            var customer = _context.Customers.GetCustomerById(order.CustomerId);
            var product = _context.Products.GetProductById(order.ProductId);

            if (orderFromDB == null || customer == null || product == null)
                return NotFound();

            else
            {
                orderFromDB.ProductId = order.ProductId;
                orderFromDB.CustomerId = order.CustomerId;
                orderFromDB.Quantity = order.Quantity;
                orderFromDB.TotalPrice = _context.Orders.CalculateTotalOrderValue(product, order.Quantity);
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteOrder(int orderId)
        {
            var order = _context.Orders.GetOrderById(orderId);

            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return Ok();
        }
    }
}