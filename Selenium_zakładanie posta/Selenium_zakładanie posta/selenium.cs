using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;
using System.Threading;
using OpenQA.Selenium.Chrome;

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
            driver = new ChromeDriver(); //FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds (10000));
            baseURL = "https://autotestdotnet.wordpress.com/wp-admin/";
            verificationErrors = new StringBuilder();
        }
        
        [Fact]
        public void The2DodaniePostuV2CsTest()
        {
            driver.Navigate().GoToUrl(baseURL + "");
            //Thread.Sleep(1000);
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("rememberme")).Click();
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Pan Tadeusz v7");
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("Litwo Ty moja Królowo...");
            driver.FindElement(By.Id("publish")).Click();
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
            driver.Navigate().GoToUrl("https://autotestdotnet.wordpress.com");
            Assert.Equal("Pan Tadeusz v7", driver.FindElement(By.LinkText("Pan Tadeusz v7")).Text);
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
