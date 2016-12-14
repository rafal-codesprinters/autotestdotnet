using Automat.Infrastruktura;
using OpenQA.Selenium;

namespace Automat.Pages
{
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
}