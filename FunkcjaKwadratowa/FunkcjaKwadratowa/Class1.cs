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

        [Theory]
        [InlineData(9, -12, 5,new double[] {  })]
        [InlineData(1, -4, 3, new double[] { 1, 3 })]
        [InlineData(9, -12, 4, new double[] { (double)2 / 3 })]
        public void NieMaMiejsc(double a, double b, double c,double[] result)
        {
            var wynik = oblicz(a, b, c).ToList();
            Assert.Equal(result,wynik);

        }

        private IEnumerable<double> oblicz(double a, double b, double c)
        {
            var delta = Math.Pow(b, 2) - 4 * a * c;

            if (delta == 0)
            {
                yield return (double)(-1 * b) / (2 * a);
            }
            if (delta > 0)
            {
                yield return ((-1 * b) - Math.Sqrt(delta)) / 2 * a;
                yield return ((-1 * b) + Math.Sqrt(delta)) / 2 * a;

            }

        }
    }

}
