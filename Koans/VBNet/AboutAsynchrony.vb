Imports System.Threading
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports Xunit
Imports System.IO

Class AboutAsynchrony
    Inherits Koan
    Private Async Function AsyncReturnOne() As Task(Of Integer)
        Return 1
    End Function

    <Koan(1)> _
    Public Async Function CallAsyncMethod() As Task
        ' Asynchronous methods are methods whose execution does not necessarily
        ' run sequentially. When you call such a method it returns a
        ' Task(Of T) object as soon as it reaches an Await statement, which means it
        ' is waiting for something to finish, so execution can go on
        ' doing other things, e.g. when it has to wait when data is read
        ' from or written to a file or socket.
        ' An asynchronous method has the keyword Async in front of it and returns
        ' a Task(Of T) object. To get its value you call 'Await task',
        ' which will cause it to wait until the method reaches its end.

        Dim task As Task(Of Integer) = AsyncReturnOne()

        ' Here we could do other work.

        ' When we're done with the other work, or need the value that the
        ' method returns, we call this:
        Dim a As Integer = Await task

        Assert.Equal(FILL_ME_IN, a)

        ' AsyncReturnOne is an overly simplistic method, it actually doesn't make use of
        ' asynchrony, because all the code can be executed directly, but we'll see better examples.
    End Function

    Private Async Function ReturnSumAsync(a As Integer, b As Integer) As Task(Of Integer)
        Return a + b
    End Function

    <Koan(2)> _
    Public Async Function AsyncMethodsCanHaveArguments() As Task
        Dim task As Task(Of Integer) = ReturnSumAsync(2, 2)

        ' Here we could do other work.

        Dim a As Integer = Await task

        Assert.Equal(FILL_ME_IN, a)
    End Function

    Private Async Function StoreOrder(order As List(Of Integer)) As Task
        order.Add(2)

        ' Next line is necessary to make the asynchronous method interrupt its flow.
        ' We'll see later what it does.
        Await Task.Run(Sub()
                       End Sub)
        order.Add(4)
    End Function

    <Koan(3)> _
    Public Async Function LetsCheckTheOrder() As Task
        ' It is important to understand how the excution flow goes.
        ' When an asynchronous method encounters an Await statement,
        ' the execution goes back to the calling function.
        ' When the calling function awaits the task later,
        ' execution flow goes back to the called function.

        Dim order = New List(Of Integer)()
        order.Add(1)
        Dim task As Task = StoreOrder(order)
        order.Add(3)

        ' Here we could do other work.

        Await task

        order.Add(5)

        ' Note that occasionally the order is different from what you expect,
        ' due to mysterious reasons, so don't be surprised if this usually
        ' passes but sometimes not.
        Assert.Equal(New List(Of Integer)() From {0, 0, 0, 0, 0}, order)
    End Function

    Private Async Function ReadFile() As Task(Of [String])
        Dim SourceReader As StreamReader = File.OpenText("..\..\..\VBNet\AboutAsynchrony.vb")
        Return Await SourceReader.ReadToEndAsync()
    End Function

    <Koan(4)> _
    Public Async Function CallReadFile() As Task
        ' Here is a more useful example. When the function ReadFile
        ' starts to read the file, it has to wait for the filesystem to
        ' access the disk. In the meantime the CPU can go on doing other
        ' things in the calling function.
        ' There are many more such methods that do this, whose name by convention
        ' end in Async.

        Dim src As String = Await ReadFile()

        ' Here we could do other things.

        Assert.Equal(FILL_ME_IN, src.Substring(0, 7))
    End Function

    Private Async Function ThrowAsync() As Task(Of Integer)
        Throw New Exception()
    End Function

    <Koan(5)> _
    Public Async Function TasksStoreTheExceptionIfOneIsThrown() As Task
        ' If an exception is thrown in the Asynchronous method,
        ' it is stored in the Task object that is returned.
        ' It is thrown again when you wait for it.

        Dim task As Task(Of Integer) = ThrowAsync()

        ' Here we could do other work.

        Dim hasThrown As Boolean = False
        Try
            Dim a As Integer = Await task
        Catch exc As Exception
            hasThrown = True
        End Try

        Assert.Equal(FILL_ME_IN, hasThrown)
    End Function

    Private a As Integer

    Private Async Function SetA() As Task
        a = 1
    End Function

    <Koan(6)> _
    Public Async Function AsyncMethodsDontHaveToReturnSomething() As Task
        ' Asynchronous methods don't have to have a return statement.
        ' In that case they return a simple Task object instead of Task(Of T),
        ' on which you can also wait.

        a = 0
        Dim t As Task = SetA()

        ' Here we could do other things.

        Await t

        Assert.Equal(FILL_ME_IN, a)
    End Function

    Private Async Sub SetAWithoutATask()
        a = 1
    End Sub

    <Koan(7)> _
    Public Async Function AsyncMethodsAreOfTheFireAndForgetTypeIfTheyReturnVoid() As Task
        ' Asynchronous methods that don't return something
        ' can also be a Sub instead of a Task object.
        ' In that case you cannot wait for it to complete.
        ' This can be dangerous because if an exception is thrown in the
        ' asynchronous method, you cannot catch it.

        a = 0
        SetAWithoutATask()

        ' Here we could do other things.

        ' To be sure that it completes, we wait a short time.
        Thread.Sleep(10)

        Assert.Equal(FILL_ME_IN, a)
    End Function

    Private theThreadId As Integer

    Private Async Function ReturnOneAndStoreThreadAsync() As Task(Of Integer)
        theThreadId = Thread.CurrentThread.ManagedThreadId
        Return 1
    End Function

    <Koan(8)> _
    Public Async Function AsyncMethodsRunOnTheSameThread() As Task
        ' Asynchronous methods are not run on a separate thread.
        ' The code that executes the compiled byte code just schedules
        ' other code to execute.

        Dim task As Task(Of Integer) = ReturnOneAndStoreThreadAsync()

        Dim a As Integer = Await task

        Assert.Equal(theThreadId = Thread.CurrentThread.ManagedThreadId, FILL_ME_IN)
    End Function

    Private Async Function ReturnCurrentThreadAsync() As Task(Of Integer)
        Dim t As Task(Of Integer) = Task.Run(Function()
                                                 Return Thread.CurrentThread.ManagedThreadId
                                             End Function)
        Return Await t
    End Function

    <Koan(9)> _
    Public Async Function ButAsyncMethodsCanBeMadeToRunOnAnotherThread() As Task
        ' If the purpose of an asynchronous method is to run code on a separate
        ' thread so you can take advantage of multiple cores,
        ' you can do so with Task.Run.

        Dim task1 As Task(Of Integer) = ReturnCurrentThreadAsync()
        Dim task2 As Task(Of Integer) = ReturnCurrentThreadAsync()
        Dim task3 As Task(Of Integer) = ReturnCurrentThreadAsync()
        Dim threadId1 As Integer = Await task1
        Dim threadId2 As Integer = Await task2
        Dim threadId3 As Integer = Await task3

        ' We use three threads because occasionally it is run on the same thread.
        ' Only Microsoft knows exactly why.
        ' Using three makes it less likely that they will all three be on the same thread,
        ' but don't be surprised if it occasionally does.
        Assert.Equal(threadId1 = Thread.CurrentThread.ManagedThreadId AndAlso _
                     threadId2 = Thread.CurrentThread.ManagedThreadId AndAlso _
                     threadId3 = Thread.CurrentThread.ManagedThreadId, _
                     FILL_ME_IN)
    End Function

    <Koan(10)> _
    Public Async Function UseTaskWhenAll() As Task
        ' When you have many tasks that you have to wait for
        ' you can wait for all of them to complete with Task.WhenAll.

        Dim tasks = New Task(Of Integer)(2) {}
        tasks(0) = ReturnSumAsync(2, 3)
        tasks(1) = ReturnSumAsync(4, 7)
        tasks(2) = ReturnSumAsync(5, 3)

        ' Here we could do other work.

        ' When we're done with the other work, or need the value that the
        ' method returns, we call this:
        Dim sums As Integer() = Await Task.WhenAll(tasks)

        Assert.Equal(FILL_ME_IN, sums(0))
        Assert.Equal(FILL_ME_IN, sums(1))
        Assert.Equal(FILL_ME_IN, sums(2))
    End Function

    <Koan(11)> _
    Public Async Function UseTaskWhenAny() As Task
        ' When you have many tasks that you have to wait for
        ' but you'd like to do some further processing if one finishes,
        ' you can do that with Task.WhenAny.
        ' Awaiting on its return value returns the first task that is finished.

        Dim tasks = New List(Of Task(Of Integer))()
        tasks.Add(ReturnSumAsync(1, 2))
        tasks.Add(ReturnSumAsync(2, 3))
        tasks.Add(ReturnSumAsync(3, 4))

        ' When we're done with the other work, or need the value that the
        ' method returns, we call this:
        Dim sumOfSums As Integer = 0
        While tasks.Count > 0
            ' Task.WhenAny returns a Task(Of Task(Of T)), so we await for it
            ' to get the Task(Of T).
            Dim task As Task(Of Integer) = Await task.WhenAny(tasks)

            tasks.Remove(task)

            sumOfSums += Await task
        End While

        Assert.Equal(FILL_ME_IN, sumOfSums)
    End Function

    Private Async Function ReturnOneAsync(ct As CancellationToken) As Task(Of Integer)
        Thread.Sleep(10)
        Await Task.Run(Sub()
                       End Sub)
        Thread.Sleep(10)
        If ct.IsCancellationRequested Then
            Throw New OperationCanceledException()
        End If
        Return 1
    End Function

    <Koan(12)> _
    Public Async Function AsyncMethodsCanBeCancelled() As Task
        ' Asynchronous methods can be cancelled if they have a CancellationToken
        ' object as an argument. The asynchronous method should check the 
        ' CancellationToken object from time to time.
        ' Most .NET classes throw an exception of the type OperationCanceledException
        ' if an asynchronous method is cancelled, so you should catch that.

        Dim cts As New CancellationTokenSource()
        Dim task As Task(Of Integer) = ReturnOneAsync(cts.Token)
        cts.Cancel()

        Dim isCancelled As Boolean = False
        Try
            Dim n As Integer = Await task
        Catch ex As OperationCanceledException
            isCancelled = True
        End Try

        ' Note that occasionally it is not canceled, so don't be surprised if this usually
        ' passes but sometimes not.
        Assert.Equal(FILL_ME_IN, isCancelled)
    End Function

    <Koan(13)> _
    Public Async Function AsyncMethodsCanBeCancelledAfterATimeout() As Task
        ' Asynchronous methods can be cancelled if they have a CancellationToken
        ' object as an argument. The asynchronous method should check the 
        ' CancellationToken object from time to time.

        Dim cts As New CancellationTokenSource()
        Dim task As Task(Of Integer) = ReturnOneAsync(cts.Token)
        cts.CancelAfter(30)

        Dim isCancelled As Boolean = False
        Try
            Dim n As Integer = Await task
        Catch ex As OperationCanceledException
            isCancelled = True
        End Try

        Assert.Equal(FILL_ME_IN, isCancelled)
    End Function
End Class
