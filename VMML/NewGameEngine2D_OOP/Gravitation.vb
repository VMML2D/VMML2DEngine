Public Class Gravitation
    'ToDo
    'Gravitate objects in stack
    Private ObjectsRecCollision As New Stack(Of System.Drawing.RectangleF)

    Private YJumpBuffer As Single
    Private NewRectangle As System.Drawing.RectangleF
    Private IsColladed As Boolean
    Private Y As Single
    Private Property Gravitation As Boolean

    Private Pressed As Boolean

    Private GravitationSpeed As Single



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
    Public Function Gravitate(Rec As System.Drawing.RectangleF, MaximumJump As Single, MaximumFall As Single, Speed As Single) As System.Drawing.RectangleF
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
                GravitationSpeed = 0
            Else
                GravitationSpeed += Speed / 3
                Y += GravitationSpeed
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
        NewRectangle = New System.Drawing.RectangleF(Rec.Location.X, Y, 30, 30)
        Return NewRectangle
    End Function
End Class
