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

namespace TestAPITests.IntegrationTests.Controllers
{
    [TestClass]
    public class CustomersControllerTest
    {
        private CustomersController _controller;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new ApplicationDbContext();
            _controller = new CustomersController(new UnitOfWork(_context));
        }


        [TestMethod]
        public void GetCustomers_GetAllCustomers_ReturnsCustomerObjects()
        {
            using (new TransactionScope())
            {
                Customer customer = CreateCustomers.CreateNewCustomer(false);
                Customer customer2nd = CreateCustomers.CreateNewCustomer(true);

                _context.Customers.Add(customer);
                _context.Customers.Add(customer2nd);
                _context.SaveChanges();

                var result = _controller.GetCustomers() as OkNegotiatedContentResult<List<Customer>>;
                var customerResult = result.Content;

                Assert.IsNotNull(customerResult);
                Assert.AreEqual(2, customerResult.Count);
            }
        }

        [TestMethod]
        public void GetCustomers_GetCustomerById_ReturnsCustomerObject()
        {
            using (new TransactionScope())
            {
                Customer customer = CreateCustomers.CreateNewCustomer(false);
                Customer customer2nd = CreateCustomers.CreateNewCustomer(true);
                Customer customer3rd = CreateCustomers.CreateNewCustomer(true);

                _context.Customers.Add(customer);
                _context.Customers.Add(customer2nd);
                _context.Customers.Add(customer3rd);
                _context.SaveChanges();

                var firstCustomer = _context.Customers.First();
                var result = _controller.GetCustomers(firstCustomer.CustomerId) as OkNegotiatedContentResult<Customer>;
                var customerResult = result.Content;

                Assert.IsNotNull(customerResult);
                Assert.AreEqual(firstCustomer.CustomerId, customerResult.CustomerId);
            }
        }

        [TestMethod]
        public void GetCustomer_GetCustomerByIncorrectId_ReturnNotFound()
        {
            var result = _controller.GetCustomers(0);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        //[TestMethod]
        //public void CreateCustomer_ValidCustomerCreated_ReturnCreated()
        //{
        //    Customer customer = new Customer() { CustomerId = 1 };

        //    HttpMethod test = new HttpMethod("Test");
        //    _controller.Request = new HttpRequestMessage(test, "/");
        //    _controller.Configuration = new HttpConfiguration();

        //    var result = _controller.CreateCustomer(customer) as CreatedNegotiatedContentResult<Customer>;

        //    Assert.AreEqual(customer.CustomerId, result.Content.CustomerId);
        //}

        //[TestMethod]
        //public void UpdateCustomer_UpdateExistingCustomer_ReturnOK()
        //{
        //    Customer existingCustomer = new Customer() { CustomerId = 10 };
        //    Customer customer = new Customer();

        //    _mockRepository.Setup(r => r.GetCustomerById(existingCustomer.CustomerId)).Returns(existingCustomer);
        //    var result = _controller.UpdateCustomer(existingCustomer.CustomerId, customer);

        //    Assert.IsInstanceOfType(result, typeof(OkResult));
        //}

        //[TestMethod]
        //public void UpdateCustomer_NoCustomerFoundWithId_ReturnNotFound()
        //{
        //    int incorrectId = 5;
        //    Customer customer = new Customer();

        //    _mockRepository.Setup(r => r.GetCustomerById(customer.CustomerId));
        //    var result = _controller.UpdateCustomer(incorrectId, customer);

        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}

        //[TestMethod]
        //public void UpdateCustomer_UpdateExistingCustomerWithNull_ReturnBadRequest()
        //{
        //    Customer existingCustomer = new Customer() { CustomerId = 10 };
        //    Customer updatingCustomer = null;

        //    _mockRepository.Setup(r => r.GetCustomerById(existingCustomer.CustomerId)).Returns(existingCustomer);
        //    var result = _controller.UpdateCustomer(existingCustomer.CustomerId, updatingCustomer);

        //    Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        //}

        //[TestMethod]
        //public void DeleteCustomer_DeleteCustomerWithCorrectId_ReturnOk()
        //{
        //    Customer customer = new Customer() { CustomerId = 2 };

        //    _mockRepository.Setup(r => r.GetCustomerById(customer.CustomerId)).Returns(customer);
        //    var result = _controller.DeleteCustomer(customer.CustomerId);

        //    Assert.IsInstanceOfType(result, typeof(OkResult));
        //}

        //[TestMethod]
        //public void DeleteCustomer_DeleteCustomerWithIncorrectId_ReturnNotFound()
        //{
        //    int incorrectId = 9;
        //    Customer customer = new Customer();

        //    _mockRepository.Setup(r => r.GetCustomerById(customer.CustomerId)).Returns(customer);
        //    var result = _controller.DeleteCustomer(incorrectId);

        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}
    }
}
