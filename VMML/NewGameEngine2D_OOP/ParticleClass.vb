Imports System.Drawing
Namespace ParticleClass
    Public Class Particles
        'ToDo
        'Particals to support a dynamic game atmosphere
        Private ListOfSplashObject As New List(Of RectangleF)
        Private MaximumParticleAmount As Integer = 9000
        Private ArrayOfSplashObject(MaximumParticleAmount) As RectangleF
        Private YPosOfSplashParticle(MaximumParticleAmount) As Single
        Private SplashedBool(MaximumParticleAmount) As Boolean
        Private MaximumHeight(MaximumParticleAmount) As Integer
        Private Collapse(MaximumParticleAmount) As Boolean
        Private XPos(MaximumParticleAmount) As Integer
        Private Rnd As New Random
        Private Done As Boolean


        Public Function Splash(MaximumFall As Integer, MaxiumSplashHeight As Integer, WindEffect As Boolean, WindStärkeMin As Integer, WindStärkeMax As Integer, Speed As Single) As RectangleF()
            Try
                ListOfSplashObject.CopyTo(ArrayOfSplashObject)
                For i = 0 To ArrayOfSplashObject.GetUpperBound(0) - 1

                    ArrayOfSplashObject(i).Location = New PointF(ArrayOfSplashObject(i).Location.X + XPos(i), ArrayOfSplashObject(i).Location.Y + YPosOfSplashParticle(i))
                    If ArrayOfSplashObject(i).Location.Y >= MaximumFall Then

                        SplashedBool(i) = True
                    ElseIf ArrayOfSplashObject(i).IntersectsWith(New RectangleF(ArrayOfSplashObject(i).Location.X, ArrayOfSplashObject(CInt(i / 6)).Location.Y, ArrayOfSplashObject(i).Size.Width, ArrayOfSplashObject(i).Size.Height)) Then

                        SplashedBool(i) = False
                        Collapse(i) = True
                    End If

                    If SplashedBool(i) Then
                        If MaximumHeight(i) >= MaxiumSplashHeight Then
                            If Done Then
                            Else
                                SplashedBool(i) = False
                            End If
                        Else

                            YPosOfSplashParticle(i) -= 0.2F

                            MaximumHeight(i) += 1
                        End If
                    ElseIf Collapse(i) Then
                    Else
                        If WindEffect Then
                            XPos(i) = Rnd.Next(WindStärkeMin, WindStärkeMax)
                        End If

                        YPosOfSplashParticle(i) += Speed
                        MaximumHeight(i) = 0
                    End If

                Next
                Return ArrayOfSplashObject
            Catch

            End Try

        End Function
        Private Amount As Integer

        Public Function AddSplashRec(SplashRecF As RectangleF) As Boolean
            If ListOfSplashObject.Count >= MaximumParticleAmount Then
                Done = True
                Return False
            Else
                ListOfSplashObject.Add(SplashRecF)
                Return True
            End If
        End Function
    End Class
End Namespace
