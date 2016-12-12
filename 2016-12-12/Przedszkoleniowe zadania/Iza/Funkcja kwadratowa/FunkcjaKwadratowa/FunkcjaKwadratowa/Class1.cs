using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowa
{
    public class ObliczanieMiejscZerowych
    {

        [Fact]
        public void DwaMiejscaZerowe()
        {
           // var a = 1;
            //var b = -4;
            //var c = 3;

            var wynik = Oblicz(1, -4, 3);
            Assert.Equal(3, wynik.MiejscePierwsze);
            Assert.Equal(1, wynik.MiejsceDrugie);
        }

        private wynik Oblicz(int a, int b, int c)
        {
            double delta;
            double x1;
            double x2;
            delta = (b * b) - (4 * a * c);

            if (delta > 0)
            {
               x1 = (-b + Math.Sqrt(delta)) / (2 * a);

               x2 = (-b - Math.Sqrt(delta)) / (2 * a);

                return new wynik
                {
                    MiejscePierwsze = x1,
                    MiejsceDrugie = x2
                };
            }


            return new wynik();

        }

    }
     

    internal class wynik
    {
    public double MiejsceDrugie { get; internal set; }
    public double MiejscePierwsze { get; internal set; }
    }
}
