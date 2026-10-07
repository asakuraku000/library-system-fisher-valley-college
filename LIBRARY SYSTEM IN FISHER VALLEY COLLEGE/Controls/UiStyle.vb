' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Module: UiStyle  -- the login screen's look, for every other screen.
'
'   Fields   TextBoxes are put inside a RoundedField (same rounded
'            box, border colour, focus colour and inner padding as
'            the login fields).
'   Buttons  RoundedButton (Primary / Outline / Neutral / Custom).
'            Admin Designer files already declare their buttons as
'            RoundedButton; member pages create them in code.
'   Tables   StyleGrid gives every DataGridView the login palette:
'            brand-blue header, soft row lines, tinted selection and
'            roomy cell padding.
'
' UiStyle.Apply(form) styles everything on a form in one call. It is
' safe to call more than once. main.vb (admin) and frmMemberDashboard
' call it for every page they embed, so the pages themselves need no
' extra code.
' ============================================================

Public Module UiStyle

    ''' <summary>Styles every TextBox and DataGridView on the form (and its children).</summary>
    Public Sub Apply(root As Control)
        If root Is Nothing Then Return

        ' snapshot first: wrapping a TextBox changes the Controls collection
        Dim all As New List(Of Control)
        Collect(root, all)

        For Each c As Control In all
            If TypeOf c Is DataGridView Then
                StyleGrid(DirectCast(c, DataGridView))
            ElseIf TypeOf c Is TextBox Then
                WrapField(DirectCast(c, TextBox))
            End If
        Next
    End Sub

    Private Sub Collect(c As Control, into As List(Of Control))
        For Each child As Control In c.Controls
            into.Add(child)
            Collect(child, into)
        Next
    End Sub

    ' ------------------------------------------------------------
    ' Fields
    ' ------------------------------------------------------------
    ''' <summary>
    ''' Moves a single-line TextBox into a RoundedField that takes its place
    ''' (same cell / dock / anchor / tab order / z-order). The TextBox keeps
    ''' its name and events, so existing code keeps working. Returns Nothing
    ''' when the box is skipped (multi-line, no parent, already wrapped).
    ''' </summary>
    Public Function WrapField(tb As TextBox, Optional icon As Image = Nothing) As RoundedField
        If tb Is Nothing OrElse tb.Parent Is Nothing Then Return Nothing
        If TypeOf tb.Parent Is RoundedField Then Return Nothing
        If tb.Multiline Then Return Nothing

        Dim parent As Control = tb.Parent
        Dim idx As Integer = parent.Controls.GetChildIndex(tb)
        Dim tabIdx As Integer = tb.TabIndex

        Dim f As New RoundedField()
        f.TabStop = False
        f.Name = "fld" & tb.Name

        If TypeOf parent Is TableLayoutPanel Then
            ' sits in a grid cell: take the same cell and fill it
            Dim tlp As TableLayoutPanel = DirectCast(parent, TableLayoutPanel)
            Dim pos As TableLayoutPanelCellPosition = tlp.GetPositionFromControl(tb)
            Dim colSpan As Integer = tlp.GetColumnSpan(tb)
            Dim rowSpan As Integer = tlp.GetRowSpan(tb)

            tlp.Controls.Remove(tb)
            f.Dock = DockStyle.Fill
            f.Margin = New Padding(0, 2, 0, 2)
            tlp.Controls.Add(f, pos.Column, pos.Row)
            tlp.SetColumnSpan(f, colSpan)
            tlp.SetRowSpan(f, rowSpan)
        Else
            ' plain panel / flow panel: a little taller than the TextBox
            ' (4 px above and below) -- the same 23 -> 31 px the login uses
            f.Size = New Size(tb.Width, tb.Height + 8)
            f.Location = New Point(tb.Left, tb.Top - 4)
            f.Anchor = tb.Anchor
            f.Dock = tb.Dock
            f.Margin = New Padding(tb.Margin.Left, Math.Max(0, tb.Margin.Top - 4),
                                   tb.Margin.Right, Math.Max(0, tb.Margin.Bottom - 4))
            parent.Controls.Add(f)
            parent.Controls.SetChildIndex(f, idx)
        End If

        f.TabIndex = tabIdx
        f.Attach(tb, icon, 8, 0)

        ' inside the field the TextBox is positioned by the field itself
        tb.Dock = DockStyle.None
        tb.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        tb.Margin = New Padding(0)

        ' hiding the TextBox from code (txt.Visible = False) hides its field too
        AddHandler tb.VisibleChanged, Sub(s, e) f.Visible = tb.Visible
        Return f
    End Function

    ' ------------------------------------------------------------
    ' Tables
    ' ------------------------------------------------------------
    ''' <summary>The login palette applied to a DataGridView. Column sizing is left alone.</summary>
    Public Sub StyleGrid(g As DataGridView)
        g.BackgroundColor = Color.White
        g.BorderStyle = BorderStyle.None
        g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        g.GridColor = Color.FromArgb(232, 238, 245)
        g.RowHeadersVisible = False
        g.AllowUserToResizeRows = False
        g.EnableHeadersVisualStyles = False

        ' header
        g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        g.ColumnHeadersHeight = 38
        With g.ColumnHeadersDefaultCellStyle
            .BackColor = BrandDark
            .ForeColor = Color.White
            .SelectionBackColor = BrandDark
            .SelectionForeColor = Color.White
            .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleLeft
            .Padding = New Padding(10, 0, 10, 0)
            .WrapMode = DataGridViewTriState.False
        End With

        ' rows
        g.RowTemplate.Height = 32
        For Each r As DataGridViewRow In g.Rows
            r.Height = 32
        Next
        With g.DefaultCellStyle
            .BackColor = Color.White
            .ForeColor = Color.FromArgb(40, 48, 56)
            .SelectionBackColor = BrandTint
            .SelectionForeColor = BrandDarker
            .Font = New Font("Segoe UI", 9.0F)
            .Padding = New Padding(10, 0, 10, 0)
        End With
        With g.AlternatingRowsDefaultCellStyle
            .BackColor = Color.FromArgb(247, 250, 254)
            .SelectionBackColor = BrandTint
            .SelectionForeColor = BrandDarker
        End With
    End Sub

End Module
