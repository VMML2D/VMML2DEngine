Public Class Gravitation
    'ToDo
    'Gravitate objects in stack
    Private ObjectsRecCollision As New Stack(Of System.Drawing.RectangleF)

    Private YJumpBuffer As Single
    Private NewLocation As System.Drawing.PointF
    Private IsColladed As Boolean
    Private Y As Single
    Private Property Gravitation As Boolean

    Private Pressed As Boolean


    Public Sub AddCollisionObjects(Rec As System.Drawing.RectangleF)
        ObjectsRecCollision.Push(Rec)
    End Sub
    Public Sub Flush()
        ObjectsRecCollision.Clear()
    End Sub
    Public Sub Jump()
        If Pressed Then
        Else
            Pressed = True
            Gravitation = False
        End If


    End Sub
    Public Function Gravitate(Rec As System.Drawing.RectangleF, MaximumJump As Single, MaximumFall As Single, Speed As Single) As System.Drawing.PointF
        For Each collisionobj In ObjectsRecCollision
            If Rec.IntersectsWith(collisionobj) Then
                IsColladed = True
                Exit For
            Else
                IsColladed = False
            End If
        Next
        If Gravitation Then
            If IsColladed Or Y >= MaximumFall Then
                Pressed = False
            Else
                Y += Speed
            End If
        Else
            If YJumpBuffer >= MaximumJump Then
                Gravitation = True
                YJumpBuffer = 0
            Else
                Y -= Speed
                YJumpBuffer += 1
            End If
        End If
        NewLocation = New Drawing.PointF(Rec.Location.X, Y)
        Return NewLocation
    End Function
End Class
