using OpenQA.Selenium;

namespace Automat.Obiektowo.Infrastruktura
{
    internal static class WordPress
    {
        internal static void Wyloguj(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(WordpressConfiguration.BaseUrl + "/wp-login.php?action=logout");
        }
    }
}