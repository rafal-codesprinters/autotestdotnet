using System;
using System.Text;
using OpenQA.Selenium;
using Xunit;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class Selenium
    {
        [Fact]
        public void Moge_opublikowac_notatke()
        {
            Test.Start();
            StronaLogowania.Otworz();
            StronaLogowania.Uzytkownik(PoprawnyUzytkownik.Nazwa);
            StronaLogowania.Haslo(PoprawnyUzytkownik.Haslo);
            StronaLogowania.Zaloguj();

            StronaAdministracyjna.Otworz();
            StronaAdministracyjna.OtworzDodanieNowegoPosta();
            StronaAdministracyjna.Wpis("jakis temat", "jakaś treść");
            StronaAdministracyjna.Opublikuj();

            WordPress.Wyloguj();
            Test.Koniec();
        }
    }

    internal class Test
    {
        internal static IWebDriver Driver;
        private static object verificationErrors;

        internal static void Koniec()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.Equal("", verificationErrors.ToString());
        }

        internal static void Start()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            verificationErrors = new StringBuilder();
            Driver.Manage()
                .Timeouts()
                .ImplicitlyWait(TimeSpan.FromSeconds(10));
        }
    }

    internal class PoprawnyUzytkownik
    {
        public static string Haslo => "codesprinters2016";
        public static string Nazwa => "autotestdotnet@gmail.com";
    }

    internal static class WordPress
    {
        internal static void Wyloguj()
        {
            Test.Driver.Navigate().GoToUrl(WordpressConfiguration.BaseUrl + "/wp-login.php?action=logout");
        }
    }

    internal class WordpressConfiguration
    {
        public static string BaseUrl => "https://autotestdotnet.wordpress.com/";
    }

    internal static class StronaLogowania
    {
        internal static void Haslo(string haslo)
        {
            var user_element = By.Id("user_pass");
            Test.Driver.FindElement(user_element).Clear();
            Test.Driver.FindElement(user_element).SendKeys(haslo);
        }

        internal static void Otworz()
        {
            Test.Driver.Navigate().GoToUrl(WordpressConfiguration.BaseUrl + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
        }

        internal static void Uzytkownik(string uzytkownik)
        {
            Test.Driver.FindElement(By.Id("user_login")).Clear();
            Test.Driver.FindElement(By.Id("user_login")).SendKeys(uzytkownik);
        }

        internal static void Zaloguj()
        {
            Test.Driver.FindElement(By.Id("wp-submit")).Click();
        }
    }

    internal static class StronaAdministracyjna
    {
        internal static void Opublikuj()
        {
            Test.Driver.FindElement(By.Id("publish")).Click();
        }

        internal static void Otworz()
        {
            Test.Driver.FindElement(By.LinkText("Posts")).Click();
        }

        internal static void OtworzDodanieNowegoPosta()
        {
            Test.Driver.FindElement(By.LinkText("Add New")).Click();
        }


        internal static void Wpis(string temat, string tresc)
        {
            Test.Driver.FindElement(By.Id("title")).Clear();
            Test.Driver.FindElement(By.Id("title")).SendKeys(temat);
            Test.Driver.FindElement(By.Id("content")).Clear();
            Test.Driver.FindElement(By.Id("content")).SendKeys(tresc);
        }
    }
}
