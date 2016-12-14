using Automat.Obiektowo.Infrastruktura;
using OpenQA.Selenium;

namespace Automat.Obiektowo.Pages
{
    internal static class StronaLogowania
    {
        internal static void Haslo(IWebDriver driver,string haslo)
        {
            var user_element = By.Id("user_pass");
            driver.FindElement(user_element).SendKeys(haslo);
            driver.FindElement(user_element).Clear();
        }

        internal static void Otworz(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(WordpressConfiguration.BaseUrl + "/wp-login.php?redirect_to=https%3A%2F%2Fautotestdotnet.wordpress.com%2Fwp-admin%2F&reauth=1");
        }

        internal static void Uzytkownik(IWebDriver driver, string uzytkownik)
        {
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys(uzytkownik);
        }

        internal static void Zaloguj(IWebDriver driver)
        {
            driver.FindElement(By.Id("wp-submit")).Click();
        }
    }
}