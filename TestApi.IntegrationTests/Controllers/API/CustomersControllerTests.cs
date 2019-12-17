using System;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Controllers;
using TestAPI.Models;
using TestAPI.Repositories;
using System.Collections.Generic;
using TestAPI.Controllers.API;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Transactions;
using System.Web.Http.Results;
using TestApi.IntegrationTests;
using System.Web.Http;
using System.Data.Entity.Validation;

namespace TestAPITests.IntegrationTests.Controllers
{
    [TestClass]
    public class CustomersControllerTest
    {
        private CustomersController _controller;
        private ApplicationDbContext _context;

        public CustomersControllerTest()
        {
            _context = new ApplicationDbContext();
            _controller = new CustomersController(new UnitOfWork(_context));
        }

        [TestMethod]
        public void GetCustomers_GetAllCustomers_ReturnsCustomerObjects()
        {
            using (new TransactionScope())
            {
                Customer customer = CreateTestObjects.CreateNewCustomer("Kiira", "Toivonen", "Helsinki", "Helsingintie 16");
                Customer secondCustomer = CreateTestObjects.CreateNewCustomer("Teuvo", "Hakkarainen", "Tampere", "Tampereentie 2");
                Customer thirdCustomer = CreateTestObjects.CreateNewCustomer("Jarno", "Kuivanen", "Joensuu", "Tulliportinkatu 8");

                _context.Customers.Add(customer);
                _context.Customers.Add(secondCustomer);
                _context.Customers.Add(thirdCustomer);
                _context.SaveChanges();

                var result = _controller.GetCustomers() as OkNegotiatedContentResult<List<Customer>>;
                var customerResult = result.Content;
                bool doesListHaveValues = (customerResult.Count > 0) ? true : false;

                Assert.IsNotNull(customerResult);
                Assert.IsTrue(doesListHaveValues);
            }
        }

        [TestMethod]
        public void GetCustomers_GetCustomerById_ReturnsCustomerObject()
        {
            using (new TransactionScope())
            {
                Customer customer = CreateTestObjects.CreateNewCustomer("Kiira", "Toivonen", "Helsinki", "Helsingintie 16");
                Customer secondCustomer = CreateTestObjects.CreateNewCustomer("Teuvo", "Hakkarainen", "Tampere", "Tampereentie 2");
                Customer thirdCustomer = CreateTestObjects.CreateNewCustomer("Jarno", "Kuivanen", "Joensuu", "Tulliportinkatu 8");

                _context.Customers.Add(customer);
                _context.Customers.Add(secondCustomer);
                _context.Customers.Add(thirdCustomer);
                _context.SaveChanges();

                var result = _controller.GetCustomers(secondCustomer.CustomerId) as OkNegotiatedContentResult<Customer>;
                var customerResult = result.Content;

                Assert.IsNotNull(customerResult);
                Assert.AreEqual(secondCustomer.CustomerId, customerResult.CustomerId);
            }
        }

        [TestMethod]
        public void GetCustomer_GetCustomerByIncorrectId_ReturnNotFound()
        {
            var result = _controller.GetCustomers(0);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateCustomer_ValidCustomerCreated_ReturnCreated()
        {
            using (new TransactionScope())
            {
                Customer customer = CreateTestObjects.CreateNewCustomer("Kiira", "Toivonen", "Helsinki", "Helsingintie 16");

                _context.Customers.Add(customer);
                _context.SaveChanges();

                HttpMethod test = new HttpMethod("Test");
                _controller.Request = new HttpRequestMessage(test, "/");
                _controller.Configuration = new HttpConfiguration();

                var result = _controller.CreateCustomer(customer) as CreatedNegotiatedContentResult<Customer>;
                var customerResult = result.Content;

                Assert.IsNotNull(customerResult);
                Assert.AreEqual(customer.CustomerId, customerResult.CustomerId);
            }
        }

        [TestMethod]
        public void UpdateCustomer_UpdateExistingCustomer_ReturnOK()
        {
            using (new TransactionScope())
            {
                Customer customerInDB = CreateTestObjects.CreateNewCustomer("Kiira", "Toivonen", "Helsinki", "Helsingintie 16");
                Customer updatingCustomer = CreateTestObjects.CreateNewCustomer("Teuvo", "Hakkarainen", "Tampere", "Tampereentie 2");

                _context.Customers.Add(customerInDB);
                _context.SaveChanges();

                var result = _controller.UpdateCustomer(customerInDB.CustomerId, updatingCustomer);

                Assert.IsInstanceOfType(result, typeof(OkResult));
            }
        }

        [TestMethod]
        public void UpdateCustomer_NoCustomerFoundWithId_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Customer updatingCustomer = CreateTestObjects.CreateNewCustomer("Teuvo", "Hakkarainen", "Tampere", "Tampereentie 2");

                var result = _controller.UpdateCustomer(7, updatingCustomer);

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }

        }

        [TestMethod]
        public void UpdateCustomer_UpdateExistingCustomerWithNull_ReturnBadRequest()
        {
            using (new TransactionScope())
            {
                Customer customerInDB = CreateTestObjects.CreateNewCustomer("Kiira", "Toivonen", "Helsinki", "Helsingintie 16");
                Customer updatingCustomer = null;

                _context.Customers.Add(customerInDB);
                _context.SaveChanges();

                var result = _controller.UpdateCustomer(customerInDB.CustomerId, updatingCustomer);

                Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            }
        }

        [TestMethod]
        public void DeleteCustomer_DeleteCustomerWithCorrectId_ReturnOk()
        {
            using (new TransactionScope())
            {
                Customer customer = CreateTestObjects.CreateNewCustomer("Kiira", "Toivonen", "Helsinki", "Helsingintie 16");

                _context.Customers.Add(customer);
                _context.SaveChanges();

                var checkIfExistsInDB = _controller.GetCustomers(customer.CustomerId) as OkNegotiatedContentResult<Customer>;
                var result = _controller.DeleteCustomer(customer.CustomerId);
                var checkIfDeleted = _controller.GetCustomers(customer.CustomerId) as OkNegotiatedContentResult<Customer>;

                Assert.IsInstanceOfType(result, typeof(OkResult));
            }
        }

        [TestMethod]
        public void DeleteCustomer_DeleteCustomerWithIncorrectId_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Customer customerInDB = CreateTestObjects.CreateNewCustomer("Kiira", "Toivonen", "Helsinki", "Helsingintie 16");

                _context.Customers.Add(customerInDB);
                _context.SaveChanges();

                var result = _controller.DeleteCustomer(0);

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }
    }
}
