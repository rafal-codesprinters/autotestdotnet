using System;

namespace Zadanie2
{
    public class Say
    {
        public void Hello()
        {
            Console.Write("Hello");
        }

        public void World()
        {
            Console.Write("World");
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            var Say1 = new Say();
            Say1.Hello();
            Say1.World();
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

}