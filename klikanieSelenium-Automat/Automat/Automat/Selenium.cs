using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace SeleniumTests
{
    public class Selenium:IDisposable
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
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            Assert.Equal("Site Title ‹ Log In", driver.Title);
            driver.FindElement(By.Id("wp-submit")).Click();
            Assert.Equal("Dashboard ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            Assert.Equal("Posts ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.CssSelector("a.page-title-action")).Click();
            Assert.Equal("Add New Post ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Krzysztof Lubartowski");
            driver.FindElement(By.Id("publish")).Click();
            for (int second = 0;; second++) {
                if (second >= 60) throw new Exception("timeout");
                try
                {
                    if (IsElementPresent(By.XPath("//span[@id='sample-permalink']/a"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            String linkDoOpublikowanejStrony = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a/@href")).GetAttribute("value");
            for (int second = 0;; second++) {
                if (second >= 60) throw new Exception("timeout");
                try
                {
                    if (IsElementPresent(By.Id("post-status-display"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            Assert.Equal("Published", driver.FindElement(By.Id("post-status-display")).Text);
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
            driver.Navigate().GoToUrl(baseURL + linkDoOpublikowanejStrony);
            Assert.Equal("Krzysztof Lubartowski | Site Title", driver.Title);
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
        }
    }
}
