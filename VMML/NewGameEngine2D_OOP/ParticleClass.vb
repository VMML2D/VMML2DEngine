Imports System.Drawing
Namespace ParticleClass
    Public Class Particles
        'ToDo
        'Particals to support a dynamic game atmosphere
        Private ParticalStack As New Stack(Of System.Drawing.Rectangle)
        Private rnd As New Random
        Private YBuffer As Integer
        Public Sub DrawRainParticles(Graphics As System.Drawing.Graphics, Optional Amount As Integer = 0)
            ParticalStack.Clear()
            YBuffer += rnd.Next(1, 1200 + rnd.Next(0, 120))
            If Amount <= 0 Then
                ParticalStack.Push(New System.Drawing.Rectangle(rnd.Next(0, 1200), YBuffer, 1, 30))
            Else
                For i = 0 To Amount
                    ParticalStack.Push(New System.Drawing.Rectangle(rnd.Next(0, 1200), YBuffer, 1, 30))
                Next
            End If
            If YBuffer >= 600 Then
                YBuffer = 0
            End If
            For Each recs In ParticalStack
                Graphics.FillRectangle(Drawing.Brushes.Blue, recs)
            Next
        End Sub
    End Class
End Namespace
