Friend Class GroupOfRectangle
    Protected Friend GroupOfRectangles As New Stack(Of System.Drawing.Rectangle)
    Protected Friend Sub Add(Rec As System.Drawing.Rectangle)
        GroupOfRectangles.Push(Rec)
    End Sub
    Protected Friend Sub Flush()
        GroupOfRectangles.Clear()
    End Sub
End Class
