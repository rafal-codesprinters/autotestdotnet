using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowa
{
    public class ObliczanieMiejscZerowych
    {

        [Fact]
        // Weryfikacja ze potrafimy znalezc dwa miejsca zerowe
        public void DwaMiejscaZerowe()
        {
           var a = 1;
           var b = -4;
           var c = 3;

            var wynik = Oblicz(a, b, c).ToList();
            Assert.Equal(3, wynik[0]);
            Assert.Equal(1, wynik[1]);
        }

        [Fact]
        
        //Weryfikacja ze potrafimy znalezc jedno miejsce zerowe
        public void JednoMiejsce()
        {
            var a =9;
            var b =-12;
            var c =4;

            var wynik = Oblicz(a, b, c);
            Assert.Equal(2.0/3, wynik.First());
            
        }

        private IEnumerable<double> Oblicz(double a, double b, double c)
        {
            double delta;
            delta = (b * b) - (4 * a * c);

            if (delta >0)
            {

                yield return (-b + Math.Sqrt(delta))/(2*a);

                yield return (-b - Math.Sqrt(delta))/(2*a);
            }
            if (delta == 0)
            {
                yield return (-b + Math.Sqrt(delta)) / (2 * a);
            }
        
        }
        [Fact]
        // Weryfikacja ze nie ma miejsc zerowych
        public void BrakMiejsc()
        {
            var a = -6;
            var b = 3;
            var c = -1;

            var wynik = Oblicz(a, b, c);
            Assert.Empty(wynik);


        }
    }

  


    internal class wynik
    {
    public double MiejsceDrugie { get; internal set; }
    public double MiejscePierwsze { get; internal set; }
    }
}
