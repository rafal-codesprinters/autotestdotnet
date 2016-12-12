Imports Xunit
Imports System.Diagnostics

Public Class AboutSymbols
    Inherits Koan

#Const CONST_A = 1

    <Koan(1)> _
    Public Sub DirectivesAllowYouToEnableCode()
        ' Code between '#If <expression>' and '#End If' is only compiled if the expression is true.
        ' The expression can only use variables defined with #Const.
        Dim b As Boolean = False
#If CONST_A = 1 Then
        b = True
#End If
        Assert.Equal(b, FILL_ME_IN)
    End Sub

    <Koan(2)> _
    Public Sub DirectivesAllowYouToDisableCode()
        Dim b As Boolean = False
#If CONST_A = 2 Then
			b = True
#End If
        Assert.Equal(b, FILL_ME_IN)
    End Sub

    <Koan(3)> _
    Public Sub YouCanAlsoUseElse()
        Dim a As Integer = 0
#If CONST_A = 2 Then
		a = 1
#Else
        a = 2
#End If
        Assert.Equal(a, FILL_ME_IN)
    End Sub

    <Koan(4)> _
    Public Sub YouCanAlsoUseElseIf()
        Dim a As Integer = 0
#If CONST_A = 2 Then
        a = 1
#ElseIf CONST_A = 1 Then
        a = 2
#End If
        Assert.Equal(a, FILL_ME_IN)
    End Sub

    Shared m_b As Boolean = False

    <Koan(5)> _
    Public Sub ChangeASymbolInTheProjectSettings()
        ' Constants can also be defined in the project settings.
        ' Change a definition in the project settings (usually Properties -> Compile -> Advanced settings)
        ' to make the assertion below succeed.
        ' These have the form SYMBOL=expression
        Dim b As Boolean = False
#If CONST_B = 2 Then
        b = True
#End If
        Assert.Equal(b, True)
    End Sub

    <Koan(6)> _
    Public Sub ASymbolWithoutAnExpressionActsAsABoolean()
        ' In the project settings the symbol CONST_C is defined, but the symbol CONST_D is not.
        ' CONST_C is defined without an expression, therefore it acts as True.
        ' CONST_D is not defined, therefore it acts as False.
        Dim b1 As Boolean = False
        Dim b2 As Boolean = False
#If CONST_C Then
        b1 = True
#End If
#If CONST_D Then
        b2 = True
#End If
        Assert.Equal(b1, FILL_ME_IN)
        Assert.Equal(b2, FILL_ME_IN)
    End Sub

    <Koan(7)> _
    Public Sub TheSymbolDEBUGIsUsuallyDefinedInTheDebugProfile()
        ' The constant DEBUG is defined in the project settings.
        ' Projects usually have two profiles, called Debug and Release
        ' (these koans only have the Debug profile for simplicity).
        ' They can have different settings, also for defining constants.
        ' Usually the symbol DEBUG is defined for the Debug profile, not for the Release profile.
        Dim b As Boolean = False
#If DEBUG Then
        b = True
#End If
        Assert.Equal(b, FILL_ME_IN)
    End Sub

    <Conditional("CONST_C")> _
    Public Shared Sub EnabledMethod()
        m_b = True
    End Sub

    <Conditional("CONST_D")> _
    Public Shared Sub DisabledMethod()
        m_b = True
    End Sub

    <Koan(8)> _
    Public Sub MethodsCanBeCompiledOrNotDependingOnWhetherASymbolIsDefined()
        ' If a Sub has the attribute <Conditional("<symbol>")> it will only be compiled
        ' if the symbol is defined. If it is not defined, all calls to the method are removed as well.
        ' This cannot be done with Functions.
        m_b = False
        EnabledMethod()
        Assert.Equal(m_b, FILL_ME_IN)

        m_b = False
        DisabledMethod()
        Assert.Equal(m_b, FILL_ME_IN)
    End Sub

    <Conditional("CONST_E")> _
    Public Shared Sub EnableInProjectSettings()
        m_b = True
    End Sub

    <Koan(9)> _
    Public Sub MethodsCanBeEnabledWithAConstantInTheProjectSettings()
        ' Enable the above method by adding a constant in the project settings, so the assertion below succeeds.
        ' In this case you don't need to add =expression to it, the name of the constant is enough.
        m_b = False
        EnableInProjectSettings()
        Assert.Equal(m_b, True)
    End Sub

    <Conditional("CONST_C"), Conditional("CONST_D")> _
    Public Shared Sub MethodEnabledByOneSymbolOfTwo()
        m_b = True
    End Sub

    <Koan(10)> _
    Public Sub MethodsCanHaveMoreThanOneConditional()
        ' If a Sub has the attribute <Conditional("<symbol>")> more than once, the code will be
        ' generated if at least one of the symbols is defined.
        m_b = False
        MethodEnabledByOneSymbolOfTwo()
        Assert.Equal(m_b, FILL_ME_IN)
    End Sub
End Class
