using System;
using System.Text;
using System.Threading;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
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
            //driver = new FirefoxDriver();
            baseURL = "https://autotestdotnet.wordpress.com/";
            verificationErrors = new StringBuilder();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(200));
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
        public void Publish_Note()
        {
            driver.Navigate().GoToUrl(baseURL + "wp-admin/"); //driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("rememberme")).Click();
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            driver.FindElement(By.Id("title-prompt-text")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("qqq235");
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("qwerty");
            driver.FindElement(By.Id("publish")).Click();
            driver.FindElement(By.LinkText("My Site")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60)
                    Assert.True(false, "timeout");
                try
                {
                    if (IsElementPresent(By.CssSelector("span.ab-site-title"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            driver.FindElement(By.CssSelector("span.ab-site-title")).Click();
            driver.FindElement(By.LinkText("qqq235")).Click();
            Assert.Equal("qqq235 | Site Title", driver.Title);
        }

        [Fact]
        public void Remove_Note()
        {
            var guid = Guid.NewGuid().ToString();
            var title = "qwerty-" + guid;
            driver.Navigate().GoToUrl(baseURL + "wp-admin/"); //driver.Navigate().GoToUrl(baseURL + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("rememberme")).Click();
            driver.FindElement(By.Id("wp-submit")).Click();
            WaitForElementClickable(By.XPath("//li[@id='menu-posts']/a/div[3]"), 1000);
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            driver.FindElement(By.Id("title-prompt-text")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys(title);
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("qwerty");
            driver.FindElement(By.Id("publish")).Click();
            driver.FindElement(By.LinkText("My Site")).Click();
            driver.FindElement(By.CssSelector("span.ab-site-title")).Click();
            driver.FindElement(By.LinkText(title)).Click();
            Assert.Equal(title + " | Site Title", driver.Title);

            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click(); // logout
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();

            // login
            driver.Navigate().GoToUrl(baseURL + "wp-admin/");
            WaitForElementVisible(By.Id("user_login"), 5000);
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("rememberme")).Click();
            driver.FindElement(By.Id("wp-submit")).Click();

            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.LinkText("All Posts")).Click();
            driver.FindElement(By.LinkText(title)).Click();
            driver.FindElement(By.LinkText("Move to Trash")).Click();

            driver.FindElement(By.LinkText("My Site")).Click();
            driver.FindElement(By.CssSelector("span.ab-site-title")).Click();
            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.LinkText(title)).Click());
        }

        [Fact]
        public void HasPages()
        {
            driver.Navigate().GoToUrl(baseURL);
            Assert.True(IsElementPresent(By.LinkText("← Older posts")));
        }

        [Fact]
        public void GotoOlderPosts()
        {
            driver.Navigate().GoToUrl(baseURL);
            //driver.FindElement(By.LinkText("← Older posts")).Click();
            driver.FindElement(By.XPath(@"//*[@id=""nav-below""]/div[@class=""nav-previous""]/a")).Click();
            Assert.True(driver.FindElements(By.XPath("//*[@id=\"content\"]/article")).Count > 0);
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

        /// <summary>
        /// wrapeer do oczekiwania na klikalność elementu
        /// </summary>
        /// <param name="by"></param>
        /// <param name="milisec"></param>
        private void WaitForElementClickable(By by, int milisec)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(milisec));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        private void WaitForElementVisible(By by, int milisec)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(milisec));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
    }
}
