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
        public IBillingRepository Billings { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(context);
            Products = new ProductRepository(context);
            Billings = new BillingRepository(context);
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}