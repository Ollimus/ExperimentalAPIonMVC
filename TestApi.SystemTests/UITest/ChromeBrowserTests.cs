using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace TestApi.SystemTests.UITest
{
    [TestClass]
    public class ChromeBrowserTests : SeleniumTest
    {
        public ChromeBrowserTests() : base("TestApi") { }

        public static IWebDriver driver;

        [ClassInitialize]
        public static void SetUpDriver(TestContext testContext)
        {
            driver = new ChromeDriver();
        }

        [ClassCleanup]
        public static void CloseDrivers()
        {
            driver.Quit();
            driver.Dispose();
        }

        [TestMethod]
        public void NavigateToCustomers_CreateCustomer_CheckIfCreatedCustomerExists()
        {
            try
            {
                driver.Navigate().GoToUrl("http://localhost:8080");
                driver.FindElement(By.LinkText("Customers")).Click();
                string customersUrl = driver.Url.ToLower();

                driver.FindElement(By.Id("CreateNewCustomer")).Click();

                IWebElement element = driver.FindElement(By.Id("FirstName"));
                element.SendKeys("Teuvo");

                element = driver.FindElement(By.Id("LastName"));
                element.SendKeys("Kirjanen");

                element = driver.FindElement(By.Id("City"));
                element.SendKeys("Joensuu");

                element = driver.FindElement(By.Id("Address"));
                element.SendKeys("Tulliportinkatu 11");

                driver.FindElement(By.Id("Submit")).Click();
                Thread.Sleep(2000);

                Assert.AreEqual(customersUrl, driver.Url.ToLower());
            }

            catch (Exception e)
            {
                Debug.Print("Error: " + e);
                Assert.Fail();
            }
        }
    }
}
