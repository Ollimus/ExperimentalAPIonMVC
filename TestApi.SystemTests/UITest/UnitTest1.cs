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
    public class UnitTest1 : SeleniumTest
    {
        public UnitTest1() : base("TestApi") { }

        [TestMethod]
        public void terer()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                try
                {
                    driver.Navigate().GoToUrl("http://localhost:8080");
                    //driver.FindElement(By.ClassName("jumbotron"));
                    driver.FindElement(By.LinkText("Customers")).Click();
                }

                catch (Exception e)
                {
                    Debug.Print("Error: " + e);
                    Assert.Fail();
                }
            }
        }
    }
}
