using System;
using Xunit;

namespace DotNetKoans.CSharp
{
    public class AboutNil : Koan
    {
        [Koan(1)]
        public void NilIsNotAnObject()
        {
            Assert.True(true); //not everything is an object
        }

        [Koan(2)]
        public void YouGetNullPointerErrorsWhenCallingMethodsOnNil()
        {
            //What is the Exception that is thrown when you call a method on a null object?
            //Don't be confused by the code below. It is using Anonymous Delegates which we will
            //cover later on. 
            object nothing = null;
            Assert.Throws(typeof(NullReferenceException), delegate() { nothing.ToString(); });

            //What's the message of the exception? What substring or pattern could you test
            //against in order to have a good idea of what the string is?
            try
            {
                nothing.ToString();
            }
            catch (System.Exception ex)
            {
                Assert.Contains("Object reference", ex.Message);
            }
        }

        [Koan(3)]
        public void CheckingThatAnObjectIsNull()
        {
            object obj = null;
            Assert.True(obj == null);
        }

        [Koan(4)]
        public void ABetterWayToCheckThatAnObjectIsNull()
        {
            object obj = null;
            Assert.Null(obj);
        }

        [Koan(5)]
        public void AWayNotToCheckThatAnObjectIsNull()
        {
            object obj = null;
            Equals(obj);
        }
    }
}
