using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestApi.IntegrationTests
{
    public static class CreateCustomer
    {
        public static Customer CreateNewCustomer(string firstName, string lastName, string city, string address)
        {
            Customer customer = new Customer()
            {
                CustomerId = 0, //Not required, but put in as mock.
                FirstName = firstName,
                LastName = lastName,
                City = city,
                Address = address,
                DateCreated = DateTime.Today
            };

            return customer;
        }
    }
}
