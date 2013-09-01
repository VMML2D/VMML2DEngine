Public Class GroupOfRectangle
    Public GroupOfRectangles As New Stack(Of System.Drawing.Rectangle)
    Private Arr As System.Drawing.Rectangle()

    Public Sub Add(Rec As System.Drawing.Rectangle)
        GroupOfRectangles.Push(Rec)
    End Sub
    Public Sub Flush()
        GroupOfRectangles.Clear()
    End Sub

    Public Function GetRectangles2D() As System.Drawing.Rectangle
        For Each recs In GroupOfRectangles
            Return recs
        Next
    End Function
End Class

