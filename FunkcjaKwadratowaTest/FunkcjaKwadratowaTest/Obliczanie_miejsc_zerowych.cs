using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowaTest
{
    public class Obliczanie_miejsc_zerowych
    {
        [Fact]
        public void Weryfikacja_ze_potrafimy_znalezc_dwa_miejsca_zerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c).ToList();
            Assert.Equal(1, wynik[0]); //wynik.MiejscePierwsze
            Assert.Equal(3, wynik[1]); //wynik.MiejsceDrugie
        }

        [Fact]
        public void Weryfikacja_ze_potrafimy_znalezc_jedno_miejsce_zerowe()
        {
            var a = 9;
            var b = -12;
            var c = 4;

            var wynik = Oblicz(a, b, c);
            Assert.Equal(2.0/3, wynik.First());
        }

        private Wynik Oblicz_v1(int a, int b, int c)
        {
            double delta = b * b - 4 * a * c;
            var x1 = (-b - Math.Sqrt(delta)) / (2 * a);
            var x2 = (-b + Math.Sqrt(delta)) / (2 * a);
            return new Wynik { MiejscePierwsze = x1, MiejsceDrugie = x2 };
        }

        private IEnumerable<double> Oblicz(double a, double b, double c)
        {
            double delta = b * b - 4 * a * c;
            yield return (-b - Math.Sqrt(delta)) / (2 * a);
            yield return (-b + Math.Sqrt(delta)) / (2 * a);
        }
    }
}
