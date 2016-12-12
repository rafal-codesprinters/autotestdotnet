Imports Xunit

Public Class AboutControlStatements
    Inherits Koan
    <Koan(1)> _
    Public Sub SingleLineIfThenStatements()
        Dim b As Boolean = False
        If True Then b = True
        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(2)> _
    Public Sub MultiLineIfStatements()
        Dim b As Boolean = False
        If True Then
            b = True
        End If
        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(3)> _
    Public Sub SingleLineIfThenElse()
        Dim b As Boolean
        If True Then b = True Else b = False
        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(4)> _
    Public Sub MultiLineIfThenElseStatement()
        Dim b As Boolean
        If True Then
            b = True
        Else
            b = False
        End If
        Assert.Equal(FILL_ME_IN, b)
    End Sub


    <Koan(5)> _
    Public Sub TernaryOperators()
        Assert.Equal(FILL_ME_IN, (If(True, 1, 0)))
        Assert.Equal(FILL_ME_IN, (If(False, 1, 0)))
    End Sub

    'This is out of place for control statements, but necessary for Koan 8
    <Koan(6)> _
    Public Sub NullableTypes()
        Dim i As Integer = 0
        'i = null 'You can't do this
        Dim nullableInt As Integer? = Nothing 'but you can do this
        Assert.NotNull(FILL_ME_IN)
        Assert.Null(FILL_ME_IN)
    End Sub

    <Koan(7)> _
    Public Sub AlternateNullableTypesSyntax()
        Dim i As Integer = 0
        Dim nullableInt As Nullable(Of Integer) = Nothing 'but you can do this
        Assert.NotNull(FILL_ME_IN)
        Assert.Null(FILL_ME_IN)
    End Sub

    'The If function with two arguments returns the first argument if it is not Nothing, otherwise the second argument
    <Koan(8)> _
    Public Sub AssignIfNullOperator()
        Dim nullableInt As Integer? = Nothing
        Dim x As Integer = If(nullableInt, 42)
        Assert.Equal(FILL_ME_IN, x)
    End Sub

    <Koan(9)> _
    Public Sub AssignIfNullOperatorWhenNotNull()
        Dim nullableInt As Integer? = Nothing
        nullableInt = 7
        Dim x As Integer = If(nullableInt, 42)
        Assert.Equal(FILL_ME_IN, x)
    End Sub

    'Note that this is very different from the If function with three arguments,
    'which returns the third argument if the first argument is Nothing (or False), otherwise the second argument
    <Koan(10)> _
    Public Sub TernaryOperatorWithNullable()
        Dim nullableInt As Integer? = Nothing
        Dim x As Integer = If(nullableInt, 42, 45)
        Assert.Equal(FILL_ME_IN, x)
    End Sub

    <Koan(11)> _
    Public Sub IsOperators()
        Dim isKoan As Boolean = False
        Dim isAboutControlStatements As Boolean = False
        Dim isAboutMethods As Boolean = False
        Dim myType = Me
        If TypeOf myType Is Koan Then
            isKoan = True
        End If
        If TypeOf myType Is AboutControlStatements Then
            isAboutControlStatements = True
        End If
        ' The following is not possible. 
        ' In C# it would generate a warning.
        ' In VB.Net it is an Error
        '
        'If TypeOf myType Is AboutMethods Then
        '    isAboutMethods = True
        'End If
        Assert.Equal(FILL_ME_IN, isKoan)
        Assert.Equal(FILL_ME_IN, isAboutControlStatements)
        Assert.Equal(FILL_ME_IN, isAboutMethods)
    End Sub

    <Koan(12)> _
    Public Sub WhileStatement()
        Dim i As Integer = 1
        Dim result As Integer = 1
        While i <= 3
            result = result + i
            i += 1
        End While
        Assert.Equal(FILL_ME_IN, result)
    End Sub

    <Koan(13)> _
    Public Sub ExitStatement()
        Dim i As Integer = 1
        Dim result As Integer = 1
        While True
            If i > 3 Then
                Exit While
            End If
            result = result + i
            i += 1
        End While
        Assert.Equal(FILL_ME_IN, result)
    End Sub

    <Koan(14)> _
    Public Sub ContinueStatement()
        Dim i As Integer = 0
        Dim result = New List(Of Integer)()
        While i < 10
            i += 1
            If (i Mod 2) = 0 Then
                Continue While
            End If
            result.Add(i)
        End While
        Assert.Equal(FILL_ME_IN, result)
    End Sub

    <Koan(15)> _
    Public Sub ForStatement()
        Dim list = New List(Of String)({"fish", "and", "chips"})
        Dim i As Integer = 0
        While i < list.Count
            list(i) = (list(i).ToUpper())
            i += 1
        End While
        Assert.Equal(FILL_ME_IN, list)
    End Sub

    <Koan(16)> _
    Public Sub ForEachStatement()
        Dim list = New List(Of String)({"fish", "and", "chips"})
        Dim finalList = New List(Of String)()
        For Each item As String In list
            finalList.Add(item.ToUpper())
        Next
        Assert.Equal(FILL_ME_IN, list)
        Assert.Equal(FILL_ME_IN, finalList)
    End Sub

    <Koan(17)> _
    Public Sub ModifyingACollectionDuringForEach()
        Dim list = New List(Of String)({"fish", "and", "chips"})
        Try
            For Each item As String In list
                list.Add(item.ToUpper())
            Next
        Catch ex As Exception
            Assert.Equal(GetType(FillMeIn), ex.GetType())
        End Try
    End Sub

    <Koan(18)> _
    Public Sub CatchingModificationExceptions()
        Dim whoCaughtTheException As String = "No one"
        Dim list = New List(Of String)({"fish", "and", "chips"})
        Try
            For Each item As String In list
                Try
                    list.Add(item.ToUpper())
                Catch
                    whoCaughtTheException = "When we tried to Add it"
                End Try
            Next
        Catch
            whoCaughtTheException = "When we tried to move to the next item in the list"
        End Try
        Assert.Equal(FILL_ME_IN, whoCaughtTheException)
    End Sub

    <Koan(19)> _
    Public Sub Switch()
        ' A switch statement allows you to execute one of several blocks of code
        ' depending on the value of an integer, give after the case keyword.

        Dim a As Integer = 2, b As Integer = 0
        Select Case a
            Case 1
                b = 10
            Case 2
                b = 11
        End Select

        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(20)> _
    Public Sub SwitchWithFallThrough()
        ' A block can handle multiple values of the variable,
        ' by separating the values with commas.

        Dim a As Integer = 1, b As Integer = 0
        Select Case a
            Case 1, 2
                b = 11
            Case 3, 4
                b = 12
        End Select

        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(21)> _
    Public Sub SwitchWithARange()
        ' A block can handle a range of values.

        Dim a As Integer = 4, b As Integer = 0
        Select Case a
            Case 1 To 4
                b = 11
            Case 5 To 8
                b = 12
        End Select

        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(22)> _
    Public Sub SwitchWithComparisons()
        ' A block can be selected using a comparison.
        ' If more than one matches, the first is used.

        Dim a As Integer = 4, b As Integer = 0
        Select Case a
            Case Is < 4
                b = 11
            Case Is < 8
                b = 12
        End Select

        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(23)> _
    Public Sub CombinationsOfSelectors()
        ' You can mix values, ranges and comparisons by separating them with commas.

        Dim a As Integer = 7, b As Integer = 0
        Select Case a
            Case 1 To 3, 7, Is > 10
                b = 11
            Case 4, Is < 0
                b = 12
        End Select

        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(24)> _
    Public Sub SwitchWithExitSelect()
        ' A block can be selected using a comparison.
        ' If more than one matches, the first is used.

        Dim cond As Boolean = True
        Dim a As Integer = 4, b As Integer = 0
        Select Case a
            Case 4
                b = 10
                If cond Then
                    Exit Select
                End If
                b = 11
            Case 8
                b = 12
        End Select

        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(25)> _
    Public Sub SwitchWithCaseElse()
        ' If the value is not matched, the code after Case Else
        ' is executed, if there is one.
        Dim a As Integer = 3, b As Integer = 0
        Select Case a
            Case 1
                b = 10
            Case 2
                b = 11
            Case Else
                b = 12
        End Select

        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(26)> _
    Public Sub SwitchWithStrings()
        ' Instead of integers you can also use strings.
        Dim a As String = "a"
        Dim b As Integer = 0
        Select Case a
            Case "a"
                b = 10
            Case "b"
                b = 11
        End Select

        Assert.Equal(FILL_ME_IN, b)
    End Sub

    Public Class Customer
        Public Property Name As String
        Public Property City As String

        Public Property Comments As New List(Of String)
    End Class

    <Koan(27)> _
    Public Sub UsingWith()
        Dim theCustomer As New Customer

        With theCustomer
            .Name = "Coho Vineyard"
            .City = "Redmond"
        End With

        Assert.Equal(FILL_ME_IN, theCustomer.City)
    End Sub

    <Koan(28)> _
    Public Sub UsingNestedWith()
        Dim theCustomer As New Customer

        With theCustomer
            .Name = "Coho Vineyard"
            .City = "Redmond"
            With .Comments
                .Add("comment1")
                .Add("comment2")
            End With
        End With

        Assert.Equal(FILL_ME_IN, theCustomer.Comments.Count)
    End Sub

    Structure Point
        Dim x As Integer
        Dim y As Integer
    End Structure

    Function GetThePoint() As Point
        Dim p As Point
        p.x = 1
        p.y = 2
        Return p
    End Function

    <Koan(29)> _
    Public Sub UsingWithOnAStructure()
        ' You can also use a structure...
        Dim p As New Point

        With p
            .x = 3
            .y = 4
        End With

        Assert.Equal(FILL_ME_IN, p.x)

        ' but if the structure is an expression more complex than a single variable
        ' you cannot assign to its fields
        Dim px As Integer
        With GetThePoint()
            '.x = 3 ' This would not work
            px = .x
        End With

        Assert.Equal(FILL_ME_IN, px)
    End Sub

    ' The Yield keyword makes a function act as an iterator.
    ' This creates an interesting control flow.
    ' Every time 'Yield' is encountered, the control goes back
    ' to the calling function. When the next element is requested,
    ' control goes back to the function right after the 'Yield' statement.
    ' All variables have kept their values, because it runs on a separate stack.
    ' Note that you need to add the keyword Iterator to the function definition.

    Private Iterator Function Fibonacci1(n As Integer) As System.Collections.Generic.IEnumerable(Of Integer)
        Dim f1 As Integer = 0, f2 As Integer = 1
        For i As Integer = 0 To n - 1
            Dim f3 As Integer = f2 + f1
            f1 = f2
            f2 = f3
            Yield f1
        Next
    End Function

    <Koan(30)> _
    Public Sub Yield()
        Dim fibonacci = New List(Of Integer)()
        For Each f As Integer In Fibonacci1(5)
            fibonacci.Add(f)
        Next
        Assert.Equal(New List(Of Integer)() From {}, fibonacci)
    End Sub

    ' Return allows you to stop the iteration
    Private Iterator Function Fibonacci2(n As Integer) As System.Collections.Generic.IEnumerable(Of Integer)
        Dim f1 As Integer = 0, f2 As Integer = 1
        Dim i As Integer = 0
        While True
            Dim f3 As Integer = f2 + f1
            f1 = f2
            f2 = f3
            Yield f1

            i += 1
            If i = n Then
                Return
            End If
        End While
    End Function

    <Koan(31)> _
    Public Sub YieldAndReturn()
        Dim fibonacci = New List(Of Integer)()
        For Each f As Integer In Fibonacci2(5)
            fibonacci.Add(f)
        Next
        Assert.Equal(New List(Of Integer)() From {}, fibonacci)
    End Sub

    ' A property can also be an iterator.
    Public ReadOnly Iterator Property FiveFibonacciNumbers _
    As System.Collections.Generic.IEnumerable(Of Integer)
        Get
            Dim f1 As Integer = 0, f2 As Integer = 1
            For i As Integer = 0 To 4
                Dim f3 As Integer = f2 + f1
                f1 = f2
                f2 = f3
                Yield f1
            Next
        End Get
    End Property

    <Koan(32)> _
    Public Sub YieldInAProperty()
        Dim fibonacci = New List(Of Integer)()
        For Each f As Integer In FiveFibonacciNumbers
            fibonacci.Add(f)
        Next
        Assert.Equal(New List(Of Integer)() From {}, fibonacci)
    End Sub
End Class
