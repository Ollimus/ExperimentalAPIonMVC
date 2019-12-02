using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPI.Models
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetCustomers { get; }

        void Add(Customer customer);
        void Remove(Customer customer);
        Customer GetCustomerById(int id);
    }
}