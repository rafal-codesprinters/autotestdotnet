using System;

namespace Zadanie3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("y = a * (x * x) + b  * y + c");
            Console.Write("podaj wartośc a = ");
            var a = Convert.ToDouble(Console.ReadLine());

            Console.Write("podaj wartośc b = ");
            var b = Convert.ToDouble(Console.ReadLine());

            Console.Write("podaj wartośc c = ");
            var c = Convert.ToDouble(Console.ReadLine());

            Calculate(a, b, c);

            Console.WriteLine("naciśnij jakikolwiek klawisz aby zakończyć");
            Console.ReadKey();

        }

        private static void Calculate(double a, double b, double c)
        {
            double x1 = 0;
            double x2 = 0;

            // todo napisz obliczanie rozwiązań (miejsc zerowych) funkcji kwadratowej 
            // jeśli nie pamiętasz jak to się liczy to tutaj jest ściąga
            // http://matma.prv.pl/kwadratowa.php
            // postaraj się napisac to samodzielnie a nie googlując implementację
            // powodzenia :)

            double delta = 0; // pośrednie obliczenie
            delta = Math.Pow(b, 2) - (4 * a* c);

            if (delta > 0)
            {

                

                x1 = (Math.Sqrt(delta) - b) / (2 * a);
                x2 = (-Math.Sqrt(delta) - b) / (2 * a);

                Console.WriteLine(delta);
                Console.WriteLine(x1);
                Console.WriteLine(x2);
            } else
            {
                Console.WriteLine("Ujemna delta ({0:G}) - x1 i x2 nie istnieją.", delta);
            }
        }
    }
}
