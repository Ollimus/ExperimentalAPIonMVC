using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using TestAPI.Models;
using Newtonsoft.Json;

namespace TestAPI.Controllers.API
{
    public class CustomersController : ApiController
    {
        private readonly IUnitOfWork _context;

        public CustomersController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            var customers = _context.Customers.GetCustomers.ToList();

            if (customers == null)
                return NotFound();

            return Ok(customers);
        }

        [HttpGet]
        // GET api/<Controller>/<Id>
        public IHttpActionResult GetCustomers(int id)
        {
            var customer = _context.Customers.GetCustomerById(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        public IHttpActionResult GetCustomers(string lastname)
        {
            var customers = _context.Customers.GetCustomers.Where(c => c.LastName == lastname).ToList();

            if (customers == null)
                return NotFound();


            return Ok(customers);
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid || customer == null)
                return BadRequest();

            customer.DateCreated = DateTime.Today.Date;

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + customer.CustomerId), customer);
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid || customer == null)
                return BadRequest();

            var existingCustomer = _context.Customers.GetCustomerById(id);

            if (existingCustomer == null)
                return NotFound();

            else
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDB = _context.Customers.GetCustomerById(id);

            if (customerInDB == null)
                return NotFound();

            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();

            return Ok();
        }
    }
}