Public Enum SvgUnitType
    px
    mm
End Enum

Public Enum PredefinedCanvasType
    canvas1920x1080px
    canvas1024x600px
    canvas800x600px
    canvas640x480px
    canvas320x240px

    canvas400x400px
    canvas1000x1000px

    canvasA0
    canvasA1
    canvasA2
    canvasA3
    canvasA4
    canvasA5
End Enum

Public Class SvgDocument
    Public ReadOnly Property Width As Double
    Public ReadOnly Property Height As Double
    Public ReadOnly Property Units As SvgUnitType = SvgUnitType.px
    Private _stream As New IO.StringWriter

    Public Sub New(width As Double, height As Double, units As SvgUnitType)
        Me.Width = width
        Me.Height = height
        Me.Units = units
        WriteHeader()
    End Sub

    Public Sub New(canvasType As PredefinedCanvasType)
        Select Case canvasType
            Case PredefinedCanvasType.canvasA0 : Width = 841 : Height = 1189 : Units = SvgUnitType.mm
            Case PredefinedCanvasType.canvasA1 : Width = 594 : Height = 841 : Units = SvgUnitType.mm
            Case PredefinedCanvasType.canvasA2 : Width = 420 : Height = 594 : Units = SvgUnitType.mm
            Case PredefinedCanvasType.canvasA3 : Width = 297 : Height = 420 : Units = SvgUnitType.mm
            Case PredefinedCanvasType.canvasA4 : Width = 210 : Height = 297 : Units = SvgUnitType.mm
            Case PredefinedCanvasType.canvasA5 : Width = 148 : Height = 210 : Units = SvgUnitType.mm

            Case PredefinedCanvasType.canvas320x240px : Width = 320 : Height = 240 : Units = SvgUnitType.px
            Case PredefinedCanvasType.canvas400x400px : Width = 400 : Height = 400 : Units = SvgUnitType.px
            Case PredefinedCanvasType.canvas640x480px : Width = 640 : Height = 480 : Units = SvgUnitType.px
            Case PredefinedCanvasType.canvas800x600px : Width = 800 : Height = 600 : Units = SvgUnitType.px
            Case PredefinedCanvasType.canvas1000x1000px : Width = 1000 : Height = 1000 : Units = SvgUnitType.px
            Case PredefinedCanvasType.canvas1024x600px : Width = 1024 : Height = 600 : Units = SvgUnitType.px
            Case PredefinedCanvasType.canvas1920x1080px : Width = 1920 : Height = 1080 : Units = SvgUnitType.px
            Case Else
                Throw New NotImplementedException
        End Select
        WriteHeader()
    End Sub

    Private Sub WriteHeader()
        _stream.WriteLine("<svg version = ""1.1""")
        _stream.WriteLine("baseProfile = ""full""")
        _stream.WriteLine("xmlns = ""http://www.w3.org/2000/svg""")
        _stream.WriteLine("xmlns:xlink = ""http://www.w3.org/1999/xlink""")
        _stream.WriteLine("xmlns:ev = ""http://www.w3.org/2001/xml-events""")
        _stream.Write(AttrCrd("x", 0))
        _stream.Write(AttrCrd("y", 0))
        _stream.Write(AttrCrd("height", Height))
        _stream.Write(AttrCrd("width", Width))
        _stream.WriteLine(">")
    End Sub

    Private Function Crd(coord As Double) As String
        Return """" + coord.ToString.Replace(",", ".") + Units.ToString + """"
    End Function

    Private Function Attr(attrName As String, val As String) As String
        Return attrName + "=""" + val + """ "
    End Function

    Private Function AttrCrd(attrName As String, coord As Double) As String
        Return attrName + "=" + Crd(coord) + " "
    End Function

    Public Sub AddLine(x1 As Double, y1 As Double, x2 As Double, y2 As Double, Optional color As String = "black", Optional width As Double = 1.0)
        _stream.Write("<line ")
        _stream.Write(AttrCrd("x1", x1))
        _stream.Write(AttrCrd("y1", y1))
        _stream.Write(AttrCrd("x2", x2))
        _stream.Write(AttrCrd("y2", y2))

        _stream.Write(Attr("stroke", color.ToLower))
        _stream.Write(AttrCrd("stroke-width", width))
        _stream.WriteLine("/>")

    End Sub

    Private Function Footer() As String
        Return "</svg>"
    End Function

    Public Function GetSVG() As String
        Return _stream.ToString + Footer()
    End Function

    Public Sub WriteSVG(filename As String)
        IO.File.WriteAllText(filename, GetSVG, System.Text.Encoding.UTF8)
    End Sub

End Class
