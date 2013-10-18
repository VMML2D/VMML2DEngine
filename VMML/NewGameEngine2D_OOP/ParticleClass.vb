Imports System.Drawing
Namespace ParticleClass
    Public Class Particles
        'ToDo
        'Particals to support a dynamic game atmosphere

        Public Sub New(Amount As Integer)
            MaximumParticleAmount = Amount
        End Sub
        Private ListOfSplashObject As New List(Of RectangleF)
        Private MaximumParticleAmount As Integer = 12000
        Private ArrayOfSplashObject(MaximumParticleAmount) As RectangleF
        Private YPosOfSplashParticle(MaximumParticleAmount) As Single
        Private SplashedBool(MaximumParticleAmount) As Boolean
        Private MaximumHeight(MaximumParticleAmount) As Integer
        Private Collapse(MaximumParticleAmount) As Boolean
        Private XPos(MaximumParticleAmount) As Integer
        Private Rnd As New Random
        Private Done As Boolean

        Private Float(MaximumParticleAmount) As Single
        Private Float1(MaximumParticleAmount) As Single

        Private AmountOfParticles(MaximumParticleAmount) As Integer

        Private FloatI(MaximumParticleAmount) As Single
        Private Float1I(MaximumParticleAmount) As Single

        Private AmountOfParticlesI(MaximumParticleAmount) As Integer


        Private CollisionObject As New List(Of RectangleF)
        Private ArrayOfCollisionObjects(MaximumParticleAmount) As RectangleF

        Public Function WaterSplash(MaximumFall As Integer, MaxiumSplashHeight As Integer, DropSpeed As Single, WindEffect As Boolean, WindStärkeMin As Integer, WindStärkeMax As Integer, SpeedMin As Integer, SpeedMax As Integer, Objects As RectangleF()) As RectangleF()
            Try
                ListOfSplashObject.CopyTo(ArrayOfSplashObject)
                For i = 0 To ArrayOfSplashObject.GetUpperBound(0) - 1
                    For j = 0 To Objects.Length - 1
                        ArrayOfSplashObject(i).Location = New PointF(ArrayOfSplashObject(i).Location.X + XPos(i) + Float(i + 1) + Float1(i) + FloatI(i + 1) + Float1I(i), ArrayOfSplashObject(i).Location.Y + YPosOfSplashParticle(i))

                        If ArrayOfSplashObject(i).Location.Y >= MaximumFall Then

                            SplashedBool(i) = True
                        ElseIf ArrayOfSplashObject(i).IntersectsWith(New RectangleF(ArrayOfSplashObject(i).Location.X, ArrayOfSplashObject(CInt(i / 2)).Location.Y, ArrayOfSplashObject(i).Size.Width, ArrayOfSplashObject(i).Size.Height)) Then
                            FloatI(i) += Rnd.Next(1, 11)
                            Float1I(i) -= Rnd.Next(1, 11)

                            SplashedBool(i) = True
                            Collapse(i) = True

                        ElseIf ArrayOfSplashObject(i).IntersectsWith(New RectangleF(Objects(j).Location.X, Objects(j).Location.Y, Objects(j).Width, Objects(j).Height)) Then


                            Float(i) += Rnd.Next(1, 15)
                            Float1(i) -= Rnd.Next(1, 12)


                            SplashedBool(i) = True
                            Collapse(i) = True
                        ElseIf Not ArrayOfSplashObject(i).IntersectsWith(Objects(j)) Then
                            Collapse(i) = False

                        End If
                    Next
                    If SplashedBool(i) Then
                        If MaximumHeight(i) >= MaxiumSplashHeight Then

                            SplashedBool(i) = False

                        Else

                            YPosOfSplashParticle(i) -= DropSpeed


                            MaximumHeight(i) += 1
                        End If
                    ElseIf Collapse(i) Then

                    Else
                        If WindEffect Then
                            XPos(i) = Rnd.Next(WindStärkeMin, WindStärkeMax)
                        End If

                        YPosOfSplashParticle(i) += Rnd.Next(SpeedMin, SpeedMax)

                        MaximumHeight(i) = 0
                    End If


                Next
                Return ArrayOfSplashObject
            Catch

            End Try

        End Function
        Public Function Length() As Integer
            Return ListOfSplashObject.Count

        End Function
        Private Amount As Integer

        Public Function AddWaterSplashRec(SplashRecF As RectangleF) As Boolean
            If ListOfSplashObject.Count >= MaximumParticleAmount Then
                Done = True
                Return False
            Else
                ListOfSplashObject.Add(SplashRecF)
                Return True
            End If
        End Function
        Function IntersectsWith(ObjectRecF As RectangleF) As Boolean
            For i = 0 To ArrayOfCollisionObjects.GetUpperBound(0)
                If ArrayOfCollisionObjects(i).IntersectsWith(ObjectRecF) Then
                    Return True
                    Exit For
                Else
                    Return False
                End If
            Next
        End Function
        Public Sub Flush()
            CollisionObject.Clear()
        End Sub
        Public Function AddCollisionObj(ObjectRecF As RectangleF) As Boolean
            Try
                CollisionObject.Add(ObjectRecF)
                CollisionObject.CopyTo(ArrayOfCollisionObjects)
            Catch
                Return False
            End Try
        End Function

    End Class
End Namespace
