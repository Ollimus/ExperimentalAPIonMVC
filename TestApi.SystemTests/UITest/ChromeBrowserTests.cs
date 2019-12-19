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
        public void NavigateToCustomers_CreateCustomer_WaitForRedirectAfterSuccess()
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

        [TestMethod]
        public void NavigateToProducts_CreateProduct_WaitForRedirectAfterSuccess()
        {
            try
            {
                driver.Navigate().GoToUrl("http://localhost:8080");
                driver.FindElement(By.LinkText("Products")).Click();
                string productsUrl = driver.Url.ToLower();

                driver.FindElement(By.Id("CreateNewProduct")).Click();

                IWebElement element = driver.FindElement(By.Id("Name"));
                element.SendKeys("Iphone 6");

                element = driver.FindElement(By.Id("Producer"));
                element.SendKeys("Apple");

                element = driver.FindElement(By.Id("Description"));
                element.SendKeys("Modern phone for modern people.");

                element = driver.FindElement(By.Id("Price"));
                element.SendKeys("13.45");

                element = driver.FindElement(By.Id("Stock"));
                element.SendKeys("7");

                driver.FindElement(By.Id("Submit")).Click();
                WebDriverWait waiting = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waiting.Until(ExpectedConditions.UrlMatches(productsUrl));

                Assert.AreEqual(productsUrl, driver.Url.ToLower());
            }

            catch (Exception e)
            {
                Debug.Print("Error: " + e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void NavigateToProducts_CreateOrder_CheckIfCreatedProductExists()
        {
            try
            {
                driver.Navigate().GoToUrl("http://localhost:8080");
                driver.FindElement(By.LinkText("Orders")).Click();
                string orderUrl = driver.Url.ToLower();

                driver.FindElement(By.Id("CreateNewOrder")).Click();

                var customers = driver.FindElement(By.Id("CustomerId"));
                var selectElement = new SelectElement(customers);
                selectElement.SelectByValue("1");

                var products = driver.FindElement(By.Id("ProductId"));
                selectElement = new SelectElement(products);
                selectElement.SelectByValue("1");

                IWebElement element = driver.FindElement(By.Id("Quantity"));
                element.SendKeys("5");

                driver.FindElement(By.Id("Submit")).Click();
                WebDriverWait waiting = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waiting.Until(ExpectedConditions.UrlMatches(orderUrl));

                Assert.AreEqual(orderUrl, driver.Url.ToLower());
            }

            catch (Exception e)
            {
                Debug.Print("Error: " + e);
                Assert.Fail();
            }
        }
    }
}
