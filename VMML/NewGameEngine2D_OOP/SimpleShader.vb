Imports System.Drawing
Public Class SimpleShader
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
    Public Sub New(Amount As Integer)
        For i = 0 To Amount
            StackOfGrass_test.Push(New RectangleF(rnd_.Next(0, 1400), rnd_.Next(0, 700), 1, rnd_.Next(2, 16)))
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
            Graphics.FillRectangle(Brushes.darkgreen, recs)
        Next
    End Sub
End Class
