using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using System.IO;

#pragma warning disable 1998

namespace DotNetKoans.CSharp
{
    class AboutAsynchrony: Koan
    {
        async Task<int> ReturnOneAsync()
        {
            return 1;
        }

        [Koan(1)]
        public async Task CallAsyncMethod()
        {
            // Asynchronous methods are methods whose execution does not necessarily
            // run sequentially. When you call such a method it returns a
            // Task<T> object as soon as it reaches an await, which means it
            // is waiting for something to finish, so execution can go on
            // doing other things, e.g. when it has to wait when data is read
            // from or written to a file or socket.
            // An asynchronous method has the keyword async in front of it and returns
            // a Task<T> object. To get its value you call 'await task',
            // which will cause it to wait until the method reaches its end.

            Task<int> task = ReturnOneAsync();

            // Here we could do other work.

            // When we're done with the other work, or need the value that the
            // method returns, we call this:
            int a = await task;

            Assert.Equal(FILL_ME_IN, a);

            // ReturnOneAsync is an overly simplistic method, it actually doesn't make use of
            // asynchrony, because all the code can be executed directly, but we'll see better examples.
        }

        async Task<int> ReturnSumAsync(int a, int b)
        {
            return a + b;
        }

        [Koan(2)]
        public async Task AsyncMethodsCanHaveArguments()
        {
            Task<int> task = ReturnSumAsync(2,2);

            // Here we could do other work.

            int a = await task;

            Assert.Equal(FILL_ME_IN, a);
        }

        async Task StoreOrder(List<int> order)
        {
            order.Add(2);

            // Next line is necessary to make the asynchronous method interrupt its flow.
            // We'll see later what it does.
            await Task.Run(() => { ; });
            order.Add(4);
        }

        [Koan(3)]
        public async Task LetsCheckTheOrder()
        {
            // It is important to understand how the excution flow goes.
            // When an asynchronous method encounters an await statement,
            // the execution goes back to the calling function.
            // When the calling function awaits the task later,
            // execution flow goes back to the called function.

            var order = new List<int>();
            order.Add(1);
            Task task = StoreOrder(order);
            order.Add(3);

            // Here we could do other work.

            await task;

            order.Add(5);

            // Note that occasionally the order is different from what you expect,
            // due to mysterious reasons, so don't be surprised if this usually
            // passes but sometimes not.
            Assert.Equal(new List<int>() { 0,0,0,0,0 }, order);
        }

        async Task<String> ReadFile()
        {
            StreamReader SourceReader = File.OpenText(@"..\..\AboutAsynchrony.cs");
            return await SourceReader.ReadToEndAsync();
        }

        [Koan(4)]
        public async Task CallReadFile()
        {
            // Here is a more useful example. When the function ReadFile
            // starts to read the file, it has to wait for the filesystem to
            // access the disk. In the meantime the CPU can go on doing other
            // things in the calling function.
            // There are many more such methods that do this, whose name by convention
            // end in Async.

            Task<String> task = ReadFile();

            // Here we could do other things.

            String src = await task;

            Assert.Equal(FILL_ME_IN, src.Substring(0, 5));
        }

        async Task<int> ThrowAsync()
        {
            throw new Exception();
        }

        [Koan(5)]
        public async Task TasksStoreTheExceptionIfOneIsThrown()
        {
            // If an exception is thrown in the asynchronous method,
            // it is stored in the Task object that is returned.
            // It is thrown again when you wait for it.

            Task<int> task = ThrowAsync();

            // Here we could do other work.

            bool hasThrown = false;
            try
            {
                int a = await task;
            }
            catch (Exception)
            {
                hasThrown = true;
            }

            Assert.Equal(FILL_ME_IN, hasThrown);
        }

        int a;

        async Task SetA()
        {
            a = 1;
        }

        [Koan(6)]
        public async Task AsyncMethodsDontHaveToReturnSomething()
        {
            // Asynchronous methods don't have to have a return statement.
            // In that case they return a simple Task object instead of a Task<T> object,
            // on which you can also wait.

            a = 0;
            Task t = SetA();

            // Here we could do other things.

            await t;

            Assert.Equal(FILL_ME_IN, a);
        }

        async void SetAWithoutATask()
        {
            a = 1;
        }

        [Koan(7)]
        public async Task AsyncMethodsAreOfTheFireAndForgetTypeIfTheyReturnVoid()
        {
            // Asynchronous methods that don't return something
            // can also return void instead of a Task object.
            // In that case you cannot wait for it to complete.
            // This can be dangerous because if an exception is thrown in the
            // asynchronous method, you cannot catch it.

            a = 0;
            SetAWithoutATask();

            // Here we could do other things.

            // To be sure that it comppletes, we wait a short time.
            Thread.Sleep(10);

            Assert.Equal(FILL_ME_IN, a);
        }

        Thread theThread;

        async Task<int> ReturnOneAndStoreThreadAsync()
        {
            theThread = Thread.CurrentThread;
            return 1;
        }

        [Koan(8)]
        public async Task AsyncMethodsRunOnTheSameThread()
        {
            // asynchronous methods are not run on a separate thread.
            // The code that executes the compiled byte code just schedules
            // other code to execute.

            Task<int> task = ReturnOneAndStoreThreadAsync();

            int a = await task;

            Assert.Equal(theThread==Thread.CurrentThread, FILL_ME_IN);
        }

        async Task<Thread> ReturnCurrentThreadAsync()
        {
            Task<Thread> t = Task.Run(() =>
            {
                return Thread.CurrentThread;
            });

            return await t;
        }

        [Koan(9)]
        public async Task ButAsyncMethodsCanBeMadeToRunOnAnotherThread()
        {
            // If the purpose of an asynchronous method is to run code on a separate
            // thread so you can take advantage of multiple cores,
            // you can do so with Task.Run.

            Task<Thread> task1 = ReturnCurrentThreadAsync();
            Task<Thread> task2 = ReturnCurrentThreadAsync();
            Task<Thread> task3 = ReturnCurrentThreadAsync();
            Thread thread1 = await task1;
            Thread thread2 = await task2;
            Thread thread3 = await task3;

            // We use three threads because occasionally it is run on the same thread.
            // Only Microsoft knows exactly why.
            // Using three makes it less likely that they will all three be on the same thread,
            // but don't be surprised if it occasionally does.
            Assert.Equal(thread1 == Thread.CurrentThread && thread2 == Thread.CurrentThread && thread3 == Thread.CurrentThread, FILL_ME_IN);
        }

        [Koan(10)]
        public async Task UseTaskWhenAll()
        {
            // When you have many tasks that you have to wait for
            // you can wait for all of them to complete with Task.WhenAll.

            var tasks = new Task<int>[3];
            tasks[0] = ReturnSumAsync(2, 3);
            tasks[1] = ReturnSumAsync(4, 7);
            tasks[2] = ReturnSumAsync(5, 3);

            // Here we could do other work.

            // When we're done with the other work, or need the value that the
            // method returns, we call this:
            int[] sums = await Task.WhenAll(tasks);

            Assert.Equal(FILL_ME_IN, sums[0]);
            Assert.Equal(FILL_ME_IN, sums[1]);
            Assert.Equal(FILL_ME_IN, sums[2]);
        }

        [Koan(11)]
        public async Task UseTaskWhenAny()
        {
            // When you have many tasks that you have to wait for
            // but you'd like to do some further processing if one finishes,
            // you can do that with Task.WhenAny.

            var tasks = new List<Task<int>>();
            tasks.Add(ReturnSumAsync(1, 2));
            tasks.Add(ReturnSumAsync(2, 3));
            tasks.Add(ReturnSumAsync(3, 4));

            // When we're done with the other work, or need the value that the
            // method returns, we call this:
            int sumOfSums = 0;
            while (tasks.Count > 0)
            {
                // Task.WhenAny returns a Task<Task<T>>, so we await for it
                // to get the Task<T>.
                Task<int> task = await Task.WhenAny(tasks);

                tasks.Remove(task);

                sumOfSums += await task;
            }

            Assert.Equal(FILL_ME_IN, sumOfSums);
        }

        async Task<int> ReturnOne(CancellationToken ct)
        {
            Thread.Sleep(10);
            await Task.Run(() => { ; });
            Thread.Sleep(10);
            if (ct.IsCancellationRequested)
                throw new OperationCanceledException();
            return 1;
        }

        [Koan(12)]
        public async Task AsyncMethodsCanBeCancelled()
        {
            // Asynchronous methods can be cancelled if they have a CancellationToken
            // object as an argument. The asynchronous method should check the 
            // CancellationToken object from time to time.
            // Most .NET classes throw an exception of the type OperationCanceledException
            // if an asynchronous method is cancelled, so you should catch that.

            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> task = ReturnOne(cts.Token);
            cts.Cancel();
            bool isCancelled = false;
            try
            {
                int n = await task;
            }
            catch (OperationCanceledException)
            {
                isCancelled = true;
            }
            // Note that occasionally it is not canceled, so don't be surprised if this usually
            // passes but sometimes not.
            Assert.Equal(FILL_ME_IN, isCancelled);
        }

        [Koan(13)]
        public async Task AsyncMethodsCanBeCancelledAfterATimeout()
        {
            // Asynchronous methods can be cancelled if they have a CancellationToken
            // object as an argument. The asynchronous method should check the 
            // CancellationToken object from time to time.

            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> task = ReturnOne(cts.Token);
            cts.CancelAfter(30);
            bool isCancelled = false;
            try
            {
                int n = await task;
            }
            catch (OperationCanceledException)
            {
                isCancelled = true;
            }
            Assert.Equal(FILL_ME_IN, isCancelled);
        }
    }
}
