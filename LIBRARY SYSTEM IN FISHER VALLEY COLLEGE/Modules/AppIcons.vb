' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Module: AppIcons
'
' Purpose: Loads the icon PNGs that are EMBEDDED inside the .exe
' (Resources\Icons\*.png, set as Embedded Resource in the .vbproj),
' so the app shows all its icons with NO internet and NO loose
' image files next to the .exe.
'
' Usage:  button.Image = AppIcons.Scaled("arrow_right_white", 16)
'         pic.Image    = AppIcons.Load("book_open_blue")
' The name is the file name without ".png".
' Icons: Lucide (ISC license) - see Resources\Icons\LICENSE-lucide.txt
' ============================================================

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection

Module AppIcons

    Private ReadOnly Cache As New Dictionary(Of String, Bitmap)(StringComparer.OrdinalIgnoreCase)
    Private ReadOnly Gate As New Object()

    ''' <summary>Returns the icon at its original size (or Nothing if the name doesn't exist).</summary>
    Public Function Load(name As String) As Image
        SyncLock Gate
            Dim hit As Bitmap = Nothing
            If Cache.TryGetValue(name, hit) Then Return hit

            Dim asm As Assembly = Assembly.GetExecutingAssembly()
            Using s As Stream = asm.GetManifestResourceStream("FVCIcons." & name & ".png")
                If s Is Nothing Then Return Nothing
                Using raw As Image = Image.FromStream(s)
                    hit = New Bitmap(raw)   ' copy, so the stream can be closed
                End Using
            End Using

            Cache(name) = hit
            Return hit
        End SyncLock
    End Function

    ''' <summary>Returns the icon smoothly scaled to size x size pixels (cached).</summary>
    Public Function Scaled(name As String, size As Integer) As Image
        Dim key As String = name & "@" & size.ToString()
        SyncLock Gate
            Dim hit As Bitmap = Nothing
            If Cache.TryGetValue(key, hit) Then Return hit
        End SyncLock

        Dim src As Image = Load(name)
        If src Is Nothing Then Return Nothing

        Dim bmp As New Bitmap(size, size, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.SmoothingMode = SmoothingMode.HighQuality
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.DrawImage(src, New Rectangle(0, 0, size, size))
        End Using

        SyncLock Gate
            Cache(key) = bmp
        End SyncLock
        Return bmp
    End Function

End Module
