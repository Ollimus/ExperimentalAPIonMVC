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
    public class OrdersControllerTest
    {
        private OrdersController _controller;
        private ApplicationDbContext _context;

        public OrdersControllerTest()
        {
            _context = new ApplicationDbContext();
            _controller = new OrdersController(new UnitOfWork(_context));
        }

        [TestMethod]
        public void GetProducts_GetAllOrders_ReturnsOrderObjects()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);
                Order secondOrder = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 7);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.Orders.Add(secondOrder);
                _context.SaveChanges();

                var result = _controller.GetOrders() as OkNegotiatedContentResult<List<Order>>;
                var ordersResults = result.Content;
                bool doesListHaveValues = (ordersResults.Count > 0) ? true : false;

                Assert.IsNotNull(ordersResults);
                Assert.IsTrue(doesListHaveValues);
            }
        }

        [TestMethod]
        public void GetOrders_GetOrderById_ReturnOrderObject()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);
                Order secondOrder = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 7);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.Orders.Add(secondOrder);
                _context.SaveChanges();

                var result = _controller.GetOrders(secondOrder.Id) as OkNegotiatedContentResult<Order>;
                var productsResult = result.Content;

                Assert.AreEqual(secondOrder.Id, productsResult.Id);
            }
        }

        [TestMethod]
        public void GetOrders_GetOrderByincorrectId_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.SaveChanges();

                var result = _controller.GetOrders(0) as NotFoundResult;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void GetProducts_GetAllOrdersByCustomerId_ReturnsOrderObjects()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);
                Order secondOrder = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 7);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.Orders.Add(secondOrder);
                _context.SaveChanges();

                var result = _controller.GetOrdersByCustomerid(customer.CustomerId) as OkNegotiatedContentResult<List<Order>>;
                var ordersResults = result.Content;
                bool doesListHaveValues = (ordersResults.Count > 0) ? true : false;

                Assert.IsNotNull(ordersResults);
                Assert.IsTrue(doesListHaveValues);
            }
        }


        [TestMethod]
        public void GetOrders_GetOrderByIncorrectCustomerId_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.SaveChanges();

                var result = _controller.GetOrdersByCustomerid(0) as NotFoundResult;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void GetProducts_GetAllOrdersByProductId_ReturnsOrderObjects()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);
                Order secondOrder = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 7);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.Orders.Add(secondOrder);
                _context.SaveChanges();

                var result = _controller.GetOrdersByProductId(product.ProductId) as OkNegotiatedContentResult<List<Order>>;
                var ordersResults = result.Content;
                bool doesListHaveValues = (ordersResults.Count > 0) ? true : false;

                Assert.IsNotNull(ordersResults);
                Assert.IsTrue(doesListHaveValues);
            }
        }


        [TestMethod]
        public void GetOrders_GetOrderByIncorrectProductId_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.SaveChanges();

                var result = _controller.GetOrdersByProductId(0) as NotFoundResult;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void CreateOrder_CreateOrderSuccesfully_ReturnCreated()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.SaveChanges();

                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);

                HttpMethod test = new HttpMethod("Test");
                _controller.Request = new HttpRequestMessage(test, "/");
                _controller.Configuration = new HttpConfiguration();

                var result = _controller.CreateOrder(order) as CreatedNegotiatedContentResult<Order>;
                var ordersResult = result.Content;

                Assert.IsNotNull(ordersResult);
            }
        }

        [TestMethod]
        public void CreateOrder_CustomerIdNotFoundInDatabase_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(0, product.ProductId, 2);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.SaveChanges();

                var result = _controller.CreateOrder(order) as NotFoundResult;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void CreateOrder_ProductIdNotFoundInDatabase_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, 0, 0);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.SaveChanges();

                var result = _controller.CreateOrder(order) as NotFoundResult;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void UpdateOrder_UpdateOrderWithCorrectDetails_ReturnOk()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.SaveChanges();

                Order updateOrder = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 11);
                var result = _controller.UpdateOrder(order.Id, updateOrder);

                Assert.IsInstanceOfType(result, typeof(OkResult));
            }
        }

        [TestMethod]
        public void UpdateOrder_IncorrectExistingOrderId_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.SaveChanges();

                Order updateOrder = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 11);
                var result = _controller.UpdateOrder(0, updateOrder);

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void RemoveOrder_RemoveOrderByCorrectOrderId_ReturnOk()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.SaveChanges();

                var result = _controller.DeleteOrder(order.Id);
                var confirmIfDeleted = _controller.GetOrders(order.Id);

                Assert.IsInstanceOfType(result, typeof(OkResult));
                Assert.IsInstanceOfType(confirmIfDeleted, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void RemoveOrder_RemoveOrderByIncorrectOrderId_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Customer customer = CreateTestObjects.CreateNewCustomer("Kia", "Hankola", "Helsinki", "Helsingintie 16");
                Order order = CreateTestObjects.CreateNewOrder(customer.CustomerId, product.ProductId, 4);

                _context.Products.Add(product);
                _context.Customers.Add(customer);
                _context.Orders.Add(order);
                _context.SaveChanges();

                var result = _controller.DeleteOrder(0);
                var confirmItExists = _controller.GetOrders(order.Id) as OkNegotiatedContentResult<Order>;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
                Assert.IsNotNull(confirmItExists);
            }
        }
    }
}
