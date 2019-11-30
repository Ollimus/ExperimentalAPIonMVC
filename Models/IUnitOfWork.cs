using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestAPI.Models
{
    public interface IUnitOfWork
    {
        CustomerRepository Customers { get; set; }

        void SaveChanges();
    }
}