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
            if (order.Quantity <= 0)
                return BadRequest();

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
        public IHttpActionResult UpdateOrder(int existingOrder, int customerId, int productId, int orderAmount)
        {
            var orderFromDB = _context.Orders.GetOrderById(existingOrder);
            var customer = _context.Customers.GetCustomerById(customerId);
            var product = _context.Products.GetProductById(productId);

            if (orderFromDB == null || customer == null || product == null)
                return NotFound();

            if (orderAmount <= 0)
                return BadRequest();

            else
            {
                orderFromDB.ProductId = product.ProductId;
                orderFromDB.CustomerId = customer.CustomerId;
                orderFromDB.Quantity = orderAmount;
                orderFromDB.TotalPrice = _context.Orders.CalculateTotalOrderValue(product, orderAmount);
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