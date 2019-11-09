﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Models;

namespace TestAPITests.Classes
{
    [TestClass]
    public class CreateCustomer_Succesfully
    {
        [TestMethod]
        public void CreateCustomer()
        {
            int mockId = 0;  //Id is generated by the database, so we use a mock one in unit test.

            Customer customer = new Customer()
            {
                CustomerId = mockId,
                FirstName = "Kia",
                LastName = "Makkonen",
                Address = "Helsinkintie 12",
                City = "Helsinki",
                DateCreated = DateTime.Today
            };

            /*
             *Database uses DataAnnotations that are ran by code when doing validation.
             *To replicate this, we'll manually run the validation using the code. 
            */
            var result = new List<ValidationResult>();  //Create list to contain all the validation results.

            /* Takes 4 parameters:
             * Object you validate, validationcontext you validate the first parameter against,
             * list that is used to store results and whether all properties are validated.
             * If all property validation isn't true, 
            */
            var isValid = Validator.TryValidateObject(customer, new ValidationContext (customer), result, true);


            Assert.IsTrue(isValid);
        }
    }
}