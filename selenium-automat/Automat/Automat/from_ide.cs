using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{

    public static class WebElementExtensions
    {
        public static bool ElementIsPresent(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }






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

        protected void waitForElemantVisible(By by, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }








        //[Fact]
        //public void DodaniePosta()
        //{
        //    driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
        //    driver.FindElement(By.Id("user_login")).Clear();
        //    driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
        //    driver.FindElement(By.Id("user_pass")).Clear();
        //    driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
        //    driver.FindElement(By.Id("wp-submit")).Click();
        //    driver.FindElement(By.CssSelector("#menu-posts > a > div.wp-menu-name")).Click();
        //    driver.FindElement(By.LinkText("Add New")).Click();
        //    driver.FindElement(By.Id("title-prompt-text")).Click();
        //    driver.FindElement(By.Id("title")).Click();
        //    driver.FindElement(By.Id("title")).Clear();
        //    driver.FindElement(By.Id("title")).SendKeys("test w 4");
        //    driver.FindElement(By.Id("content")).Clear();
        //    driver.FindElement(By.Id("content")).SendKeys("test w 41");
        //    driver.FindElement(By.Id("publish")).Click();
        //    driver.Navigate().GoToUrl(baseURL + "/");
        //    var link = driver.FindElement(By.XPath("//a[contains(@href, 'test-w-4')]")).Text;
        //    Assert.Contains("test w 4", link);
        //    Console.WriteLine(link);
        //    driver.FindElement(By.Id("wp-admin-bar-my-account")).Click();
        //    driver.Navigate().GoToUrl(baseURL + "/");
        //    Assert.Equal("Site Title", driver.Title);
        //}

        [Fact]
        public void DodaniePostaPageObject()
        {
            //Test.Start();
            StronaLogowania.Otworz(driver);
            StronaLogowania.Uzytkownik(PoprawnyUzytkownik.Nazwa, driver);
            StronaLogowania.Haslo(PoprawnyUzytkownik.Haslo, driver);
            StronaLogowania.Zaloguj(driver);
            WordPress.Wyloguj(driver);

        }


        //[Fact]
        //public void UsunieciePosta()
        //{
        //    driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
        //    waitForElemantClicable(By.Id("user_login"), 5);
        //    driver.FindElement(By.Id("user_login")).Clear();
        //    driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
        //    driver.FindElement(By.Id("user_pass")).Clear();
        //    driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
        //    driver.FindElement(By.Id("wp-submit")).Click();

        //    driver.FindElement(By.CssSelector("#menu-posts > a > div.wp-menu-name")).Click();
        //    driver.FindElement(By.LinkText("Add New")).Click();
        //    driver.FindElement(By.Id("title-prompt-text")).Click();
        //    driver.FindElement(By.Id("title")).Click();
        //    driver.FindElement(By.Id("title")).Clear();
        //    var guid = Guid.NewGuid().ToString();
        //    driver.FindElement(By.Id("title")).SendKeys(guid);
        //    driver.FindElement(By.Id("content")).Clear();
        //    driver.FindElement(By.Id("content")).SendKeys("test w 41");
        //    driver.FindElement(By.Id("publish")).Click();
        //    driver.Navigate().GoToUrl(baseURL + "/");

        //    driver.FindElement(By.Id("wp-admin-bar-my-account")).Click();
        //    driver.Navigate().GoToUrl(baseURL + "/");
        //    Assert.Equal("Site Title", driver.Title);

        //    driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
        //    driver.FindElement(By.Id("user_login")).Clear();
        //    driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
        //    driver.FindElement(By.Id("user_pass")).Clear();
        //    driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
        //    driver.FindElement(By.Id("wp-submit")).Click();
        //    driver.FindElement(By.CssSelector("#menu-posts > a > div.wp-menu-name")).Click();
        //    driver.FindElement(By.Id("post-search-input")).SendKeys(guid);
        //    driver.FindElement(By.Id("search-submit")).Click();
        //    //driver.FindElement(By.XPath("//*[contains(@class,'screen-reader-text') and .//text()='"+guid+"']"));
        //    driver.FindElement(By.Id("cb-select-all-1")).Click();
        //    //driver.FindElement(By.Id("bulk-action-selector-top")).Click();
        //    IWebElement sTag = driver.FindElement(By.Id("bulk-action-selector-top"));
        //    SelectElement selectTag = new OpenQA.Selenium.Support.UI.SelectElement(sTag);
        //    selectTag.SelectByText("Move to Trash");
        //    driver.FindElement(By.Id("post-search-input")).SendKeys(guid);
        //    driver.FindElement(By.Id("search-submit")).Click();
        //    var noposts = driver.FindElement(By.ClassName("colspanchange")).Text;
        //    Assert.Equal("No posts found.", noposts);
        //}


        //[Fact]
        //public void Czy_istnieja_dwie_strony_z_postami()
        //{
        //    driver.Navigate().GoToUrl(baseURL + "/");
        //    Assert.Equal(true, driver.ElementIsPresent(By.ClassName("nav-previous")));
        //    waitForElemantClicable(By.ClassName("nav-previous"), 5);
        //    driver.FindElement(By.ClassName("nav-previous")).Click();
        //    Assert.Equal(true, driver.ElementIsPresent(By.ClassName("nav-next")));
        //    var ilosc = driver.FindElements(By.ClassName("post-title")).Count;
        //    bool ilosc_el;
        //    if (ilosc > 0)
        //    {
        //        ilosc_el = true;
        //    }
        //    else
        //    {
        //        ilosc_el = false;
        //    }
        //    Assert.Equal(true, ilosc_el);
        //}



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






        internal class WordPress
        {
            internal static void Wyloguj(IWebDriver driver)
            {
                driver.FindElement(By.Id("wp-admin-bar-my-account")).Click();
            }
        }

        public class PoprawnyUzytkownik
        {
            public static string Nazwa = "autotestdotnet@gmail.com";

            public static string Haslo = "codesprinters2016";
        }


        static class StronaLogowania
        {
            private static string baseURL;

            internal static void Otworz(IWebDriver driver)
            {
                baseURL = "https://autotestdotnet.wordpress.com";
                driver.Navigate().GoToUrl(baseURL + "/wp-login.php");
            }

            internal static void Uzytkownik(string Nazwa, IWebDriver driver)
            {
                driver.FindElement(By.Id("user_login")).Clear();
                driver.FindElement(By.Id("user_login")).SendKeys(Nazwa);
            }

            internal static void Haslo(string Haslo, IWebDriver driver)
            {
                driver.FindElement(By.Id("user_pass")).Clear();
                driver.FindElement(By.Id("user_pass")).SendKeys(Haslo);
            }


            internal static void Zaloguj(IWebDriver driver)
            {
                driver.FindElement(By.Id("wp-submit")).Click();
            }


        }
    }
    }



