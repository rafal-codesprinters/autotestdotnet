
Imports System.Collections.Generic
Imports Xunit

Public Class AboutMethods
    Inherits Koan
    Private Class InnerSecret
        Public Shared Function Key() As String
            Return "Key"
        End Function
        Public Function Secret() As String
            Return "Secret"
        End Function
        Protected Function SuperSecret() As String
            Return "This is secret"
        End Function
        Private Function SooperSeekrit() As String
            Return "No one will find me!"
        End Function
    End Class

    Private Class StateSecret
        Inherits InnerSecret
        Public Function InformationLeak() As String
            Return SuperSecret()
        End Function
    End Class

    'Static methods don't require an instance of the object
    'in order to be called. 
    <Koan(1)> _
    Public Sub CallingStaticMethodsWithoutAnInstance()
        Assert.Equal(FILL_ME_IN, InnerSecret.Key())
    End Sub

    'In fact, you can't call it on an instance variable
    'of the object. So this wouldn't compile:
    'Sub Test()
    '   Dim secret = New InnerSecret()
    '   Assert.Equal(FILL_ME_IN, secret.Key())
    'End Sub

    <Koan(2)> _
    Public Sub CallingPublicMethodsOnAnInstance()
        Dim secret As New InnerSecret()
        Assert.Equal(FILL_ME_IN, secret.Secret())
    End Sub

    'Protected methods can only be called by a subclass
    'We're going to call the public method called
    'InformationLeak of the StateSecret class which returns
    'the value from the protected method SuperSecret
    <Koan(3)> _
    Public Sub CallingProtectedMethodsOnAnInstance()
        Dim secret As New StateSecret()
        Assert.Equal(FILL_ME_IN, secret.InformationLeak())
    End Sub

    'But, we can't call the private methods of InnerSecret
    'either through an instance, or through a subclass. It
    'just isn't available.

    'Ok, well, that isn't entirely true. Reflection can get
    'you just about anything, and though it's way out of scope
    'for this...
    <Koan(4)> _
    Public Sub SubvertPrivateMethods()
        Dim secret As New InnerSecret()
        Dim superSecretMessage As String = TryCast(secret.[GetType]().GetMethod("SooperSeekrit", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance).Invoke(secret, Nothing), String)
        Assert.Equal(FILL_ME_IN, superSecretMessage)
    End Sub

    'Up till now we've had explicit return types. It's also
    'possible to create methods which dynamically shift
    'the type based on the input. These are referred to
    'as generics

    Public Shared Function GiveMeBack(Of T)(p1 As T) As T
        Return p1
    End Function

    <Koan(5)> _
    Public Sub CallingGenericMethods()
        Assert.Equal(GetType(FillMeIn), GiveMeBack(Of Integer)(1).[GetType]())

        Assert.Equal(FILL_ME_IN, GiveMeBack(Of String)("Hi!"))
    End Sub

    Private Function Overload(n As Integer) As Integer
        Return 1
    End Function

    Private Function Overload(s As String) As Integer
        Return 2
    End Function

    Private Function Overload(n As Integer, s As String) As Integer
        Return 3
    End Function

    ' This would result in a compilation error because it differs from the first Overload
    ' method in the return type:
    'Private Function Overload(n As Integer) As String
    '    Return "abc"
    'End Function

    <Koan(6)> _
    Public Sub MethodsCanBeOverloaded()
        ' Methods can be overloaded, which means that you can have different methods
        ' with the same name but different types of arguments. They even can have a
        ' different number of arguments. However, you cannot have two methods with the
        ' same name and arguments that only have a different return type.
        ' When you call a method with that name, the compiler chooses the one with
        ' the matching arguments that you supply.
        Assert.Equal(FILL_ME_IN, Overload(1))
        Assert.Equal(FILL_ME_IN, Overload("abc"))
        Assert.Equal(FILL_ME_IN, Overload(1, "abc"))
    End Sub

    Private Function CalculateBMI(weight As Double, height As Double) As Double
        Return weight / (height * height)
    End Function

    <Koan(7)> _
    Public Sub MethodCallsCanUseNamedParameters()
        ' Method calls can have named arguments, by giving the name of the argument and a colon
        ' before the argument.
        ' This is a safe practice for methods of which you might be confused about the order of
        ' the arguments.
        Assert.Equal(FILL_ME_IN, CalculateBMI(weight:=100, height:=2.0))
        Assert.Equal(FILL_ME_IN, CalculateBMI(height:=2.0, weight:=100))
    End Sub

    Private Function MethodWithOptionalArguments(a As Integer, Optional b As Integer = 1, Optional c As Integer = 2) As Integer
        Return a + b + c * 2
    End Function

    <Koan(8)> _
    Public Sub MethodsCanHaveOptionalArguments()
        ' Methods can have optional arguments, if they have an = and a default value after the argument.
        ' If you don't pass that argument, the default value is used; otherwise the value you pass is used.
        Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1, 2, 3))
        Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1, 2))
        Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1))

        ' You cannot omit an optional argument if there are optional arguments after it...
        ' Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1, , 3))

        ' ... unless you make it a named argument:
        Assert.Equal(FILL_ME_IN, MethodWithOptionalArguments(1, c:=3))
    End Sub

    Private Class Example1
        Public a As Integer = 1
    End Class

    Private Sub PassClassNotByRef(obj As Example1)
        obj = New Example1()
    End Sub

    Private Sub PassClassByRef(ByRef obj As Example1)
        obj = New Example1()
    End Sub

    <Koan(9)> _
    Public Sub PassingClassObjectsByReference()
        Dim obj1 As New Example1()
        Dim obj2 As Example1 = obj1

        ' obj1 and obj2 are references that point to the same object in memory
        Assert.Equal(FILL_ME_IN, obj1 Is obj2)

        ' If an object is passed to a method but not by reference, the address of the object is passed,
        ' so the address that is stored in obj1 cannot be changed by the method.
        ' (the same as in Java, or as passing a pointer in C/C++)
        PassClassNotByRef(obj1)
        Assert.Equal(FILL_ME_IN, obj1 Is obj2)

        ' However, if an object is passed by reference, by adding ByRef in front of the argumeny,
        ' the address of the variable is passed to the method,
        ' so it can change to memory location obj1 is pointing.
        ' (the same as a pointer to a pointer in C/C++)
        PassClassByRef(obj1)
        Assert.Equal(FILL_ME_IN, obj1 Is obj2)
    End Sub

    Private Structure Example2
        Public a As Integer, b As Integer
    End Structure

    Private Sub PassStructNotByRef(obj As Example2)
        obj.a = 2
    End Sub

    Private Sub PassStructByRef(ByRef obj As Example2)
        obj.a = 2
    End Sub

    <Koan(10)> _
    Public Sub PassingStructObjectsByReference()
        Dim obj As New Example2()
        obj.a = 1

        ' Unlike a variable that holds a class object,
        ' a variable that holds a struct object has the object in the variable it self.
        ' If a struct object is passed to a method but not by reference, a copy of the object is passed
        ' (the same as an int in Java, or as passing by value in C/C++),
        ' so changes in the method are not seen in the original object.
        PassStructNotByRef(obj)
        Assert.Equal(FILL_ME_IN, obj.a)

        ' However, if a struct object is passed by reference, the address of the variable is passed to the method,
        ' so it can makes changes in the memory location of obj.
        ' (the same as a pointer in C/C++)
        PassStructByRef(obj)
        Assert.Equal(FILL_ME_IN, obj.a)
    End Sub

    Private Function LocalMethodWithVariableParameters(ParamArray names As String()) As String()
        Return names
    End Function

    <Koan(11)> _
    Public Sub MethodsCanHaveAVariableNumberOfParametersInAnArray()
        ' If the last argument of a method is an array preceded by the keyword ParamArray,
        ' you can pass an arbitrary number of arguments for that argument, which will
        ' be accessible as an array in the method.
        Assert.Equal(FILL_ME_IN, LocalMethodWithVariableParameters("Cory", "Will", "Corey"))
    End Sub

    'Extension Methods allow us to "add" methods to any class
    'without having to recompile. You only have to reference the
    'namespace the methods are in to use them. Here, since both the
    'ExtensionMethods class in the file ExtensionMethods.vb and
    'the AboutMethods class are not in a
    'namespace, AboutMethods can automatically find them.

    <Koan(12)> _
    Public Sub ExtensionMethodsShowUpInTheCurrentClass()
        Assert.Equal(FILL_ME_IN, HelloWorld())
    End Sub

    <Koan(13)> _
    Public Sub ExtensionMethodsWithParameters()
        Assert.Equal(FILL_ME_IN, SayHello("Cory"))
    End Sub

    <Koan(14)> _
    Public Sub ExtensionMethodsWithVariableParameters()
        Assert.Equal(FILL_ME_IN, MethodWithVariableArguments("Cory", "Will", "Corey"))
    End Sub

    'Extension methods can extend any class my referencing 
    'the name of the class they are extending.

    <Koan(15)> _
    Public Sub ExtendingCoreClasses()
        Assert.Equal(FILL_ME_IN, "Cory".SayHi())
    End Sub
End Class
