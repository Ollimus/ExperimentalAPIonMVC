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
    public class OrdersControllerTest
    {
        private OrdersController _controller;
        Mock<IOrdersRepository> _mockRepository;
        Mock<IProductRepository> _productRepository;
        Mock<ICustomerRepository> _customerRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IOrdersRepository>();
            _productRepository = new Mock<IProductRepository>();
            _customerRepository = new Mock<ICustomerRepository>();

            var mockContext = new Mock<IUnitOfWork>();

            mockContext.SetupGet(u => u.Orders).Returns(_mockRepository.Object);
            mockContext.SetupGet(c => c.Customers).Returns(_customerRepository.Object);
            mockContext.Setup(p => p.Products).Returns(_productRepository.Object);

            _controller = new OrdersController(mockContext.Object);
        }

        [TestMethod]
        public void GetOrders_GetAllOrders_ReturnsAllOrderObjects()
        {
            List<Order> orderList = new List<Order>();

            Order order = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            orderList.Add(order);
            orderList.Add(order);

            _mockRepository.SetupGet(r => r.GetOrders).Returns(orderList.AsQueryable());

            var result = _controller.GetOrders() as OkNegotiatedContentResult<List<Order>>;
            var orders = result.Content;

            Assert.AreEqual(2, orders.Count);
        }

        [TestMethod]
        public void GetOrders_GetAllOrderById_ReturnsOrderObject()
        {
            Order order = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            _mockRepository.Setup(r => r.GetOrderById(2)).Returns(order);

            var result = _controller.GetOrders(2) as OkNegotiatedContentResult<Order>;
            var orders = result.Content;

            Assert.AreEqual(2, result.Content.Id);
        }

        [TestMethod]
        public void GetOrders_GetOrderByIncorrectId_ReturnNotFound()
        {
            Order order = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            _mockRepository.Setup(r => r.GetOrderById(2)).Returns(order);

            var result = _controller.GetOrders(1) as NotFoundResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetOrders_GetOrdersByProductId_ReturnsTwoOrderObjects()
        {
            List<Order> orderList = new List<Order>();

            Order order = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            orderList.Add(order);
            orderList.Add(order);

            _mockRepository.Setup(r => r.GetOrdersByProductId(2)).Returns(orderList.AsQueryable());

            var result = _controller.GetOrdersByProductId(2) as OkNegotiatedContentResult<List<Order>>;
            var orders = result.Content;

            Assert.AreEqual(2, orders.Count);
        }

        [TestMethod]
        public void GetOrders_GetOrdersByIncorrectProductId_ReturnsNotFound()
        {
            List<Order> orderList = new List<Order>();

            Order order = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            orderList.Add(order);
            orderList.Add(order);

            _mockRepository.Setup(r => r.GetOrdersByProductId(2)).Returns(orderList.AsQueryable());
            var result = _controller.GetOrdersByProductId(1) as NotFoundResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetOrders_GetOrdersByCustomerId_ReturnsTwoOrderObjects()
        {
            List<Order> orderList = new List<Order>();

            Order order = new Order(){CustomerId = 4};

            orderList.Add(order);
            orderList.Add(order);

            _mockRepository.Setup(r => r.GetOrdersByCustomerid(4)).Returns(orderList.AsQueryable());
            var result = _controller.GetOrdersByCustomerid(4) as OkNegotiatedContentResult<List<Order>>;

            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void GetOrders_GetOrdersByIncorrectCustomerId_ReturnsNotFound()
        {
            List<Order> orderList = new List<Order>();

            Order order = new Order() { CustomerId = 4 };

            orderList.Add(order);
            orderList.Add(order);

            _mockRepository.Setup(r => r.GetOrdersByCustomerid(4)).Returns(orderList.AsQueryable());

            var result = _controller.GetOrdersByCustomerid(1);


            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateOrder_ValidOrderCreated_ReturnCreated()
        {
            HttpMethod test = new HttpMethod("Test");
            _controller.Request = new HttpRequestMessage(test, "/");
            _controller.Configuration = new HttpConfiguration();

            int orderAmount = 4;
            Product product = new Product() { ProductId = 1, Price = 240.34m };
            Customer customer = new Customer() { CustomerId = 2 };

            _customerRepository.Setup(c => c.GetCustomerById(2)).Returns(customer);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _mockRepository.Setup(o => o.CalculateTotalOrderValue(product, orderAmount)).Returns(orderAmount * product.Price);

            var result = _controller.CreateOrder(customer.CustomerId, product.ProductId, orderAmount) as CreatedNegotiatedContentResult<Order>;
            var totalPrice = result.Content.TotalPrice;

            Assert.AreEqual(961.36m, totalPrice);
        }

        [TestMethod]
        public void CreateOrder_CustomerNotFound_ReturnNotFound()
        {
            HttpMethod test = new HttpMethod("Test");
            _controller.Request = new HttpRequestMessage(test, "/");
            _controller.Configuration = new HttpConfiguration();

            int orderAmount = 4;
            Product product = new Product() { ProductId = 1, Price = 240.34m };
            Customer customer = new Customer() { CustomerId = 2 };

            _customerRepository.Setup(c => c.GetCustomerById(2)).Returns(customer);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _mockRepository.Setup(o => o.CalculateTotalOrderValue(product, orderAmount)).Returns(orderAmount * product.Price);

            var result = _controller.CreateOrder(1, product.ProductId, orderAmount);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateOrder_ProductNotFound_ReturnNotFound()
        {
            HttpMethod test = new HttpMethod("Test");
            _controller.Request = new HttpRequestMessage(test, "/");
            _controller.Configuration = new HttpConfiguration();

            int orderAmount = 4;
            Product product = new Product() { ProductId = 1, Price = 240.34m };
            Customer customer = new Customer() { CustomerId = 2 };

            _customerRepository.Setup(c => c.GetCustomerById(2)).Returns(customer);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _mockRepository.Setup(o => o.CalculateTotalOrderValue(product, orderAmount)).Returns(orderAmount * product.Price);

            var result = _controller.CreateOrder(customer.CustomerId, 10, orderAmount);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateOrder_OrderAmountIsZero_ReturnBadRequest()
        {
            HttpMethod test = new HttpMethod("Test");
            _controller.Request = new HttpRequestMessage(test, "/");
            _controller.Configuration = new HttpConfiguration();

            int orderAmount = 4;
            Product product = new Product() { ProductId = 1, Price = 240.34m };
            Customer customer = new Customer() { CustomerId = 2 };

            _customerRepository.Setup(c => c.GetCustomerById(2)).Returns(customer);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _mockRepository.Setup(o => o.CalculateTotalOrderValue(product, orderAmount)).Returns(orderAmount * product.Price);

            var result = _controller.CreateOrder(customer.CustomerId, product.ProductId, 0);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void CreateOrder_OrderAmountIsNegativeValue_ReturnBadRequest()
        {
            HttpMethod test = new HttpMethod("Test");
            _controller.Request = new HttpRequestMessage(test, "/");
            _controller.Configuration = new HttpConfiguration();

            int orderAmount = 4;
            Product product = new Product() { ProductId = 1, Price = 240.34m };
            Customer customer = new Customer() { CustomerId = 2 };

            _customerRepository.Setup(c => c.GetCustomerById(2)).Returns(customer);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _mockRepository.Setup(o => o.CalculateTotalOrderValue(product, orderAmount)).Returns(orderAmount * product.Price);

            var result = _controller.CreateOrder(customer.CustomerId, product.ProductId, -1);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void UpdateOrder_UpdateOrderCorrectly_ReturnOk()
        {
            int orderAmount = 4;
            Product product = new Product() { ProductId = 1, Price = 250.34m };
            Customer customer = new Customer() { CustomerId = 3 };

            Order order = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            _mockRepository.Setup(o => o.GetOrderById(2)).Returns(order);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _customerRepository.Setup(p => p.GetCustomerById(3)).Returns(customer);

            var result = _controller.UpdateOrder(order.Id, customer.CustomerId, product.ProductId, orderAmount);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateOrder_NoOrderFoundWithOrderId_ReturnNotFound()
        {
            int orderAmount = 4;
            Product product = new Product() { ProductId = 1, Price = 250.34m };
            Customer customer = new Customer() { CustomerId = 3 };

            Order existingOrder = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            _mockRepository.Setup(o => o.GetOrderById(2)).Returns(existingOrder);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _customerRepository.Setup(p => p.GetCustomerById(3)).Returns(customer);

            var result = _controller.UpdateOrder(7, customer.CustomerId, product.ProductId, orderAmount);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateOrder_NoCustomerFoundWithCustomerId_ReturnNotFound()
        {
            int orderAmount = 4;
            Product product = new Product() { ProductId = 1, Price = 250.34m };
            Customer customer = new Customer() { CustomerId = 3 };

            Order existingOrder = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            _mockRepository.Setup(o => o.GetOrderById(2)).Returns(existingOrder);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _customerRepository.Setup(p => p.GetCustomerById(3)).Returns(customer);

            var result = _controller.UpdateOrder(existingOrder.Id, 7, product.ProductId, orderAmount);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateOrder_NoProductFoundWithProductId_ReturnNotFound()
        {
            int orderAmount = 4;
            Product product = new Product() { ProductId = 1, Price = 250.34m };
            Customer customer = new Customer() { CustomerId = 3 };

            Order order = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            _mockRepository.Setup(o => o.GetOrderById(2)).Returns(order);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _customerRepository.Setup(p => p.GetCustomerById(3)).Returns(customer);

            var result = _controller.UpdateOrder(order.Id, customer.CustomerId, 7, orderAmount);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateOrder_OrderAmountIsZero_ReturnBadRequest()
        {
            int orderAmount = 0;
            Product product = new Product() { ProductId = 1, Price = 250.34m };
            Customer customer = new Customer() { CustomerId = 3 };

            Order existingOrder = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            _mockRepository.Setup(o => o.GetOrderById(2)).Returns(existingOrder);
            _productRepository.Setup(p => p.GetProductById(1)).Returns(product);
            _customerRepository.Setup(p => p.GetCustomerById(3)).Returns(customer);

            var result = _controller.UpdateOrder(existingOrder.Id, customer.CustomerId, product.ProductId, orderAmount);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void RemoveOrder_RemoveOrderByCorrectOrderId_ReturnOk()
        {
            Order order = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            _mockRepository.Setup(o => o.GetOrderById(2)).Returns(order);
            _mockRepository.Setup(o => o.Remove(order));

            var result = _controller.DeleteOrder(2);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void RemoveOrder_RemoveOrderByIncorrectId_ReturnNotFound()
        {
            Order order = new Order()
            {
                Id = 2,
                CustomerId = 4,
                ProductId = 6,
                OrderAmount = 2,
                TotalPrice = 100
            };

            _mockRepository.Setup(o => o.GetOrderById(1)).Returns(order);
            _mockRepository.Setup(o => o.Remove(order));

            var result = _controller.DeleteOrder(2);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
