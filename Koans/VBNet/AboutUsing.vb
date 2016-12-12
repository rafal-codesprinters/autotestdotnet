Imports Xunit

Public Class AboutUsing
    Inherits Koan
    Public Shared disposeCalled As Boolean = False
    Private Class DisposableClass
        Implements IDisposable
        Public value As String = "abc"
        Public Sub New()
            disposeCalled = False
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            disposeCalled = True
            value = Nothing
        End Sub
    End Class

    <Koan(1)> _
    Public Sub UsingCallsDisposeAtTheEndOfTheBlock()
        ' Objects of classes that implement IDisposable can be used in a using statement which is followed by a block.
        ' This construction makes sure that Dispose is called when the block is left.
        ' Dispose usually does cleanup of the object, e.g. to free resources.
        ' An example of such a class is File, whose Dispose method closes the file.
        Using obj As New DisposableClass()
            Assert.Equal(FILL_ME_IN, disposeCalled)
        End Using
        Assert.Equal(FILL_ME_IN, disposeCalled)
    End Sub

    <Koan(2)> _
    Public Sub UsingCallsDisposeAtTheEndOfTheBlockEvenIfAnExceptionIsThrown()
        Try
            Using obj As New DisposableClass()
                Assert.Equal(FILL_ME_IN, disposeCalled)
            End Using
        Catch generatedExceptionName As Exception
            Assert.Equal(FILL_ME_IN, disposeCalled)
        End Try
    End Sub

    <Koan(3)> _
    Public Sub TheUsedObjectCannotBeReassigned()
        Using obj As New DisposableClass()
            ' This would be illegal:
            'obj = new MyClass()

            ' But its members can be changed:
            obj.value = "xyz"
            Assert.Equal(FILL_ME_IN, obj.value)
        End Using
    End Sub

    <Koan(4)> _
    Public Sub TheUsedObjectShouldNotBeUsedAfterwards()
        ' It is possible to use an object in the using statement that was created before,
        ' but it is not recommended because the object is probably no longer usable after the using block.
        Dim obj As New DisposableClass()
        Using obj
        End Using
        Assert.Equal(FILL_ME_IN, obj.value)
    End Sub
End Class
