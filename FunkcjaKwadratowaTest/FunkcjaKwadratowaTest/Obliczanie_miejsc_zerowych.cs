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
        public void Potrafimy_znalezc_dwa_miejsca_zerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c);
            Assert.Equal(1, wynik.MiejscePierwsze);
            Assert.Equal(3, wynik.MiejsceDrugie);
        }
        [Fact]
        public void Potrafimy_znalezc_jedno_miejsca_zerowe()
        {
            var a = 9;
            var b = -12;
            var c = 4;

            var wynik = Oblicz(a, b, c);
            Assert.Equal(2.0/3, wynik.MiejscePierwsze);
            Assert.Equal(double.NaN, wynik.MiejsceDrugie);
        }
        [Fact]
        public void Funkcja_nie_zwraca_zadnych_miejsc_zerowych()
        {
            var a = -6;
            var b = 3;
            var c = -1;

            var wynik = Oblicz(a, b, c);
            Assert.Equal(double.NaN, wynik.MiejscePierwsze);
            Assert.Equal(double.NaN, wynik.MiejsceDrugie);
        }
        private Wynik Oblicz(int a, int b, int c)
        {
            double delta = b * b - 4 * a * c;
            double x1;
            double x2;

            if (delta >= 0)
            {
                x1 = (-b - Math.Sqrt(delta)) / (2 * a);
            }
            else
            {
                x1 = double.NaN;
            }
            if (delta > 0)
            {
                x2 = (-b + Math.Sqrt(delta)) / (2 * a);
            }
            else
            {
                x2 = double.NaN;
            }

            return new Wynik { MiejscePierwsze = x1, MiejsceDrugie = x2 };
        }
    }
}
