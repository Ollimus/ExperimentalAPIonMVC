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
    public class ProductsControllerTest
    {
        private ProductsController _controller;
        private ApplicationDbContext _context;

        public ProductsControllerTest()
        {
            _context = new ApplicationDbContext();
            _controller = new ProductsController(new UnitOfWork(_context));
        }

        [TestMethod]
        public void GetProducts_GetAllProducts_ReturnsProductObjects()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Product secondProduct = CreateTestObjects.CreateNewProduct("Wooden sheds", "Wooden sheds built by local company.", "Shedders Oy", 790.4m, 1);
                Product thirdProduct = CreateTestObjects.CreateNewProduct("Multi-use drill", "Multi-use drill meant for all types of surfaces.", "Specialized tools Oy", 1900m, 10);

                _context.Products.Add(product);
                _context.Products.Add(secondProduct);
                _context.Products.Add(thirdProduct);
                _context.SaveChanges();

                var result = _controller.GetProducts() as OkNegotiatedContentResult<List<Product>>;
                var productsResult = result.Content;
                bool doesListHaveValues = (productsResult.Count > 0) ? true : false;

                Assert.IsNotNull(productsResult);
                Assert.IsTrue(doesListHaveValues);
            }
        }

        [TestMethod]
        public void GetProducts_GetProductById_ReturnProductObject()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Product secondProduct = CreateTestObjects.CreateNewProduct("Wooden sheds", "Wooden sheds built by local company.", "Shedders Oy", 790.4m, 1);
                Product thirdProduct = CreateTestObjects.CreateNewProduct("Multi-use drill", "Multi-use drill meant for all types of surfaces.", "Specialized tools Oy", 1900m, 10);

                _context.Products.Add(product);
                _context.Products.Add(secondProduct);
                _context.Products.Add(thirdProduct);
                _context.SaveChanges();

                var result = _controller.GetProducts(secondProduct.ProductId) as OkNegotiatedContentResult<Product>;
                var productsResult = result.Content;

                Assert.AreEqual(secondProduct.ProductId, productsResult.ProductId);
            }
        }

        [TestMethod]
        public void GetProducts_GetProductByIncorrectId_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);

                _context.Products.Add(product);
                _context.SaveChanges();

                var result = _controller.GetProducts(0) as NotFoundResult;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void SearchByProducer_GetProductByProducer_ReturnProductObject()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);

                _context.Products.Add(product);
                _context.SaveChanges();

                var result = _controller.SearchByProducer(product.Producer) as OkNegotiatedContentResult<Product>;

                Assert.AreEqual(product.Producer, result.Content.Producer);
            }
        }

        [TestMethod]
        public void SearchByProducer_NoProductsFoundByProducer_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                string invalidProducer = "Apple";
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);

                _context.Products.Add(product);
                _context.SaveChanges();

                var result = _controller.SearchByProducer(invalidProducer) as NotFoundResult;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void SearchByProducer_SearchProducersByEmptyString_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                string invalidProducer = "";
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);

                _context.Products.Add(product);
                _context.SaveChanges();

                var result = _controller.SearchByProducer(invalidProducer) as NotFoundResult;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public void CreateProduct_ValidProductCreated_ReturnCreated()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);

                HttpMethod test = new HttpMethod("Test");
                _controller.Request = new HttpRequestMessage(test, "/");
                _controller.Configuration = new HttpConfiguration();

                var result = _controller.CreateProduct(product) as CreatedNegotiatedContentResult<Product>;

                Assert.IsNotNull(result);
                Assert.AreEqual(product.ProductId, result.Content.ProductId);
            }
        }

        [TestMethod]
        public void UpdateProduct_UpdateExistingCustomer_ReturnOK()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Product removeSoldProductFromStock = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 1);
                _context.Products.Add(product);
                _context.SaveChanges();

                var result = _controller.UpdateProduct(product.ProductId, removeSoldProductFromStock);
                var updatedProduct = _controller.GetProducts(product.ProductId) as OkNegotiatedContentResult<Product>;

                Assert.IsInstanceOfType(result, typeof(OkResult));
                Assert.AreEqual(removeSoldProductFromStock.Stock, updatedProduct.Content.Stock);
            }
        }

        [TestMethod]
        public void UpdateProduct_UpdateExistingProduct_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                int incorrectId = 0;
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);
                Product removeSoldProductFromStock = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 1);

                _context.Products.Add(product);
                _context.SaveChanges();

                var result = _controller.UpdateProduct(incorrectId, removeSoldProductFromStock);
                var updatedProduct = _controller.GetProducts(product.ProductId) as OkNegotiatedContentResult<Product>;

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
                Assert.AreNotEqual(removeSoldProductFromStock.Stock, updatedProduct.Content.Stock);
            }
        }

        [TestMethod]
        public void DeleteProduct_DeleteProductWithCorrectId_ReturnOk()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);

                _context.Products.Add(product);
                _context.SaveChanges();

                var checkIfExistsInDB = _controller.GetProducts(product.ProductId) as OkNegotiatedContentResult<Product>;
                var result = _controller.DeleteProduct(product.ProductId);
                var findIfDeleted = _controller.GetProducts(product.ProductId) as OkNegotiatedContentResult<Product>;

                Assert.IsInstanceOfType(result, typeof(OkResult));
                Assert.IsNotNull(checkIfExistsInDB);
                Assert.IsNull(findIfDeleted);
            }
        }

        [TestMethod]
        public void DeleteProduct_DeleteProductWithIncorrectId_ReturnNotFound()
        {
            using (new TransactionScope())
            {
                Product product = CreateTestObjects.CreateNewProduct("G15", "Keyboard for gaming.", "Logitech", 125.4m, 2);

                _context.Products.Add(product);
                _context.SaveChanges();

                var checkIfExistsInDB = _controller.GetProducts(product.ProductId) as OkNegotiatedContentResult<Product>;
                var result = _controller.DeleteProduct(0);

                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
                Assert.IsNotNull(checkIfExistsInDB);
            }
        }
    }
}
