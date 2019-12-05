using System;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Controllers;
using TestAPI.Models;
using TestAPI.Repositories;
using Moq;
using System.Collections.Generic;

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
        public void NullIdParameter_CustomerManagementNullableIntParameterAsNull_ReturnsNewCustomerObject()
        {
            var result = _controller.CustomerManagement(null) as ViewResult;
            var customer = result.Model as Customer;

            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Customer));
            Assert.AreEqual(0, customer.CustomerId); 
        }

        [TestMethod]
        public void IdParameterAsZero_CustomerManagementNullableIntParameter_ReturnsNewCustomerObject()
        {
            var result = _controller.CustomerManagement(0) as ViewResult;
            var customer = result.Model as Customer;

            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Customer));
            Assert.AreEqual(0, customer.CustomerId);
        }

        [TestMethod]
        public void ExistingCustomerId_CustomerManagementPassCorrectId_ReturnsExistingCustomerObject()
        {
            List<Customer> data = new List<Customer>();
            int customerIdAsMock = 30;

            Customer customer = new Customer()
            {
                CustomerId = customerIdAsMock,
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            _mockRepository.Setup(c => c.GetCustomerById(customerIdAsMock)).Returns(customer);

            var result = _controller.CustomerManagement(customerIdAsMock) as ViewResult;
            var resultModel = result.Model as Customer;

            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Customer));
            Assert.AreEqual(customerIdAsMock, resultModel.CustomerId);
        }
    }
}
