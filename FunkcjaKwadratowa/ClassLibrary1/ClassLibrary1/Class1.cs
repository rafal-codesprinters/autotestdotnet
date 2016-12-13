using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClassLibrary1
{
    public class Obliczanie_miejsc_zerowych   //w .NET to jest po to żeby 
    {
        [Fact]
        public void Weryfikacja_ze_potrafimy_znalezc_dwa_miejsca_zerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c).ToList();

            Assert.Equal(1, wynik[1]);
            Assert.Equal(3, wynik[0]);

        }

        [Fact]
        public void Weryfikacja_ze_potrafimy_znalezc_jedno_miejsce_zerowe()
        {
            var a = 9;
            var b = -12;
            var c = 4;

            var wynik = Oblicz(a, b, c).ToList();

            Assert.Equal(2.0/3, wynik.First()); //2.0 bo zamieniane typy są

        }

        [Fact]
        public void Weryfikacja_ze_potrafimy_znalezc_brak_miejsc_zerowych()
        {
            var a = -6;
            var b = -3;
            var c = -1;

            var wynik = Oblicz(a, b, c).ToList();
            var delta = (b * b) - (4 * a * c);
            Assert.True(delta < 0);

        }



        private IEnumerable <double> Oblicz(double a, double b, double c)  //inumerable, nie tworzymy tablicy, inumerable ma numerator, nie określone jak kolekcja działa w środku, ma 3 metody, Current-aktualny element, MoveNext, Reset
        {
            var delta = (b * b) - (4 * a * c);
            yield return  (-b + Math.Sqrt(delta)) / (2 * a);
            yield return  (-b - Math.Sqrt(delta)) / (2 * a);


        }
    }
}
//stara wersja
//private Wynik Oblicz(int a, int b, int c)
//{
//    double MiejscePierwsze = 0;
//    double MiejsceDrugie = 0;

//    var delta = (b * b) - (4 * a * c);

//    if (delta > 0)
//    {
//        MiejscePierwsze = (-b + Math.Sqrt(delta)) / (2 * a);  //w poprzedniej wersji było źle bo nie było nawiasów, czy test to wykaże?
//        MiejsceDrugie = (-b - Math.Sqrt(delta)) / (2 * a);

//        return new Wynik
//        {
//            MiejscePierwsze = MiejscePierwsze,
//            MiejsceDrugie = MiejsceDrugie
//        };
//    }
//    else if (delta < 0)
//    {
//        return (null); //"brak miejsc zerowych"
//    }
//    else // delta = 0
//    {
//        MiejscePierwsze = MiejsceDrugie = -b / (2 * a);

//        return new Wynik
//        {
//            MiejscePierwsze = MiejscePierwsze,
//            MiejsceDrugie = MiejsceDrugie
//        };
//    }

//}
