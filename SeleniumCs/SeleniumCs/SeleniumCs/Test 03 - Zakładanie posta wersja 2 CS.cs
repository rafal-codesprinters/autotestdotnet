using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public static class Extensions
    {
    }

    public class ObslugaPostow : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        public ObslugaPostow()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
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
        public void ZakladaniePosta()
        {
            Login();
            driver.FindElement(By.XPath(@"//li[@id='menu-posts']/a")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.Id("publish")));
            driver.FindElement(By.Id("title")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("test pg");
            driver.FindElement(By.Id("content")).Click();
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("test pg");
            driver.FindElement(By.Id("publish")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists(By.CssSelector("#message")));
            var linkToPost = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).GetAttribute("href");
            Logout();
            driver.Navigate().GoToUrl(baseURL + linkToPost);
            Assert.NotEqual("Page not found | Site Title", driver.Title);
        }

        [Fact]
        public void DodawanieIUsuwanie()
        {
            Login();
            driver.FindElement(By.XPath(@"//li[@id='menu-posts']/a")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.Id("publish")));
            driver.FindElement(By.Id("title")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("test pg");
            driver.FindElement(By.Id("content")).Click();
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("test pg");
            driver.FindElement(By.Id("publish")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists(By.CssSelector("#message")));
            var editUri = driver.Url;
            var linkToPost = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).GetAttribute("href");
            Logout();
            driver.Navigate().GoToUrl(baseURL + linkToPost);
            Assert.NotEqual("Page not found | Site Title", driver.Title);

            Login();
            driver.Navigate().GoToUrl(editUri);
            driver.FindElement(By.XPath(@"//*[@id=""delete-action""]/a")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists(By.CssSelector("#message")));
            driver.Navigate().GoToUrl(editUri);
            var element = driver.FindElement(By.Id("error-page")).Text;
            Assert.Equal("You can’t edit this item because it is in the Trash. Please restore it and try again.", element);


        }

        [Fact]
        public void Stronicowanie()
        {
            Login();
            for (var i = 0; i < 6; i++)
            {
                driver.FindElement(By.XPath(@"//li[@id='menu-posts']/a")).Click();
                driver.FindElement(By.LinkText("Add New")).Click();
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.Id("publish")));
                driver.FindElement(By.Id("title")).Click();
                driver.FindElement(By.Id("title")).Clear();
                driver.FindElement(By.Id("title")).SendKeys("test pg " + i);
                driver.FindElement(By.Id("content")).Click();
                driver.FindElement(By.Id("content")).Clear();
                driver.FindElement(By.Id("content")).SendKeys("test pg " + i);
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.Id("publish")));
                driver.FindElement(By.Id("publish")).Click();
                new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists(By.CssSelector("#message")));
            }
            Logout();
            driver.Navigate().GoToUrl(baseURL);
            var beforeUrl = driver.Url;
            driver.FindElement(By.XPath(@"//*[@id=""nav-below""]/div[@class=""nav-previous""]/a")).Click();
            var afterUrl = driver.Url;
            var articles = driver.FindElements(By.XPath(@"//*[@id=""content""]/article"));
            Assert.NotEmpty(articles);
            Assert.NotEqual(beforeUrl, afterUrl);
        }

        private void Login()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable((By.Id("wp-submit"))));
            driver.FindElement(By.Id("user_login")).Click();
            driver.FindElement(By.Id("user_login")).Clear();            
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Click();
            driver.FindElement(By.Id("user_pass")).Clear();            
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Click();
        }

        private void Logout()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath(@"//*[@id=""wp-admin-bar-my-account""]/a")));
            driver.FindElement(By.XPath(@"//*[@id=""wp-admin-bar-my-account""]/a")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable((By.CssSelector("button.ab-sign-out"))));
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
        }
    }
}
