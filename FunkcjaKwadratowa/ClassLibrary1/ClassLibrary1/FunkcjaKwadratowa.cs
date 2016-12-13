using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClassLibrary1
{
    public class Obliczanie_miejsc_zerowych
    {

        [Fact]
        public void Weryfikacja_ze_potrafimy_znalezc_dwa_miejsca_zerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c).ToList();
            
            Assert.Equal(3, wynik[0]);
            Assert.Equal(1, wynik[1]);
            
        }

        [Fact]
        public void Weryfikacja_ze_potrafimy_znalezc_jedno_miejsce_zerowe()
        {
            var a = 9;
            var b = -12;
            var c = 4;

            var wynik = Oblicz(a, b, c);
            double expected = (double)2 / 3;
            //double expected = 2.0 / 3; //mozna tez tak
            Assert.Equal(expected, wynik.First());

        }

        [Fact]
        public void Weryfikacja_ze_funkcja_nie_zwraca_miejsc_zerowych()
        {
            var a = -6;
            var b = 3;
            var c = -1;

            var wynik = Oblicz(a, b, c);
            //Assert.NotEmpty(wynik);
            var list = new List<int>();

            Assert.Collection(wynik, item => Assert.True(false));
            //Assert.Collection(item => Assert.True(false), wynik);

        }

        private IEnumerable<double> Oblicz(double a, double b, double c)
        {

            // todo napisz obliczanie rozwiązań (miejsc zerowych) funkcji kwadratowej 
            // jeśli nie pamiętasz jak to się liczy to tutaj jest ściąga
            // http://matma.prv.pl/kwadratowa.php
            double delta = 0;
            double p = 0;
            double q = 0;

            delta = (b * b) - (4 * a * c);
            p = -b / (2 * a);
            q = -delta / (4 * a);

            yield return (-b + Math.Sqrt(delta)) / (2 * a);
            yield return (-b - Math.Sqrt(delta)) / (2 * a);

        }
    }
}
