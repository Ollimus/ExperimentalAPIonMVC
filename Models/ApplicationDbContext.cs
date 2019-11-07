using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Products> Products { get; set; }

        public  ApplicationDbContext()
        {

        }
    }
}
