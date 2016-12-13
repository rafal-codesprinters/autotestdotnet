using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace SeleniumTests
{
    public class Selenium : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        public Selenium()
        {
            driver = new FirefoxDriver();
            baseURL = "https://autotestdotnet.wordpress.com/";
            verificationErrors = new StringBuilder();
        }
        
        [Fact]
        public void TheSeleniumTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-admin/");
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.CssSelector("a.page-title-action")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("szy 3");
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("notatka 5");
            driver.FindElement(By.Id("publish")).Click();
            for (int second = 0;; second++) {
                if (second >= 60) throw new Exception ("timeout");
                try
                {
                    if (IsElementPresent(By.Id("sample-permalink"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).Click();
            Assert.Equal("szy 3", driver.FindElement(By.CssSelector("header.post-title > h1")).Text);
            Assert.Equal("notatka 5", driver.FindElement(By.CssSelector("div.post-entry > p")).Text);
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

            throw new NotImplementedException();
        }
    }
}
