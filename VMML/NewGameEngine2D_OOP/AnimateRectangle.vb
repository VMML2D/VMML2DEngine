Imports System.Drawing
Public Class AnimateRectangleInStack
    Private ListOfRectanlesF As New List(Of RectangleF)
    Private MaximumArrayAmount As Integer = 9000
    Private ArrayOfRectangles(MaximumArrayAmount) As RectangleF

    Public Function AnimatedRectangles(NewPos() As PointF, SizeF() As SizeF) As RectangleF()
        Try
            ListOfRectanlesF.CopyTo(ArrayOfRectangles)
            For i = 0 To ArrayOfRectangles.GetUpperBound(0) - 1
                ArrayOfRectangles(i) = New RectangleF(NewPos(i).X, NewPos(i).Y, SizeF(i).Width, SizeF(i).Height)
            Next
            Return ArrayOfRectangles
        Catch
            MessageBox.Show(Err.GetException.ToString)
        End Try
    End Function
    Private Buffer As Integer
    Public Function Add(RectangleF As RectangleF) As Boolean
        Try
            If Buffer >= MaximumArrayAmount Then
                Return False
            Else
                ListOfRectanlesF.Add(RectangleF)
                Buffer += 1
            End If
        Catch
            Return False
        End Try
    End Function
    Public Function Length() As Integer
        Return ArrayOfRectangles.GetUpperBound(0)
    End Function
End Class


