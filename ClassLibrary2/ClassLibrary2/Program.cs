using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowa
{
    public class Program
    {
       
        [Fact]
        public void CzyDobrzeLiczyMiejscaZerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c);


            Assert.Equal(3, wynik.MiejscePierwsze);
            Assert.Equal(1, wynik.MiejsceDrugie);
        }

        [Fact]
        public void CzyDobrzeLiczyJednoMiejsceZerowe()
        {
            var a = 1;
            var b = -4;
            var c = 4;

            var wynik = Oblicz(a, b, c);


            Assert.Equal(0, wynik.MiejscePierwsze);
            Assert.Equal(0, wynik.MiejsceDrugie);
            Assert.Equal(2, wynik.JednoMiejsce);
        }

        private Wynik Oblicz(int a, int b, int c)
        {
            double x1 = 0;
            double x2 = 0;

            double x0 = 0;
            string x = "";

            double delta = b * b - 4 * a * c;
            double pierwiastek = 0;

            if (delta > 0)
            {
                pierwiastek = Math.Sqrt(delta);
                x1 = (-b + pierwiastek) / (2 * a);
                x2 = (-b - pierwiastek) / (2 * a);
            }
            else if (delta == 0)
            {
                x0 = -b / (2 * a);
            }
            else
            {
                x = "brak";
            }

            return new Wynik
            {
                MiejscePierwsze = x1,
                MiejsceDrugie = x2,
                JednoMiejsce = x0,
                BrakMiejsca = x
            };
            

            throw new NotImplementedException();
        }
    }
}

