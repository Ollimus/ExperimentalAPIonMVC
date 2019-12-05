using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestAPI.Models;
using TestAPI.Repositories;

namespace TestAPI.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _context;
        public ICustomerRepository Customers { get; set; } //private set
        public IProductRepository Products { get; set; }
        public IOrdersRepository Orders { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(context);
            Products = new ProductRepository(context);
            Orders = new OrdersRepository(context);
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}