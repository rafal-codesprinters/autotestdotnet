using System;
using System.Text;
using Automat.Obiektowo.Infrastruktura;
using Automat.Obiektowo.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Automat.Obiektowo
{
    public class Selenium : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly StringBuilder _verificationErrors;

        public Selenium()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _verificationErrors = new StringBuilder();
            _driver.Manage()
                .Timeouts()
                .ImplicitlyWait(TimeSpan.FromSeconds(10));
        }
        [Fact]
        public void Moge_opublikowac_notatke()
        {
            StronaLogowania.Otworz(_driver);
            StronaLogowania.Uzytkownik(_driver, PoprawnyUzytkownik.Nazwa);
            StronaLogowania.Haslo(_driver, PoprawnyUzytkownik.Haslo);
           StronaLogowania.Zaloguj(_driver);

            StronaAdministracyjna.Otworz(_driver);
            StronaAdministracyjna.OtworzDodanieNowegoPosta(_driver);
            StronaAdministracyjna.Wpis(_driver, "jakis temat", "jakaś treść");
            StronaAdministracyjna.Opublikuj(_driver);

            WordPress.Wyloguj(_driver);
        }

        public void Dispose()
        {
            try
            {
                _driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.Equal("", _verificationErrors.ToString());
        }
    }
}
