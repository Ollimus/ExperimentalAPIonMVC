using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProducts { get; }

        void Add(Product product);
        void Remove(Product product);
        Product GetProductById(int id);
        Product GetProductByProducer(string producer);
    }
}