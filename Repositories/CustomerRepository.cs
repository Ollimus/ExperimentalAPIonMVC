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
            _context = context;
        }

        public IQueryable<Customer> GetCustomers { get { return _context.Customers; } }

        public void Remove (Customer customer) { _context.Customers.Remove(customer); }

        public void Add (Customer customer) { _context.Customers.Add(customer); }

        public Customer GetCustomerById (int id) { return _context.Customers.Where(c => c.CustomerId == id).SingleOrDefault(); }
    }
}