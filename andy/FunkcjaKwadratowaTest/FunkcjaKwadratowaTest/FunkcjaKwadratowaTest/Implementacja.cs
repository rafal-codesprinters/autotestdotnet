using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunkcjaKwadratowaTest
{
    class Implementacja
    {

        public IEnumerable<double> Calculate(double a, double b, double c)
        {

            double delta = Math.Pow(b, 2) - (4 * a * c);
            Func<double, double, double> x1 = (a1, b1) => (Math.Sqrt(delta) - b1) / (2 * a1);

            if (delta > 0)
            {

                double x2 = (-Math.Sqrt(delta) - b) / (2 * a);

                yield return x1(a, b);
                yield return x2;

            }
            else
            {
                if (delta == 0)
                {
                    yield return x1(a, b);
                }

            }
        }
    }
}
