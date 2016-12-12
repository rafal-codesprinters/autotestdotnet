using System;
using Xunit;

namespace DotNetKoans.CSharp
{
    public class AboutUsing: Koan
    {
        static public bool disposeCalled = false;
        class MyClass : IDisposable
        {
            public string value = "abc";
            public MyClass()
            {
                disposeCalled = false;
            }
            public void Dispose()
            {
                disposeCalled = true;
                value = null;
            }
        }

        [Koan(1)]
        public void UsingCallsDisposeAtTheEndOfTheBlock()
        {
            // Objects of classes that implement IDisposable can be used in a using statement which is followed by a block.
            // This construction makes sure that Dispose is called when the block is left.
            // Dispose usually does cleanup of the object, e.g. to free resources.
            // An example of such a class is File, whose Dispose method closes the file.
            using (MyClass obj = new MyClass())
            {
                Assert.Equal(FILL_ME_IN, disposeCalled);
            }
            Assert.Equal(FILL_ME_IN, disposeCalled);
        }

        [Koan(2)]
        public void UsingCallsDisposeAtTheEndOfTheBlockEvenIfAnExceptionIsThrown()
        {
            try
            {
                using (MyClass obj = new MyClass())
                {
                    Assert.Equal(FILL_ME_IN, disposeCalled);
                }
            }
            catch (Exception)
            {
                Assert.Equal(FILL_ME_IN, disposeCalled);
            }
        }

        [Koan(3)]
        public void TheUsedObjectCannotBeReassigned()
        {
            using (MyClass obj = new MyClass())
            {
                // This would be illegal:
                // obj = new MyClass();

                // But its members can be changed:
                obj.value = "xyz";
                Assert.Equal(FILL_ME_IN, obj.value);
            }
        }

        [Koan(4)]
        public void TheUsedObjectShouldNotBeUsedAfterwards()
        {
            // It is possible to use an object in the using statement that was created before,
            // but it is not recommended because the object is probably no longer usable after the using block.
            MyClass obj = new MyClass();
            using (obj)
            {
            }
            Assert.Equal(FILL_ME_IN, obj.value);
        }
    }
}