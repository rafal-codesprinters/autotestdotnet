using System;
using System.Collections.Generic;
using System.Linq;
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
            Assert.Equal(new[] { 1d, 3d }, wynik);
        }

        [Fact]
        public void JednoMiejsceZerowe()
        {
            double a = 1, b = 0, c = 0;

            var wynik = Oblicz(a, b, c);
            Assert.Equal(new[] { 0d }, wynik);
        }

        [Fact]
        public void BrakMiejscZerowych()
        {
            double a = 1, b = 0, c = 5;

            var wynik = Oblicz(a, b, c);

            Assert.Empty(wynik);
        }

        private WynikOblicz Oblicz(double a, double b, double c)
        {
            var miejscaZerowe = new List<double>();
            var delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                miejscaZerowe.Add((-b - Math.Sqrt(delta)) / 2 * a);
                miejscaZerowe.Add((-b + Math.Sqrt(delta)) / 2 * a);
            }
            else if (Math.Abs(delta) < double.Epsilon)
                miejscaZerowe.Add(-b / 2 * a);
         
            return new WynikOblicz(miejscaZerowe.ToArray());
        }
    }
}
