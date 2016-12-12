using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace DotNetKoans.CSharp
{
    public class AboutArrays : Koan
    {
        [Koan(1)]
        public void CreatingAnArray()
        {
            var array = new int[3];
            Assert.Equal(typeof(int[]), array.GetType());
            Assert.Equal(4, array.Length);
        }

        [Koan(2)]
        public void ArrayLiterals()
        {
            // You can create an array and fill it with elements in one go,
            // in that case you don't need to specify the length.
            int[] array1 = new int[] { 42, 50 };
            Assert.Equal(typeof(int), array1[0].GetType());

            //You don't have to specify a type if the arguments can be inferred
            var array2 = new [] { 42, 50 };
            Assert.Equal(typeof(int), array2[0].GetType());

            // or even simpler:
            int[] array3 = { 42, 50 };
            Assert.Equal(typeof(int), array3[0].GetType());
        }

        [Koan(3)]
        public void AccessingElements()
        {
            int[] array1 = { 42, 50 };

            //Are arrays 0-based or 1-based?
            Assert.Equal(42, array1[((int)FILL_ME_IN)]);

            //This is important because...
            Assert.Equal(array1.IsFixedSize, FILL_ME_IN);

            //...it means we can't do this: array1[2] = 13;
            Assert.Throws(typeof(FillMeIn), delegate() { array1[2] = 13; });

            //This is because the array is fixed at length 1. You could write a function
            //which created a new array bigger than the last, copied the elements over, and
            //returned the new array. Or you could use a container, which we'll see later.
        }

        [Koan(4)]
        public void AccessingArrayElements()
        {
            var array = new[] { "peanut", "butter", "and", "jelly" };

            Assert.Equal(FILL_ME_IN, array[0]);
            Assert.Equal(FILL_ME_IN, array[3]);
            
            //This doesn't work: Assert.Equal(FILL_ME_IN, array[-1]);
        }

        [Koan(5)]
        public void SlicingArrays()
        {
            var array = new[] { "peanut", "butter", "and", "jelly" };

			Assert.Equal(new string[] { (string)FILL_ME_IN, (string)FILL_ME_IN }, array.Take(2).ToArray());
			Assert.Equal(new string[] { (string)FILL_ME_IN, (string)FILL_ME_IN }, array.Skip(1).Take(2).ToArray());
        }

        [Koan(6)]
        public void CreatingATwoDimensionalArray()
        {
            int[,] array = new int[3,2];
            array[2, 1] = 5;
            Assert.Equal(FILL_ME_IN, array[2, 1]);
        }

        [Koan(7)]
        public void CreatingATwoDimensionalArrayLiteral()
        {
            int[,] array = new int[3, 2]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
            };
            Assert.Equal(FILL_ME_IN, array[2, 1]);
        }

        [Koan(8)]
        public void CreatingAnArrayOfArrays()
        {
            // If you need a non-rectangular two dimensional array,
            // you need to create an array of arrays:
            int[][] array = new int[3][];
            array[0] = new int[1];
            array[1] = new int[2];
            array[2] = new int[3];

            array[2][2] = 5;
            Assert.Equal(FILL_ME_IN, array[2][2]);
        }
    }
}
