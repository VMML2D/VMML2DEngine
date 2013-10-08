Imports System.IO
Imports System.Drawing

Imports System.Net
Imports System.Threading
Imports System.Text.RegularExpressions

Namespace TileMap
    ''' <summary>
    ''' Loading a 2D-TileMap from a file(supporting all extensions!)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LoadMap
        'Declarations
        Private ReadFile As String
        '---LoadedMap---
        Private Map As String
        Public CompleteMap As String

        Private IntBuffer As Integer
        Private RndNumber As New Random
        'Konstruktor
        ''' <summary>
        ''' Intialize a new File.
        ''' </summary>
        ''' <param name="Path"></param>
        ''' <remarks></remarks>
        Public Sub New(Path As String)
            Try
                If File.Exists(Path) Then

                        ReadFile = File.ReadAllText(Path)
                        Map = ReadFile
                        If Not Map.Contains(",") Then
                            File.WriteAllText(Path, Map.Replace(" ", ","))
                            CompleteMap = File.ReadAllText(Path)
                            MessageBox.Show("Converted map to VMML2D_Version  0.21", "Converted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
1:                      Else
                            CompleteMap = File.ReadAllText(Path)
                        End If
                Else
                    Throw New ArgumentException("This path is not existing: " & Path)

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
        Public Property TileSize As Integer


        Private XGrid, YGrid, BufferGrid As Integer

        '---Texture_TileSets---
        Private TileSet2D As Bitmap
        ''' <summary>
        ''' Converts the map from binaries in tiles
        ''' </summary>
        ''' <param name="Path"></param>
        ''' <param name="Map"></param>
        ''' <param name="IDListPath"></param>
        ''' <remarks></remarks>
        Public Sub New(Path As String, Map As String, TileSet2D_vmml As vmml_texture2d)
            MapPath = Path
            MapBuffer = Map
            SBuffer_LineForLine = File.ReadAllLines(MapPath)
            TileSet2D = TileSet2D_vmml.Texture_2d
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
        Private Tile As RectangleF
        Private Tile_Wall As RectangleF

        Private InMapX, InMapY As Integer

        Public Sub ImageTile_In(Graphics As Graphics, ConvertSizeBuffer As Double, TileSize_ As Integer, Size As Integer)
            Try
                For Y = 0 To MapBuffer.Length
                    Y_LineBuffer = SBuffer_LineForLine(Y)
                    For X = 0 To Convert.ToInt32(Y_LineBuffer.Length / ConvertSizeBuffer)
                        For i = 1 To 20
                            Dim ConvertMap As String() = Y_LineBuffer.Split(","c)
                            Select Case ConvertMap(Convert.ToInt32(X))
                                Case i.ToString
                                    Tile = New RectangleF(Convert.ToSingle(i * TileSize_), 0.0F, Size, Size)
                                    InMapX = X * Size
                                    InMapY = Y * Size
                                    NewPos = Camera.MoveCamera(InMapX, InMapY)
                                    Graphics.DrawImage(TileSet2D, New RectangleF(NewPos, New SizeF(Size, Size)), Tile, GraphicsUnit.Pixel)
                                Case "-" & i
                                    Tile_Wall = New RectangleF(Convert.ToSingle(i * TileSize_), 0.0F, Size, Size)
                                    InMapX = X * Size
                                    InMapY = Y * Size
                                    NewPos = Camera.MoveCamera(InMapX, InMapY)
                                    Graphics.DrawImage(TileSet2D, New RectangleF(NewPos, New SizeF(Size, Size)), Tile_Wall, GraphicsUnit.Pixel)
                                    CollisionStack.AddObject(MapBuffer, New RectangleF(NewPos, New SizeF(Size, Size)))
                                Case "!" & i
                            End Select
                        Next
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
