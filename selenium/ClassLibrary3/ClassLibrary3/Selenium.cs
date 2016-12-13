using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;
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
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            baseURL = "https://autotestdotnet.wordpress.com/";
            verificationErrors = new StringBuilder();
        }
        
        [Fact]
        public void TestCzyNowoUtworzonaNotatkaPublikujeSieNaStronie()
        {
            Logon();

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
            string myLink = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).Text;
            driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).Click();

            //wyloguj sie z konta admina
            driver.FindElement(By.Id("wp-admin-bar-my-account")).Click();
            driver.FindElement(By.ClassName("ab-sign-out")).Click();
            
            driver.Navigate().GoToUrl(myLink);
            Assert.Equal("szy 3", driver.FindElement(By.CssSelector("header.post-title > h1")).Text);
            Assert.Equal("notatka 5", driver.FindElement(By.CssSelector("div.post-entry > p")).Text);
        }

        [Fact]
        public void TestCzyNowoUtworzonaNotatkaMozeBycUsunieta()
        {
            Logon();

            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.CssSelector("a.page-title-action")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("szy 3");
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("notatka 5");
            driver.FindElement(By.Id("publish")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) throw new Exception("timeout");
                try
                {
                    if (IsElementPresent(By.Id("sample-permalink"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            string myLink = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).Text;
            driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).Click();

            //wyloguj sie z konta admina
            driver.FindElement(By.Id("wp-admin-bar-my-account")).Click();
            driver.FindElement(By.ClassName("ab-sign-out")).Click();

            driver.Navigate().GoToUrl(myLink);
            Assert.Equal("szy 3", driver.FindElement(By.CssSelector("header.post-title > h1")).Text);
            Assert.Equal("notatka 5", driver.FindElement(By.CssSelector("div.post-entry > p")).Text);
        }

        private void Logon()
        {
            driver.Navigate().GoToUrl(baseURL + "wp-admin/");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) throw new Exception("timeout");
                try
                {
                    if (IsElementPresent(By.Id("menu-posts"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(3000);
            }
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
