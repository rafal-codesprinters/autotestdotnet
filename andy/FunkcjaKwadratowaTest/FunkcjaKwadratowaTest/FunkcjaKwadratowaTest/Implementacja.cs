﻿using System;
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
            double x1 = 0;
            double x2 = 0;
            double result = 0;

            double delta = 0; // pośrednie obliczenie
            delta = Math.Pow(b, 2) - (4 * a * c);

            if (delta > 0)
            {
                
                x1 = (Math.Sqrt(delta) - b) / (2 * a);
                x2 = (-Math.Sqrt(delta) - b) / (2 * a);

                yield return x1;
                yield return x2;

            }
            else
            {
                if (delta == 0)
                {

                    x1 = (Math.Sqrt(delta) - b) / (2 * a);
                    yield return x1;
                } 
                
            }
        }
    }
}
