using System;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            Say.Hello();
            Say.World();
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

    public class Say
    {
        public static void Hello()
        {
            Console.Write("Hello");
        }

        public static void World()
        {
            Console.Write("World");
        }
    }
}
