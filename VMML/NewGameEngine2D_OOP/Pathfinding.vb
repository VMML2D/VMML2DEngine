Option Strict On
Imports System.Drawing
Namespace Pathfinding
    'Coded by 3r0rXx/eniking1998


    Public Class Pathfindig_Bot
        Dim Buffer_1_X As Integer
        Dim Buffer_1_Y As Integer

        Dim rndm_int As New Random

        Dim Rndm_Buffer(3) As Integer
        '---Bot_Func()---
        Dim Hit_Bottom As Boolean

        'Steuerung_Bot
        Dim bool_a As Boolean
        Dim bool_d As Boolean
        Dim bool_s As Boolean
        Dim bool_w As Boolean

        Dim Calc_lineX As Integer
        Dim Calc_lineY As Integer

        Dim reached As Boolean

        '---Public_Variables---

        ''' <summary>
        ''' Die berechnete Variable, kann nun abgegriffen werden, um die Position(PointX) des Bot_Rectangles
        ''' neu festzulegen.(Nutzen Sie die Me.Invalidate()-Methode)
        ''' </summary>
        ''' <remarks></remarks>
        Public PosXBot As Double = 898
        ''' <summary>
        ''' Die berechnete Variable, kann nun abgegriffen werden, um die Position(PointY) des Bot_Rectangles
        ''' neu festzulegen.(Nutzen Sie die Me.Invalidate()-Methode)
        ''' </summary>
        ''' <remarks></remarks>
        Public PosYBot As Double = 280
        ''' <summary>
        ''' Die berechnete Variable, kann nun abgegriffen werden, um die Breite des Bot_Rectangles
        ''' neu festzulegen.(Nutzen Sie die Me.Invalidate()-Methode)
        ''' </summary>
        ''' <remarks></remarks>
        Public Bot_Width As Double = 81
        ''' <summary>
        ''' Die berechnete Variable, kann nun abgegriffen werden, um die Höhe des Bot_Rectangles
        ''' neu festzulegen.(Nutzen Sie die Me.Invalidate()-Methode)
        ''' </summary>
        ''' <remarks></remarks>
        Public Bot_Height As Double = 231

        '---Schlüssel_Deklaration---
        Dim Schlüssel As String = "Coded by 3r0rXx/eniking1998"

        '---RaumDimension_Height---
        ''' <summary>
        ''' Gibt die Höhe des Rectangles, unmittelbar am Y:0-Bereich,
        ''' und demzufolge die Höhe unmittelbar am  Y>0-Bereich an.
        ''' Dieses muss im folgendem Schema angegeben werden "zB:3.0"
        ''' </summary>
        ''' <remarks></remarks>
        Public Expanse As Double = 3.0
        ''' <summary>
        ''' Findet einen Weg zum Spieler, um Ihn anzugreifen.
        ''' Funktion befindet sich in der PreAlpha-Phase!
        ''' RecPlayer, der Spieler
        ''' RecBot, die Targetion
        ''' Object_Bot,festlegen des unteren Bereiches
        ''' Object_Top,festlegen des oberen Bereiches
        ''' SpeedX,Übergang des Rectangles(PointX)
        ''' SpeedY,Übergang des Rectangles(PointY)
        ''' </summary>
        ''' <param name="RecPlayer"></param>
        ''' <param name="RecBot"></param>
        ''' <param name="Object_Bot"></param>
        ''' <param name="Object_Top"></param>
        ''' <remarks></remarks>
        Public Sub Pathfinding(ByVal RecPlayer As Rectangle, ByVal RecBot As Rectangle, ByVal Object_Bot As Rectangle, ByVal Object_Top As Rectangle, ByVal SpeedX As Double, ByVal SpeedY As Double)
            '---Path(PointY)---
            If RecBot.IntersectsWith(Object_Bot) Then

                Hit_Bottom = True
            ElseIf RecBot.IntersectsWith(Object_Top) Then

                Hit_Bottom = False
            End If


            If reached = True Then
            Else
                If Hit_Bottom Then
                    If Rndm_Buffer(1) = rndm_int.Next(0, 120) Then
                        PosYBot -= SpeedY
                        Bot_Height -= Expanse
                    Else
                        Rndm_Buffer(1) = rndm_int.Next(0, 120)

                    End If

                ElseIf Rndm_Buffer(1) = rndm_int.Next(0, 120) Then
                    PosYBot += SpeedY
                    Bot_Height += Expanse
                Else
                    Rndm_Buffer(1) = rndm_int.Next(0, 120)
                End If
            End If
            '---Check reach---
            If RecBot.IntersectsWith(RecPlayer) Then
                reached = True
            Else
                reached = False
            End If

            '---Path(PointX)---

            Select Case Chr(rndm_int.Next(0, 28))
                Case ChrW(3)

                    If bool_a = False Then
                        bool_d = True
                    Else

                        If Rndm_Buffer(0) = rndm_int.Next(0, 1001) Then
                            bool_a = False

                        Else
                            Rndm_Buffer(0) += rndm_int.Next(0, 1001)
                            bool_a = True

                            PosXBot -= SpeedX

                        End If

                    End If

                    If Calc_lineX > 120 Then
                        bool_d = True
                        bool_a = False


                    Else
                        Calc_lineX = RecPlayer.Location.X - RecBot.Location.X

                    End If

                Case ChrW(8)
                    If bool_d = False Then
                        bool_a = True

                    Else

                        If Rndm_Buffer(0) = rndm_int.Next(0, 1001) Then
                            bool_a = False

                        Else
                            Rndm_Buffer(0) += rndm_int.Next(0, 1001)
                            bool_a = True

                            PosXBot += SpeedX

                        End If

                    End If

                    If Calc_lineX < 30 Then
                        bool_d = False
                        bool_a = True


                    Else
                        Calc_lineX = RecPlayer.Location.X - RecBot.Location.X

                    End If

            End Select
        End Sub
    End Class

End Namespace

