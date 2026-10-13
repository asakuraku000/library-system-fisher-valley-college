' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Controls: the shared look & feel from the new login design.
'
'   RoundedButton  - rounded button with an optional icon
'                    (Log In ->, Create Account, Cancel ...)
'   RoundedField   - rounded text field with real inner padding
'                    and an optional icon (user / lock / search)
'   RoundedComboField - same rounded look around a dropdown list
'                    (Course / Department on Create Account)
'   SidebarButton  - icon-on-top nav item for the left sidebar
'   UiTheme        - colours + helpers (WrapTextBox etc.)
'
' You can use RoundedButton on any form: draw it from the
' Toolbox (after a build) or change "As Button" to
' "As RoundedButton" in a form's Designer file.
' ============================================================

Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Enum ButtonKind
    Primary     ' solid blue, white text            (Log In)
    Outline     ' transparent, blue border + text   (Create Account)
    Neutral     ' transparent, grey border          (Cancel)
    Custom      ' uses BackColor / ForeColor        (e.g. green Update, red Delete)
End Enum

' ------------------------------------------------------------
' Colours + helpers
' ------------------------------------------------------------
Public Module UiTheme

    Public ReadOnly Brand As Color = Color.FromArgb(24, 110, 190)
    Public ReadOnly BrandDark As Color = Color.FromArgb(16, 92, 160)
    Public ReadOnly BrandDarker As Color = Color.FromArgb(12, 72, 126)
    Public ReadOnly BrandTint As Color = Color.FromArgb(232, 242, 252)
    Public ReadOnly FieldBorder As Color = Color.FromArgb(200, 214, 230)
    Public ReadOnly MutedText As Color = Color.FromArgb(100, 110, 120)

    Public Function RoundedPath(r As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Integer = Math.Max(1, Math.Min(radius * 2, Math.Min(r.Width, r.Height)))
        path.AddArc(r.X, r.Y, d, d, 180, 90)
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    ''' <summary>Gives any control rounded corners (clips it with a region).</summary>
    Public Sub ApplyRoundedRegion(c As Control, radius As Integer)
        If c.Width <= 0 OrElse c.Height <= 0 Then Return
        Using p As GraphicsPath = RoundedPath(New Rectangle(0, 0, c.Width, c.Height), radius)
            Dim old As Region = c.Region
            c.Region = New Region(p)
            If old IsNot Nothing Then old.Dispose()
        End Using
    End Sub

    Public Function Darken(c As Color, amount As Single) As Color
        Dim k As Single = 1.0F - amount
        Return Color.FromArgb(c.A, CInt(c.R * k), CInt(c.G * k), CInt(c.B * k))
    End Function

    ''' <summary>
    ''' Puts an existing TextBox inside a new RoundedField at fieldBounds,
    ''' keeping its name, events, tab order and z-order. hPad = left/right
    ''' padding inside the field; rightReserve = extra room on the right
    ''' (e.g. for the show/hide-password eye button).
    ''' </summary>
    Public Function WrapTextBox(tb As TextBox, icon As Image, fieldBounds As Rectangle,
                                hPad As Integer, Optional rightReserve As Integer = 0) As RoundedField
        Dim parent As Control = tb.Parent
        Dim idx As Integer = parent.Controls.GetChildIndex(tb)
        Dim tab As Integer = tb.TabIndex

        Dim f As New RoundedField()
        f.SetBounds(fieldBounds.X, fieldBounds.Y, fieldBounds.Width, fieldBounds.Height)
        f.Anchor = tb.Anchor
        f.TabStop = False

        parent.Controls.Add(f)
        parent.Controls.SetChildIndex(f, idx)
        f.TabIndex = tab
        f.Attach(tb, icon, hPad, rightReserve)
        Return f
    End Function

    ''' <summary>
    ''' Same idea as WrapTextBox, but for a ComboBox: puts it inside a new
    ''' RoundedComboField at fieldBounds (rounded border, icon, chevron),
    ''' keeping its name, events, tab order and z-order.
    ''' </summary>
    Public Function WrapComboBox(cb As ComboBox, icon As Image, fieldBounds As Rectangle,
                                 hPad As Integer) As RoundedComboField
        Dim parent As Control = cb.Parent
        Dim idx As Integer = parent.Controls.GetChildIndex(cb)
        Dim tab As Integer = cb.TabIndex

        Dim f As New RoundedComboField()
        f.SetBounds(fieldBounds.X, fieldBounds.Y, fieldBounds.Width, fieldBounds.Height)
        f.Anchor = cb.Anchor
        f.TabStop = False

        parent.Controls.Add(f)
        parent.Controls.SetChildIndex(f, idx)
        f.TabIndex = tab
        f.Attach(cb, icon, hPad)
        Return f
    End Function

End Module

' ------------------------------------------------------------
' RoundedButton
' ------------------------------------------------------------
Public Class RoundedButton
    Inherits Button

    Private _kind As ButtonKind = ButtonKind.Primary
    Private _icon As Image
    Private _iconOnRight As Boolean = True
    Private _iconSize As Integer = 16
    Private _radius As Integer = 10
    Private _hover As Boolean
    Private _down As Boolean

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        FlatStyle = FlatStyle.Flat
        FlatAppearance.BorderSize = 0
        Cursor = Cursors.Hand
        Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
    End Sub

    <Category("Appearance"), DefaultValue(ButtonKind.Primary)>
    Public Property Kind As ButtonKind
        Get
            Return _kind
        End Get
        Set(value As ButtonKind)
            _kind = value
            Invalidate()
        End Set
    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property IconImage As Image
        Get
            Return _icon
        End Get
        Set(value As Image)
            _icon = value
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), DefaultValue(True)>
    Public Property IconOnRight As Boolean
        Get
            Return _iconOnRight
        End Get
        Set(value As Boolean)
            _iconOnRight = value
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), DefaultValue(16)>
    Public Property IconSize As Integer
        Get
            Return _iconSize
        End Get
        Set(value As Integer)
            _iconSize = Math.Max(8, value)
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), DefaultValue(10)>
    Public Property CornerRadius As Integer
        Get
            Return _radius
        End Get
        Set(value As Integer)
            _radius = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        _hover = True
        Invalidate()
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _hover = False
        _down = False
        Invalidate()
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnMouseDown(mevent As MouseEventArgs)
        _down = True
        Invalidate()
        MyBase.OnMouseDown(mevent)
    End Sub

    Protected Overrides Sub OnMouseUp(mevent As MouseEventArgs)
        _down = False
        Invalidate()
        MyBase.OnMouseUp(mevent)
    End Sub

    Protected Overrides Sub OnEnabledChanged(e As EventArgs)
        MyBase.OnEnabledChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnGotFocus(e As EventArgs)
        MyBase.OnGotFocus(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)
        Invalidate()
    End Sub

    Private Sub ResolveColors(ByRef fill As Color, ByRef border As Color, ByRef text As Color)
        Select Case _kind
            Case ButtonKind.Primary
                If Not Enabled Then
                    fill = Color.FromArgb(150, 182, 214)
                ElseIf _down Then
                    fill = BrandDarker
                ElseIf _hover Then
                    fill = BrandDark
                Else
                    fill = Brand
                End If
                border = Color.Transparent
                text = Color.White

            Case ButtonKind.Outline
                If _down Then
                    fill = Color.FromArgb(214, 230, 247)
                ElseIf _hover Then
                    fill = BrandTint
                Else
                    fill = Color.Transparent
                End If
                border = If(Enabled, Brand, Color.FromArgb(170, 190, 210))
                text = If(Enabled, Brand, Color.Gray)

            Case ButtonKind.Neutral
                If _down Then
                    fill = Color.FromArgb(226, 232, 238)
                ElseIf _hover Then
                    fill = Color.FromArgb(240, 244, 248)
                Else
                    fill = Color.Transparent
                End If
                border = Color.FromArgb(160, 170, 180)
                text = If(Enabled, Color.FromArgb(50, 60, 70), Color.Gray)

            Case Else ' Custom
                If _down Then
                    fill = Darken(BackColor, 0.2F)
                ElseIf _hover Then
                    fill = Darken(BackColor, 0.1F)
                Else
                    fill = BackColor
                End If
                border = Color.Transparent
                text = ForeColor
        End Select
    End Sub

    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        Dim g As Graphics = pevent.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.PixelOffsetMode = PixelOffsetMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic

        ' Corners show whatever is behind the button.
        If Parent IsNot Nothing AndAlso Parent.BackColor.A = 255 Then
            g.Clear(Parent.BackColor)
        Else
            ButtonRenderer.DrawParentBackground(g, ClientRectangle, Me)
        End If

        Dim fill As Color, border As Color, textColor As Color
        ResolveColors(fill, border, textColor)

        Dim body As New Rectangle(1, 1, Width - 3, Height - 3)
        Using path As GraphicsPath = RoundedPath(body, _radius)
            If fill.A > 0 Then
                Using br As New SolidBrush(fill)
                    g.FillPath(br, path)
                End Using
            End If
            If border.A > 0 Then
                Using pen As New Pen(border, 1.5F)
                    g.DrawPath(pen, path)
                End Using
            End If
        End Using

        DrawContent(g, textColor)

        ' Keyboard focus hint (only when tabbing).
        If Focused AndAlso ShowFocusCues Then
            Dim inner As New Rectangle(4, 4, Width - 9, Height - 9)
            Using path As GraphicsPath = RoundedPath(inner, Math.Max(2, _radius - 3))
                Using pen As New Pen(Color.FromArgb(150, textColor), 1.0F)
                    pen.DashStyle = DashStyle.Dot
                    g.DrawPath(pen, path)
                End Using
            End Using
        End If
    End Sub

    Private Sub DrawContent(g As Graphics, textColor As Color)
        Dim flags As TextFormatFlags = TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or
                                       TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or
                                       TextFormatFlags.NoPrefix
        Dim textSize As Size = TextRenderer.MeasureText(g, Text, Font, New Size(Integer.MaxValue, Integer.MaxValue), flags)

        Dim hasIcon As Boolean = (_icon IsNot Nothing)
        Dim gap As Integer = If(hasIcon AndAlso Text.Length > 0, 8, 0)
        Dim iconW As Integer = If(hasIcon, _iconSize, 0)
        Dim x As Integer = (Width - (textSize.Width + gap + iconW)) \ 2
        Dim iconTop As Integer = (Height - _iconSize) \ 2

        If hasIcon AndAlso Not _iconOnRight Then
            g.DrawImage(_icon, New Rectangle(x, iconTop, _iconSize, _iconSize))
            x += iconW + gap
        End If

        TextRenderer.DrawText(g, Text, Font, New Rectangle(x, 0, textSize.Width + 2, Height), textColor, flags)
        x += textSize.Width + gap

        If hasIcon AndAlso _iconOnRight Then
            g.DrawImage(_icon, New Rectangle(x, iconTop, _iconSize, _iconSize))
        End If
    End Sub

End Class

' ------------------------------------------------------------
' RoundedField - rounded box holding a borderless TextBox, with
' real padding (the thing a plain WinForms TextBox can't do).
' ------------------------------------------------------------
Public Class RoundedField
    Inherits Panel

    Private Const IconPx As Integer = 18

    Private _inner As TextBox
    Private _icon As Image
    Private _hPad As Integer = 12
    Private _rightReserve As Integer = 0
    Private _focused As Boolean

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        BackColor = Color.White
    End Sub

    Public ReadOnly Property InnerTextBox As TextBox
        Get
            Return _inner
        End Get
    End Property

    ''' <summary>Moves tb inside this field and lays it out with padding.</summary>
    Public Sub Attach(tb As TextBox, icon As Image, hPad As Integer, rightReserve As Integer)
        _inner = tb
        _icon = icon
        _hPad = hPad
        _rightReserve = rightReserve

        tb.BorderStyle = BorderStyle.None
        tb.BackColor = BackColor
        Controls.Add(tb)

        AddHandler tb.Enter, AddressOf InnerFocusChanged
        AddHandler tb.Leave, AddressOf InnerFocusChanged

        LayoutInner()
        Invalidate()
    End Sub

    Private Sub InnerFocusChanged(sender As Object, e As EventArgs)
        _focused = (_inner IsNot Nothing AndAlso _inner.Focused)
        Invalidate()
    End Sub

    Private Sub LayoutInner()
        If _inner Is Nothing Then Return
        Dim left As Integer = _hPad + If(_icon IsNot Nothing, IconPx + 10, 0)
        Dim w As Integer = Math.Max(20, Width - left - _hPad - _rightReserve)
        _inner.Width = w
        _inner.Left = left
        _inner.Top = Math.Max(0, (Height - _inner.Height) \ 2)
    End Sub

    Protected Overrides Sub OnResize(eventargs As EventArgs)
        MyBase.OnResize(eventargs)
        LayoutInner()
    End Sub

    ' Clicking anywhere in the padding focuses the text box.
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If _inner IsNot Nothing Then _inner.Focus()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.InterpolationMode = InterpolationMode.HighQualityBicubic

        If Parent IsNot Nothing Then g.Clear(Parent.BackColor)

        Dim body As New Rectangle(1, 1, Width - 3, Height - 3)
        Using path As GraphicsPath = RoundedPath(body, 9)
            Using br As New SolidBrush(BackColor)
                g.FillPath(br, path)
            End Using
            Using pen As New Pen(If(_focused, Brand, FieldBorder), If(_focused, 1.6F, 1.0F))
                g.DrawPath(pen, path)
            End Using
        End Using

        If _icon IsNot Nothing Then
            g.DrawImage(_icon, New Rectangle(_hPad, (Height - IconPx) \ 2, IconPx, IconPx))
        End If
    End Sub

End Class

' ------------------------------------------------------------
' RoundedComboField - rounded box holding a dropdown list, so a
' ComboBox matches the RoundedField text boxes (border, focus
' colour, icon, padding). The ComboBox's own arrow button is
' pushed out of view and a chevron is drawn by the field instead.
' ------------------------------------------------------------
Public Class RoundedComboField
    Inherits Panel

    Private Const IconPx As Integer = 18
    Private Const ChevronZone As Integer = 34    ' room kept on the right for the chevron
    Private Const HiddenButton As Integer = 26   ' strip of the combo that is clipped away (its own arrow)

    Private _combo As ComboBox
    Private _clip As Panel
    Private _icon As Image
    Private _hPad As Integer = 12
    Private _focused As Boolean
    Private _placeholder As String = ""

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        BackColor = Color.White
    End Sub

    Public ReadOnly Property InnerComboBox As ComboBox
        Get
            Return _combo
        End Get
    End Property

    ''' <summary>Grey hint shown while nothing is selected.</summary>
    <DefaultValue("")>
    Public Property PlaceholderText As String
        Get
            Return _placeholder
        End Get
        Set(value As String)
            _placeholder = If(value, "")
            If _combo IsNot Nothing Then _combo.Invalidate()
        End Set
    End Property

    ''' <summary>Moves cb inside this field (list-only, custom drawn, no own border).</summary>
    Public Sub Attach(cb As ComboBox, icon As Image, hPad As Integer)
        _combo = cb
        _icon = icon
        _hPad = hPad

        cb.DropDownStyle = ComboBoxStyle.DropDownList
        cb.FlatStyle = FlatStyle.Flat
        cb.BackColor = Color.White
        cb.ForeColor = Color.FromArgb(40, 48, 56)
        cb.DrawMode = DrawMode.OwnerDrawFixed
        cb.ItemHeight = 22
        cb.Dock = DockStyle.None
        cb.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' The clip panel hides the combo's own arrow button (the part of
        ' the combo that sticks out past the clip panel's right edge).
        _clip = New Panel()
        _clip.BackColor = Color.White
        Controls.Add(_clip)
        _clip.Controls.Add(cb)
        cb.Location = New Point(0, 0)

        AddHandler cb.DrawItem, AddressOf Combo_DrawItem
        AddHandler cb.Enter, AddressOf InnerFocusChanged
        AddHandler cb.Leave, AddressOf InnerFocusChanged
        AddHandler cb.DropDown, AddressOf InnerFocusChanged
        AddHandler cb.DropDownClosed, AddressOf InnerFocusChanged

        LayoutInner()
        Invalidate()
    End Sub

    Private Sub InnerFocusChanged(sender As Object, e As EventArgs)
        _focused = (_combo IsNot Nothing AndAlso (_combo.Focused OrElse _combo.DroppedDown))
        Invalidate()
    End Sub

    Private Sub LayoutInner()
        If _combo Is Nothing OrElse _clip Is Nothing Then Return
        Dim left As Integer = _hPad + If(_icon IsNot Nothing, IconPx + 10, 0)
        Dim clipW As Integer = Math.Max(20, Width - left - ChevronZone)
        _clip.SetBounds(left, Math.Max(0, (Height - _combo.Height) \ 2), clipW, _combo.Height)
        _combo.Width = clipW + HiddenButton
        _combo.DropDownWidth = Math.Max(clipW + HiddenButton, 120)
    End Sub

    Protected Overrides Sub OnResize(eventargs As EventArgs)
        MyBase.OnResize(eventargs)
        LayoutInner()
    End Sub

    ' Clicking the padding / chevron opens the list.
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If _combo IsNot Nothing Then
            _combo.Focus()
            _combo.DroppedDown = Not _combo.DroppedDown
        End If
    End Sub

    ' Draws both the closed box (ComboBoxEdit) and every list row, so the
    ' selected text never turns into the default blue highlight block.
    Private Sub Combo_DrawItem(sender As Object, e As DrawItemEventArgs)
        Dim cb As ComboBox = DirectCast(sender, ComboBox)
        Dim inEdit As Boolean = (e.State And DrawItemState.ComboBoxEdit) = DrawItemState.ComboBoxEdit
        Dim hot As Boolean = Not inEdit AndAlso (e.State And DrawItemState.Selected) = DrawItemState.Selected

        Using br As New SolidBrush(If(hot, BrandTint, Color.White))
            e.Graphics.FillRectangle(br, e.Bounds)
        End Using

        Dim text As String
        Dim fore As Color
        If e.Index < 0 Then
            If Not inEdit OrElse _placeholder.Length = 0 Then Return
            text = _placeholder
            fore = Color.FromArgb(140, 150, 160)
        Else
            text = cb.GetItemText(cb.Items(e.Index))
            fore = If(hot, BrandDarker, Color.FromArgb(40, 48, 56))
        End If

        Dim pad As Integer = If(inEdit, 0, 6)
        Dim flags As TextFormatFlags = TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or
                                       TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine Or
                                       TextFormatFlags.NoPadding Or TextFormatFlags.EndEllipsis
        TextRenderer.DrawText(e.Graphics, text, cb.Font,
                              New Rectangle(e.Bounds.X + pad, e.Bounds.Y, e.Bounds.Width - pad, e.Bounds.Height),
                              fore, flags)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.InterpolationMode = InterpolationMode.HighQualityBicubic

        If Parent IsNot Nothing Then g.Clear(Parent.BackColor)

        Dim body As New Rectangle(1, 1, Width - 3, Height - 3)
        Using path As GraphicsPath = RoundedPath(body, 9)
            Using br As New SolidBrush(BackColor)
                g.FillPath(br, path)
            End Using
            Using pen As New Pen(If(_focused, Brand, FieldBorder), If(_focused, 1.6F, 1.0F))
                g.DrawPath(pen, path)
            End Using
        End Using

        If _icon IsNot Nothing Then
            g.DrawImage(_icon, New Rectangle(_hPad, (Height - IconPx) \ 2, IconPx, IconPx))
        End If

        ' chevron
        Dim cx As Single = Width - 20.0F
        Dim cy As Single = Height / 2.0F
        Using pen As New Pen(MutedText, 1.8F)
            pen.StartCap = LineCap.Round
            pen.EndCap = LineCap.Round
            pen.LineJoin = LineJoin.Round
            g.DrawLines(pen, New PointF() {
                New PointF(cx - 4.5F, cy - 2.0F),
                New PointF(cx, cy + 2.5F),
                New PointF(cx + 4.5F, cy - 2.0F)})
        End Using
    End Sub

End Class

' ------------------------------------------------------------
' SidebarButton - icon on top, small caption underneath,
' lighter rounded tile when selected (like the mockup).
' ------------------------------------------------------------
Public Class SidebarButton
    Inherits Control

    Private Const IconPx As Integer = 26

    Private _icon As Image
    Private _selected As Boolean
    Private _hover As Boolean

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.Selectable, False)
        Cursor = Cursors.Hand
        Font = New Font("Segoe UI", 8.25F)
        ForeColor = Color.White
        Size = New Size(84, 72)
    End Sub

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property IconImage As Image
        Get
            Return _icon
        End Get
        Set(value As Image)
            _icon = value
            Invalidate()
        End Set
    End Property

    <DefaultValue(False)>
    Public Property Selected As Boolean
        Get
            Return _selected
        End Get
        Set(value As Boolean)
            If _selected <> value Then
                _selected = value
                Invalidate()
            End If
        End Set
    End Property

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        _hover = True
        Invalidate()
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _hover = False
        Invalidate()
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = PixelOffsetMode.HighQuality

        g.Clear(If(Parent IsNot Nothing, Parent.BackColor, BrandDark))

        Dim tile As New Rectangle(10, 4, Width - 21, Height - 9)
        If _selected OrElse _hover Then
            Dim tileColor As Color = If(_selected, Color.FromArgb(52, 138, 214), Color.FromArgb(34, 112, 184))
            Using path As GraphicsPath = RoundedPath(tile, 12)
                Using br As New SolidBrush(tileColor)
                    g.FillPath(br, path)
                End Using
            End Using
        End If

        Dim iconTop As Integer = tile.Top + 10
        If _icon IsNot Nothing Then
            g.DrawImage(_icon, New Rectangle((Width - IconPx) \ 2, iconTop, IconPx, IconPx))
        End If

        Dim flags As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.Top Or
                                       TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or
                                       TextFormatFlags.NoPrefix
        TextRenderer.DrawText(g, Text, Font, New Rectangle(0, iconTop + IconPx + 3, Width, 18), ForeColor, flags)
    End Sub

End Class
