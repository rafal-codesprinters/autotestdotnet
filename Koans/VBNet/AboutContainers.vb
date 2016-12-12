Imports System.Collections
Imports System.Collections.Generic
Imports Xunit

Public Class AboutContainers
    Inherits Koan
    <Koan(1)> _
    Public Sub ArrayListSizeIsDynamic()
        'When you worked with Array, the fact that Array is fixed size was glossed over.
        'The size of an array cannot be changed after you allocate it. To get around that
        'you need a class from the System.Collections namespace such as ArrayList
        Dim list As New ArrayList()
        Assert.Equal(FILL_ME_IN, list.Count)

        list.Add(42)
        Assert.Equal(FILL_ME_IN, list.Count)
    End Sub
    <Koan(2)> _
    Public Sub ArrayListHoldsObjects()
        Dim list As New ArrayList()
        Dim method As System.Reflection.MethodInfo = list.[GetType]().GetMethod("Add")
        Assert.Equal(GetType(FillMeIn), method.GetParameters()(0).ParameterType)
    End Sub
    <Koan(3)> _
    Public Sub MustCastWhenRetrieving()
        'There are a few problems with ArrayList holding object references. The first 
        'is that you must cast the items you fetch back to the original type.
        Dim list As New ArrayList()
        list.Add(42)
        Dim x As Integer = 0
        'x = CType(list(0), Integer)
        Assert.Equal(x, 42)
    End Sub
    <Koan(4)> _
    Public Sub ArrayListIsNotStronglyTyped()
        'Having to cast everywhere is tedious. But there is also another issue lurking
        'ArrayList can hold more than one type. 
        Dim list As New ArrayList()
        list.Add(42)
        list.Add("fourty two")
        Assert.Equal(FILL_ME_IN, list(0))
        Assert.Equal(FILL_ME_IN, list(1))

        'While there are a few cases where it could be nice, instead what it means is that 
        'anytime your code works with an array list you have to check that the element is 
        'of the type you expect.
    End Sub
    <Koan(5)> _
    Public Sub Boxing()
        Dim s As Short = 5
        Dim os As Object = s
        Assert.Equal(s.[GetType](), os.[GetType]())
        Assert.Equal(s, os)

        'While this it is true that everything is an object and all the above passes. Not everything is quite as it seems.
        'Under the covers .Net allocates memory for all value type objects (int, double, bool,...) on the stack. This is 
        'considerably more efficient than a heap allocation. .Net also has the ability to put a value type onto the heap.
        '(for calling methods and other reasons). The process of putting stack data into the heap is called "boxing" the 
        'process of taking the value type off the heap is called "unboxing". We won't go into the details (see Jeffrey 
        'Richter's book if you want details). This subject comes up because every time you put a value type into an 
        'ArrayList it must be boxed. Every time you read it from the ArrayList it must be unboxed. This can be a significat
        'cost.
    End Sub
    <Koan(6)> _
    Public Sub ABetterDynamicSizeContainer()
        'ArrayList is a .Net 1.0 container. With .Net 2.0 generics were introduced and with it a new set of collections in
        'System.Collections.Generic The array like container is List(T). List(T) (read "list of T") is a generic class. 
        'The "T" in the definition of List(T) is the type argument. You cannot declare an instance of List(T) without also
        'supplying a type in place of T.
        Dim list = New List(Of Integer)()
        Assert.Equal(FILL_ME_IN, list.Count)

        list.Add(42)
        Assert.Equal(FILL_ME_IN, list.Count)

        'Now just like Integer(), you can have a type safe dynamic sized container
        'list.Add("fourty two") '<--Unlike ArrayList this is illegal.

        'List(T) also solves the boxing/unboxing issues of ArrayList. Unfortunately, you'll have to take Microsoft's word for it
        'as I can't find a way to prove it without some ugly MSIL beyond the scope of these Koans.
    End Sub
    Public Class Widget
    End Class
    <Koan(7)> _
    Public Sub ListWorksWithAnyType()
        'Just as with Array, list will work with any type
        Dim list As New List(Of Widget)()
        list.Add(New Widget())
        Assert.Equal(FILL_ME_IN, list.Count)
    End Sub
    <Koan(8)> _
    Public Sub InitializingWithValues()
        'Like array you can create a list with an initial set of values easily
        Dim list = New List(Of Integer)() From { _
            1, _
            2, _
            3 _
        }
        Assert.Equal(FILL_ME_IN, list.Count)
    End Sub
    <Koan(9)> _
    Public Sub AddMultipleItems()
        'You can add multiple items to a list at once
        Dim list As New List(Of Integer)()
        list.AddRange(New Integer() {1, 2, 3})
        Assert.Equal(FILL_ME_IN, list.Count)
    End Sub
    <Koan(10)> _
    Public Sub RandomAccess()
        'Just as with array, you can use the subscript notation to access any element in a list.
        ' No casting is needed with generic containers, because the compiler knows it can only have one type,
        ' in this case int:
        Dim list As New List(Of Integer)() From { _
            5, _
            6, _
            7 _
        }
        Assert.Equal(FILL_ME_IN, list(2))
    End Sub
    <Koan(11)> _
    Public Sub BeyondTheLimits()
        Dim list As New List(Of Integer)() From { _
            1, _
            2, _
            3 _
        }
        'You cannot attempt to get data that doesn't exist
        Assert.Throws(GetType(FillMeIn), Sub()
                                             Dim x As Integer = list(3)
                                         End Sub)
    End Sub
    <Koan(12)> _
    Public Sub ConvertingToFixedSize()
        Dim list As New List(Of Integer)() From { _
            1, _
            2, _
            3 _
        }
        Assert.Equal(FILL_ME_IN, list.ToArray())
    End Sub
    <Koan(13)> _
    Public Sub InsertingInTheMiddle()
        Dim list As New List(Of Integer)() From { _
            1, _
            2, _
            3 _
        }
        list.Insert(1, 6)
        Assert.Equal(FILL_ME_IN, list.ToArray())
    End Sub
    <Koan(14)> _
    Public Sub RemovingItems()
        ' The method Remove removes the first occurance of its argument
        Dim list As New List(Of Integer)() From { _
            2, _
            1, _
            2, _
            3 _
        }
        list.Remove(2)
        Assert.Equal(FILL_ME_IN, list.ToArray())
    End Sub
    <Koan(15)> _
    Public Sub RemovingItemsAtACertainPosition()
        ' The method RemoveAt removes the element at the given position
        Dim list As New List(Of Integer)() From { _
            2, _
            1, _
            2, _
            3 _
        }
        list.RemoveAt(2)
        Assert.Equal(FILL_ME_IN, list.ToArray())
    End Sub

    <Koan(16)> _
    Public Sub OldStack()
        ' Stack is another container type that can contain elements of different types
        Dim array = New Integer() {1, 2}
        Dim stack As New Stack(array)
        stack.Push("last")
        Assert.Equal(FILL_ME_IN, stack.ToArray())
    End Sub

    <Koan(17)> _
    Public Sub StackPushPop()
        ' The generic version of stack is restricted to having elements of only one type
        Dim stack = New Stack(Of Integer)()
        Assert.Equal(FILL_ME_IN, stack.Count)

        stack.Push(42)
        Assert.Equal(FILL_ME_IN, stack.Count)

        Dim x As Integer = stack.Pop()
        Assert.Equal(FILL_ME_IN, x)

        Assert.Equal(FILL_ME_IN, stack.Count)
    End Sub
    <Koan(18)> _
    Public Sub StackOrder()
        ' Stack is a first in, last out container (FILO).
        Dim stack = New Stack(Of Integer)()
        stack.Push(1)
        stack.Push(2)
        stack.Push(3)

        Assert.Equal(FILL_ME_IN, stack.Pop())
        Assert.Equal(FILL_ME_IN, stack.Pop())
        Assert.Equal(FILL_ME_IN, stack.Pop())
        Assert.Equal(FILL_ME_IN, stack.Count)
    End Sub
    <Koan(19)> _
    Public Sub PeekingIntoAStack()
        ' You can peek at the element that was last added, without removing it as Pop would do.
        Dim stack = New Stack(Of Integer)()
        stack.Push(1)
        stack.Push(2)
        Assert.Equal(FILL_ME_IN, stack.Peek())
        Assert.Equal(FILL_ME_IN, stack.Count)
    End Sub
    <Koan(20)> _
    Public Sub Queue()
        ' A Queue is a first in first out container (FIFO).
        Dim queue As New Queue(Of String)()
        queue.Enqueue("one")
        queue.Enqueue("two")
        queue.Enqueue("three")

        Assert.Equal(FILL_ME_IN, queue.Dequeue())
        Assert.Equal(FILL_ME_IN, queue.Dequeue())
        Assert.Equal(FILL_ME_IN, queue.Dequeue())
        Assert.Equal(FILL_ME_IN, queue.Count)
    End Sub
    <Koan(21)> _
    Public Sub PeekingIntoAQueue()
        ' Peek allows you to see which element Dequeue would return, but without removing it.
        Dim queue As New Queue(Of String)()
        queue.Enqueue("one")
        queue.Enqueue("two")
        Assert.Equal(FILL_ME_IN, queue.Peek())
    End Sub
    <Koan(22)> _
    Public Sub AddingToADictionary()
        'Dictionary<TKey, TValue> is .Net's key value store. The key and the value do not need to be the same types.
        Dim dictionary As New Dictionary(Of Integer, String)()
        Assert.Equal(FILL_ME_IN, dictionary.Count)
        dictionary(1) = "one"
        Assert.Equal(FILL_ME_IN, dictionary.Count)
    End Sub
    <Koan(23)> _
    Public Sub AccessingData()
        Dim dictionary As New Dictionary(Of String, String)()
        dictionary("one") = "uno"
        dictionary("two") = "dos"
        'The most common way to locate data is with the subscript notation.
        Assert.Equal(FILL_ME_IN, dictionary("one"))
        Assert.Equal(FILL_ME_IN, dictionary("two"))
    End Sub
    <Koan(24)> _
    Public Sub AccessingDataNotAdded()
        Dim dictionary As New Dictionary(Of String, String)()
        dictionary("one") = "uno"
        Assert.Throws(GetType(FillMeIn), Sub()
                                             Dim s As String = dictionary("two")
                                         End Sub)
    End Sub
    <Koan(25)> _
    Public Sub CatchingMissingData()
        'To deal with the throw when data is not there, you could wrap the data access in a try/catch block...
        Dim dictionary As New Dictionary(Of String, String)()
        dictionary("one") = "uno"
        Dim result As String
        Try
            result = dictionary("two")
        Catch generatedExceptionName As Exception
            result = "dos"
        End Try
        Assert.Equal(FILL_ME_IN, result)
    End Sub
    <Koan(26)> _
    Public Sub PreCheckForMissingData()
        Dim dictionary As New Dictionary(Of String, String)()
        dictionary("one") = "uno"
        Dim result As String
        If dictionary.ContainsKey("two") Then
            result = dictionary("two")
        Else
            result = "dos"
        End If
        Assert.Equal(FILL_ME_IN, result)
    End Sub
    <Koan(27)> _
    Public Sub TryGetValueForMissingData()
        Dim dictionary As New Dictionary(Of String, String)()
        dictionary("one") = "uno"
        Dim result As String
        If Not dictionary.TryGetValue("two", result) Then
            result = "dos"
        End If
        Assert.Equal(FILL_ME_IN, result)
    End Sub
    <Koan(28)> _
    Public Sub InitializingADictionary()
        'Although it is not common, you can initialize a dictionary...
        Dim dictionary = New Dictionary(Of String, String)() From { _
            {"one", "uno"}, _
            {"two", "dos"} _
        }
        Assert.Equal(FILL_ME_IN, dictionary("one"))
        Assert.Equal(FILL_ME_IN, dictionary("two"))
    End Sub
    <Koan(29)> _
    Public Sub ModifyingData()
        Dim dictionary As New Dictionary(Of String, String)()
        dictionary("one") = "uno"
        dictionary("two") = "dos"
        dictionary("one") = "ein"
        Assert.Equal(FILL_ME_IN, dictionary("one"))
    End Sub
    <Koan(30)> _
    Public Sub KeyExists()
        Dim dictionary As New Dictionary(Of String, String)()
        dictionary("one") = "uno"
        Assert.Equal(FILL_ME_IN, dictionary.ContainsKey("one"))
        Assert.Equal(FILL_ME_IN, dictionary.ContainsKey("two"))
    End Sub
    <Koan(31)> _
    Public Sub ValueExists()
        Dim dictionary As New Dictionary(Of String, String)()
        dictionary("one") = "uno"
        Assert.Equal(FILL_ME_IN, dictionary.ContainsValue("uno"))
        Assert.Equal(FILL_ME_IN, dictionary.ContainsValue("dos"))
    End Sub
    <Koan(32)> _
    Public Sub f()
        Dim one As New Dictionary(Of String, Integer)()
        one("jim") = 53
        one("amy") = 20
        one("dan") = 23

        Dim two As New Dictionary(Of String, Integer)()
        two("jim") = 54
        two("jenny") = 26

        For Each item As KeyValuePair(Of String, Integer) In two
            one(item.Key) = item.Value
        Next

        Assert.Equal(FILL_ME_IN, one("jim"))
        Assert.Equal(FILL_ME_IN, one("jenny"))
        Assert.Equal(FILL_ME_IN, one("amy"))
    End Sub
End Class
