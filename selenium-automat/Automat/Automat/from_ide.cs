using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

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
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            baseURL = "https://autotestdotnet.wordpress.com";
            verificationErrors = new StringBuilder();



        }


        protected void waitForElemantClicable(By by, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
        [Fact]
        public void DodaniePosta()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.CssSelector("#menu-posts > a > div.wp-menu-name")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            driver.FindElement(By.Id("title-prompt-text")).Click();
            driver.FindElement(By.Id("title")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("test w 4");
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("test w 41");
            driver.FindElement(By.Id("publish")).Click();
            driver.Navigate().GoToUrl(baseURL + "/");
            //driver.FindElement(By.XPath("//*[contains(@class,'post-title') and .//text()='test w 4']"));
            var link = driver.FindElement(By.XPath("//a[contains(@href, 'test-w-4')]")).Text;
            Assert.Contains("test w 4", link);
            Console.WriteLine(link);
            //Assert.Equal("test w 4", driver.FindElement(By.XPath("//*[contains(@class,'post-title') and .//text()='test w 4']")).ToString());
            driver.FindElement(By.Id("wp-admin-bar-my-account")).Click();
            driver.Navigate().GoToUrl(baseURL + "/");
            Assert.Equal("Site Title", driver.Title);
        }

        [Fact]
        public void UsunieciePosta()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
            waitForElemantClicable(By.Id("user_login"), 5);
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Click();

            driver.FindElement(By.CssSelector("#menu-posts > a > div.wp-menu-name")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            driver.FindElement(By.Id("title-prompt-text")).Click();
            driver.FindElement(By.Id("title")).Click();
            driver.FindElement(By.Id("title")).Clear();
            var guid = Guid.NewGuid().ToString();
            driver.FindElement(By.Id("title")).SendKeys(guid);
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("test w 41");
            driver.FindElement(By.Id("publish")).Click();
            driver.Navigate().GoToUrl(baseURL + "/");

            driver.FindElement(By.Id("wp-admin-bar-my-account")).Click();
            driver.Navigate().GoToUrl(baseURL + "/");
            Assert.Equal("Site Title", driver.Title);

            driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.CssSelector("#menu-posts > a > div.wp-menu-name")).Click();
            driver.FindElement(By.Id("post-search-input")).SendKeys(guid);
            driver.FindElement(By.Id("search-submit")).Click();
            //driver.FindElement(By.XPath("//*[contains(@class,'screen-reader-text') and .//text()='"+guid+"']"));
            driver.FindElement(By.Id("cb-select-all-1")).Click();











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

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
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


        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Selenium() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.

    }
}
