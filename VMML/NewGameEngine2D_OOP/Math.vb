﻿Imports System.Math

Namespace Math

    Public Structure Trigonemetry
        Public Const PI As Double = 3.1415926535897931
        Private Shared Function Sqrt(a As Double) As Double
            Dim xn As Double = 1.0
            Do
                xn = (xn + (a / xn)) / 2
            Loop While ((xn * xn) - a) > 0.0001
            Return xn
        End Function
        Public Shared Function Angle2(AngleA As Double, AngleB As Double) As Double
            Dim a As Double = AngleA
            Dim b As Double = AngleB

            Dim c As Double
            Dim alpha As Double

            c = Sqrt(a ^ 2 + b ^ 2)
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
            Return Sqrt(a ^ 2 + b ^ 2)
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
    End Structure
    Public Structure Vector2
        'ToDo
        'Several operators
        'Calculating and returning of 2 points (Vector2)
        Public Sub New(ComponentX As Single, ComponentY As Single)
            ComponentX = X
            ComponentY = X

        End Sub
        Public Property X() As Single
        Public Property Y() As Single

        Public ReadOnly Property Length() As Double
            Get
                Return Sqrt(Pow(X, 2.0F) + Pow(Y, 2.0F))
            End Get
        End Property
        Public Overrides Function ToString() As String
            Return String.Format("({0},{0})", X, Y)
        End Function

        Public Shared Operator +(ByVal v1 As Vector2, v2 As Vector2) As Vector2
            Return New Vector2(v1.X + v2.Y, v1.Y + v2.Y)
        End Operator
        Public Shared Operator -(v1 As Vector2, v2 As Vector2) As Vector2
            Return New Vector2(v1.X - v2.X, v1.Y - v2.Y)
        End Operator
        Public Shared Operator *(v1 As Vector2, d As Single) As Vector2
            Return New Vector2(v1.X * d, v1.Y * d)
        End Operator
        Public Shared Operator *(d As Single, v1 As Vector2) As Vector2
            Return New Vector2(v1.X * d, v1.Y * d)
        End Operator
        Public Shared Operator /(v1 As Vector2, d As Single) As Vector2
            Return New Vector2(v1.X / d, v1.Y / d)
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
End Namespace