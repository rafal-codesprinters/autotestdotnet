using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FunkcjaKwadratowa
{

    public class Obliczanie_miejsc_zerowych
    {
        [Theory,
            InlineData(1, -4, 3, new double[] { 1, 3 }),
            InlineData(9, -12, 4, new double[] { 2.0 / 3 }),
            InlineData(9, -12, 4, new double[] { 2 / 3 }),
            InlineData(-6, 3, -1, new double[] { })]
        public void Funkcja_kwadratowa_zwraca_miejsca_zerowe(double a, double b, double c, double[] oczekiwane)
        {
            var wynik = Oblicz(a, b, c);

            Assert.Equal(oczekiwane, wynik);
        }

        [Fact]
        public void Weryfikacja_ze_potrafimy_znalzesc_dwa_miejsca_zerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c).ToList();

            Assert.Equal(1, wynik[0]);
            Assert.Equal(3, wynik[1]);
        }


        [Fact]
        public void Weryfikacja_ze_potrafimy_znalzesc_jedno_miejsce_zerowe()
        {
            var a = 9;
            var b = -12;
            var c = 4;

            var wynik = Oblicz(a, b, c);

            Assert.Equal(2.0 / 3, wynik.First());
        }

        [Fact]
        public void Weryfikacja_ze_funkcja_nie_zwraca_miejsc_zerowych()
        {
            var a = -6;
            var b = 3;
            var c = -1;

            var wynik = Oblicz(a, b, c);

            Assert.Empty(wynik);
        }

        private IEnumerable<double> Oblicz(double a, double b, double c)
        {
            var obl_delta = (b * b) - 4 * a * c;

            if (obl_delta >= 0)
            {
                yield return (-b - Math.Sqrt(obl_delta)) / (2 * a);
            }
            if (obl_delta > 0)
                yield return (-b + Math.Sqrt(obl_delta)) / (2 * a);
        }

    }
}
