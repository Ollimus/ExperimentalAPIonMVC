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

namespace TestAPITests.UnitTests.Controllers
{
    [TestClass]
    public class CustomersControllerTest
    {
        private CustomersController _controller;
        Mock<ICustomerRepository> _mockRepository;


        public CustomersControllerTest()
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

        //[TestMethod]
        //public void GetCustomers_GetAllCustomers_ReturnsCustomerObjects()
        //{
        //    List<Customer> customerList = new List<Customer>();

        //    Customer customer = new Customer()
        //    {
        //        CustomerId = 10,
        //        FirstName = "Kia",
        //        LastName = "Hankola",
        //        City = "Helsinki",
        //        Address = "Helsingintie 16",
        //        DateCreated = DateTime.Today
        //    };

        //    customerList.Add(customer);

        //    _mockRepository.SetupGet(r => r.GetCustomers).Returns(customerList.AsQueryable());

        //    var result = _controller.GetCustomers() as OkNegotiatedContentResult<List<Customer>>;
        //    var customers = result.Content;

        //    Assert.AreEqual(1, customers.Count);
        //}
    }
}
