using System;
using System.Collections.Generic;
using Xunit;
using DotNetKoans.CSharp;

namespace DotNetKoans.CSharp
{
    public static class ExtensionMethods
    {
        public static string HelloWorld(this Koan koan)
        {
            return "Hello!";
        }

        public static string SayHello(this Koan koan, string name)
        {
            return String.Format("Hello, {0}!", name);
        }

        public static string[] MethodWithVariableArguments(this Koan koan, params string[] names)
        {
            return names;
        }

        public static string SayHi(this String str)
        {
            return "Hi, " + str;
        }
    }

    public class AboutMethods : Koan
    {
        class InnerSecret
        {
            public static string Key() { return "Key"; }
            public string Secret() { return "Secret"; }
            protected string SuperSecret() { return "This is secret"; }
            private string SooperSeekrit() { return "No one will find me!"; }
        }

        class StateSecret : InnerSecret
        {
            public string InformationLeak() { return SuperSecret(); }
        }

        //Static methods don't require an instance of the object
        //in order to be called. 
        [Koan(1)]
        public void CallingStaticMethodsWithoutAnInstance()
        {
            Assert.Equal(FILL_ME_IN, InnerSecret.Key());
        }

        //In fact, you can't call it on an instance variable
        //of the object. So this wouldn't compile:
        //InnerSecret secret = new InnerSecret();
        //Assert.Equal(FILL_ME_IN, secret.Key());


        [Koan(2)]
        public void CallingPublicMethodsOnAnInstance()
        {
            InnerSecret secret = new InnerSecret();
            Assert.Equal(FILL_ME_IN, secret.Secret());
        }

        //Protected methods can only be called by a subclass
        //We're going to call the public method called
        //InformationLeak of the StateSecret class which returns
        //the value from the protected method SuperSecret
        [Koan(3)]
        public void CallingProtectedMethodsOnAnInstance()
        {
            StateSecret secret = new StateSecret();
            Assert.Equal(FILL_ME_IN, secret.InformationLeak());
        }

        //But, we can't call the private methods of InnerSecret
        //either through an instance, or through a subclass. It
        //just isn't available.

        //Ok, well, that isn't entirely true. Reflection can get
        //you just about anything, and though it's way out of scope
        //for this...
        [Koan(4)]
        public void SubvertPrivateMethods()
        {
            InnerSecret secret = new InnerSecret();
            string superSecretMessage = secret.GetType()
                .GetMethod("SooperSeekrit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(secret, null) as string;
            Assert.Equal(FILL_ME_IN, superSecretMessage);
        }

        //Up till now we've had explicit return types. It's also
        //possible to create methods which dynamically shift
        //the type based on the input. These are referred to
        //as generics

        public static T GiveMeBack<T>(T p1)
        {
            return p1;
        }

        [Koan(5)]
        public void CallingGenericMethods()
        {
            Assert.Equal(typeof(FillMeIn), GiveMeBack<int>(1).GetType());

            Assert.Equal(FILL_ME_IN, GiveMeBack<string>("Hi!"));
        }

        private int Overload(int n)
        {
            return 1;
        }

        private int Overload(string s)
        {
            return 2;
        }

        private int Overload(int n, string s)
        {
            return 3;
        }

        // This would result in a compilation error because it differs from the first Overload
        // method in the return type:
        /*private string Overload(int n)
        {
            return "abc";
        }*/

        [Koan(6)]
        public void MethodsCanBeOverloaded()
        {
            // Methods can be overloaded, which means that you can have different methods
            // with the same name but different types of arguments. They even can have a
            // different number of arguments. However, you cannot have two methods with the
            // same name and arguments that only have a different return type.
            // When you call a method with that name, the compiler chooses the one with
            // the matching arguments that you supply.
            Assert.Equal(FILL_ME_IN, Overload(1));
            Assert.Equal(FILL_ME_IN, Overload("abc"));
            Assert.Equal(FILL_ME_IN, Overload(1, "abc"));
        }

        private double CalculateBMI(double weight, double height)
        {
            return weight / (height * height);
        }

        [Koan(7)]
        public void MethodCallsCanUseNamedParameters()
        {
            // Method calls can have named arguments, by giving the name of the argument and a colon
            // before the argument.
            // This is a safe practice for methods of which you might be confused about the order of
            // the arguments.
            Assert.Equal(FILL_ME_IN, CalculateBMI(weight: 100, height: 2.0));
            Assert.Equal(FILL_ME_IN, CalculateBMI(height: 2.0, weight: 100));
        }

        private int MethodWithOptionalArguments(int a, int b = 1, int c = 2)
        {
            return a + b + c*2;
        }

        [Koan(8)]
        public void MethodsCanHaveOptionalArguments()
        {
            // Methods can have optional arguments, if they have an = and a default value after the argument.
            // If you don't pass that argument, the default value is used; otherwise the value you pass is used.
            Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1,2,3));
            Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1,2));
            Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1));

            // You cannot omit an optional argument if there are optional arguments after it...
            // Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1, , 3));

            // ... unless you make it a named argument:
            Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1, c: 3));
        }

        class Example1
        {
            public int a=1;
        }

        void PassClassNotByRef(Example1 obj)
        {
            obj = new Example1();
        }

        void PassClassByRef(ref Example1 obj)
        {
            obj = new Example1();
        }

        [Koan(9)]
        public void PassingClassObjectsByReference()
        {
            Example1 obj1 = new Example1();
            Example1 obj2 = obj1;

            // obj1 and obj2 are references that point to the same object in memory
            Assert.Equal(FILL_ME_IN, obj1 == obj2);

            // If an object is passed to a method but not by reference, the address of the object is passed,
            // so the address that is stored in obj1 cannot be changed by the method.
            // (the same as in Java, or as passing a pointer in C/C++)
            PassClassNotByRef(obj1);
            Assert.Equal(FILL_ME_IN, obj1 == obj2);

            // However, if an object is passed by reference, the address of the variable is passed to the method,
            // so it can change to memory location obj1 is pointing.
            // Note that you need to add the keyword ref in front of the argument in both the method and the call.
            // (the same as a pointer to a pointer in C/C++)
            PassClassByRef(ref obj1);
            Assert.Equal(FILL_ME_IN, obj1 == obj2);
        }

        void OutClass(out Example1 obj)
        {
            // Next line gives compile error if not commented out,
            // because the method has to suppose that the object is not yet initialized.
            // obj.a = 1;

            // Commenting out the next line would give a compilation error because the method
            // has to assign to the argument.
            obj = new Example1();
        }

        [Koan(10)]
        public void PassingClassObjectsWithTheOutKeyword()
        {
            Example1 obj1 = new Example1();
            Example1 obj2 = obj1;

            // obj1 and obj2 are references that point to the same object in memory
            Assert.Equal(FILL_ME_IN, obj1 == obj2);

            // If an object is passed to a method with the out keyword, the address of the variable is passed,
            // so the address that is stored in obj1 can be changed by the method.
            // This is identical to ref, except that the method is guaranteed to work if the object is not
            // initialized, and that an assignment to it will be done.
            // Otherwise there will be a compilation error in the method.
            // Note that you need to add the out keyword in the call as well, this makes it clearer at the
            // call site what the intention of the method is.
            OutClass(out obj1);
            Assert.Equal(FILL_ME_IN, obj1 == obj2);
        }

        struct Example2
        {
            public int a, b;
        }

        void PassStructNotByRef(Example2 obj)
        {
            obj.a = 2;
        }

        void PassStructByRef(ref Example2 obj)
        {
            obj.a = 2;
        }

        [Koan(11)]
        public void PassingStructObjectsByReference()
        {
            Example2 obj = new Example2();
            obj.a = 1;

            // Unlike a variable that holds a class object,
            // a variable that holds a struct object has the object in the variable it self.
            // If a struct object is passed to a method but not by reference, a copy of the object is passed
            // (the same as an int in Java, or as passing by value in C/C++),
            // so changes in the method are not seen in the original object.
            PassStructNotByRef(obj);
            Assert.Equal(FILL_ME_IN, obj.a);

            // However, if a struct object is passed by reference, the address of the variable is passed to the method,
            // so it can makes changes in the memory location of obj.
            // Note that you need to add the keyword ref in front of the argument in both the method and the call.
            // (the same as a pointer in C/C++)
            PassStructByRef(ref obj);
            Assert.Equal(FILL_ME_IN, obj.a);
        }

        void PassStructWithTheOutKeyword(out Example2 obj)
        {
            // Next line gives compile error if not commented out,
            // because the method has to suppose that the object is not yet initialized.
            //int n = obj.a;

            // Commenting out one of the next lines would give a compilation error because the method
            // has to completely initialize the object.
            obj.a = 2;
            obj.b = 3;
        }

        [Koan(12)]
        public void PassingStructObjectsWithTheOutKeyword()
        {
            Example2 obj;

            // If a struct object is passed to a method with the out keyword, the address of the variable is passed,
            // so the values in obj can be changed by the method.
            // This is identical to ref, except that the method is guaranteed to work if the object is not
            // initialized, and that an assignment to its members will be done.
            // Otherwise there will be a compilation error in the method.
            // Note that you need to add the out keyword in the call as well, this makes it clearer at the
            // call site what the intention of the method is.
            PassStructWithTheOutKeyword(out obj);
            Assert.Equal(FILL_ME_IN, obj.a);
            Assert.Equal(FILL_ME_IN, obj.b);
        }

        private string[] LocalMethodWithVariableParameters(params string[] names)
        {
            return names;
        }

        [Koan(13)]
        public void MethodsCanHaveAVariableNumberOfParametersInAnArray()
        {
            // If the last argument of a method is an array preceded by the keyword params,
            // you can pass an arbitrary number of arguments for that argument, which will
            // be accessible as an array in the method.
            Assert.Equal(FILL_ME_IN, LocalMethodWithVariableParameters("Cory", "Will", "Corey"));
        }

        //Extension Methods allow us to "add" methods to any class
        //without having to recompile. You only have to reference the
        //namespace the methods are in to use them. Here, since both the
        //ExtensionMethods class and the AboutMethods class are in the
        //DotNetKoans.CSharp namespace, AboutMethods can automatically
        //find them

        [Koan(14)]
        public void ExtensionMethodsShowUpInTheCurrentClass()
        {
            // Note that you have to add 'this.' in front of the call for extension methods.
            Assert.Equal(FILL_ME_IN, this.HelloWorld());
        }

        [Koan(15)]
        public void ExtensionMethodsWithParameters()
        {
            Assert.Equal(FILL_ME_IN, this.SayHello("Cory"));
        }

        [Koan(16)]
        public void ExtensionMethodsWithVariableParameters()
        {
            Assert.Equal(FILL_ME_IN, this.MethodWithVariableArguments("Cory", "Will", "Corey"));
        }

        //Extension methods can extend any class my referencing 
        //the name of the class they are extending. For example, 
        //we can "extend" the string class like so:

        [Koan(17)]
        public void ExtendingCoreClasses()
        {
            Assert.Equal(FILL_ME_IN, "Cory".SayHi());
        }
    }
}