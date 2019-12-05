//using System;
//using System.Data.Entity;
//using System.Web.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TestAPI.Controllers;
//using TestAPI.Models;
//using TestAPI.Repositories;
//using Moq;
//using System.Collections.Generic;
//using TestAPI.Controllers.API;
//using System.Linq;
//using System.Web.Http.Results;
//using System.Net;
//using System.Web;
//using System.Web.Routing;
//using System.Net.Http;
//using System.Web.Http;

//namespace TestAPITests.UnitTests.Controllers
//{
//    [TestClass]
//    public class ProductsControllerTest
//    {
//        private ProductsController _controller;
//        Mock<IProductRepository> _mockRepository;

//        [TestInitialize]
//        public void TestInitialize()
//        {
//            _mockRepository = new Mock<IProductRepository>();
//            var mockContext = new Mock<IUnitOfWork>();

//            mockContext.SetupGet(u => u.Products).Returns(_mockRepository.Object);

//            _controller = new ProductsController(mockContext.Object);
//        }


//        [TestMethod]
//        public void GetProducts_GetAllProducts_ReturnsAllProducts()
//        {
//            List<Product> productList = new List<Product>();

//            Product product = new Product()
//            {
//                ProductId = 0,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            productList.Add(product);
//            _mockRepository.SetupGet(r => r.GetProducts).Returns(productList.AsQueryable());

//            var result = _controller.GetProducts() as OkNegotiatedContentResult<List<Product>>;
//            var products = result.Content;

//            Assert.AreEqual(1, products.Count);
//        }

//        [TestMethod]
//        public void GetProducts_GetProductById_ReturnProductObject()
//        {
//            Product product = new Product()
//            {
//                ProductId = 10,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            _mockRepository.Setup(r => r.GetProductById(10)).Returns(product);

//            var result = _controller.GetProducts(10) as OkNegotiatedContentResult<Product>;
//            var productResult = result.Content;

//            Assert.AreEqual(10, productResult.ProductId);
//        }

//        [TestMethod]
//        public void GetProducts_GetProductByIncorrectId_ReturnNotFound()
//        {
//            Product product = new Product()
//            {
//                ProductId = 10,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            _mockRepository.Setup(r => r.GetProductById(10)).Returns(product);
//            var result = _controller.GetProducts(5);

//            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//        }

//        [TestMethod]
//        public void SearchByProducer_GetProductByProducer_ReturnProductObject()
//        {
//            Product product = new Product()
//            {
//                ProductId = 10,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            _mockRepository.Setup(r => r.GetProductByProducer("Logitech")).Returns(product);
//            var result = _controller.SearchByProducer("Logitech") as OkNegotiatedContentResult<Product>;

//            Assert.AreEqual("Logitech", result.Content.Producer);
//        }

//        [TestMethod]
//        public void SearchByProducer_NoProductsFoundByProducer_ReturnNotFound()
//        {
//            Product product = new Product()
//            {
//                ProductId = 10,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            _mockRepository.Setup(r => r.GetProductByProducer("Logitech")).Returns(product);
//            var result = _controller.SearchByProducer("Apple");

//            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//        }

//        [TestMethod]
//        public void CreateProduct_ValidProductCreated_ReturnCreated()
//        {
//            HttpMethod test = new HttpMethod("Test");
//            _controller.Request = new HttpRequestMessage(test, "/");
//            _controller.Configuration = new HttpConfiguration();

//            Product product = new Product()
//            {
//                ProductId = 10,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            var result = _controller.CreateProduct(product) as CreatedNegotiatedContentResult<Product>;

//            Assert.IsNotNull(result);
//            Assert.AreEqual(10, result.Content.ProductId);
//        }

//        [TestMethod]
//        public void UpdateProduct_UpdateExistingCustomer_ReturnOK()
//        {
//            Product product = new Product()
//            {
//                ProductId = 10,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            _mockRepository.Setup(r => r.GetProductById(10)).Returns(product);
//            var result = _controller.UpdateProduct(10, product);

//            Assert.IsInstanceOfType(result, typeof(OkResult));
//        }

//        [TestMethod]
//        public void UpdateProduct_UpdateExistingProduct_ReturnNotFound()
//        {
//            Product product = new Product()
//            {
//                ProductId = 10,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            _mockRepository.Setup(r => r.GetProductById(10)).Returns(product);
//            var result = _controller.UpdateProduct(5, product);

//            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//        }

//        [TestMethod]
//        public void DeleteProduct_DeleteProductWithCorrectId_ReturnOk()
//        {
//            Product product = new Product()
//            {
//                ProductId = 10,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            _mockRepository.Setup(r => r.GetProductById(10)).Returns(product);
//            var result = _controller.DeleteProduct(10);

//            Assert.IsInstanceOfType(result, typeof(OkResult));
//        }

//        [TestMethod]
//        public void DeleteProduct_DeleteProductWithIncorrectId_ReturnNotFound()
//        {
//            Product product = new Product()
//            {
//                ProductId = 10,
//                Name = "G15 Keyboard",
//                Description = "Gaming keyboard for many uses.",
//                Price = 120,
//                Producer = "Logitech",
//                Stock = 10
//            };

//            _mockRepository.Setup(r => r.GetProductById(10)).Returns(product);
//            var result = _controller.DeleteProduct(5);

//            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//        }
//    }
//}
