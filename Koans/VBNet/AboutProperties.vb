
Imports Xunit

Public Class AboutProperties
    Inherits Koan
    Private Class ClassWithGet
        Public Sub New()
            m_a = 1
        End Sub
        Private m_a As Integer
        Public ReadOnly Property A() As Integer
            Get
                Return m_a
            End Get
        End Property
    End Class
    <Koan(1)> _
    Public Sub ClassesCanHaveGetters()
        ' Classes can can have properties with just a getter.
        Dim obj As New ClassWithGet()

        ' This class only has a get for A, so this would be illegal:
        'obj.A = 5
        Assert.Equal(FILL_ME_IN, obj.A)
    End Sub

    Private Class ClassWithGetSet
        Public Sub New()
            m_a = 1
        End Sub
        Private m_a As Integer
        Public Property A() As Integer
            Get
                Return m_a
            End Get
            Set(value As Integer)
                m_a = value
            End Set
        End Property
    End Class

    <Koan(2)> _
    Public Sub ClassesCanHaveGettersAndSetters()
        ' Classes can can have properties with both getters and setters.
        Dim obj As New ClassWithGetSet()

        ' This class has a get and set for A, so you can do this:
        obj.A = 5
        Assert.Equal(FILL_ME_IN, obj.A)
    End Sub

    Private Class ClassWithPublicGetAndPrivateSet
        Public Sub New()
            Side = 0
            Area = 0
        End Sub
        Private m_side As Integer
        Public Property Side() As Integer
            Get
                Return m_side
            End Get
            Set(value As Integer)
                m_side = value
                Area = m_side * m_side
            End Set
        End Property
        Public Property Area() As Integer
            Get
                Return m_Area
            End Get
            Private Set(value As Integer)
                m_Area = value
            End Set
        End Property
        Private m_Area As Integer
    End Class

    <Koan(3)> _
    Public Sub ClassesCanHavePublicGettersWithPrivateSetters()
        ' It is possible to have a property with a public getter and a private setter,
        ' so a user of the class can get the value but not set it, although methods of
        ' the class can set it.
        ' The property declaration has to be public, while the setter is declared private.
        Dim obj As New ClassWithPublicGetAndPrivateSet()

        ' So this would be illegal:
        ' obj.Area = 20

        obj.Side = 5
        Assert.Equal(FILL_ME_IN, obj.Side)
        Assert.Equal(FILL_ME_IN, obj.Area)
    End Sub

    Private Class ClassWithIndexer
        Private arr As Integer()
        Public Sub New()
            arr = New Integer() {1, 2, 3}
        End Sub
        Default Public Property Item(index As Long) As Integer
            Get
                Return arr(index)
            End Get
            Set(value As Integer)
                arr(index) = value
            End Set
        End Property
    End Class

    <Koan(4)> _
    Public Sub ClassesCanHaveIndexers()
        ' An indexer allows a class to act as an array.
        Dim obj As New ClassWithIndexer()

        obj(0) = 5
        obj(1) = 7
        Assert.Equal(FILL_ME_IN, obj(0))
        Assert.Equal(FILL_ME_IN, obj(1))
    End Sub
End Class
