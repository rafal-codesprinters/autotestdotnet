using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace SeleniumTests
{
        public class ZSelenium : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private WebDriverWait wait;

        public ZSelenium()
        {

            driver = new ChromeDriver();
            baseURL = "https://autotestdotnet.wordpress.com/";
            verificationErrors = new StringBuilder();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage()
                .Timeouts()
                .ImplicitlyWait(TimeSpan.FromSeconds(5));
        }
    
        
        [Fact]
        public void Dodawanie_notatki()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("user_pass")).Click();
            driver.FindElement(By.Id("wp-submit")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menu-posts']/a/div[3]")));
            driver.FindElement(By.XPath("//*[@id='menu-posts']/a/div[3]")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@id='menu-posts']/a/div[3]")));
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.CssSelector("a.page-title-action")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Olaboga");
            driver.FindElement(By.Id("content")).Click();
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("COś tu nie jest halo");
            Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("publish")));
            driver.FindElement(By.Id("publish")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='message']/p/a")));
            driver.FindElement(By.XPath("//*[@id='message']/p/a")).Click();
        }

        [Fact]
        public void Usuwanie_notatki()
        {
            var guid = Guid.NewGuid().ToString();

            driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("user_pass")).Click();
            driver.FindElement(By.Id("wp-submit")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menu-posts']/a/div[3]")));
            driver.FindElement(By.XPath("//*[@id='menu-posts']/a/div[3]")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@id='menu-posts']/a/div[3]")));
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.CssSelector("a.page-title-action")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys(guid);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("content")));
            driver.FindElement(By.Id("content")).Click();
            driver.FindElement(By.Id("content")).SendKeys(guid);
            Thread.Sleep(4000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("publish")));
            driver.FindElement(By.Id("publish")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menu-posts']/a/div[3]")));
            driver.FindElement(By.XPath("//*[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.Id("post-search-input")).SendKeys(guid + Keys.Enter);
            driver.FindElement(By.XPath("//tr/td[1]/strong/a")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='delete-action']/a")));
            driver.FindElement(By.XPath("//*[@id='delete-action']/a")).Click();
            driver.FindElement(By.Id("post-search-input")).SendKeys(guid + Keys.Enter);
            //Assert.True((driver.FindElement(By.XPath("//*[@id='posts-filter']/div[2]/div[2]/span[1]")).Text) = "0 items");
            //Assert.False(driver.FindElement(By.XPath("//tr/td[1]/strong/a")));

        }

        [Fact]
        public void Weryfikacja_ilosci_stron()
        {
            var guid = Guid.NewGuid().ToString();

            driver.Navigate().GoToUrl(baseURL);
            Assert.Contains("Older",driver.FindElement(By.XPath("//*[@id='nav-below']/div[1]/a")).Text);
            
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
            //throw new NotImplementedException();
        }
    }
}
