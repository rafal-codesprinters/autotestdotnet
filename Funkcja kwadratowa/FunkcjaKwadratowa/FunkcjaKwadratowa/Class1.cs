using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowa
{
    public class Obliczenie_miejsc_zerwych
    {

        [Fact]
        public void Weryfikacja_ze_otrafimy_znalesc_dwa_miejsca_zerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c).ToList();
            Assert.Equal(1, wynik[0]);
            Assert.Equal(3, wynik[1]);
        }

        [Fact]
        public void Weryfikacja_ze_potrafimy_znalesc_jedno_miejsca_zerowe()
        {
            var a = 9;
            var b = -12;
            var c = 4;

            var wynik = Oblicz(a, b, c);
            Assert.Equal(2.0 / 3, wynik.First()); //pierwszy sposob
            Assert.Equal((double)2 / 3, wynik.First()); //drugi sposob

        }

        [Fact]
        public void Weryfikacja_ze_funkcja_nie_zwraca_miejsc_zerowych()
        {
            var a = -6;
            var b = 3;
            var c = -1;

            var wynik = Oblicz(a, b, c);
            Assert.Empty(wynik);



        }

        private IEnumerable<double> Oblicz(double a, double b, double c)
        {
            double delta;
            delta = (b * b) - (4 * a * c);

            if (delta > 0)
            {
                yield return (-b - Math.Sqrt(delta)) / (2 * a);
                yield return (-b + Math.Sqrt(delta)) / (2 * a);
            }
            if (delta == 0)
            {
                yield return (-b - Math.Sqrt(delta)) / (2 * a);
            }


        }

    }


}
