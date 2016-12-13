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
        public void First_test()
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
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c).ToList();

            Assert.Equal(1, wynik[1]);
            Assert.Equal(3, wynik[0]);

        }

        private IEnumerable <double> Oblicz(double a, double b, double c)  //inumerable, nie tworzymy tablicy, inumerable ma numerator, nie określone jak kolekcja działa w środku, ma 3 metody, Current-aktualny element, MoveNext, Reset
        {
            var delta = (b * b) - (4 * a * c);
            yield return  (-b + Math.Sqrt(delta)) / (2 * a);
            yield return  (-b - Math.Sqrt(delta)) / (2 * a);


        }
    }
}
