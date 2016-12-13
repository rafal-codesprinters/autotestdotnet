using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunkcjaKwadratowa
{
    public class Program
    {
       
        [Fact]
        public void CzyDobrzeLiczyDwaMiejscaZerowe()
        {
            var a = 1;
            var b = -4;
            var c = 3;

            var wynik = Oblicz(a, b, c);


            Assert.Equal(3, wynik.MiejscePierwsze);
            Assert.Equal(1, wynik.MiejsceDrugie);
            Assert.Equal(null, wynik.JednoMiejsce);
            Assert.Equal(2, wynik.IloscMiejscZerowych);
        }

        [Fact]
        public void CzyDobrzeLiczyJednoMiejsceZerowe()
        {
            var a = 1;
            var b = -4;
            var c = 4;

            var wynik = Oblicz(a, b, c);


            Assert.Equal(null, wynik.MiejscePierwsze);
            Assert.Equal(null, wynik.MiejsceDrugie);
            Assert.Equal(2, wynik.JednoMiejsce);
            Assert.Equal(1, wynik.IloscMiejscZerowych);
        }

        [Fact]
        public void CzyDobrzeLiczyBrakMiejscZerowych()
        {
            var a = 1;
            var b = 2;
            var c = 4;

            var wynik = Oblicz(a, b, c);


            Assert.Equal(null, wynik.MiejscePierwsze);
            Assert.Equal(null, wynik.MiejsceDrugie);
            Assert.Equal(null, wynik.JednoMiejsce);
            Assert.Equal(0, wynik.IloscMiejscZerowych);
        }

        private Wynik Oblicz(int a, int b, int c)
        {
            double? x1 = null;
            double? x2 = null;

            double? x0 = null;
            int ilosc_miejsc_zerowych;

            double delta = b * b - 4 * a * c;
            double pierwiastek = 0;

            if (delta > 0)
            {
                pierwiastek = Math.Sqrt(delta);
                x1 = (-b + pierwiastek) / (2 * a);
                x2 = (-b - pierwiastek) / (2 * a);
                ilosc_miejsc_zerowych = 2;
            }
            else if (delta == 0)
            {
                x0 = -b / (2 * a);
                ilosc_miejsc_zerowych = 1;
            }
            else
            {
                ilosc_miejsc_zerowych = 0;
            }

            return new Wynik
            {
                MiejscePierwsze = x1,
                MiejsceDrugie = x2,
                JednoMiejsce = x0,
                IloscMiejscZerowych = ilosc_miejsc_zerowych
            };
            

            throw new NotImplementedException();
        }
    }
}

