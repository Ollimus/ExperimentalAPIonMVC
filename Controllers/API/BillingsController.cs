using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TestAPI.Models;
using System.Web.Http.Results;

namespace TestAPI.Controllers.API
{
    [RoutePrefix("api/billings")]
    public class BillingsController : ApiController
    {
        ApplicationDbContext _context;

        public BillingsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetBillings()
        {
            var billings = _context.Billings.ToList();

            return Ok(billings);
        }

        [HttpGet]
        public IHttpActionResult GetBillings(int id)
        {
            var billing = _context.Billings.Where(c => c.Id == id).FirstOrDefault();

            if (billing == null)
                return NotFound();

            return Ok(billing);
        }

        [HttpGet]
        [Route("customers/{customerId}")]
        public IHttpActionResult GetBillingsByCustomer(int customerId)
        {
            var billings = _context.Billings.Where(c => c.CustomerId == customerId).ToList();

            if (billings.Count == 0)
                return NotFound();

            return Ok(billings);
        }

        [HttpGet]
        [Route("products/{productId}")]
        public IHttpActionResult GetBillingsByProduct(int productId)
        {
            var billings = _context.Billings.Where(c => c.ProductId == productId).ToList();

            if (billings.Count == 0)
                return NotFound();

            return Ok(billings);
        }
    }
}