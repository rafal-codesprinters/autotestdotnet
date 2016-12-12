using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowa
{
    public class Obliczanie_miejsc_zerowych
    {
        [Fact]
        public void TestDla2()
        {
            var a = 1;
            var b = -4;
            var c = 3;
            var wynik = Oblicz(a, b, c);
            Assert.Equal(1, wynik.MiejscePierwsze);
            Assert.Equal(3, wynik.MiejsceDrugie);
            Assert.Equal(2, wynik.IloscRozwiazan);
        }
        [Fact]
        public void TestDla1()
        {
            var a = 2;
            var b = 4;
            var c = 2;
            var wynik = Oblicz(a, b, c);
            Assert.Equal(-1, wynik.MiejscePierwsze);
            Assert.Equal(1, wynik.IloscRozwiazan);
        }
        [Fact]
        public void TestDla0()
        {
            var a = -4;
            var b = 2;
            var c = -5;
            var wynik = Oblicz(a, b, c);
            Assert.Equal(0, wynik.IloscRozwiazan);
        }

        private Wynik Oblicz(int a, int b, int c)
        {
            Wynik Obliczenie = new Wynik();
            var delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                Obliczenie.MiejscePierwsze = (int)(-b - (double)Math.Sqrt(delta)) / 2 * a;
                Obliczenie.MiejsceDrugie = (int)(-b + (double)Math.Sqrt(delta)) / 2 * a;
                Obliczenie.IloscRozwiazan = 2;
            }
            else if (delta == 0)
            {
                Obliczenie.MiejscePierwsze = -b / 2 * a;
                Obliczenie.IloscRozwiazan = 1;
            }
            else
            {
                Obliczenie.IloscRozwiazan = 0;
            }
            return Obliczenie;
            throw new NotImplementedException();
        }
    }
}
