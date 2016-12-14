using System;

namespace SeleniumTests
{
    internal static class StronaLogowania
    {
        internal static void Otworz(string baseurl)
        {
            driver.Navigate().GoToUrl(baseurl);
        }

        internal static void Haslo(object haslo)
        {
            throw new NotImplementedException();
        }

        internal static void Uzytkownik(object nazwa)
        {
            throw new NotImplementedException();
        }

        internal static void Zaloguj()
        {
            throw new NotImplementedException();
        }
    }
}