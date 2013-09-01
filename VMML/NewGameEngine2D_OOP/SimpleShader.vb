Public Class SimpleShader
    Public Property ShaderQuality As Single

    Private VMMLStack_Top, VMMLStack_Middle, VMMLStack_Bottom As New Stack(Of System.Drawing.Rectangle)

    Private BufferTop1, BufferTop2, BufferMiddle1, BufferMiddle2, BufferBottom1, BufferBottom2 As Integer
    Private Texture2d As System.Drawing.Bitmap
    Public Sub AddToContent(fragment_top_2d As System.Drawing.Rectangle, fragment_middle_2d As System.Drawing.Rectangle, fragment_bottom_2d As System.Drawing.Rectangle, vmml_2d_texture As vmml_texture2d)
        VMMLStack_Top.Push(fragment_top_2d)
        VMMLStack_Middle.Push(fragment_middle_2d)
        VMMLStack_Bottom.Push(fragment_bottom_2d)
        Texture2d = vmml_2d_texture.Texture_2d
    End Sub
    Public Sub VMMLSV(renderer As System.Drawing.Graphics, Optional MaximumBuffer As Integer = 50)
        Select Case ShaderQuality
            Case 1
                For Each recs In VMMLStack_Bottom
                    If BufferTop1 >= MaximumBuffer Then

                    End If
                Next
            Case 2

            Case 3

            Case 4

            Case 5

            Case Else
                Throw New Exception("This engine supports only a quality of maximum fifth strogness.")
        End Select

    End Sub
End Class
