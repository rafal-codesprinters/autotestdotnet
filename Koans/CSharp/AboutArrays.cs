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
        public void CreatingArrays()
        {
            var empty_array = new object[] { };
            Assert.Equal(typeof(object[]), empty_array.GetType());

            //Note that you have to explicitly check for subclasses
            Assert.True(typeof(Array).IsAssignableFrom(empty_array.GetType()));

            Assert.Equal(0, empty_array.Length);
        }

        [Koan(2)] //12
        public void ArrayLiterals()
        {
            //You don't have to specify a type if the arguments can be inferred
            var array = new [] { 42 };
            Assert.Equal(typeof(int[]), array.GetType());
            Assert.Equal(new int[] { 42 }, array);

            //Are arrays 0-based or 1-based?
            Assert.Equal(42, array[((int)0)]);

            //This is important because...
            Assert.True(array.IsFixedSize);

            //...it means we can't do this: array[1] = 13;
            Assert.Throws(typeof(System.IndexOutOfRangeException), delegate() { array[1] = 13; });

            //This is because the array is fixed at length 1. You could write a function
            //which created a new array bigger than the last, copied the elements over, and
            //returned the new array. Or you could do this:
            List<int> dynamicArray = new List<int>();
            dynamicArray.Add(42);
            Assert.Equal(array, dynamicArray.ToArray());

            dynamicArray.Add(13);
            Assert.Equal((new int[] { 42, (int)13}), dynamicArray.ToArray());
        }

        [Koan(3)] //13
        public void AccessingArrayElements()
        {
            var array = new[] { "peanut", "butter", "and", "jelly" };

            Assert.Equal("peanut", array[0]);
            Assert.Equal("jelly", array[3]);
            
            //This doesn't work: Assert.Equal(FILL_ME_IN, array[-1]);
        }

        [Koan(4)]
        public void SlicingArrays()
        {
            var array = new[] { "peanut", "butter", "and", "jelly" };

			Assert.Equal(new string[] { (string)"peanut", (string)"butter" }, array.Take(2).ToArray());
			Assert.Equal(new string[] { (string)"butter", (string)"and" }, array.Skip(1).Take(2).ToArray());
        }

        [Koan(5)]
        public void PushingAndPopping()
        {
            //var array = new[] { 1, 2 };
            //Stack stack = new Stack(array);
            //stack.Push("last");

            //String[] array2 = { "last", "2", "1" };

            //Assert.Equal(array2, stack.ToArray());
            //var poppedValue = stack.Pop();
            //Assert.Equal(1, poppedValue);
            //var array3 = new[] { "last", "2"};
            //Assert.Equal(array3, stack.ToArray());
            //XXX

        }

        [Koan(6)]
        public void Shifting()
        {
            ////Shift == Remove First Element
            ////Unshift == Insert Element at Beginning
            ////C# doesn't provide this natively. You have a couple
            ////of options, but we'll use the LinkedList<T> to implement
            //var array = new[] { "Hello", "World" };
            //var list = new LinkedList<string>(array);

            //list.AddFirst("Say");
            //Assert.Equal({ "Say", "Hello", "World" }, list.ToArray());

            //list.RemoveLast();
            //Assert.Equal(FILL_ME_IN, list.ToArray());

            //list.RemoveFirst();
            //Assert.Equal(FILL_ME_IN, list.ToArray());

            //list.AddAfter(list.Find("Hello"), "World");
            //Assert.Equal(FILL_ME_IN, list.ToArray());
            //XXX
        }

    }
}
