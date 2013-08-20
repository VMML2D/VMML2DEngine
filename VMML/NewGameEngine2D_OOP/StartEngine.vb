Imports System.IO
Namespace StartEngine
    Public Class LoadEngine
        Private btm As System.Drawing.Bitmap
        Private graphics As System.Drawing.Graphics
        Private Buffer As Integer

        Protected Friend loaded As Boolean

        Public Shared Sub Start(Form As Form)
            Try
                Form.Show()
                Form.Focus()
                Cursor.Hide()
                Do While True
                    Application.DoEvents()
                    Form.Invalidate()
                Loop
            Catch
                MessageBox.Show(Err.GetException.ToString)
            End Try

        End Sub
    End Class
    Public Class Unload
        Public Shared Sub Unload()
            Environment.Exit(Environment.ExitCode)
        End Sub
    End Class
End Namespace

