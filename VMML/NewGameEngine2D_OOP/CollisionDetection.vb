Public Class CollisionDetection
    'ToDo
    'Detect all recs in stack for collision and return a boolean
    Private GroupOfRectangles As New GroupOfRectangle
    Private Collapse As Boolean
    Public Function DetectCollision(RecToDetect As System.Drawing.Rectangle) As Boolean
        For Each Recs In GroupOfRectangles.GroupOfRectangles
            If RecToDetect.IntersectsWith(Recs) Then
                Collapse = True
                Exit For
            Else
                Collapse = False
            End If
        Next
        Return Collapse
    End Function
    Public Sub Add(rec As System.Drawing.Rectangle)
        GroupOfRectangles.Add(rec)
    End Sub
    Public Sub Flush()
        GroupOfRectangles.Flush()
    End Sub
End Class
