using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestApi.IntegrationTests
{
    public static class CreateTestObjects
    {
        public static Customer CreateNewCustomer(string firstName, string lastName, string city, string address)
        {
            Customer customer = new Customer()
            {
                CustomerId = 0,
                FirstName = firstName,
                LastName = lastName,
                City = city,
                Address = address,
                DateCreated = DateTime.Today
            };

            return customer;
        }

        public static Product CreateNewProduct(string name, string description, string producer, decimal price, int stock)
        {
            Product product = new Product()
            {
                ProductId = 0,
                Name = name,
                Description = description,
                Producer = producer,
                Price = price,
                Stock = stock
            };

            return product;
        }

        public static Order CreateNewOrder(int customerId, int productId, int quantity)
        {
            Order order = new Order()
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity,
                TotalPrice = 0,
                TimeAdded = DateTime.Now.Date
            };

            return order;
        }
    }
}
