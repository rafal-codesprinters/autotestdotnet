using System;
using Xunit;

namespace DotNetKoans.CSharp
{
    public class AboutClassesAndStructs : Koan
    {
        class Example1
        {
            public Example1()
            {
                a = 1;
                b = 2;
            }
            public Example1(int n)
            {
                a = n;
                b = 2;
            }
            public int a;
            private int b;
            public int GetSquareOfA()
            {
                return a*a;
            }
            public int GetB()
            {
                return b;
            }
        }
        [Koan(1)]
        public void FieldsAndMethods()
        {
            // Objects of classes can have fields and methods.
            Example1 obj = new Example1();

            // modify a field:
            obj.a = 2;
            Assert.Equal(FILL_ME_IN, obj.a);

            // call a method:
            Assert.Equal(FILL_ME_IN, obj.GetSquareOfA());

            // But you can only access them if they are public. This would be illegal:
            //obj.b = 3;

            // But methods of the class can access non-public fields:
            Assert.Equal(FILL_ME_IN, obj.GetB());
        }
        [Koan(2)]
        public void Constructors()
        {
            // Classes can can have constructors which are called when you create an object of the class with new:
            Example1 obj = new Example1();
            Assert.Equal(FILL_ME_IN, obj.a);
        }
        [Koan(3)]
        public void ConstructorsWithArguments()
        {
            // Classes can can have constructors with arguments:
            Example1 obj = new Example1(3);
            Assert.Equal(FILL_ME_IN, obj.a);
        }

        class ClassWithPublicField
        {
            public int n;
        }
        
        [Koan(4)]
        public void VariablesHoldReferencesToObjectsOfClasses()
        {
            // If you create an object of a class and assign it to a variable,
            // the variable has a reference to the object. This means that if
            // the object at the address X in memory, the variable holds the
            // value of X. If you assign it to another variable, that new
            // variable will hold the address X, so changes to it are also seen
            // with the first variable.
            ClassWithPublicField obj1 = new ClassWithPublicField();
            obj1.n = 1;
            ClassWithPublicField obj2 = obj1;
            obj2.n = 5;
            Assert.Equal(FILL_ME_IN, obj1.n);
        }
        
        void Assign5ToN(ClassWithPublicField objInFunction)
        {
            objInFunction.n = 5;
        }
        
        [Koan(5)]
        public void PassingAnObjectOfAClassIsByReference()
        {
            // Passing an object of a class to a function is by reference.
            // This means that if the object is at address X in memory, X is
            // passed to the function.
            ClassWithPublicField obj = new ClassWithPublicField();
            obj.n = 1;
            Assign5ToN(obj);
            Assert.Equal(FILL_ME_IN, obj.n);
        }
        
        void RecreateObject(ClassWithPublicField objInFunction)
        {
            objInFunction = new ClassWithPublicField();
            objInFunction.n = 5;
        }
        [Koan(6)]
        public void ObjectPassedToAFunctionCannotBeReassigned()
        {
            // If a function is passed a reference to an object of a class, and
            // reassigns it to a new object, this change is not seen by the
            // caller because if the first object is at address X and the second
            // at address Y, Y will be assigned to objInFunction while obj in the
            // calling function still holds X, because the function does not
            // have access to obj.
            ClassWithPublicField obj = new ClassWithPublicField();
            obj.n = 1;
            RecreateObject(obj);
            Assert.Equal(FILL_ME_IN, obj.n);
        }

        struct StructWithPublicField
        {
            public int n;
        }
        // A struct is much like a class, it can have fields, methods, properties, etc.
        [Koan(7)]
        public void StructsAreCopied()
        {
            // An important difference is that objects of structs are not held
            // by reference. So assigning it to another variable copies the
            // object. If an object of struct is at address X in memory, and is
            // assigned to another variable, that new variable will point to a
            // copy at another address Y.
            StructWithPublicField obj1 = new StructWithPublicField();
            obj1.n = 1;
            StructWithPublicField obj2 = obj1;
            obj2.n = 2;
            Assert.Equal(FILL_ME_IN, obj1.n);
        }

        void Assign10ToN(StructWithPublicField objInFunction)
        {
            objInFunction.n = 10;
        }

        [Koan(8)]
        public void StructsPassedToFunctionsAreCopied()
        {
            // When a struct is passed to a function, it is also copied.
            // So if an object of a struct is at address X in memory, and is
            // passed to a function, the variable in the function will point to a
            // copy at another address Y.
            StructWithPublicField obj = new StructWithPublicField();
            obj.n = 1;
            Assign10ToN(obj);
            Assert.Equal(FILL_ME_IN, obj.n);
        }
    }
}
