using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TestAPITests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void CreateProduct_CorrectDetails_ValidationSuccess()
        {
            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = 120,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CreateProduct_NoProperties_ValidationFailure()
        {
            Product product = new Product();

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CreateProduct_NamePropertyTooShort_ReturnError()
        {
            Product product = new Product() { Name = "G" };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Name"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Name, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_NamePropertyTooLong_ReturnError()
        {
            string testString = new string('a', 101);
            Product product = new Product() { Name = testString };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Name"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Name, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_DescriptionValueTooShort_ReturnError()
        {
            Product product = new Product() { Description = "Ga" };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Description"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Description, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_DescriptionPropertyTooLong_ReturnError()
        {
            string testString = new string('a', 256);
            Product product = new Product() { Description = testString };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Description"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Description, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_ProducerValueTooShort_ReturnError()
        {
            Product product = new Product() { Producer = "Lo" };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Producer"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Producer, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_ProducerPropertyTooLong_ReturnError()
        {
            string testString = new string('a', 41);
            Product product = new Product() { Producer = testString };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Producer"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Producer, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_PricePropertyIsZero_ReturnError()
        {
            Product product = new Product() { Price = 0 };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Price"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Price, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_PricePropertyIsOver5000Limit_ReturnError()
        {
            int testableNumber = 5001;
            Product product = new Product() { Price = testableNumber };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Price"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Price, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_PricePropertyHasTooManyDecimalPlaces_ReturnError()
        {
            decimal testableNumber = 211.211m;
            Product product = new Product() { Price = testableNumber };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Price"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Price, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_PricePropertyHasNegativeValue_ReturnError()
        {
            decimal testableNumber = -11.21m;
            Product product = new Product() { Price = testableNumber };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Price"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Price, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_StockNegativeValue_ReturnError()
        {
            int testableNumber = -11;
            Product product = new Product() { Stock = testableNumber };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Stock"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Stock, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_StockValueOverLimit_ReturnError()
        {
            int testableNumber = 501;
            Product product = new Product() { Stock = testableNumber };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Stock"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Stock, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateProduct_StockValueIsZero_ReturnError()
        {
            int testableNumber = 0;
            Product product = new Product() { Stock = testableNumber };

            var validationContext = new ValidationContext(product, null, null)
            {
                MemberName = "Stock"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(product.Stock, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }
    }
}
