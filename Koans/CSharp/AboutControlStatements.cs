using System;
using System.Collections.Generic;
using Xunit;

namespace DotNetKoans.CSharp
{
    public class AboutControlStatements : Koan
    {
#pragma warning disable 162
        [Koan(1)]
        public void IfThenStatementsWithBrackets()
        {
            bool b = false;
            if (true)
            {
                b = true;
            }

            Assert.Equal(FILL_ME_IN, b);
        }

        [Koan(2)]
        public void IfThenStatementsWithoutBrackets()
        {
            bool b = false;
            if (true)
                b = true;

            Assert.Equal(FILL_ME_IN, b);
        }

        [Koan(3)]
        public void WhyItsWiseToAlwaysUseBrackets()
        {
            bool b1 = false;
            bool b2 = false;

            int counter = 1;

            if (counter == 0)
                b1 = true;
                b2 = true;

            Assert.Equal(FILL_ME_IN, b1);
            Assert.Equal(FILL_ME_IN, b2);
        }

        [Koan(4)]
        public void IfThenElseStatementsWithBrackets()
        {
            bool b;
            if (true)
            {
                b = true;
            }
            else
            {
                b = false;
            }

            Assert.Equal(FILL_ME_IN, b);
        }

        [Koan(5)]
        public void IfThenElseStatementsWithoutBrackets()
        {
            bool b;
            if(true)
                b = true;
            else
                b = false;

            Assert.Equal(FILL_ME_IN, b);

        }

        [Koan(6)]
        public void TernaryOperators()
        {
            Assert.Equal(FILL_ME_IN, (true ? 1 : 0));
            Assert.Equal(FILL_ME_IN, (false ? 1 : 0));
        }

#pragma warning disable 219
        //This is out of place for control statements, but necessary for Koan 8
        [Koan(7)]
        public void NullableTypes()
        {
            int i = 0;
            //i = null; //You can't do this

            int? nullableInt = null; //but you can do this
			Assert.NotNull(FILL_ME_IN);
			Assert.Null(FILL_ME_IN);
        }

        [Koan(8)]
        public void AssignIfNullOperator()
        {
            int? nullableInt = null;

            int x = nullableInt ?? 42;

            Assert.Equal(FILL_ME_IN, x);
        }

#pragma warning disable 184
        [Koan(9)]
        public void IsOperators()
        {
            bool isKoan = false;
            bool isAboutControlStatements = false;
            bool isAboutMethods = false;

            var myType = this;

            if (myType is Koan)
                isKoan = true;

            if (myType is AboutControlStatements)
                isAboutControlStatements = true;

            if (myType is AboutMethods)
                isAboutMethods = true;

            Assert.Equal(FILL_ME_IN, isKoan);
            Assert.Equal(FILL_ME_IN, isAboutControlStatements);
            Assert.Equal(FILL_ME_IN, isAboutMethods);

        }

        [Koan(10)]
        public void WhileStatement()
        {
            int i = 1;
            int result = 1;
            while (i <= 3)
            {
                result = result + i;
                i += 1;
            }
            Assert.Equal(FILL_ME_IN, result);
        }

        [Koan(11)]
        public void BreakStatement()
        {
            int i = 1;
            int result = 1;
            while (true)
            {
                if (i > 3) { break; }
                result = result + i;
                i += 1;    
            }
            Assert.Equal(FILL_ME_IN, result);
        }

        [Koan(12)]
        public void ContinueStatement()
        {
            int i = 0;
            var result = new List<int>();
            while(i < 10)
            {
                i += 1;
                if ((i % 2) == 0) { continue; }
                result.Add(i);
            }
            Assert.Equal(FILL_ME_IN, result);
        }

        [Koan(13)]
        public void ForStatement()
        {
            var list = new List<string> { "fish", "and", "chips" };
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = (list[i].ToUpper());
            }
            Assert.Equal(FILL_ME_IN, list);
        }

        [Koan(14)]
        public void ForEachStatement()
        {
            var list = new List<string> { "fish", "and", "chips" };
            var finalList = new List<string>();
            foreach (string item in list)
            {
                finalList.Add(item.ToUpper());
            }
            Assert.Equal(FILL_ME_IN, list);
            Assert.Equal(FILL_ME_IN, finalList);
        }

        [Koan(15)]
        public void ModifyingACollectionDuringForEach()
        {
            var list = new List<string> { "fish", "and", "chips" };
            try
            {
                foreach (string item in list)
                {
                    list.Add(item.ToUpper());
                }
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(FillMeIn), ex.GetType());
            }
        }

        [Koan(16)]
        public void CatchingModificationExceptions()
        {
            string whoCaughtTheException = "No one";

            var list = new List<string> { "fish", "and", "chips" };
            try
            {
                foreach (string item in list)
                {
                    try
                    {
                        list.Add(item.ToUpper());
                    }
                    catch
                    {
                        whoCaughtTheException = "When we tried to Add it";
                    }
                }
            }
            catch
            {
                whoCaughtTheException = "When we tried to move to the next item in the list";
            }

            Assert.Equal(FILL_ME_IN, whoCaughtTheException);
        }

        [Koan(17)]
        public void Switch()
        {
            // A switch statement allows you to execute one of several blocks of code
            // depending on the value of an integer, give after the case keyword.
            // Each block ends with break (but we'll see an alternative soon).

            int a = 2, b = 0;
            switch (a)
            {
                case 1:
                    b = 10;
                    break;
                case 2:
                    b = 11;
                    break;
            }

            Assert.Equal(FILL_ME_IN, b);
        }

        [Koan(18)]
        public void SwitchWithFallThrough()
        {
            // A block can handle multiple values of the variable,
            // by grouping the case statements.
            // In this case there can be no code between the case statements.

            int a = 1, b = 0;
            switch (a)
            {
                case 1:
                case 2:
                    b = 11;
                    break;
            }

            Assert.Equal(FILL_ME_IN, b);
        }

        [Koan(19)]
        public void SwitchWithDefault()
        {
            // If the value is not matched, the code after the default keyword
            // is executed, if there is one.
            int a = 3, b = 0;
            switch (a)
            {
                case 1:
                    b = 10;
                    break;
                case 2:
                    b = 11;
                    break;
                default:
                    b = 12;
                    break;
            }

            Assert.Equal(FILL_ME_IN, b);
        }

        [Koan(20)]
        public void SwitchWithGoto()
        {
            // Instead of break you can end a block in a switch block with any statement
            // that ends the block, like return or throw.
            // In this case we use another one, goto case.
            // This makes it continue in another block.

            int a = 2, b = 0;
            switch (a)
            {
                case 1:
                    b = 10;
                    break;
                case 2:
                    b = 11;
                    goto case 1;
            }

            Assert.Equal(FILL_ME_IN, b);
        }

        [Koan(21)]
        public void SwitchWithStrings()
        {
            // Instead of integers you can also use strings.
            string a = "a";
            int b = 0;
            switch (a)
            {
                case "a":
                    b = 10;
                    break;
                case "b":
                    b = 11;
                    break;
            }

            Assert.Equal(FILL_ME_IN, b);
        }

        // The yield keyword makes a function act as an iterator.
        // This creates an interesting control flow.
        // Every time 'yield return' is encountered, the control goes back
        // to the calling function. When the next element is requested,
        // control goes back to the function right after the 'yield return'.
        // All variables still have their values, because it runs on a separate stack.

        private static System.Collections.Generic.IEnumerable<int> Fibonacci1(int n)
        {
            int f1 = 0, f2 = 1;
            for (int i = 0; i < n; ++i)
            {
                int f3 = f2 + f1;
                f1 = f2;
                f2 = f3;
                yield return f1;
            }
        }

        [Koan(22)]
        public void Yield()
        {
            var fibonacci = new List<int>();
            foreach (int f in Fibonacci1(5))
            {
                fibonacci.Add(f);
            }
            Assert.Equal(new List<int>() { }, fibonacci);
        }

        // yield break allows you to stop the iteration
        private static System.Collections.Generic.IEnumerable<int> Fibonacci2(int n)
        {
            int f1 = 0, f2 = 1;
            int i = 0;
            while (true)
            {
                int f3 = f2 + f1;
                f1 = f2;
                f2 = f3;
                yield return f1;

                ++i;
                if (i == n)
                    yield break;
            }
        }

        [Koan(23)]
        public void YieldWithBreak()
        {
            var fibonacci = new List<int>();
            foreach (int f in Fibonacci2(5))
            {
                fibonacci.Add(f);
            }
            Assert.Equal(new List<int>() { }, fibonacci);
        }
    }
}