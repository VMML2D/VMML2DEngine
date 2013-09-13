Imports System.IO
Imports System.Drawing

Imports System.Net
Imports System.Threading
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
                    Throw New ArgumentException("This path is not exist: " & Path)
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
        Private ShowedGrid As Boolean
        'Public properties
        Public Property TextureWall As Bitmap
        Public Property TextureGround As Bitmap
        Public Property TileSize As Integer

        '---INI---
        Private ReadINI As INIDatei

        Private ID_Wall As String
        Private ID_Ground As String

        Private ConvertSize As Integer

        Private TextureWallINI As Bitmap
        Private TextureGroundINI As Bitmap

        Private XGrid, YGrid, BufferGrid As Integer


        ''' <summary>
        ''' Converts the map from binaries in tiles
        ''' </summary>
        ''' <param name="Path"></param>
        ''' <param name="Map"></param>
        ''' <param name="IDListPath"></param>
        ''' <remarks></remarks>
        Public Sub New(Path As String, Map As String, Optional ByVal IDListPath As String = "")
            If IDListPath = "" Then
            Else
                ReadINI = New INIDatei(IDListPath)
                ReadID()
            End If

            MapPath = Path
            MapBuffer = Map
            SBuffer_LineForLine = File.ReadAllLines(MapPath)
        End Sub
        Private Sub ReadID()
            ID_Wall = ReadINI.WertLesen("ID", "WALL_")
            ID_Ground = ReadINI.WertLesen("ID", "GROUND_")


            TextureWallINI = New Bitmap(ReadINI.WertLesen("TEXTURE2D", "TEXTURE_WALL_"))
            TextureGroundINI = New Bitmap(ReadINI.WertLesen("TEXTURE2D", "TEXTURE_GROUND_"))
            ConvertSize = Integer.Parse(ReadINI.WertLesen("MAPSIZE", "MAPSIZE_X_"))
        End Sub
        ''' <summary>
        ''' Show the map before it's converted.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Return MapBuffer
        End Function
        ''' <summary>
        ''' Filling tiles via ini-file-informations.
        ''' </summary>
        ''' <param name="graphics"></param>
        ''' <remarks></remarks>
        Public Sub RecsByID(graphics As Graphics)
            Try
                For y = 0 To MapBuffer.Length
                    Y_LineBuffer = SBuffer_LineForLine(y)
                    For x = 0 To Y_LineBuffer.Length / ConvertSize
                        Dim ConvertMap() As String = Y_LineBuffer.Split(" "c)
                        Select Case ConvertMap(Convert.ToInt32(x))
                            Case ID_Ground.ToString
                                graphics.DrawImage(TextureGroundINI, New Rectangle(Convert.ToInt32(x * 30), y * 30, 30, 30))
                            Case ID_Wall.ToString
                                graphics.DrawImage(TextureWallINI, New Rectangle(Convert.ToInt32(x * 30), y * 30, 30, 30))
                        End Select
                    Next
                Next
            Catch
            End Try
        End Sub
        ''' <summary>
        ''' Moving the tilecamera to show other regions of the map.
        ''' </summary>
        ''' <param name="PositionX"></param>
        ''' <param name="Speed"></param>
        ''' <remarks></remarks>
        Public Sub MoveCamera(PositionX As Boolean, Speed As Single)
            If PositionX Then
                Camera.CameraX += Speed
            Else
                Camera.CameraY += Speed
            End If
            CollisionStack.Flush()

        End Sub
        ''' <summary>
        ''' Show grid, tile by tile.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ShowTileGrid(Graphics As Graphics)

            For i = -80 To 600
                If BufferGrid >= 16 Then
                    YGrid += TileSize
                    XGrid = 0
                    BufferGrid = 0
                Else
                    XGrid += TileSize
                    BufferGrid += 1
                End If
                Graphics.DrawRectangle(Pens.Red, New Rectangle(YGrid, XGrid, TileSize, TileSize))

            Next
            YGrid = 0
            XGrid = -TileSize
            BufferGrid = 0
        End Sub
        ''' <summary>
        ''' Converting and filling tilesets with the declared textures.
        ''' </summary>
        ''' <param name="Graphics"></param>
        ''' <param name="ConvertSize"></param>
        ''' <remarks></remarks>
        Public Sub DrawTiles(Graphics As Graphics, ConvertSize As Double)
            Try
                For y = 0 To MapBuffer.Length
                    Y_LineBuffer = SBuffer_LineForLine(y)
                    For x = 0 To Y_LineBuffer.Length / ConvertSize
                        Dim ConvertMap As String() = Y_LineBuffer.Split(" "c)
                        Select Case ConvertMap(Convert.ToInt32(x))
                            Case "0"
                                If TextureGround Is Nothing Then
                                    Exit Select
                                Else
                                    MapX = Convert.ToInt32(x * TileSize)
                                    MapY = y * TileSize
                                    NewPos = Camera.MoveCamera(MapX, MapY)
                                    Boden = New RectangleF(NewPos, New Size(TileSize, TileSize))
                                    Graphics.DrawImage(TextureGround, Boden)
                                End If
                            Case "1"
                                MapX = Convert.ToInt32(x * TileSize)
                                MapY = y * TileSize
                                NewPos = Camera.MoveCamera(MapX, MapY)
                                Wand = New RectangleF(NewPos, New Size(TileSize, TileSize))
                                CollisionStack.AddObject(MapBuffer, Wand)
                                Graphics.DrawImage(TextureWall, Wand)
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
        ''' <summary>
        ''' Filling tiles with textures, that belongs a tileset-texture-image.
        ''' </summary>
        ''' <param name="Graphics"></param>
        ''' <param name="TileSetPath2D"></param>
        ''' <param name="ConverterSizeBuffer"></param>
        ''' <remarks></remarks>
        Public Sub DrawTileSetsFromBitmap(Graphics As Graphics, TileSetPath2D As String, ConverterSizeBuffer As Double)
            TileTexture = New TileTexture(New vmml_texture2d(TileSetPath2D))
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
        ''' <summary>
        ''' Check out a collision between defined rectangle and tilerecs.
        ''' </summary>
        ''' <param name="rec"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Collision(rec As RectangleF) As Boolean
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
        Public Sub New(Texture2D As vmml_texture2d)
            TexturePlayer = Texture2D.Texture_2d
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
        ''' <summary>
        ''' Initialize a new TileSet_Bitmap.
        ''' </summary>
        ''' <param name="TileTexturePath2D"></param>
        ''' <remarks></remarks>
        Public Sub New(TileTexturePath2D As vmml_texture2d)
            MapTile = TileTexturePath2D.Texture_2d
        End Sub
        ''' <summary>
        ''' Show and fill a rectangle with the defined tileset-texture.
        ''' </summary>
        ''' <param name="Graphics"></param>
        ''' <param name="TileSize"></param>
        ''' <param name="TileNumberX"></param>
        ''' <param name="TileNumberY"></param>
        ''' <param name="X"></param>
        ''' <param name="Y"></param>
        ''' <param name="Size"></param>
        ''' <remarks></remarks>
        Public Sub GetTileSet(Graphics As Graphics, TileSize As Integer, TileNumberX As Double, TileNumberY As Double, X As Integer, Y As Integer, Size As Integer)
            TileNumberX = TileNumberX * TileSize
            TileNumberY = TileNumberY * TileSize
            Tile = New RectangleF(Convert.ToSingle(TileNumberX), Convert.ToSingle(TileNumberY), Size, Size)
            Graphics.DrawImage(MapTile, X, Y, Tile, GraphicsUnit.Pixel)
        End Sub
    End Class
    Public Class NetworkTileMap
        'ToDo
        'Sending an Connected-Size by 1 Byte(1)
        'Recieving the TileMap(Downloading TileMap)

        'Declarations

        Private TileMap_BufferString As String

        Public Sub HostTileMap(ByVal ToUploadTileMap As String)
            'Get TileMap by constructor
            'Set TileMap_BufferString to custom constructor- Expression
            'Upload TileMap_BufferString

            TileMap_BufferString = ToUploadTileMap

        End Sub
        Public Sub DownloadTileMap(IP As String, Port As Integer, Path As String)
            'Recieve TileMap from custom Ip and port.
            'Save on custom path.
        End Sub
    End Class
End Namespace
