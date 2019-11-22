using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPI.Models
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> Customers { get; }
    }
}