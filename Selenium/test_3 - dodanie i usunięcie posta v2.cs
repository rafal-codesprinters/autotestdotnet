using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Test3DodanieIUsuniCiePostaV2
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://autotestdotnet.wordpress.com/wp-admin/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void The3DodanieIUsuniCiePostaV2Test()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-admin/edit.php");
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab21395140282350378 | ]]
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab21395140282350378 | ]]
            driver.FindElement(By.Id("post-search-input")).Clear();
            driver.FindElement(By.Id("post-search-input")).SendKeys("guid");
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab21395140282350378 | ]]
            driver.FindElement(By.Id("search-submit")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab21395140282350378 | ]]
            driver.FindElement(By.LinkText("guid")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab030467289585749735 | ]]
            driver.FindElement(By.LinkText("Move to Trash")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab030467289585749735 | ]]
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab030467289585749735 | ]]
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab030467289585749735 | ]]
            driver.FindElement(By.Id("s")).Clear();
            driver.FindElement(By.Id("s")).SendKeys("guid");
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab030467289585749735 | ]]
            driver.FindElement(By.Name("search")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | name=oktab030467289585749735 | ]]
            Assert.AreEqual("No articles found.", driver.FindElement(By.CssSelector("h1.page-title")).Text);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
