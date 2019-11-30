using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace TestAPI.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = new ApplicationDbContext();
        }

        public IQueryable<Customer> Customers { get { return _context.Customers; } }

        public void Add (Customer customer) { _context.Customers.Add(customer); }
    }
}