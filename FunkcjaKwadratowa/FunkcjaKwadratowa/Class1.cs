using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowa
{
    public class FunkjaKwadratowa
    { 
        [Fact]
        public void Weyfikacja_dwa_miejsca_zerowe()
        {
            double a = 1;
            double b = -4;
            double c = 3;
            var wynik = oblicz(a, b, c).ToList();
            Assert.Equal(1, wynik[0]);
            Assert.Equal(3, wynik[1]);
        }
        [Fact]
        public void Weyfikacja_jedno_miejsce_zerowe()
        {
            double a = 9;
            double b = -12;
            double c = 4;
            var wynik = oblicz(a, b, c).ToList();
            Assert.Equal((double)2 / 3, wynik[0]);

        }
        [Theory]
        [InlineData(9, -12, 5, default(double))]
        public void NieMaMiejsc(double a, double b, double c, double first, double second)
        {
            var wynik = oblicz(a, b, c).ToList();
            Assert.Equal(default(double), wynik[0]);

        }

        private IEnumerable<double> oblicz(double a, double b, double c)
        {
            var delta = Math.Pow(b, 2) - 4 * a * c;

            if (delta < 0)
            {
                yield return default(double);
            }
            if (delta == 0)
            {
                yield return (double) (-1 * b) / (2 * a);
            }
            if (delta > 0)
            {
                yield return ((-1 * b) - Math.Sqrt(delta)) / 2 * a;
                yield return ((-1 * b) + Math.Sqrt(delta)) / 2 * a;

            }
        }
    }

}
