using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace TestAPI.Models
{
    public class EFCustomerRepository : ICustomerRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IQueryable<Customer> Customers { get { return context.Customers; } }
    }
}