using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunkcjaKwadratowaTest
{
    class Wynik
    {
        public double MiejsceDrugie { get; internal set; }
        public double MiejscePierwsze { get; internal set; }

        public void Oblicz(double a, double b, double c)
        {
            double delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                var deltaSqrt = Math.Sqrt(delta);
                MiejscePierwsze = (-b - deltaSqrt) / (2 * a);
                MiejsceDrugie = (-b + deltaSqrt) / (2 * a);
            }
            else if (delta == 0)
            {
                MiejscePierwsze = MiejsceDrugie = -b / (2 * a);

            }
            else
            {
                MiejscePierwsze = double.NaN;
                MiejsceDrugie = double.NaN;
            }
        }
    }
}
