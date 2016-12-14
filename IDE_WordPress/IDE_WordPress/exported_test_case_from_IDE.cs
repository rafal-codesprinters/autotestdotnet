using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace SeleniumTests
{
    public class ExportedTestCaseFromIDE : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private string tematNotatki;

        public ExportedTestCaseFromIDE()
        {
            driver = new ChromeDriver();
            baseURL = "https://autotestdotnet.wordpress.com/";
            verificationErrors = new StringBuilder();
            driver.Manage().Window.Maximize();
            tematNotatki = "Notatka ID:" + getGuid();
        }

        

        [Fact]
        public void Logging()
        {
            MetodaLogowania();
            waitForElementIsVisible(By.LinkText("My Site"), 10);
            Assert.Equal("My Site", driver.FindElement(By.LinkText("My Site")).Text);
            MetodaWylogowania();
        }

        [Fact]
        public void Logowanie_TworzenieNotatki_Wylogowanie_i_Sprawdzenie_Czy_Widoczna ()
        {
            MetodaLogowania();
            MetodaTworzenieNotatki();

            String postLink = driver.FindElement(By.CssSelector("#message>p>a")).GetAttribute("href"); //znalazienie i zapisanie linku do stworzonego posta

            MetodaWylogowania();
            driver.Navigate().GoToUrl(postLink);
            Assert.Equal(tematNotatki, driver.FindElement(By.CssSelector(".post-title>h1")).Text);
        }

        [Fact]
        public void TworzenieIUsuniecieNotatki ()
        {
            MetodaLogowania();
            MetodaTworzenieNotatki();
        driver.Navigate().GoToUrl("https://wordpress.com/posts/autotestdotnet.wordpress.com");
            waitForElementClickable(By.CssSelector("div.search__icon-navigation"), 10);

            driver.FindElement(By.CssSelector("div.search__icon-navigation")).Click();
            //driver.FindElement(By.CssSelector("#search-component-5")).Click();
        }

        [Fact]
        public void SprawdzenieCzyJestDrugaStrona()
        {
            driver.Navigate().GoToUrl("https://autotestdotnet.wordpress.com/");
            Assert.Equal("← Older posts", driver.FindElement(By.LinkText("← Older posts")).Text);
            String url1 = driver.Url;
            driver.FindElement(By.CssSelector(".nav-previous>a")).Click();
            String url2 = driver.Url;
            IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            Assert.NotEqual(url1, url2);
        }





        protected void waitForElementIsVisible(By by, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        protected void waitForElementClickable(By by, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        protected void MetodaLogowania ()
        {
        driver.Navigate().GoToUrl("https://autotestdotnet.wordpress.com/wp-admin/");
        Assert.Equal("Site Title ‹ Log In", driver.Title);
        driver.FindElement(By.Id("user_login")).Clear();
        driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
        driver.FindElement(By.Id("user_pass")).Clear();
        driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
        driver.FindElement(By.Id("wp-submit")).Click();
        }

        protected void MetodaTworzenieNotatki()
        {
            driver.Navigate().GoToUrl("https://autotestdotnet.wordpress.com/wp-admin/post-new.php");
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys(tematNotatki);
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("tresc 35");
            driver.FindElement(By.Id("publish")).Click();
            waitForElementClickable(By.Id("post-status-display"), 10);
            Assert.Equal("Published", driver.FindElement(By.Id("post-status-display")).Text);
        }

        protected void MetodaWylogowania()
        {
            driver.Navigate().GoToUrl("https://wordpress.com/me");
            waitForElementClickable(By.CssSelector(".button.me-sidebar__signout-button.is-compact"), 10);
            driver.FindElement(By.CssSelector(".button.me-sidebar__signout-button.is-compact")).Click();
        }

        protected string getGuid ()
        {
            var guid = Guid.NewGuid().ToString();
            return guid;
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
