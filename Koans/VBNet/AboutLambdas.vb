Imports System.Collections.Generic
Imports System.Linq
Imports Xunit

Public Class AboutLambdas
    Inherits Koan
    <Koan(1)> _
    Public Sub UsingLambdas()
        'The AboutDelegates Koans introduced you to delegates. In all of those koans, 
        'the delegate was assigned to a predefined method. 
        'Lambdas let you define the method in place.
        'This Koan produces the same result as AboutDelegates.ChangingTypesWithConverter, but it uses 
        'a lambda instead. As you can see there is no method name.
        Dim numbers = New Integer() {1, 2, 3, 4}
        Dim result = Array.ConvertAll(numbers, Function(x As Integer) x.ToString())

        Assert.Equal(FILL_ME_IN, result)
    End Sub
    <Koan(2)> _
    Public Sub LambdasCanAccessOuterVariables()
        'Lambdas can access variable defined in the scope of the method where they are defined.
        'In VB this is called accessing an Outer Variable. In other languages it is called closure. 
        Dim numbers = New Integer() {4, 5, 6, 7, 8, 9}
        Dim toFind As Integer = 7
        Assert.Equal(FILL_ME_IN, Array.FindIndex(numbers, Function(x As Integer) x = toFind))
    End Sub
    <Koan(3)> _
    Public Sub AccessEvenAfterVariableIsOutOfScope()
        Dim criteria As Predicate(Of Integer)
        If True Then
            'Lambdas even have access to the value after the value has gone out of scope
            Dim toFind As Integer = 7
            criteria = Function(x As Integer) x = toFind
        End If
        Dim numbers = New Integer() {4, 5, 6, 7, 8, 9}
        'toFind is not available here, yet criteria still works
        Assert.Equal(FILL_ME_IN, Array.FindIndex(numbers, criteria))
    End Sub
    <Koan(4)> _
    Public Sub MultiStatementLambdas()
        Dim numbers = New Integer() {1, 2, 3, 4}
        'Lambda expressions can have multiple statements if you put them on separate lines
        'and end them with 'End Function'.
        Dim lambda = Array.ConvertAll(numbers, Function(x As Integer)
                                                   x += 1
                                                   Return x.ToString()
                                               End Function)
        Assert.Equal(FILL_ME_IN, lambda)
    End Sub
    <Koan(5)> _
    Public Sub LambdasCanBeSubs()
        Dim numbers = New Integer() {1, 2, 3, 4}
        Dim sum = 0
        ' In places where no return value is needed of the lambda, a Sub can be used
        ' This can also have multiple statements by using separate lines and ending with 'End Sub'
        Array.ForEach(numbers, Sub(x As Integer) sum += x)
        Assert.Equal(FILL_ME_IN, sum)
    End Sub
    <Koan(6)> _
    Public Sub TypeNotNeeded()
        ' It is not necessary to specify the type of the arguments of a lambda
        ' This is handy when you want to reuse the lambda with different types

        Dim lambdaToString = Function(x) x.ToString()

        Dim integerStrings = Array.ConvertAll(New Integer() {1, 2, 3, 4}, lambdaToString)
        Assert.Equal(FILL_ME_IN, integerStrings)

        Dim doubleStrings = Array.ConvertAll(New Double() {1.5, 2.1, 3.14, 4.2}, lambdaToString)
        Assert.Equal(FILL_ME_IN, doubleStrings)
    End Sub
End Class
