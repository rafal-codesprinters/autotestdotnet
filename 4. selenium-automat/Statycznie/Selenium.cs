using Automat.Statycznie.Infrastruktura;
using Automat.Statycznie.Pages;
using Xunit;

namespace Automat.Statycznie
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
}
