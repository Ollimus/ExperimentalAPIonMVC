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
    public class ProductsController : ApiController
    {
        private ApplicationDbContext _context;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            var products = _context.Products.ToList();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet]
        // GET api/<Controller>/<Id>
        public IHttpActionResult GetProducts(int id)
        {
            var product = _context.Products.Where(c => c.ProductId == id).SingleOrDefault();

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        public IHttpActionResult SearchByProducer(string producer)
        {
            var products = _context.Products.ToList().Where(c => c.Producer == producer);

            if (products == null)
                return NotFound();


            return Ok(products);
        }

        [HttpPost]
        public IHttpActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid || product == null)
                return BadRequest();

            _context.Products.Add(product);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + product.ProductId), product);
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid || product == null)
                return BadRequest();

            var existingProduct = _context.Products.Where(c => c.ProductId == id).SingleOrDefault();

            if (existingProduct == null)
                return NotFound();

            else
            {
                //
                //
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDB = _context.Products.SingleOrDefault(c => c.ProductId == id);

            if (productInDB == null)
                return NotFound();

            _context.Products.Remove(productInDB);
            _context.SaveChanges();

            return Ok();

        }
    }
}