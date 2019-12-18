using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TestApi.UnitTests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void CreateCustomer_CorrectlyValidateAllProperties_ValidationSuccess()
        {
            Customer customer = new Customer()
            {
                CustomerId = 0, //Not required, but put in as mock.
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            var validationContext = new ValidationContext(customer, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customer, validationContext, errors, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CreateCustomer_ValidateAllPropertiesWithNoValues_ValidationFailure()
        {
            Customer customer = new Customer();

            var validationContext = new ValidationContext(customer, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customer, validationContext, errors, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CreateCustomer_FirstNameValueTooShort_ReturnError()
        {
            Customer customer = new Customer() { FirstName = "K" };

            var validationContext = new ValidationContext(customer, null, null)
            {
                MemberName = "FirstName"
            };

            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(customer.FirstName, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateCustomer_FirstNameValueTooLong_ReturnError()
        {
            string testString = new string('a', 101);
            Customer customer = new Customer() { FirstName = testString };

            var validationContext = new ValidationContext(customer, null, null)
            {
                MemberName = "FirstName"
            };

            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(customer.FirstName, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateCustomer_LastNameValueTooShort_ReturnError()
        {
            Customer customer = new Customer() { LastName = "H" };

            var validationContext = new ValidationContext(customer, null, null)
            {
                MemberName = "LastName"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(customer.LastName, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateCustomer_LastNameValueTooLong_ReturnError()
        {
            string testString = new string('a', 101);
            Customer customer = new Customer() { LastName = testString };

            var validationContext = new ValidationContext(customer, null, null)
            {
                MemberName = "LastName"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(customer.LastName, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateCustomer_CityValueTooShort_ReturnError()
        {
            Customer customer = new Customer() { City = "He" };

            var validationContext = new ValidationContext(customer, null, null)
            {
                MemberName = "City"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(customer.City, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateCustomer_CityValueTooLong_ReturnError()
        {
            string testString = new string('a', 101);
            Customer customer = new Customer() { City = testString };

            var validationContext = new ValidationContext(customer, null, null)
            {
                MemberName = "City"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(customer.City, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateCustomer_AddressValueTooShort_ReturnError()
        {
            Customer customer = new Customer() { Address = "He" };

            var validationContext = new ValidationContext(customer, null, null)
            {
                MemberName = "Address"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(customer.Address, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void CreateCustomer_AddressValueTooLong_ReturnError()
        {
            string testString = new string('a', 101);
            Customer customer = new Customer() { Address = testString };

            var validationContext = new ValidationContext(customer, null, null)
            {
                MemberName = "Address"
            };
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(customer.Address, validationContext, errors);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errors.Count);
        }
    }
}
