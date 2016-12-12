using System;
using System.Linq;
using System.Diagnostics;
using Xunit;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DotNetKoans.KoanRunner
{
    class Program
    {
        static bool aKoanHasFailed = false;
        static int numberKoansProcessed = 0;
        static int firstFailingKoan = -1;
        static List<int> progress = new List<int>();
        const string progressFilename = "progress";

        static int Main(string[] args)
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("*******************************************************************");
                Console.WriteLine("*******************************************************************");

                ReadProgress();

                string koan_path = args[0];
                Xunit.ExecutorWrapper wrapper = new ExecutorWrapper(koan_path, null, false);
                System.Reflection.Assembly koans = System.Reflection.Assembly.LoadFrom(koan_path);
                if (koans == null) { Console.WriteLine("Bad Assembly"); return -1; }
                Type pathType = null;
                foreach (Type type in koans.GetExportedTypes())
                {
                    if (typeof(KoanHelpers.IAmThePathToEnlightenment).IsAssignableFrom(type))
                    {
                        pathType = type;
                        break;
                    }
                }

                KoanHelpers.IAmThePathToEnlightenment path = Activator.CreateInstance(pathType) as KoanHelpers.IAmThePathToEnlightenment;
                string thePath = path.ThePath;
                string[] theKoanNames = KoanNames;

                foreach (string koanName in theKoanNames)
                {
                    Run(thePath + "." + koanName, koans, wrapper);
                }

                Console.WriteLine("{0}", Encouragement());
                progress.Add(firstFailingKoan);
                WriteProgress();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Karma has killed the runner. Exception was: " + ex.ToString());
                return -1;
            }
            if (!aKoanHasFailed)
                firstFailingKoan = numberKoansProcessed;
            Console.WriteLine("Koan progress: {0}/{1}", firstFailingKoan, numberKoansProcessed);
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("");
            Console.WriteLine("");
            return 0;
        }

        static void ReadProgress()
        {
            if (File.Exists(progressFilename))
            {
                string progressInFile = File.ReadAllText(progressFilename);
                string[] progressParts = progressInFile.Split(',');
                foreach (string progressPart in progressParts)
                {
                    progress.Add(int.Parse(progressPart));
                }
            }
        }

        static void WriteProgress()
        {
            List<string> progressStrings = progress.ConvertAll<string>(x => x.ToString());
            File.WriteAllText(progressFilename, string.Join(",", progressStrings.ToArray()));
        }

        static string Encouragement()
        {
            if (!aKoanHasFailed)
                return "You have completed all the koans. You are now enlightened.";
            int failedAttempts = 0;
            for (int i = progress.Count - 1; i >= 0; --i)
                if (progress[i] == firstFailingKoan)
                    ++failedAttempts;
            if (failedAttempts >= 3)
                return "I sense frustration. Do not be afraid to ask for help.";
            if (failedAttempts >= 1)
                return "Do not lose hope.";
            if (firstFailingKoan == 0)
                return "Enjoy these koans.";
            else
                return string.Format("You are progressing. Excellent. {0} completed.", firstFailingKoan);
        }

        static string GetFilenameAndLine(string st)
        {
            int posParen = st.IndexOf(')');
            if (posParen < 0) return null;
            int posColonOfFilename = st.IndexOf(':', posParen);
            if (posColonOfFilename < 0) return null;
            int posColonBeforeLine = st.IndexOf(':', posColonOfFilename + 1);
            if (posColonBeforeLine < 0) return null;
            int posSpaceBeforeLine = st.IndexOf(' ', posColonBeforeLine);
            if (posSpaceBeforeLine < 0) return null;
            string filename = st.Substring(posColonOfFilename - 1, posColonBeforeLine - posColonOfFilename + 1);
            string lineNr = st.Substring(posSpaceBeforeLine + 1).Trim(); // trim to remove new lines that are sometimes there

            return filename + '(' + lineNr + ')';
        }

        static void Run(string className, System.Reflection.Assembly koanAssembly, ExecutorWrapper wrapper)
        {
            Type classToRun = koanAssembly.GetType(className);

            if (classToRun == null)
            {
                Console.WriteLine("Class {0} not found", className);
            }

            object koans = Activator.CreateInstance(classToRun);

            MethodInfo[] queue = new MethodInfo[classToRun.GetMethods().Length + 1];
            int highestKoanNumber = 0;
            foreach (MethodInfo method in classToRun.GetMethods())
            {
                if (method.Name == null) { continue; }
                DotNetKoans.KoanAttribute custAttr = method.GetCustomAttributes(typeof(DotNetKoans.KoanAttribute), false).FirstOrDefault() as DotNetKoans.KoanAttribute;
                if (custAttr == null) { continue; }
                if (queue[custAttr.Position] != null)
                    Console.WriteLine("More than one koan in {0} has the position {1}", className, custAttr.Position);
                queue[custAttr.Position] = method;
                if (custAttr.Position > highestKoanNumber) { highestKoanNumber = custAttr.Position; }
            }

            int numberOfKoansRunInThisCollection = 0;
            int numberOfKoansPassedInThisCollection = 0;

            BindingFlags flags = BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public;
            foreach (MethodInfo test in queue)
            {
                if (test == null)
                    continue;

                ++numberKoansProcessed;
                numberOfKoansRunInThisCollection++;
                if (aKoanHasFailed)
                    continue;
                try
                {
                    Type attType = typeof(AsyncStateMachineAttribute);

                    // Obtain the custom attribute for the method. 
                    // The value returned contains the StateMachineType property. 
                    // Null is returned if the attribute isn't present for the method. 
                    var attrib = (AsyncStateMachineAttribute)test.GetCustomAttribute(attType);

                    if (attrib != null)
                    {
                        Task t = (classToRun.InvokeMember(test.Name, flags, null, koans, new Object[] { }) as Task);
                        t.Wait();
                    }
                    else
                    {
                        classToRun.InvokeMember(test.Name, flags, null, koans, new Object[] { });
                    }
                }
                catch (Exception e)
                {
                    aKoanHasFailed = true;
                    firstFailingKoan = numberKoansProcessed - 1;
                    Exception ie = e.InnerException;
                    if (ie == null)
                        ie = e;
                    string st = ie.StackTrace;
                    string[] stLines = st.Split(new char[] { '\n' });
                    // uncomment if there are problems parsing the stack trace:
                    /*foreach (string line in stLines)
                        Console.WriteLine(line);*/
                    string filenameAndLine = "";
                    foreach (string s in stLines)
                    {
                        filenameAndLine = GetFilenameAndLine(s);
                        if (filenameAndLine != null)
                            break;
                    }

                    // make the message a single line, so that it is completely seen if the Error List pane is selected instead of the Output pane:
                    string message = ie.Message.Replace("\n", " \\ ").Replace("\r", "");

                    Console.WriteLine("The test {0} has damaged your karma.", test);
                    // Use this format: "file(linenr): warning: " followed by anything, so it shows up as a warning in the Error List pane.
                    // In both the Error List pane and Output pane clicking it jumps to the line where a fix is needed.
                    // Replacing 'warning' by 'error' would work too, but also give a distracting extra error message.
                    Console.WriteLine(filenameAndLine + ": warning: {0}", message);

                }
                if (!aKoanHasFailed)
                    numberOfKoansPassedInThisCollection++;
            }

            if (numberOfKoansRunInThisCollection != highestKoanNumber)
            {
                Console.WriteLine("!!!!WARNING - Some Koans in {0} appear disabled. The highest koan found was {1} but we ran {2} koan(s)",
                    className, highestKoanNumber, numberOfKoansRunInThisCollection);
            }
        }

        static public string[] KoanNames
        {
            get
            {
                return new string[] {
				"AboutAsserts",
				"AboutNull",
				"AboutArrays",
				"AboutStrings",
                "AboutClassesAndStructs",
				"AboutInheritance",
                "AboutProperties",
				"AboutMethods",
				"AboutControlStatements",
				"AboutContainers",
				"AboutDelegates",
				"AboutLambdas",
                "AboutUsing",
                "AboutSymbols",
                "AboutAsynchrony"
                };
            }
        }
    }
}
