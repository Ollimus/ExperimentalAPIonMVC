using System;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Controllers;
using TestAPI.Models;
using TestAPI.Repositories;
using Moq;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace TestAPITests.UnitTests.Controllers
{
    [TestClass]
    public class CustomerControllerTest
    {
        private CustomerController _controller;
        Mock<ICustomerRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            var mockContext = new Mock<IUnitOfWork>();
            mockContext.SetupGet(u => u.Customers).Returns(_mockRepository.Object);

            _controller = new CustomerController(mockContext.Object);
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
            Customer customer = new Customer() { CustomerId = 2 };

            _mockRepository.Setup(c => c.GetCustomerById(customer.CustomerId)).Returns(customer);

            var result = _controller.CustomerManagement(customer.CustomerId) as ViewResult;
            var resultModel = result.Model as Customer;

            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Customer));
            Assert.AreEqual(customer.CustomerId, resultModel.CustomerId);
        }
    }
}
