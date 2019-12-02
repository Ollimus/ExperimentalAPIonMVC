using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestAPI.Repositories;

namespace TestAPI.Models
{
    public interface IUnitOfWork
    {
        CustomerRepository Customers { get; set; }
        BillingRepository Billings { get; set; }
        ProductRepository Products { get; set; }

        void SaveChanges();
    }
}