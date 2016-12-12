Imports Xunit
Imports System.Text

Public Class AboutDelegates
    Inherits Koan

    Delegate Function BinaryOp(lhs As Integer, rhs As Integer) As Integer

    Private Class MyMath
        'Add has the same signature as BinaryOp
        Public Function Add(lhs As Integer, rhs As Integer) As Integer
            Return lhs + rhs
        End Function

        Public Shared Function Subtract(lhs As Integer, rhs As Integer) As Integer
            Return lhs - rhs
        End Function
    End Class

    <Koan(1)> _
    Public Sub DelegatesCanBeInstantiated()
        Dim math = New MyMath()
        Dim op = New BinaryOp(AddressOf math.Add)
        Assert.Equal(FILL_ME_IN, op.Method.Name)
        'You can also instantiate them like this:
        Dim op2 As BinaryOp = AddressOf math.Add
        Assert.Equal(FILL_ME_IN, op2.Method.Name)
    End Sub
    <Koan(2)> _
    Public Sub DelegatesCanReferenceStaticMethods()
        Dim op = New BinaryOp(AddressOf MyMath.Subtract)
        Assert.Equal(FILL_ME_IN, op.Method.Name)
    End Sub
    <Koan(3)> _
    Public Sub DelegatesCanBeAssigned()
        Dim Math = New MyMath()
        Dim op = New BinaryOp(AddressOf MyMath.Subtract)
        op = New BinaryOp(AddressOf Math.Add)
        Assert.Equal(FILL_ME_IN, op.Method.Name)
    End Sub
    <Koan(4)> _
    Public Sub MethodsCalledViaDelegate()
        Dim Math = New MyMath()
        Dim op = New BinaryOp(AddressOf Math.Add)
        Assert.Equal(FILL_ME_IN, op(3, 3))
    End Sub
    Private Sub PassMeTheDelegate(passed As BinaryOp)
        Assert.Equal(FILL_ME_IN, passed(3, 3))
    End Sub
    <Koan(5)> _
    Public Sub DelegatesCanBePassed()
        Dim Math = New MyMath()
        Dim op = New BinaryOp(AddressOf Math.Add)
        PassMeTheDelegate(op)
    End Sub
    <Koan(6)> _
    Public Sub MethodCanBePassedDirectly()
        Dim Math = New MyMath()
        PassMeTheDelegate(AddressOf Math.Add)
    End Sub
    <Koan(7)> _
    Public Sub DelegatesAreImmutable()
        'Like strings it looks like you can change what a delegate references, but really they are immutable objects
        Dim m = New MyMath()
        Dim a = New BinaryOp(AddressOf m.Add)
        Dim original = a
        Assert.Same(a, original)
        a = AddressOf MyMath.Subtract
        'a is now a different instance
        Assert.Same(a, original)
    End Sub
    Delegate Function Curry(val As Integer) As Integer
    Public Class FunctionalTricks
        Public Function Add5(x As Integer) As Integer
            Return x + 5
        End Function
        Public Function Add10(x As Integer) As Integer
            Return x + 10
        End Function
    End Class
    <Koan(8)> _
    Public Sub DelegatesHaveAnInvocationList()
        Dim f = New FunctionalTricks()
        Dim add5 = New Curry(AddressOf f.Add5)
        Dim add10 = New Curry(AddressOf f.Add10)
        'So far we've only seen one method attached to a delegate. 
        Assert.Equal(FILL_ME_IN, add5.GetInvocationList().Length)
        'However, you can attach multiple methods to a delegate
        'Don't forget to specify the type of the delegate.
        Dim adding5And10 As Curry = [Delegate].Combine(add5, add10)
        Assert.Equal(FILL_ME_IN, adding5And10.GetInvocationList().Length)
    End Sub
    <Koan(9)> _
    Public Sub OnlyLastResultReturned()
        Dim f = New FunctionalTricks()
        Dim add5 = New Curry(AddressOf f.Add5)
        Dim add10 = New Curry(AddressOf f.Add10)
        Dim adding5And10 As Curry = [Delegate].Combine(add5, add10)
        'Delegates may have more than one method attached, but only the result of the last method is returned.
        Assert.Equal(FILL_ME_IN, adding5And10(5))
    End Sub
    <Koan(10)> _
    Public Sub RemovingMethods()
        Dim f = New FunctionalTricks()
        Dim add5 = New Curry(AddressOf f.Add5)
        Dim add10 = New Curry(AddressOf f.Add10)
        Dim addingA As Curry = [Delegate].Combine(add5, add10)
        Assert.Equal(2, addingA.GetInvocationList().Length)
        'Remove Add5 from the invocation list
        Dim addingB As Curry = [Delegate].Remove(addingA, add10)
        Assert.Equal(FILL_ME_IN, addingB.GetInvocationList().Length)
        Assert.Equal(FILL_ME_IN, addingB.Method.Name)
    End Sub

    Private Sub AssertIntEqualsFourtyTwo(x As Integer)
        Assert.Equal(42, x)
    End Sub
    Private Sub AssertStringEqualsFourtyTwo(s As String)
        Assert.Equal("42", s)
    End Sub
    Private Sub AssertAddEqualsFourtyTwo(x As Integer, s As String)
        Dim y = Integer.Parse(s)
        Assert.Equal(42, x + y)
    End Sub
    <Koan(11)> _
    Public Sub BuiltInActionDelegateTakesInt()
        'With the release of generics in .Net 2.0 we got some delegates which will cover most of our needs. 
        'You will see them in the base class libraries, so knowing about them will be helpful. 
        'The first is Action(). Action() can take a variety of parameters and has a void return type.
        '  Public Delegate Sub Action(T)(obj As T)

        Dim i As Action(Of Integer) = AddressOf AssertIntEqualsFourtyTwo
        i(0) 'Replace 0 by the appropriate number
    End Sub
    <Koan(12)> _
    Public Sub BuiltInActionDelegateTakesString()
        ' Because the delegate is a template, it also works with any other type. 
        Dim s As Action(Of String) = AddressOf AssertStringEqualsFourtyTwo
        s("FILL_ME_IN")
    End Sub
    <Koan(13)> _
    Public Sub BuiltInActionDelegateIsOverloaded()
        'Action is an overloaded delegate so it can take more than one paramter
        Dim a As Action(Of Integer, String) = AddressOf AssertAddEqualsFourtyTwo
        a(12, "FILL_ME_IN")
    End Sub
    Public Class Seen
        Private _letters As StringBuilder = New StringBuilder()
        ReadOnly Property Letters() As String
            Get
                Return _letters.ToString()
            End Get
        End Property
        Public Sub Look(letter As Char)
            _letters.Append(letter)
        End Sub
    End Class
    <Koan(14)> _
    Public Sub ActionInTheBcl()
        'You will find Action used within the Base Class Library, often when iterating over a container.
        ' E.g. the second argument of Array.ForEach is Action<T>, for which we can use any method that takes a T.
        ' In this example we have a Array<char>, so we need a Action<char>, to which we can assign a 
        Dim greeting = "Hello world"
        Dim s = New Seen()

        Array.ForEach(Of Char)(greeting.ToCharArray(), AddressOf s.Look)

        Assert.Equal(FILL_ME_IN, s.Letters)
    End Sub

    Private Function IntEqualsFourtyTwo(x As Integer) As Boolean
        Return x.Equals(42)
    End Function
    Private Function StringEqualsFourtyTwo(s As String) As Boolean
        Return s.Equals("42")
    End Function
    <Koan(15)> _
    Public Sub BuiltInPredicateDelegateIntSatisfied()
        'The Predicate(Of T) delegate 
        '  Public Delegate Function Predicate(Of T)(T obj) As Boolean
        'Predicate allows you to codify a condition and pass it around. 
        'You use it to determine if an object satisfies some criteria. 

        Dim i As Predicate(Of Integer) = FILL_ME_IN
        Assert.True(i(42))
    End Sub
    <Koan(16)> _
    Public Sub BuiltInPredicateDelegateStringSatisfied()
        'Because it is a template, you can work with any type
        Dim s As Predicate(Of String) = FILL_ME_IN
        Assert.True(s("42"))

        'Predicate can have only one argument, so unlike Action() you cannot do this...
        'Dim a As Predicate(Of Integer, String) = FILL_ME_IN
        'Assert.True(a(42, "42"))
    End Sub

    Private Function StartsWithS(country As String) As Boolean
        Return country.StartsWith("S")
    End Function
    <Koan(17)> _
    Public Sub FindingWithPredicate()
        'Predicate can be used to find an element in an array
        Dim countries = New String() {"Greece", "Spain", "Uruguay", "Japan"}

        Assert.Equal(FILL_ME_IN, Array.Find(countries, AddressOf StartsWithS))
    End Sub

    Private Function IsInSouthAmerica(country As String) As Boolean
        Dim countries = New String() {"Argentina", "Bolivia", "Brazil", "Chile", "Colombia", "Ecuador", "French Guiana", "Guyana", "Paraguay", "Peru", "Suriname", "Uruguay", "Venezuela"}
        Return countries.Contains(country)
    End Function
    <Koan(18)> _
    Public Sub ValidationWithPredicate()
        'Predicate can also be used when verifying 
        Dim countries = New String() {"Greece", "Spain", "Uruguay", "Japan"}

        Assert.Equal(FILL_ME_IN, Array.TrueForAll(countries, AddressOf IsInSouthAmerica))
    End Sub

    Private Function FirstMonth() As String
        Return "January"
    End Function
    Private Function Add(x As Integer, y As Integer) As Integer
        Return x + y
    End Function
    <Koan(19)> _
    Public Sub FuncWithNoParameters()
        'The Func<> delegate 
        '  public delegate TResult Func<T, TResult>(T arg)
        'Is very similar to the Action<> delegate. However, Func<> does not require any parameters, while does require returns a value.
        'The last type parameter specifies the return type. If you only specify a single 
        'type, Func<int>, then the method takes no paramters and returns an int.
        'If you specify more than one parameter, then you are specifying the paramter types as well.

        Dim d As Func(Of String) = AddressOf FirstMonth
        Assert.Equal(FILL_ME_IN, d())
    End Sub
    <Koan(20)> _
    Public Sub FunctionReturnsInt()
        'Like Action<>, Func<> is overloaded and can take a variable number of parameters.
        'The first type parameters define the parameter types and the last one is the return type. So the following matches
        'a method which takes two int parameters and returns a string.
        Dim a As Func(Of Integer, Integer, Integer) = AddressOf Add
        Assert.Equal(FILL_ME_IN, a(1, 1))
    End Sub

    Public Class Car
        Public Property Make As String
        Public Property Model As String
        Public Property Year As Integer
        Sub New(make_ As String, model_ As String, year_ As Integer)
            Make = make_
            Model = model_
            Year = year_
        End Sub
    End Class
    Private Function SortByModel(lhs As Car, rhs As Car) As Integer
        Return lhs.Model.CompareTo(rhs.Model)
    End Function
    <Koan(21)> _
    Public Sub SortingWithComparison()
        'You could make classes sortable by implementing IComparable or IComparer. But the Comparison() delegate makes it easier
        '	public delegate Integer Comparison(T)(x As T, y As T)
        'All you need is a method which takes two of the same type and returns -1, 0, or 1 depending upon what order they should go in.
        Dim cars = New Car() {New Car("Alfa Romero", "GTV-6", 1986), New Car("BMC", "Mini", 1959)}
        Dim by As Comparison(Of Car) = AddressOf SortByModel
        Array.Sort(cars, by)

        Assert.Equal(FILL_ME_IN, cars(0).Model)
    End Sub

    Private Function Stringify(x As Integer) As String
        Return x.ToString()
    End Function
    <Koan(22)> _
    Public Sub ChangingTypesWithConverter()
        'The Converter() delegate
        '	public delegate U Converter(T, U)(from As T)
        'Can be used to change an object from one type to another
        Dim numbers = New Integer() {1, 2, 3, 4}
        Dim c As Converter(Of Integer, String) = AddressOf Stringify

        Dim result = Array.ConvertAll(numbers, c)

        Assert.Equal(FILL_ME_IN, result)
    End Sub
End Class
