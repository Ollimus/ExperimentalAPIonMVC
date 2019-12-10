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
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IUnitOfWork _context;

        public ProductsController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            var products = _context.Products.GetProducts.ToList();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet]
        // GET api/<Controller>/<Id>
        public IHttpActionResult GetProducts(int id)
        {
            var product = _context.Products.GetProductById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Route("producer/{producer}")]
        [HttpGet]
        public IHttpActionResult SearchByProducer(string producer)
        {
            var products = _context.Products.GetProductByProducer(producer);

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

            var existingProduct = _context.Products.GetProductById(id);

            if (existingProduct == null)
                return NotFound();

            else
            {
                existingProduct.Name = product.Name;
                existingProduct.Producer = product.Producer;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDB = _context.Products.GetProductById(id);

            if (productInDB == null)
                return NotFound();

            _context.Products.Remove(productInDB);
            _context.SaveChanges();

            return Ok();

        }
    }
}