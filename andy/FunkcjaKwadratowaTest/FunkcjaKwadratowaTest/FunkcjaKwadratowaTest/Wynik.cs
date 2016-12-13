using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowaTest
{
    /// <summary>
    /// Funkcja kwadratowa, wyliczanie miejsc zerowych
    ///  
    /// y = ax2 + bx + c
    /// </summary>
   

    public class Testy
    {
        [Theory,
            InlineData(1,-4,3, new double[] { 3,1}),
            InlineData(9, -12, 4, new double[] { 2.0/3 }),
            InlineData(-6, 3, -1, new double[] { })
            ]
        public void FunkcjaKwadratowaTest(double a, double b, double c, double[] oczekiwane)
        {
            var wynik = new Implementacja().Calculate(a, b, c);
            Assert.Equal(oczekiwane, wynik);
        }

        [Fact]
        public void WeryfikacjaDwaMiejscaZerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var rezultat = new Implementacja().Calculate(a, b, c).ToList();
            Assert.Equal(3, rezultat[0]);
            Assert.Equal(1, rezultat[1]);
        }

        [Fact]
        public void WeryfikacjaJednoMiejsceZerowe()
        {
            var a = 9;
            var b = -12;
            var c = 4;

            var  rezultat = new Implementacja().Calculate(a, b, c);
            Assert.Equal((double)2/3, rezultat.First());
        }

        [Fact]
        public void WeryfikacjaBrakMejscaZerowego()
        {
            var a = -6;
            var b = 3;
            var c = -1;

            var rezultat = new Implementacja().Calculate(a, b, c);
            Assert.Empty(rezultat);
        }

    }
}
