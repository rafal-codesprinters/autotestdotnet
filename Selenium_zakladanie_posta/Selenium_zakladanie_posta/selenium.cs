using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;
using System.Threading;
using OpenQA.Selenium.Chrome;
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
            driver = new ChromeDriver(); //FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds (10000));
            baseURL = "https://autotestdotnet.wordpress.com/wp-admin/";
            verificationErrors = new StringBuilder();
        }
       
        [Fact]
        public void Dodanie_postu_sprawdzenie_czy_jest_dodany()
        {
            driver.Navigate().GoToUrl(baseURL + "");
            //Thread.Sleep(1000); - 
            driver.FindElement(By.Id("user_login")).Click();
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Click();
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("rememberme")).Click();
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Pan Tadeusz v7");
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys("Litwo Ty moja Królowo...");
            driver.FindElement(By.Id("publish")).Click();
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            Thread.Sleep(1000); //pauza miedzy poszczegolnymi krokami
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
            driver.Navigate().GoToUrl("https://autotestdotnet.wordpress.com");
            Assert.Equal("Pan Tadeusz v7", driver.FindElement(By.LinkText("Pan Tadeusz v7")).Text);
        }
        [Fact]
        public void Dodanie_postu_sprawdzenie_czy_jest_dodany_usuniecie_postu_sprawdzenie_ze_jest_usuniety()
        {
            var guid = Guid.NewGuid().ToString();
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("user_login")).Click();
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Click();
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("rememberme")).Click();
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.LinkText("Add New")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys(guid);
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys(guid);
            driver.FindElement(By.Id("publish")).Click();
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
            driver.Navigate().GoToUrl("https://autotestdotnet.wordpress.com");
            Assert.Equal(guid, driver.FindElement(By.LinkText(guid)).Text);
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("user_login")).Click();
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("autotestdotnet@gmail.com");
            driver.FindElement(By.Id("user_pass")).Click();
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("codesprinters2016");
            driver.FindElement(By.Id("rememberme")).Click();
            driver.FindElement(By.Id("wp-submit")).Click();
            driver.FindElement(By.XPath("//li[@id='menu-posts']/a/div[3]")).Click();
            driver.FindElement(By.Id("post-search-input")).Clear();
            driver.FindElement(By.Id("post-search-input")).SendKeys(guid);
            driver.FindElement(By.Id("search-submit")).Click();
            driver.FindElement(By.LinkText(guid)).Click();
            driver.FindElement(By.LinkText("Move to Trash")).Click();
            driver.FindElement(By.CssSelector("img.avatar.avatar-32")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button.ab-sign-out")).Click();
            driver.Navigate().GoToUrl("https://autotestdotnet.wordpress.com");
            driver.FindElement(By.Id("s")).Clear();
            driver.FindElement(By.Id("s")).SendKeys(guid);
            driver.FindElement(By.Name("search")).Click();
            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.LinkText(guid)).Click());
        }
        [Fact]
        public void Sprawdzenie_obecnosci_postow_na_drugiej_stronie()
        {
            driver.Navigate().GoToUrl("https://autotestdotnet.wordpress.com");
            var beforeUrl = driver.Url;
            driver.FindElement(By.XPath(@"//*[@id=""nav-below""]/div[@class=""nav-previous""]/a")).Click();
            var afterUrl = driver.Url;
            var articles = driver.FindElements(By.XPath(@"//*[@id=""content""]/article"));
            Assert.NotEmpty(articles);
            Assert.NotEqual(beforeUrl, afterUrl);
        }
        [Fact]
        public void Publikowanie_notatki_Funkcje()
        {
            //Test.Start();

            StronaLogowania.Otworz();
            StronaLogowania.Wprowadzenie_Uzytkownika();
            StronaLogowania.Wprowadzenie_Hasla();
            StronaLogowania.Odznacz_Zapamietaj_mnie();
            StronaLogowania.Przycisk_Zaloguj();

            StronaAdministracyjna.Lista_Postow();
            StronaAdministracyjna.Lista_postów_Nowy_Post();
            StronaAdministracyjna.Nowy_Post_Temat();
            StronaAdministracyjna.Nowy_Post_Tresc();
            StronaAdministracyjna.Nowy_Post_Publikuj();

            StronaLogowania.Wyloguj();

            StronaWordPress.Lista_Postow();

            //Test.Koniec();
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

    public class StronaWordPress
    {
        public static void Lista_Postow()
        {
            throw new NotImplementedException();
        }
    }

    public class StronaAdministracyjna
    {
        public static void Lista_Postow()
        {
            throw new NotImplementedException();
        }

        public static void Lista_postów_Nowy_Post()
        {
            throw new NotImplementedException();
        }

        public static void Nowy_Post_Publikuj()
        {
            throw new NotImplementedException();
        }

        public static void Nowy_Post_Temat()
        {
            throw new NotImplementedException();
        }

        public static void Nowy_Post_Tresc()
        {
            throw new NotImplementedException();
        }
    }

    public class StronaLogowania
    {
        public static void Odznacz_Zapamietaj_mnie()
        {
            driver.FindElement(By.Id("rememberme")).Click();
        }

        public static void Otworz()
        {
            throw new NotImplementedException();
        }

        public static void Przycisk_Zaloguj()
        {
            throw new NotImplementedException();
        }

        public static void Wprowadzenie_Hasla()
        {
            throw new NotImplementedException();
        }

        public static void Wprowadzenie_Uzytkownika()
        {
            throw new NotImplementedException();
        }

        public static void Wyloguj()
        {
            throw new NotImplementedException();
        }
    }
}
