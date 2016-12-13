using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Automat
{

    public class AddNote : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;


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
            String PostTitle = "Demo6661";
            String PostBody = "Lubię placki 6661";
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

            //     driver.Navigate().GoToUrl(baseURL + "/wp-admin/post-new.php");
            //Thread.Sleep(3000);
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys(PostTitle);
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys(PostBody);
            WaitForClickable(By.Id("publish"), 5);
            driver.FindElement(By.Id("publish")).Click();
            WaitForElementExists(By.XPath("//span[@id='sample-permalink']/a"), 5);
            String link = driver.FindElement(By.XPath("//span[@id='sample-permalink']/a")).GetAttribute("href");
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            WaitForClickable(By.CssSelector("button.ab-sign-out"),5);
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
            driver.Navigate().GoToUrl(baseURL + link);
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
