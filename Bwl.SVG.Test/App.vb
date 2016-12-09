Public Class App
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim svg As New SvgDocument(PredefinedCanvasType.canvasA1)
        For x = 0 To svg.Width Step 20
            svg.AddLine(x, 0, x, svg.Height,, 0.2)
        Next
        For y = 0 To svg.Height Step 20
            svg.AddLine(0, y, svg.Width, y,, 0.2)
        Next
        svg.WriteSVG("grid-a1-20mm-0.2mm.svg")
        Shell("explorer grid-a1-20mm-0.2mm.svg")
    End Sub
End Class
