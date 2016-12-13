using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace SeleniumTests
{
    public class Test01KlinaniePoNaglowkachCS : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        public Test01KlinaniePoNaglowkachCS()
        {
            driver = new FirefoxDriver();
            baseURL = "https://autotestdotnet.wordpress.com/";
            verificationErrors = new StringBuilder();
        }
        
        public void Dispose()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.Equal("", verificationErrors.ToString());
        }
        
        [Fact]
        public void The01KlinaniePoNaglowkachCSTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("test pg");
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            driver.FindElement(By.Id("publish")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            for (int second = 0;; second++) {
                Assert.False(second >= 60);
                try
                {
                    if (IsElementPresent(By.CssSelector("#message"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("test pg");
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            driver.FindElement(By.Id("publish")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            String linkToPost = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a/@href")).Text;
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            driver.FindElement(By.LinkText("Me")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
            driver.Navigate().GoToUrl(baseURL + linkToPost);
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            Assert.NotEqual("Page not found | Site Title", driver.Title);
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
