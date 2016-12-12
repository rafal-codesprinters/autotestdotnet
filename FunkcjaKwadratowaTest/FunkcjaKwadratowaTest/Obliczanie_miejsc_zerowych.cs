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
        public void First_test()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c);
            Assert.Equal(1, wynik.MiejscePierwsze);
            Assert.Equal(3, wynik.MiejsceDrugie);
        }

        private Wynik Oblicz(int a, int b, int c)
        {
            //Wynik w = new Wynik();
            //w.Oblicz(a, b, c);
            //return w;
            double delta = b * b - 4 * a * c;
            var x1 = (-b - Math.Sqrt(delta)) / (2 * a);
            var x2 = (-b + Math.Sqrt(delta)) / (2 * a);
            return new Wynik { MiejscePierwsze = x1, MiejsceDrugie = x2 };
        }
    }
}
