using System;
using Xunit;

namespace FunkcjaKwadratowa
{
    public class ObliczanieMiejscZerowych
    {
        [Fact]
        public void DwaMiejscaZerowe()
        {
            double a = 1, b = -4, c = 3;

            var wynik = Oblicz(a, b, c);
            Assert.Contains(1d, new[] { wynik.X1, wynik.X2 });
            Assert.Contains(3d, new[] { wynik.X1, wynik.X2 });
        }

        [Fact]
        public void JednoMiejsceZerowe()
        {
            double a = 1, b = 0, c = 0;

            var wynik = Oblicz(a, b, c);

            Assert.Equal(wynik.X1, 0d);
            Assert.Equal(wynik.X2, 0d);
        }

        [Fact]
        public void BrakMiejscZerowych()
        {
            double a = 1, b = 0, c = 5;

            var wynik = Oblicz(a, b, c);

            Assert.Equal(wynik.X1, double.NaN);
            Assert.Equal(wynik.X2, double.NaN);
        }

        private WynikOblicz Oblicz(double a, double b, double c)
        {
            double x1, x2;

            var delta = b * b - 4 * a * c;
            if (delta < 0)
            {
                x1 = double.NaN;
                x2 = double.NaN;
            }
            else if (Math.Abs(delta) < double.Epsilon)
            {
                x1 = x2 = -b / 2 * a;
            }
            else
            {
                x1 = (-b + Math.Sqrt(delta)) / 2 * a;
                x2 = (-b - Math.Sqrt(delta)) / 2 * a;
            }
            return new WynikOblicz { X1 = x1, X2 = x2 };
        }
    }
}
