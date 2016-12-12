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

        }

        private static void Calculate(double a, double b, double c)
        {
            double x1 = 0;
            double x2 = 0;

            var delta = b * b - 4 * a * c;
            if (delta < 0)
            {
                x1 = double.NaN;
                x2 = double.NaN;
            }
            else if (Math.Abs(delta) < double.Epsilon)
            {
                x1 = x2 = -b / 2 * a;
            }
            else
            {
                x1 = (-b + delta) / 2 * a;
                x2 = (-b - delta) / 2 * a;
            }
           

            Console.WriteLine(x1);
            Console.WriteLine(x2);
        }
    }
}
