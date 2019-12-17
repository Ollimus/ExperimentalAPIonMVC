using System;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Controllers;
using TestAPI.Models;
using TestAPI.Repositories;
using System.Collections.Generic;
using System.Transactions;

namespace TestAPITests.IntegrationTests.Controllers
{
    [TestClass]
    public class CustomerControllerTest
    {
        private CustomerController _controller;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new ApplicationDbContext();
            _controller = new CustomerController(new UnitOfWork(_context));
        }

        [TestMethod]
        public void CustomerManagement_PassNullableIntParameterAsNull_ReturnsNewCustomerObject()
        {
            var result = _controller.CustomerManagement(null) as ViewResult;
            var customer = result.Model as Customer;

            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Customer));
            Assert.AreEqual(0, customer.CustomerId); 
        }

        [TestMethod]
        public void CustomerManagement_PassZeroAsCustomerId_ReturnsNewCustomerObject()
        {
            var result = _controller.CustomerManagement(0) as ViewResult;
            var customer = result.Model as Customer;

            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Customer));
            Assert.AreEqual(0, customer.CustomerId);
        }

        [TestMethod]
        public void CustomerManagement_PassCorrectCustomerId_ReturnsExistingCustomerObject()
        {
            using (new TransactionScope())
            {
                Customer customer = new Customer()
                {
                    FirstName = "Kia",
                    LastName = "Hankila",
                    City = "Helsinki",
                    Address = "Helsingintie 16",
                    DateCreated = DateTime.Today
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();

                var result = _controller.CustomerManagement(customer.CustomerId) as ViewResult;
                var resultModel = result.Model as Customer;

                Assert.IsNotNull(result.Model);
                Assert.IsInstanceOfType(result.Model, typeof(Customer));
                Assert.AreEqual(customer.CustomerId, resultModel.CustomerId);
            }
        }
    }
}
