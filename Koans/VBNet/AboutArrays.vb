Imports Xunit

Public Class AboutArrays
    Inherits Koan

    <Koan(1)> _
    Public Sub CreatingArrays()
        'Arrays can be created by adding () to the variable name
        Dim empty_array = New Object() {}

        Assert.Equal(FILL_ME_IN, empty_array.Length)
    End Sub

    <Koan(2)> _
    Public Sub CreatingArraysWithASize()
        ' You can also make it non-empty by adding the maximum index
        Dim array(4) As Object
        Assert.Equal(FILL_ME_IN, array.Length)
        ' Note that arrays are 0-based:
        array(0) = "abc"
        array(4) = "xyz"
        Assert.Equal(FILL_ME_IN, array(0))
        Assert.Equal(FILL_ME_IN, array(4))
        'This doesn't work:
        'Assert.Equal(FILL_ME_IN, array(-1))
    End Sub

    <Koan(3)> _
    Public Sub ArrayLiterals()
        'You don't have to specify a type if the arguments can be inferred
        Dim array = {42, 3, 2, 4}
        Assert.Equal(New Integer() {42}, array)
        Assert.True(array.IsFixedSize)
        '...it means we can't do this: array(4) = 13
        Assert.Throws(GetType(FillMeIn), Sub() array(4) = 13)
        'This is because the array is fixed at length 4.
    End Sub

    <Koan(4)> _
    Public Sub SlicingArrays()
        Dim array = New String() {"peanut", "butter", "and", "jelly"}
        Assert.Equal(New String() {FILL_ME_IN, FILL_ME_IN}, array.Take(2).ToArray())
        Assert.Equal(New String() {FILL_ME_IN, FILL_ME_IN}, array.Skip(1).Take(2).ToArray())
    End Sub

    <Koan(5)> _
    Public Sub ResizingWithReDim()
        ' You can make an array bigger or smaller
        Dim array = {42, 3, 2, 4}
        ReDim array(6)
        Assert.Equal(FILL_ME_IN, array.Length)
        ' but the values are not kept
        Assert.Equal(FILL_ME_IN, array(0))
    End Sub

    <Koan(6)> _
    Public Sub ResizingWithReDimAndPreserve()
        ' Adding Preserve keeps the values
        Dim array = {42, 3, 2, 4}
        ReDim Preserve array(6)
        Assert.Equal(FILL_ME_IN, array.Length)
        Assert.Equal(FILL_ME_IN, array(0))
    End Sub

    <Koan(7)> _
    Public Sub TwoDimensionalArrays()
        Dim array(2, 3) As Integer
        array(2, 3) = 5
        Assert.Equal(FILL_ME_IN, array(2, 3))
    End Sub
End Class
