using System;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Controllers;
using TestAPI.Models;
using TestAPI.Repositories;
using Moq;
using System.Collections.Generic;
using TestAPI.Controllers.API;
using System.Linq;
using System.Web.Http.Results;
using System.Net;
using System.Web;
using System.Web.Routing;
using System.Net.Http;
using System.Web.Http;

namespace TestAPITests.UnitTests.Controllers
{
    [TestClass]
    public class CustomersControllerTest
    {
        private CustomersController _controller;
        Mock<ICustomerRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            var mockContext = new Mock<IUnitOfWork>();

            mockContext.SetupGet(u => u.Customers).Returns(_mockRepository.Object);

            _controller = new CustomersController(mockContext.Object);
        }


        [TestMethod]
        public void GetCustomers_GetAllCustomers_ReturnsCustomerObjects()
        {
            List<Customer> customerList = new List<Customer>();
            Customer customer = new Customer();

            customerList.Add(customer);
            customerList.Add(customer);
            customerList.Add(customer);

            _mockRepository.SetupGet(r => r.GetCustomers).Returns(customerList.AsQueryable());

            var result = _controller.GetCustomers() as OkNegotiatedContentResult<List<Customer>>;
            var customerResult = result.Content;

            Assert.AreEqual(customerList.Count, customerResult.Count);
        }

        [TestMethod]
        public void GetCustomers_GetCustomerById_ReturnsCustomerObject()
        {
            Customer customer = new Customer() { CustomerId = 10 };

            _mockRepository.Setup(r => r.GetCustomerById(customer.CustomerId)).Returns(customer);
            var result = _controller.GetCustomers(customer.CustomerId) as OkNegotiatedContentResult<Customer>;
            var customerResult = result.Content;

            Assert.IsNotNull(customerResult);
            Assert.AreEqual(customer.CustomerId, customerResult.CustomerId);
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
            Customer customer = new Customer() { CustomerId = 1 };

            HttpMethod test = new HttpMethod("Test");
            _controller.Request = new HttpRequestMessage(test, "/");
            _controller.Configuration = new HttpConfiguration();

            var result = _controller.CreateCustomer(customer) as CreatedNegotiatedContentResult<Customer>;

            Assert.AreEqual(customer.CustomerId, result.Content.CustomerId);
        }

        [TestMethod]
        public void UpdateCustomer_UpdateExistingCustomer_ReturnOK()
        {
            Customer existingCustomer = new Customer() { CustomerId = 10 };
            Customer customer = new Customer();

            _mockRepository.Setup(r => r.GetCustomerById(existingCustomer.CustomerId)).Returns(existingCustomer);
            var result = _controller.UpdateCustomer(existingCustomer.CustomerId, customer);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateCustomer_NoCustomerFoundWithId_ReturnNotFound()
        {
            int incorrectId = 5;
            Customer customer = new Customer();

            _mockRepository.Setup(r => r.GetCustomerById(customer.CustomerId));
            var result = _controller.UpdateCustomer(incorrectId, customer);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateCustomer_UpdateExistingCustomerWithNull_ReturnBadRequest()
        {
            Customer existingCustomer = new Customer() { CustomerId = 10 };
            Customer updatingCustomer = null;

            _mockRepository.Setup(r => r.GetCustomerById(existingCustomer.CustomerId)).Returns(existingCustomer);
            var result = _controller.UpdateCustomer(existingCustomer.CustomerId, updatingCustomer);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void DeleteCustomer_DeleteCustomerWithCorrectId_ReturnOk()
        {
            Customer customer = new Customer() { CustomerId = 2 };

            _mockRepository.Setup(r => r.GetCustomerById(customer.CustomerId)).Returns(customer);
            var result = _controller.DeleteCustomer(customer.CustomerId);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteCustomer_DeleteCustomerWithIncorrectId_ReturnNotFound()
        {
            int incorrectId = 9;
            Customer customer = new Customer();

            _mockRepository.Setup(r => r.GetCustomerById(customer.CustomerId)).Returns(customer);
            var result = _controller.DeleteCustomer(incorrectId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
