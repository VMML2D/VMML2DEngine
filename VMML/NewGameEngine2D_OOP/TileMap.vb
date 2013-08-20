Imports System.IO
Imports System.Drawing
Namespace TileMap
    ''' <summary>
    ''' Loading a 2D-TileMap from a file(supporting all extensions!)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LoadMap
        'Declarations
        Private Open_ReadFile As StreamReader
        '---LoadedMap---
        Public Map As String
        'Konstruktor
        ''' <summary>
        ''' Intialize a new File.
        ''' </summary>
        ''' <param name="Path"></param>
        ''' <remarks></remarks>
        Public Sub New(Path As String)
            Try
                If File.Exists(Path) Then
                    Open_ReadFile = New StreamReader(Path)
                    Map = Open_ReadFile.ReadToEnd
                Else
                    Throw New ArgumentException("This path is not exist!")
                End If
            Catch
                Throw New ArgumentException(Err.GetException.ToString)
            End Try
        End Sub
    End Class
    ''' <summary>
    ''' A class to detect collision by rec in a (!) converted TileMap.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CollisionDetection_TileMap
        Private Objects As New Stack(Of RectangleF)
        Private int_buffer As Integer
        Private Loaded As Boolean
        Protected Friend Collapse As Boolean

        Public Sub AddObject(ByVal Map As String, Rectangle As RectangleF)
            If Loaded Then
            Else
                Objects.Push(Rectangle)
                If int_buffer >= Map.Length Then
                    Loaded = True
                Else
                    int_buffer += 1
                End If
            End If
        End Sub
        Public Sub Flush()
            Objects.Clear()
            int_buffer = 0
            Loaded = False
        End Sub
        Public Sub CollisionDetection(Rec As RectangleF)
            For Each RectangleCollision In Objects
                If Rec.IntersectsWith(RectangleCollision) Then
                    Collapse = True
                    Exit For
                Else
                    Collapse = False
                End If
            Next
        End Sub

    End Class
    ''' <summary>
    ''' Class to convert a map to the supported tilemap.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConvertMap
        Private MapBuffer As String
        Private Y_LineBuffer As String
        Private MapPath As String
        Private MapX, MapY As Integer

        Private SBuffer_LineForLine As String()

        'Collision
        Private Wand As RectangleF
        Private Boden As RectangleF
        Public CollisionStack As New CollisionDetection_TileMap
        'Camera
        Private Camera As New Camera
        Private NewPos As PointF
        'Public properties
        Public Property TextureWall As Bitmap
        Public Property TextureGround As Bitmap


        Public Sub New(Path As String, Map As String)
            MapPath = Path
            MapBuffer = Map
            SBuffer_LineForLine = File.ReadAllLines(MapPath)
        End Sub
        Public Overrides Function ToString() As String
            Return MapBuffer
        End Function
        Public Sub MoveCamera(PositionX As Boolean, Speed As Single)
            If PositionX Then
                Camera.CameraX += Speed
            Else
                Camera.CameraY += Speed
            End If
        End Sub
        Public Sub DrawTiles(Graphics As Graphics, ConvertSize As Double)
            Try
                For y = 0 To MapBuffer.Length
                    Y_LineBuffer = SBuffer_LineForLine(y)
                    For x = 0 To Y_LineBuffer.Length / ConvertSize
                        Dim ConvertMap As String() = Y_LineBuffer.Split(" "c)
                        Select Case ConvertMap(Convert.ToInt32(x))
                            Case "0"
                                MapX = Convert.ToInt32(x * 30)
                                MapY = y * 30
                                NewPos = Camera.MoveCamera(MapX, MapY)
                                Boden = New RectangleF(NewPos, New Size(30, 30))
                                Graphics.DrawImage(TextureGround, Boden)
                            Case "1"
                                MapX = Convert.ToInt32(x * 30)
                                MapY = y * 30
                                NewPos = Camera.MoveCamera(MapX, MapY)
                                Wand = New RectangleF(NewPos, New Size(30, 30))
                                CollisionStack.AddObject(MapBuffer, Wand)
                                Graphics.DrawImage(TextureWall, Wand)
                            Case Else
                                MessageBox.Show("This converted map contains charcters, that don't exist in this version of the engine!", "CastInvalidException", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Environment.Exit(Environment.ExitCode)

                        End Select
                    Next
                Next
                Graphics.Dispose()
                TextureWall.Dispose()
                TextureGround.Dispose()
            Catch
            End Try
        End Sub

        Private TileTexture As TileTexture

        Public Sub DrawTileSetsFromBitmap(Graphics As Graphics, TileSetPath2D As String, ConverterSizeBuffer As Double)
            TileTexture = New TileTexture(TileSetPath2D)
            Try
                For y = 0 To MapBuffer.Length
                    Y_LineBuffer = SBuffer_LineForLine(y)
                    For x = 0 To Y_LineBuffer.Length / ConverterSizeBuffer
                        Dim ConvertMap As String() = Y_LineBuffer.Split(" "c)

                        Select Case ConvertMap(Convert.ToInt32(x))
                            Case "0"
                                TileTexture.GetTileSet(Graphics, 32, 0, 0, Convert.ToInt32(x * 32), y * 32, 32)
                            Case "1"
                                TileTexture.GetTileSet(Graphics, 32, 5, 0, Convert.ToInt32(x * 32), y * 32, 32)
                        End Select
                    Next
                Next
            Catch
            End Try
        End Sub
        Public Function Collision(rec As Rectangle) As Boolean
            CollisionStack.CollisionDetection(rec)
            If CollisionStack.Collapse Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
    ''' <summary>
    ''' Player class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Player
        Private TexturePlayer As Bitmap
        Private LocationOfPlayerRec As PointF
        Private Player As RectangleF
        Private X, Y As Single
        Public Sub New(Texture2D As String)
            TexturePlayer = New Bitmap(Texture2D)
        End Sub
        Public Sub MoveLocation(SpeedF As Single, XBoolean As Boolean)
            If XBoolean Then

                X += SpeedF
            Else
                Y += SpeedF
            End If
        End Sub
        Public Sub RenderPlayer(Graphics As Graphics, WidthF As Single, HeightF As Single)
            Player = New RectangleF(X, Y, WidthF, HeightF)
            Graphics.DrawImage(TexturePlayer, Player)
        End Sub
    End Class
    ''' <summary>
    ''' This class allows you move the camera in a 2D-sphere
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Camera
        Public Property CameraX As Single
        Public Property CameraY As Single

        Protected Friend Function MoveCamera(MapX As Single, MapY As Single) As PointF
            Dim ResultX As Single = MapX + CameraX
            Dim ResultY As Single = MapY + CameraY
            Return New PointF(ResultX, ResultY)
        End Function
    End Class
    ''' <summary>
    ''' This class allows you to load a tile from a *.bmp-TileSet.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TileTexture
        Private MapTile As Bitmap
        Private BufferTile As Bitmap

        Private Tile As RectangleF
        Public Sub New(TileTexturePath2D As String)
            MapTile = New System.Drawing.Bitmap(TileTexturePath2D)
        End Sub
        Public Sub GetTileSet(Graphics As Graphics, TileSize As Integer, TileNumberX As Double, TileNumberY As Double, X As Integer, Y As Integer, Size As Integer)
            TileNumberX = TileNumberX * TileSize
            TileNumberY = TileNumberY * TileSize
            Tile = New RectangleF(Convert.ToSingle(TileNumberX), Convert.ToSingle(TileNumberY), Size, Size)
            Graphics.DrawImage(MapTile, X, Y, Tile, GraphicsUnit.Pixel)
        End Sub
    End Class
End Namespace
