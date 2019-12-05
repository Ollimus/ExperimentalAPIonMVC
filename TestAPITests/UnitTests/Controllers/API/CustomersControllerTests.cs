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

            Customer customer = new Customer()
            {
                CustomerId = 10,
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            customerList.Add(customer);

            _mockRepository.SetupGet(r => r.GetCustomers).Returns(customerList.AsQueryable());

            var result = _controller.GetCustomers() as OkNegotiatedContentResult<List<Customer>>;
            var customers = result.Content;

            Assert.AreEqual(1, customers.Count);
        }

        [TestMethod]
        public void GetCustomers_GetCustomerById_ReturnsCustomerObject()
        {
            int customerId = 10;

            Customer customer = new Customer()
            {
                CustomerId = customerId,
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            _mockRepository.Setup(r => r.GetCustomerById(customerId)).Returns(customer);

            var result = _controller.GetCustomers(customerId) as OkNegotiatedContentResult<Customer>;
            var customerResult = result.Content;

            Assert.IsNotNull(customerResult);
            Assert.AreEqual(10, customer.CustomerId);
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
            HttpMethod test = new HttpMethod("Test");

            _controller.Request = new HttpRequestMessage(test, "/");
            _controller.Configuration = new HttpConfiguration();

            Customer customer = new Customer()
            {
                CustomerId = 10,
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            var result = _controller.CreateCustomer(customer) as CreatedNegotiatedContentResult<Customer>;

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Content.CustomerId);
        }

        [TestMethod]
        public void UpdateCustomer_UpdateExistingCustomer_ReturnOK()
        {
            Customer customer = new Customer()
            {
                CustomerId = 10,
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            _mockRepository.Setup(r => r.GetCustomerById(10)).Returns(customer);
            var result = _controller.UpdateCustomer(10, customer);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateCustomer_NoCustomerFoundWithId_ReturnNotFound()
        {
            Customer customer = new Customer()
            {
                CustomerId = 10,
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            _mockRepository.Setup(r => r.GetCustomerById(9));
            var result = _controller.UpdateCustomer(10, customer);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateCustomer_UpdateExistingCustomerWithNull_ReturnBadRequest()
        {
            Customer customer = new Customer()
            {
                CustomerId = 10,
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            Customer updateCustomer = null;

            _mockRepository.Setup(r => r.GetCustomerById(10)).Returns(customer);
            var result = _controller.UpdateCustomer(10, updateCustomer);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void DeleteCustomer_DeleteCustomerWithCorrectId_ReturnOk()
        {
            int customerId = 10;

            Customer customer = new Customer()
            {
                CustomerId = customerId,
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            _mockRepository.Setup(r => r.GetCustomerById(customerId)).Returns(customer);
            var result = _controller.DeleteCustomer(10);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteCustomer_DeleteCustomerWithIncorrectId_ReturnNotFound()
        {
            int customerId = 10;
            int incorrectId = 9;

            Customer customer = new Customer()
            {
                CustomerId = customerId,
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            _mockRepository.Setup(r => r.GetCustomerById(incorrectId)).Returns(customer);
            var result = _controller.DeleteCustomer(10);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
