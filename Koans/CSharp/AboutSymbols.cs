#define MACRO_1
#define MACRO_3
#undef MACRO_3

using Xunit;
using System.Diagnostics;

namespace DotNetKoans.CSharp
{
    public class AboutSymbols : Koan
    {
        [Koan(1)]
        public void MacrosAllowYouToEnableCode()
        {
            // Code between '#if <symbol>' and '#endif' is only compiled if the symbol is defined.
            // Note: unlike in C/C++ and VB symbols cannot have a value, they are either defined or not.
            bool b = false;
#if MACRO_1
            b = true;
#endif
            Assert.Equal(b, FILL_ME_IN);
        }

        [Koan(2)]
        public void MacrosAllowYouToDisableCode()
        {
            bool b = false;
#if MACRO_2
            b = true;
#endif
            Assert.Equal(b, FILL_ME_IN);
        }

        [Koan(3)]
        public void YouCanAlsoUseElse()
        {
            int a = 0;
#if MACRO_2
            a = 1;
#else
            a = 2;
#endif
            Assert.Equal(a, FILL_ME_IN);
        }

        [Koan(4)]
        public void YouCanAlsoUseElif()
        {
            int a = 0;
#if MACRO_2
            a = 1;
#elif MACRO_1
            a = 2;
#endif
            Assert.Equal(a, FILL_ME_IN);
        }

        [Koan(5)]
        public void SymbolsCanBeUndefined()
        {
            bool b = false;
#if MACRO_3
            b = true;
#endif
            Assert.Equal(b, FILL_ME_IN);
        }

        [Koan(6)]
        public void SymbolsCanBeDefinedInTheProjectSettings()
        {
            // Symbols can also be defined in the project settings.
            // Add the symbol in the project settings (usually Properties -> Build -> Conditional compilation symbols)
            // to make this assertion below succeed.
            bool b = false;
#if MACRO_4
            b = true;
#endif
            Assert.Equal(b, true);
        }

        [Koan(7)]
        public void DebugIsUsuallyDefinedInTheDebugProfile()
        {
            // The symbol DEBUG is defined in the project settings.
            // Projects usually have two profiles, Debug and Release
            // (these koans only have the Debug profile for simplicity).
            // The symbol DEBUG is usually defined in the Debug profile, not in the Release profile.
            bool b = false;
#if DEBUG
            b = true;
#endif
            Assert.Equal(b, FILL_ME_IN);
        }

        static bool m_b = false;

        [Conditional("MACRO_1")]
        public static void foo()
        {
            m_b = true;
        }

        [Conditional("MACRO_2")]
        public static void bar()
        {
            m_b = true;
        }

        [Koan(8)]
        public void MethodsCanBeCompiledOrNotDependingOnWhetherASymbolIsDefined()
        {
            // If a method has the attribute [Conditional("<symbol>")] it will only be compiled
            // if the symbol is defined. If it is not defined, all calls to the method are removed as well.
            // Such function have to have a void return type.
            m_b = false;
            foo();
            Assert.Equal(m_b, FILL_ME_IN);

            m_b = false;
            bar();
            Assert.Equal(m_b, FILL_ME_IN);
        }

        [Conditional("MACRO_1"), Conditional("MACRO_2")]
        public static void baz()
        {
            m_b = true;
        }

        [Koan(9)]
        public void AMethodWithMoreThanOneConditionalIsEnabledIfAtLeastOneOfTheSymbolsIsDefined()
        {
            // If a method has more than one Conditional it will only be compiled
            // if at least one of the symbols is defined.
            m_b = false;
            baz();
            Assert.Equal(m_b, FILL_ME_IN);
        }
    }
}
