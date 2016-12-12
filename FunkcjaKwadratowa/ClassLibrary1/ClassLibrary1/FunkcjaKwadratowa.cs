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
        public void Wartosc_Ujemna()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c);
            
            Assert.Equal(3, wynik.MiejscePierwsze);
            Assert.Equal(1, wynik.MiejsceDrugie);
            
        }

        private wynik Oblicz(int a, int b, int c)
        {
            double x1 = 0;
            double x2 = 0;

            // todo napisz obliczanie rozwiązań (miejsc zerowych) funkcji kwadratowej 
            // jeśli nie pamiętasz jak to się liczy to tutaj jest ściąga
            // http://matma.prv.pl/kwadratowa.php
            // postaraj się napisac to samodzielnie a nie googlując implementację
            // powodzenia :)
            double delta = 0;
            double p = 0;
            double q = 0;

            delta = (b * b) - (4 * a * c);
            p = -b / (2 * a);
            q = -delta / (4 * a);

            //if (delta > 0)
            //{
            //    MiejscePierwsze = (-b + Math.Sqrt(delta)) / (2 * a);
            //    MiejsceDrugie = (-b - Math.Sqrt(delta)) / (2 * a);

            //}
            //else
            //{
            //    MiejscePierwsze = -b / (2 * a);
            //    MiejsceDrugie = null;

            //}

            x1 = (-b + Math.Sqrt(delta)) / (2 * a);
            x2 = (-b - Math.Sqrt(delta)) / (2 * a);


            wynik Wynik = new wynik {MiejscePierwsze = x1, MiejsceDrugie = x2 };

            return Wynik;
        }
    }
}
