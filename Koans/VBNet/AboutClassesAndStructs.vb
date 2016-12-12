Imports Xunit

Public Class AboutClassesAndStructs
    Inherits Koan
    Private Class Example1
        Public Sub New()
            a = 1
            b = 2
        End Sub
        Public Sub New(n As Integer)
            a = n
            b = 2
        End Sub
        Public a As Integer
        Private b As Integer
        Public Function GetSquareOfA() As Integer
            Return a * a
        End Function
        Public Function GetB() As Integer
            Return b
        End Function
    End Class
    <Koan(1)> _
    Public Sub FieldsAndMethods()
        ' Objects of classes can have fields and methods.
        Dim obj As New Example1()

        ' modify a field:
        obj.a = 2
        Assert.Equal(FILL_ME_IN, obj.a)

        ' call a method:
        Assert.Equal(FILL_ME_IN, obj.GetSquareOfA())

        ' But you can only access them if they are public. This would be illegal:
        'obj.b = 3

        ' But methods of the class can access non-public fields:
        Assert.Equal(FILL_ME_IN, obj.GetB())
    End Sub
    <Koan(2)> _
    Public Sub Constructors()
        ' Classes can can have constructors which are called when you create an object of the class with new:
        Dim obj As New Example1()
        Assert.Equal(FILL_ME_IN, obj.a)
    End Sub
    <Koan(3)> _
    Public Sub ConstructorsWithArguments()
        ' Classes can can have constructors with arguments:
        Dim obj As New Example1(3)
        Assert.Equal(FILL_ME_IN, obj.a)
    End Sub

    Private Class ClassWithPublicField
        Public n As Integer
    End Class

    <Koan(4)> _
    Public Sub VariablesHoldReferencesToObjectsOfClasses()
        ' If you create an object of a class and assign it to a variable,
        ' the variable has a reference to the object. This means that if
        ' the object at the address X in memory, the variable holds the
        ' value of X. If you assign it to another variable, that new
        ' variable will hold the address X, so changes to it are also seen
        ' with the first variable.
        Dim obj1 As New ClassWithPublicField()
        obj1.n = 1
        Dim obj2 As ClassWithPublicField = obj1
        obj2.n = 5
        Assert.Equal(FILL_ME_IN, obj1.n)
    End Sub

    Private Sub Assign5ToN(objInFunction As ClassWithPublicField)
        objInFunction.n = 5
    End Sub

    <Koan(5)> _
    Public Sub PassingAnObjectOfAClassIsByReference()
        ' Passing an object of a class to a function is by reference.
        ' This means that if the object is at address X in memory, X is
        ' passed to the function.
        Dim obj As New ClassWithPublicField()
        obj.n = 1
        Assign5ToN(obj)
        Assert.Equal(FILL_ME_IN, obj.n)
    End Sub

    Private Sub RecreateObject(objInFunction As ClassWithPublicField)
        objInFunction = New ClassWithPublicField()
        objInFunction.n = 5
    End Sub
    <Koan(6)> _
    Public Sub ObjectPassedToAFunctionCannotBeReassigned()
        ' If a function is passed a reference to an object of a class, and
        ' reassigns it to a new object, this change is not seen by the
        ' caller because if the first object is at address X and the second
        ' at address Y, Y will be assigned to objInFunction while obj in the
        ' calling function still holds X, because the function does not
        ' have access to obj.
        Dim obj As New ClassWithPublicField()
        obj.n = 1
        RecreateObject(obj)
        Assert.Equal(FILL_ME_IN, obj.n)
    End Sub

    Private Structure StructWithPublicField
        Public n As Integer
    End Structure
    ' A struct is much like a class, it can have fields, methods, properties, etc.
    <Koan(7)> _
    Public Sub StructsAreCopied()
        ' An important difference is that objects of structs are not held
        ' by reference. So assigning it to another variable copies the
        ' object. If an object of struct is at address X in memory, and is
        ' assigned to another variable, that new variable will point to a
        ' copy at another address Y.
        Dim obj1 As New StructWithPublicField()
        obj1.n = 1
        Dim obj2 As StructWithPublicField = obj1
        obj2.n = 2
        Assert.Equal(FILL_ME_IN, obj1.n)
    End Sub

    Private Sub Assign10ToN(objInFunction As StructWithPublicField)
        objInFunction.n = 10
    End Sub

    <Koan(8)> _
    Public Sub StructsPassedToFunctionsAreCopied()
        ' When a struct is passed to a function, it is also copied.
        ' So if an object of a struct is at address X in memory, and is
        ' passed to a function, the variable in the function will point to a
        ' copy at another address Y.
        Dim obj As New StructWithPublicField()
        obj.n = 1
        Assign10ToN(obj)
        Assert.Equal(FILL_ME_IN, obj.n)
    End Sub
End Class
