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
    End Structure
End Namespace

