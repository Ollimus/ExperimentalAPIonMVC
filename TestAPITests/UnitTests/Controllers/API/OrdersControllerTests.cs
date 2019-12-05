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
    public class BillingsControllerTest
    {
        private OrdersController _controller;
        Mock<IOrdersRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IOrdersRepository>();
            var mockContext = new Mock<IUnitOfWork>();

            mockContext.SetupGet(u => u.Orders).Returns(_mockRepository.Object);

            _controller = new OrdersController(mockContext.Object);
        }
    }
}
