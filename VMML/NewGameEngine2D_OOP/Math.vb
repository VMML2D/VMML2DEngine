Imports System.Math
Imports System.Numerics


Namespace Math

    Public Class Trigonemetry
        Public Const PI As Double = 3.1415926535897931
        Private Shared MathTools1 As New MathTools

        Public Shared Function Angle2(AngleA As Double, AngleB As Double) As Double
            Dim a As Double = AngleA
            Dim b As Double = AngleB

            Dim c As Double
            Dim alpha As Double

            c = MathTools1.Sqrt(a ^ 2 + b ^ 2)
            alpha = System.Math.Asin(a / c) * 180 / PI
            Return alpha
        End Function
        'Rectangle
        Public Shared Function AreaRec(a As Integer, b As Integer) As Integer
            Return a * b
        End Function
        Public Shared Function ScopeRec(a As Integer, b As Integer) As Integer
            Return 2 * a + 2 * b
        End Function
        Public Shared Function DiagonalRec(a As Integer, b As Integer) As Double
            Return MathTools1.Sqrt(a ^ 2 + b ^ 2)
        End Function
        'PI
        Public Shared Function AreaPI(r As Double) As Double
            Return PI * r ^ 2
        End Function
        Public Shared Function ScopePI(r As Double) As Double
            Return 2 * PI * r
        End Function
        Public Shared Function Diameter(r As Double) As Double
            Return r * 2
        End Function
    End Class
    Public Class MathTools
        Public Function Faculty(Number As Integer) As BigInteger
            If Number <= 1 Then
                Return 1
            Else
                Return Number * Faculty(Number - 1)
            End If
        End Function
        Public Function Sqrt(a As Double) As Double
            Dim xn As Double = 1.0
            Do
                xn = (xn + (a / xn)) / 2
            Loop While ((xn * xn) - a) > 1.0E-27
            Return xn
        End Function
    End Class
    Public Structure Point2D
        Public Sub New(ComponentX As Single, ComponentY As Single)
            X = ComponentX
            Y = ComponentY
        End Sub
        Public Property X() As Single
        Public Property Y() As Single
        Public ReadOnly Property Location() As Point2D
            Get
                Return New Point2D(Me.X, Me.Y)
            End Get
        End Property
        Public Shared Function Add(Point2D As Point2D, Vector2 As Vector2) As Point2D
            Return New Point2D(Point2D.X + Vector2.X, Point2D.Y + Vector2.Y)
        End Function
        Public Shared Function Round(Point2D As Point2D) As Point2D
            Dim BufferX, BufferY As Single

            BufferX = Point2D.X
            BufferY = Point2D.Y


            Return New Point2D(CSng(System.Math.Round(BufferX)), CSng(System.Math.Round(BufferY)))

        End Function
        Public Overrides Function ToString() As String
            Return String.Format("({0},{1})", X, Y)
        End Function
    End Structure
    Public Structure Vector2
        'ToDo
        'Several operators
        'Calculating and returning of 2 points (Vector2)
        Public Sub New(Point2D As Point2D)
            X = Point2D.X
            Y = Point2D.Y
        End Sub
        Public Property X() As Single
        Public Property Y() As Single
        Public Property Location() As Point2D
            Get
                Return New Point2D(Me.X, Me.Y)
            End Get
            Set(value As Point2D)
                Me.X = value.X
                Me.Y = value.Y
            End Set
        End Property
        Public ReadOnly Property Length() As Double
            Get
                Return Sqrt(Pow(X, 2.0F) + Pow(Y, 2.0F))
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("({0},{1})", X, Y)
        End Function

        Public Shared Operator +(ByVal v1 As Vector2, v2 As Vector2) As Vector2
            Return New Vector2(New Point2D(v1.X + v2.Y, v1.Y + v2.Y))
        End Operator
        Public Shared Operator -(v1 As Vector2, v2 As Vector2) As Vector2
            Return New Vector2(New Point2D(v1.X - v2.X, v1.Y - v2.Y))
        End Operator
        Public Shared Operator *(v1 As Vector2, d As Single) As Vector2
            Return New Vector2(New Point2D(v1.X * d, v1.Y * d))
        End Operator
        Public Shared Operator *(d As Single, v1 As Vector2) As Vector2
            Return New Vector2(New Point2D(v1.X * d, v1.Y * d))
        End Operator
        Public Shared Operator /(v1 As Vector2, d As Single) As Vector2
            Return New Vector2(New Point2D(v1.X / d, v1.Y / d))
        End Operator
        Public Shared Function ScalarProduct(v1 As Vector2, v2 As Vector2) As Single
            Return v1.X * v2.X + v1.Y * v2.Y
        End Function
        Public Shared Function IsOrthogenal(v1 As Vector2, v2 As Vector2) As Boolean
            If Single.Equals(ScalarProduct(v1, v2), 0) Then
                Return True
            Else
                Return False

            End If
        End Function
    End Structure
    Public Structure Point3D
        Public Sub New(ComponentX As Single, ComponentY As Single, ComponentZ As Single)
            X = ComponentX
            Y = ComponentY
            Z = ComponentZ
        End Sub
        Public Property X() As Single
        Public Property Y() As Single
        Public Property Z() As Single
        Public ReadOnly Property Location() As Point3D
            Get
                Return New Point3D(Me.X, Me.Y, Me.Y)
            End Get
        End Property
        Public Shared Function Round(Point3D As Point3D) As Point3D
            Dim BufferX, BufferY, BufferZ As Single
            BufferX = Point3D.X
            BufferY = Point3D.Y
            BufferZ = Point3D.Z

            Return New Point3D(CSng(System.Math.Round(BufferX)), CSng(System.Math.Round(BufferY)), CSng(System.Math.Round(BufferZ)))

        End Function

        Public Shared Function Add(Point3D As Point3D, Vector3 As Vector3) As Point3D
            Return New Point3D(Point3D.X + Vector3.X, Point3D.Y + Vector3.Y, Point3D.Z + Vector3.Z)
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("({0},{1},{2})", X, Y, Z)

        End Function

    End Structure
    Public Structure Vector3
        Public Sub New(Point3D As Point3D)
            X = Point3D.X
            Y = Point3D.Y
            Z = Point3D.Z
        End Sub
        Public Property X() As Single
        Public Property Y() As Single
        Public Property Z() As Single

        Public ReadOnly Property Length() As Double
            Get
                Return Sqrt(Pow(X, 2.0F) + Pow(Y, 2.0F) + Pow(Z, 2.0F))

            End Get
        End Property
        Public Property Location() As Point3D
            Get
                Return New Point3D(Me.X, Me.Y, Me.Z)
            End Get
            Set(value As Point3D)
                Me.X = value.X
                Me.Y = value.Y
                Me.Z = value.Z
            End Set
        End Property
        Public Overrides Function ToString() As String
            Return String.Format("({0},{1},{2})", X, Y, Z)
        End Function
        Public Shared Operator +(Vector1 As Vector3, Vector2 As Vector3) As Vector3
            Return New Vector3(New Point3D(Vector1.X + Vector2.X, Vector1.Y + Vector2.Y, Vector1.Z + Vector2.Z))
        End Operator
        Public Shared Operator -(Vector1 As Vector3, Vector2 As Vector3) As Vector3
            Return New Vector3(New Point3D(Vector1.X + Vector2.X, Vector1.Y + Vector2.Y, Vector1.Z + Vector2.Z))
        End Operator
        Public Shared Operator *(Vector1 As Vector3, d As Single) As Vector3
            Return New Vector3(New Point3D(Vector1.X * d, Vector1.Y * d, Vector1.Z * d))
        End Operator
        Public Shared Operator *(d As Single, Vector1 As Vector3) As Vector3
            Return New Vector3(New Point3D(Vector1.X * d, Vector1.Y * d, Vector1.Z * d))
        End Operator
        Public Shared Operator /(Vector1 As Vector3, d As Single) As Vector3
            Return New Vector3(New Point3D(Vector1.X / d, Vector1.Y / d, Vector1.Z / d))
        End Operator
        Public Shared Function ScalarProduct(Vector1 As Vector3, Vector2 As Vector3) As Single
            Return Vector1.X * Vector2.X + Vector1.Y * Vector2.Y + Vector1.Z * Vector2.Z
        End Function
        Public Shared Function IsOrthogenal(Vector1 As Vector3, Vector2 As Vector3) As Boolean
            If Single.Equals(ScalarProduct(Vector1, Vector2), 0) Then
                Return True
            Else
                Return False
            End If
        End Function
    End Structure
End Namespace