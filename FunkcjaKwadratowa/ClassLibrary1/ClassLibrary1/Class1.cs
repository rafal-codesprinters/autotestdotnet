using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClassLibrary1
{
    public class Obliczanie_miejsc_zerowych   //w .NET to jest po to żeby 
    {
        [Fact]
        public void First_test()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c);

            Assert.Equal(3, wynik.MiejscePierwsze);
            Assert.Equal(1, wynik.MiejsceDrugie);

        }

        private Wynik Oblicz(int a, int b, int c)
        {
            double MiejscePierwsze = 0;
            double MiejsceDrugie = 0;

            var delta = (b * b) - (4 * a * c);

            if (delta > 0)
            {
                MiejscePierwsze = (-b + Math.Sqrt(delta)) / (2 * a);  //w poprzedniej wersji było źle bo nie było nawiasów, czy test to wykaże?
                MiejsceDrugie = (-b - Math.Sqrt(delta)) / (2 * a);

                return new Wynik
                {
                    MiejscePierwsze = MiejscePierwsze,
                    MiejsceDrugie = MiejsceDrugie
                };
            }
            else if (delta < 0)
            {
                return(null); //"brak miejsc zerowych"

            }
            else // delta = 0
            {
                MiejscePierwsze = MiejsceDrugie = -b / (2 * a);

                return new Wynik
                {
                    MiejscePierwsze = MiejscePierwsze,
                    MiejsceDrugie = MiejsceDrugie
                };
            }

        }
    }
}
