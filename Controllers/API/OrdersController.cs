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
        public IHttpActionResult GetBillings()
        {
            var billings = _context.Orders.GetOrders.ToList();

            return Ok(billings);
        }

        [HttpGet]
        public IHttpActionResult GetBillings(int id)
        {
            var billing = _context.Orders.GetOrderById(id);

            if (billing == null)
                return NotFound();

            return Ok(billing);
        }

        [HttpGet]
        [Route("customers/{customerId}")]
        public IHttpActionResult GetBillingsByCustomer(int customerId)
        {
            var billings = _context.Orders.GetOrdersByCustomerid(customerId).ToList();

            if (billings.Count == 0)
                return NotFound();

            return Ok(billings);
        }

        [HttpGet]
        [Route("products/{productId}")]
        public IHttpActionResult GetBillingsByProduct(int productId)
        {
            var billings = _context.Orders.GetOrdersByCustomerid(productId).ToList();

            if (billings.Count == 0)
                return NotFound();

            return Ok(billings);
        }
    }
}