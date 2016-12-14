using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Automat.Infrastruktura
{
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
}