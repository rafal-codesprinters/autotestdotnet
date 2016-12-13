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

        //ZAsianie danych do testow
        [Theory,
            InlineData(1, -4, 3, new double[] { 1, 3 }),
            InlineData(9, -12, 4, new double[] { 2.0 / 3 }),
            InlineData(-6, 3, -1, new double[] { })]
        //Koniec zasianych danych
        public void FKwadratowa(double a, double b, double c, double[] oczekiwane)
        {
            var wynik = Oblicz(a, b, c);
            Assert.Equal(oczekiwane, wynik);
        }

        [Fact]
        public void Potrafimy_znalesc_dwa_miejsca_zerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;
            var wynik = Oblicz(a, b, c).ToList();
            Assert.Equal(1, wynik[0]);
            Assert.Equal(3, wynik[1]);
        }
        [Fact]
        public void Potrafimy_znalesc_jedno_miejsce_zerowe()
        {
            var a = 9;
            var b = -12;
            var c = 4;
            var wynik = Oblicz(a, b, c).ToList();
            Assert.Equal( (double) 2 / (double) 3, wynik[0]);
        }
        [Fact]
        public void Potrafimy_znalesc_zero_miejsc_zerowe()
        {
            var a = -4;
            var b = 2;
            var c = -5;
            var wynik = Oblicz(a, b, c).ToList();
            Assert.Empty(wynik);
        }

        private IEnumerable<double> Oblicz(double a, double b, double c)
        {
            var delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                yield return (-b - Math.Sqrt(delta)) / (2 * a);
                yield return (-b + Math.Sqrt(delta)) / (2 * a);
            }
            else if (delta == 0)
            {
                yield return (-b / (2 * a));
            }
        }
    }
}
