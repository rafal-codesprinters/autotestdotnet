using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Xunit;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    //[TestFixture]
    public class Klik :IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        public Klik()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(5000));
            baseURL = "https://autotestdotnet.wordpress.com/wp-admin/";
            verificationErrors = new StringBuilder();

        }

        public void WaiteForElementClickable(By by, int seconds)
        {
            WebDriverWait waite = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            waite.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        
        [Fact]
        public void DodajUsunTest()
        {
            driver.Navigate().GoToUrl(baseURL);
           
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Click();

            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.CssSelector("a.page-title-action")).Click();
            driver.FindElement(By.Id("title")).Clear();

            var guid = Guid.NewGuid().ToString();
            driver.FindElement(By.Id("title")).SendKeys(guid);
          
            driver.FindElement(By.Id("content")).Click();
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("Testowy post dla");
       
            WaiteForElementClickable(By.XPath("//*[@id='sample-permalink']/a"),10);

            driver.FindElement(By.Id("publish")).Click();
            
            Assert.Equal("Published", driver.FindElement(By.Id("post-status-display")).Text);

            driver.FindElement(By.LinkText("View post")).Click();

           // Assert.Equal( guid, driver.FindElement(By.Id("post-title")));

            //wylogowywanie
            driver.Navigate().GoToUrl(baseURL);
           // WaiteForElementClickable(By.CssSelector("button.ab-sign-out"),10);

            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();

            driver.Navigate().GoToUrl(baseURL);
            WaiteForElementClickable(By.XPath(".//*[@id='wp-submit']"),10);
            //WaiteForElementClickable(By.Id("user_login"),10);
            driver.FindElement(By.Id("user_login")).Click();
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();

            //wyszukiwanie postu i usuwanie
            driver.FindElement(By.Id("post-search-input")).SendKeys(guid);
            driver.FindElement(By.Id("search-submit")).Click();
            driver.FindElement(By.Id("cb-select-all-1")).Click();
            driver.FindElement(By.XPath(".//*[@id='bulk-action-selector-top']")).Click();
            driver.FindElement(By.XPath(".//*[@id='bulk-action-selector-top']/option[3]")).Click();
            driver.FindElement(By.Id("doaction")).Click();

            Assert.Equal("No posts found.", driver.FindElement(By.XPath(".//*[@id='the-list']/tr/td")).Text);
            

        }

        [Fact]
        public void TheKlikTest()
        {
            driver.Navigate().GoToUrl(baseURL);

            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");

            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");

            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.CssSelector("a.page-title-action")).Click();

            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Nowy post ILO");

            driver.FindElement(By.Id("content")).Click();
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("Testowy post dla");

            WaiteForElementClickable(By.XPath("//*[@id='sample-permalink']/a"), 10);

            driver.FindElement(By.Id("publish")).Click();



            Assert.Equal("Published", driver.FindElement(By.Id("post-status-display")).Text);
            driver.FindElement(By.LinkText("View post")).Click();
            Assert.Equal("Nowy post ILO | Site Title", driver.Title);
        }



        [Fact]
        public void OlderPostTest()
        {
            driver.Navigate().GoToUrl("https://autotestdotnet.wordpress.com");

           //Assert.Equal(true, driver.FindElement(By.XPath(".//*[@id=\'nav-below\']/div[1]/a")).Text);

            driver.FindElement(By.XPath(".//*[@id=\'nav-below\']/div[1]/a")).Click();
            Assert.Equal("https://autotestdotnet.wordpress.com/page/2/",driver.Url);

            var ilosc = driver.FindElements(By.ClassName("post-title")).Count;
             bool ilosc_el;
            if (ilosc > 0)
            {
                ilosc_el = true;
            }
            else
            {
                ilosc_el = false;
            }
            Assert.Equal(true, ilosc_el);

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
