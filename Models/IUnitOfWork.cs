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
        ICustomerRepository Customers { get; set; }
        IOrdersRepository Orders { get; set; }
        IProductRepository Products { get; set; }

        void SaveChanges();
    }
}