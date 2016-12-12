using System;
using Xunit;

namespace FunkcjaKwadratowa
{
    public class Obliczanie_miejsc_zerowych
    {
        [Fact]
        public void TODO_nadac_nazwe()
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
            var obl_delta = (b * b) - 4 * a * c;

            var x1 = (-b - (int)Math.Sqrt(obl_delta)) / (2 * a);
            var x2 = (-b + (int)Math.Sqrt(obl_delta)) / (2 * a);
            return new Wynik
            {
                MiejscePierwsze = x1,
                MiejsceDrugie = x2
            };
        }
    }
}
