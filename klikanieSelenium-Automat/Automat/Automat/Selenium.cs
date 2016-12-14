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
    public class Selenium:IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        public Selenium()
        {
            //driver = new FirefoxDriver();
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            baseURL = "https://autotestdotnet.wordpress.com/";
            verificationErrors = new StringBuilder();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); //czekanie!!!
        }
        protected void waitForElementClickable(By by, int seconds)//czekanie!!!
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
                       
        [Fact]
        public void TheSeleniumTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
            Assert.Equal("Site Title ‹ Log In", driver.Title);

            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");

            waitForElementClickable(By.Id("user_login"), 10); //wykorzystanie waita zaimplementowanego przez nas

            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");

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

            String linkDoOpublikowanejStrony = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).GetAttribute("href");


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
        [Fact]
        public void UsuwanieJednejNotkiJakoAdminWSelenium()
        {
            ///Dodawanie
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
            Assert.Equal("Site Title ‹ Log In", driver.Title);

            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");

            waitForElementClickable(By.Id("user_login"), 10); //wykorzystanie waita zaimplementowanego przez nas

            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");

            driver.FindElement(By.Id("wp-submit")).Click();
            Assert.Equal("Dashboard ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            Assert.Equal("Posts ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.CssSelector("a.page-title-action")).Click();
            Assert.Equal("Add New Post ‹ Site Title — WordPress", driver.Title);
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Krzysztof Lubartowski");
            driver.FindElement(By.Id("publish")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) throw new Exception("timeout");
                try
                {
                    if (IsElementPresent(By.XPath("//span[@id='sample-permalink']/a"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            String linkDoOpublikowanejStrony = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).GetAttribute("href");


            for (int second = 0; ; second++)
            {
                if (second >= 60) throw new Exception("timeout");
                try
                {
                    if (IsElementPresent(By.Id("post-status-display"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            Assert.Equal("Published", driver.FindElement(By.Id("post-status-display")).Text);
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
            driver.Navigate().GoToUrl(baseURL + linkDoOpublikowanejStrony);
            Assert.Equal("Krzysztof Lubartowski | Site Title", driver.Title);
            ///Usuwanie
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
            Assert.Equal("Site Title ‹ Log In", driver.Title);

            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");

            waitForElementClickable(By.Id("user_login"), 10); //wykorzystanie waita zaimplementowanego przez nas

            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");

            driver.FindElement(By.Id("wp-submit")).Click();
            Assert.Equal("Dashboard ‹ Site Title — WordPress", driver.Title);


            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            Assert.Equal("Posts ‹ Site Title — WordPress", driver.Title);
            
                driver.FindElement(By.Id("post-search-input")).Clear();
                driver.FindElement(By.Id("post-search-input")).SendKeys("Lubartowski");
                driver.FindElement(By.Id("search-submit")).Click();


                Assert.Equal("Posts Add New Search results for “Lubartowski”", driver.FindElement(By.CssSelector("h1")).Text);
                driver.FindElement(By.LinkText("Krzysztof Lubartowski")).Click();

                Assert.Equal("Edit Post Add New", driver.FindElement(By.CssSelector("h1")).Text);
                driver.FindElement(By.LinkText("Move to Trash")).Click();
        }
        /*[Fact]
        public void UsuwanieWsyzstkichNotatekJakoAdminWSelenium()
        {
            driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
            Assert.Equal("Site Title ‹ Log In", driver.Title);

            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");

            waitForElementClickable(By.Id("user_login"), 10); //wykorzystanie waita zaimplementowanego przez nas

            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");

            driver.FindElement(By.Id("wp-submit")).Click();
            Assert.Equal("Dashboard ‹ Site Title — WordPress", driver.Title);

            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.Id("post-search-input")).Clear();
            driver.FindElement(By.Id("post-search-input")).SendKeys("Lubartowski");
            driver.FindElement(By.Id("search-submit")).Click();
            driver.FindElement(By.Id("cb-select-all-1")).Click();
            new SelectElement(driver.FindElement(By.Id("bulk-action-selector-top"))).SelectByText("Move to Trash");
            driver.FindElement(By.Id("doaction")).Click();
            Assert.Equal("Congratulations", driver.FindElement(By.CssSelector("p > strong")).Text);
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
        }*/
        [Fact]
        public void SprawdzenieDrugiejStrony()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            Assert.Equal("Site Title", driver.Title);
            driver.FindElement(By.LinkText("← Older posts")).Click();

            Assert.Equal("Leave a comment", driver.FindElement(By.LinkText("Leave a comment")).Text);
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
