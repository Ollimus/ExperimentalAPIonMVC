using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestAPI.Models;

namespace TestAPI.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _context;
        public CustomerRepository Customers { get; set; } //private set

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}