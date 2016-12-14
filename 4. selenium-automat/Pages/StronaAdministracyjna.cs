using Automat.Infrastruktura;
using OpenQA.Selenium;

namespace Automat.Pages
{
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