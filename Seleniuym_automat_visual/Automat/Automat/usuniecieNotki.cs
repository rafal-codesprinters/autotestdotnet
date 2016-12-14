using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class UsuniecieNotki : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        public UsuniecieNotki()
        {

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            baseURL = "https://autotestdotnet.wordpress.com/";
            verificationErrors = new StringBuilder();
            driver.Manage()
                .Timeouts()
                .ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void TheUsuniecieNotkiTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Click();
            Assert.Equal("Dashboard ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.LinkText("All Posts")).Click();
            driver.FindElement(By.Id("post-search-input")).Clear();
            driver.FindElement(By.Id("post-search-input")).SendKeys("Test_MP");
            driver.FindElement(By.Id("search-submit")).Click();
            Assert.Equal("Posts ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.LinkText("Trash")).Click();
            Assert.Equal("Posts ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
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
            throw new NotImplementedException();
        }
    }
}
