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

namespace TestApi.UnitTests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        Mock<IProductRepository> _mockRepository;

        public ProductsControllerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            var mockContext = new Mock<IUnitOfWork>();

            mockContext.SetupGet(u => u.Products).Returns(_mockRepository.Object);

            _controller = new ProductsController(mockContext.Object);
        }

        [TestMethod]
        public void GetProducts_GetAllProducts_ReturnsAllProducts()
        {
            List<Product> productList = new List<Product>();
            Product product = new Product();

            productList.Add(product);
            productList.Add(product);

            _mockRepository.SetupGet(r => r.GetProducts).Returns(productList.AsQueryable());

            var result = _controller.GetProducts() as OkNegotiatedContentResult<List<Product>>;
            var resultProducts = result.Content;

            Assert.AreEqual(productList.Count, resultProducts.Count);
        }

        [TestMethod]
        public void GetProducts_GetProductById_ReturnProductObject()
        {
            Product product = new Product() { ProductId = 10 };

            _mockRepository.Setup(r => r.GetProductById(10)).Returns(product);

            var result = _controller.GetProducts(10) as OkNegotiatedContentResult<Product>;
            var productResult = result.Content;

            Assert.AreEqual(product.ProductId, productResult.ProductId);
        }

        [TestMethod]
        public void GetProducts_GetProductByIncorrectId_ReturnNotFound()
        {
            int incorrectProductId = 5;
            Product product = new Product() { ProductId = 2 };

            _mockRepository.Setup(r => r.GetProductById(product.ProductId)).Returns(product);
            var result = _controller.GetProducts(incorrectProductId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void SearchByProducer_GetProductByProducer_ReturnProductObject()
        {
            Product product = new Product() { Producer = "Logitech" };

            _mockRepository.Setup(r => r.GetProductByProducer(product.Producer)).Returns(product);
            var result = _controller.SearchByProducer(product .Producer) as OkNegotiatedContentResult<Product>;

            Assert.AreEqual(product.Producer, result.Content.Producer);
        }

        [TestMethod]
        public void SearchByProducer_NoProductsFoundByProducer_ReturnNotFound()
        {
            string invalidProducer = "Apple";
            Product product = new Product() {Producer = "Logitech" };

            _mockRepository.Setup(r => r.GetProductByProducer(product.Producer)).Returns(product);
            var result = _controller.SearchByProducer(invalidProducer);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateProduct_ValidProductCreated_ReturnCreated()
        {
            Product product = new Product() { ProductId = 2 };

            HttpMethod test = new HttpMethod("Test");
            _controller.Request = new HttpRequestMessage(test, "/");
            _controller.Configuration = new HttpConfiguration();

            var result = _controller.CreateProduct(product) as CreatedNegotiatedContentResult<Product>;

            Assert.IsNotNull(result);
            Assert.AreEqual(product.ProductId, result.Content.ProductId);
        }

        [TestMethod]
        public void UpdateProduct_UpdateExistingCustomer_ReturnOK()
        {
            Product product = new Product() { ProductId = 10 };
            Product newProduct = new Product();

            _mockRepository.Setup(r => r.GetProductById(product.ProductId)).Returns(product);
            var result = _controller.UpdateProduct(product.ProductId, newProduct);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateProduct_UpdateExistingProduct_ReturnNotFound()
        {
            int incorrectId = 5;
            Product product = new Product() { ProductId = 2 };

            _mockRepository.Setup(r => r.GetProductById(product.ProductId)).Returns(product);
            var result = _controller.UpdateProduct(incorrectId, product);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteProduct_DeleteProductWithCorrectId_ReturnOk()
        {
            Product product = new Product() { ProductId = 2 };

            _mockRepository.Setup(r => r.GetProductById(product.ProductId)).Returns(product);
            var result = _controller.DeleteProduct(product.ProductId);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteProduct_DeleteProductWithIncorrectId_ReturnNotFound()
        {
            int incorrectId = 5;
            Product product = new Product() { ProductId = 2 };

            _mockRepository.Setup(r => r.GetProductById(product.ProductId)).Returns(product);
            var result = _controller.DeleteProduct(incorrectId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
