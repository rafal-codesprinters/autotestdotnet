using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowaTest
{
    /// <summary>
    /// Funkcja kwadratowa, wyliczanie miejsc zerowych
    ///  
    /// y = ax2 + bx + c
    /// </summary>
    internal class Wynik
    {
     
        public double? MiejscePierwsze { get ; set ; }
        public double? MiejsceDrugie { get; set; }
                
    }

    public class Testy
    {

        [Fact]
        public void SzybkiTest()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            Wynik rezultat = new Implementacja().Calculate(a, b, c);
            Assert.Equal(1, rezultat.MiejsceDrugie);
            Assert.Equal(3, rezultat.MiejscePierwsze);
        }
    }
}
