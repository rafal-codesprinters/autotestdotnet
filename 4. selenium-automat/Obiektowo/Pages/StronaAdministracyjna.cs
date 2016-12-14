using OpenQA.Selenium;

namespace Automat.Obiektowo.Pages
{
    internal static class StronaAdministracyjna
    {
        internal static void Opublikuj(IWebDriver driver)
        {
            driver.FindElement(By.Id("publish")).Click();
        }

        internal static void Otworz(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Posts")).Click();
        }

        internal static void OtworzDodanieNowegoPosta(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Add New")).Click();
        }


        internal static void Wpis(IWebDriver driver,string temat, string tresc)
        {
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys(temat);
            driver.FindElement(By.Id("content")).Clear();
            driver.FindElement(By.Id("content")).SendKeys(tresc);
        }
    }
}