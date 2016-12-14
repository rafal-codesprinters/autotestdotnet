using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace Automat
{

    public class AddNote : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private String PostTitle = "Demo6661";
        private String PostBody = "Lubię placki 6661";

        public AddNote()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            baseURL = "https://autotestdotnet.wordpress.com/";
            verificationErrors = new StringBuilder();
        }
        private void WaitForClickable(By by, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
        private void WaitForElementExists(By by, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementExists(by));
        }
        [Fact]
        public void TheAddNoteTest()
        {

            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.LinkText("Log in")).Click();

            ///       Thread.Sleep(3000);
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Submit();


            driver.FindElement(By.ClassName("resource-post")).Click();
            String myWindowHandle = driver.WindowHandles[1];
            driver.SwitchTo().Window(myWindowHandle);

            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys(PostTitle);
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys(PostBody);
            WaitForClickable(By.Id("publish"), 10);
            driver.FindElement(By.Id("publish")).Click();


            WaitForElementExists(By.XPath("//span[@id='sample-permalink']/a"), 5);
            String link = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).Text;
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            WaitForClickable(By.CssSelector("button.ab-sign-out"), 5);
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();

            driver.Navigate().GoToUrl(link);

        }
        [Fact]
        public void TheRemoveNote()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.LinkText("Log in")).Click();

            ///       Thread.Sleep(3000);
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Submit();
            driver.FindElement(By.ClassName("resource-post")).Click();
            String myWindowHandle = driver.WindowHandles[1];
            driver.SwitchTo().Window(myWindowHandle);

            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys(PostTitle);
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys(PostBody);
            WaitForClickable(By.Id("publish"), 10);
            driver.FindElement(By.Id("publish")).Click();

            driver.FindElement(By.XPath("//li[@id='menu-posts']")).Click();
            driver.SwitchTo().Alert().Accept();
            driver.FindElement(By.Id("post-search-input")).Clear();
            driver.FindElement(By.Id("post-search-input")).SendKeys(PostTitle);
            driver.FindElement(By.Id("search-submit")).Click();
            driver.FindElement(By.Id("cb-select-all-1")).Click();
            new SelectElement(driver.FindElement(By.Id("bulk-action-selector-top"))).SelectByText("Move to Trash");
            driver.FindElement(By.Id("doaction")).Click();


        }
        [Fact]
        public void TheRemoveNotePerm()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.LinkText("Log in")).Click();
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("wp-submit")).Submit();

            driver.FindElement(By.ClassName("resource-post")).Click();
            String myWindowHandle = driver.WindowHandles[1];
            driver.SwitchTo().Window(myWindowHandle);

            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys(PostTitle);
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys(PostBody);
            WaitForClickable(By.Id("publish"), 10);
            driver.FindElement(By.Id("publish")).Click();


            driver.FindElement(By.XPath("//li[@id='menu-posts']")).Click();
            driver.SwitchTo().Alert().Accept();
            driver.FindElement(By.Id("post-search-input")).Clear();
            driver.FindElement(By.Id("post-search-input")).SendKeys(PostTitle);
            driver.FindElement(By.Id("search-submit")).Click();
            driver.FindElement(By.Id("cb-select-all-1")).Click();
            new SelectElement(driver.FindElement(By.Id("bulk-action-selector-top"))).SelectByText("Move to Trash");
            driver.FindElement(By.Id("doaction")).Click();


            driver.FindElement(By.XPath("//li[@id='menu-posts']")).Click();
            driver.FindElement(By.XPath("//li[@class='trash']")).Click();
            driver.FindElement(By.Id("post-search-input")).Clear();
            driver.FindElement(By.Id("post-search-input")).SendKeys(PostTitle);
            driver.FindElement(By.Id("search-submit")).Click();
            driver.FindElement(By.Id("cb-select-all-1")).Click();
            new SelectElement(driver.FindElement(By.Id("bulk-action-selector-top"))).SelectByText("Delete Permanently");
            driver.FindElement(By.Id("doaction")).Click();
        }
        [Fact]
        public void CheckPosts()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            WaitForClickable(By.XPath("//div[@class='nav-previous']/a"), 10);
            driver.FindElement(By.XPath("//div[@class='nav-previous']/a")).Click();

            var url= new Uri(driver.Url);
            Assert.Contains("page/", url.PathAndQuery);
            Regex r = new Regex(@"\d+\/+$");
            Assert.True(r.IsMatch(url.PathAndQuery));
            driver.FindElement(By.XPath("//div[@id='content']/article[1]"));
      

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
            //      Assert.AreEqual("", verificationErrors.ToString());
        }
    }
}
