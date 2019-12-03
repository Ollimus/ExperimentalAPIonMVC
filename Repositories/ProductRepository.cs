using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
            ApplicationDbContext _context;

            public ProductRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public IQueryable<Product> GetProducts { get { return _context.Products; } }

            public void Remove(Product product) { _context.Products.Remove(product); }

            public void Add(Product product) { _context.Products.Add(product); }

            public Product GetProductById(int id) { return _context.Products.Where(p => p.ProductId == id).SingleOrDefault(); }
    }
}