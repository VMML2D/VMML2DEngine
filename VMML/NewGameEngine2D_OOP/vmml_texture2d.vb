Public Class vmml_texture2d

    Public Sub New(Texture2D_vmml As String)
        Texture_2d = New Drawing.Bitmap(Texture2D_vmml)
    End Sub

    Protected Friend Texture_2d As System.Drawing.Bitmap
End Class
