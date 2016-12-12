using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Zadanie1
{
    class Program
    {
        static void Main(string[] args)
        {
            var hello = new SayHello();
            hello.Say();

            Bravo();
        }

        private static void Bravo()
        {
            Console.WriteLine("");
            Console.WriteLine("Brawo! pierwsze zadanie zrobione :)");
            Console.WriteLine("");
            Console.WriteLine("naciśnij jakikolwiek klawisz aby zakończyć");
            Console.ReadKey();
        }
    }

    public class SayHello
    {
        public void Say()
        {
            Console.WriteLine("Hello world");
          
        }
    }
}
