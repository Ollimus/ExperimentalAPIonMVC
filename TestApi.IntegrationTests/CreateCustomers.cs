using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestApi.IntegrationTests
{
    public static class CreateCustomers
    {
        public static Customer CreateNewCustomer(bool generateSecondCustomer)
        {
            Customer customer;

            if (!generateSecondCustomer)
            {
                customer = new Customer()
                {
                    FirstName = "Kia",
                    LastName = "Hankila",
                    City = "Helsinki",
                    Address = "Helsingintie 16",
                    DateCreated = DateTime.Today
                };
            }

            else
            {
                customer = new Customer()
                {
                    FirstName = "Teppo",
                    LastName = "Tavunen",
                    City = "Tampere",
                    Address = "Tampereentie 16",
                    DateCreated = DateTime.Today
                };
            }

            return customer;
        }
    }
}
