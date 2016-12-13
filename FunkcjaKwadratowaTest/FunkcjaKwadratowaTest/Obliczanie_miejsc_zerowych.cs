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
        [Theory,
            InlineData(1,-4,3, new double[] { 1, 3 }),
            //InlineData(9, -12, 4, new double[] { 2/3 }),
            InlineData(9, -12, 4, new double[] { 2.0/3 }),
            InlineData(-6, 3, -1, new double[] { })]
        public void FKwadratowa(double a, double b, double c, double[] oczekiwane)
        {
            var wynik = Oblicz(a, b, c).ToList();
            Assert.Equal(oczekiwane, wynik);
        }

        //[Fact]
        public void Weryfikacja_ze_potrafimy_znalezc_dwa_miejsca_zerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c).ToList();
            Assert.Equal(1, wynik[0]); //wynik.MiejscePierwsze
            Assert.Equal(3, wynik[1]); //wynik.MiejsceDrugie
        }

        //[Fact]
        public void Weryfikacja_ze_potrafimy_znalezc_jedno_miejsce_zerowe()
        {
            var a = 9;
            var b = -12;
            var c = 4;

            var wynik = Oblicz(a, b, c);
            Assert.Equal(2.0/3, wynik.First());
        }

        //[Fact]
        public void Weryfikacja_ze_nie_ma_miejsc_zerowych()
        {
            var a = -6;
            var b = 3;
            var c = -1;

            var wynik = Oblicz(a, b, c).ToList();
            Assert.Equal(0, wynik.Count);
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
            //Func<double, double, double> x1 =(a1, b1) => ((-b1 - Math.Sqrt(delta)) / (2 * a1));

            if (delta > 0)
            {
                yield return (-b - Math.Sqrt(delta)) / (2 * a);
                yield return (-b + Math.Sqrt(delta)) / (2 * a);
            }
            if(delta==0)
                yield return -b / (2 * a);
        }
    }
}
