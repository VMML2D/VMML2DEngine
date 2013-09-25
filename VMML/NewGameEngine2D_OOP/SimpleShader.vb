Imports System.Drawing
Namespace SimpleShader
    Public Class PixelShader
        'ToDo
        'Pixelshader
        'Waterreflexion(Soon)
        'Light and Shadow
        Public Property ShaderQuality As Single

        'VMML_EasyShader
        Private StackOfGrass_test As New Stack(Of RectangleF)
        Private MatrixTransforming As Single
        Private Site_bool As Boolean
        Private rnd_ As New Random
        Public Sub New(Amount As Integer, ShaderSize As Point)
            For i = 0 To Amount
                StackOfGrass_test.Push(New RectangleF(rnd_.Next(0, ShaderSize.X), rnd_.Next(0, ShaderSize.Y), 1, rnd_.Next(2, 16)))
            Next
        End Sub
        Public Sub ShaderGrass(Graphics As Graphics)
            If MatrixTransforming >= 6 Then
                Site_bool = True
            ElseIf MatrixTransforming <= 0 Then
                Site_bool = False
            End If

            If Site_bool Then
                MatrixTransforming -= 0.1F
            Else

                MatrixTransforming += 0.1F
            End If

            Graphics.RotateTransform(MatrixTransforming / 12)

            For Each recs In StackOfGrass_test
                Graphics.FillRectangle(Brushes.DarkGreen, recs)
            Next
        End Sub
    End Class
End Namespace
