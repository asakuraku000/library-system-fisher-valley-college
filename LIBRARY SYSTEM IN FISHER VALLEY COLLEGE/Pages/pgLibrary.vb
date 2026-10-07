' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Page: pgLibrary  (sidebar > Library)
'
' A read-only "browse the catalog" page that anyone can open from
' the login screen's sidebar, before logging in. Search filters
' live (title / author / category). Built entirely in code so there
' is no Designer file to keep in sync.
' Uses Kahleb's shared DBConnection module.
' ============================================================

Imports System.Data
Imports System.Drawing.Drawing2D

Public Class pgLibrary
    Inherits Panel

    Private ReadOnly card As New Panel()
    Private ReadOnly picIcon As New PictureBox()
    Private ReadOnly lblTitle As New Label()
    Private ReadOnly lblSub As New Label()
    Private ReadOnly searchField As New RoundedField()
    Private ReadOnly txtSearch As New TextBox()
    Private ReadOnly btnRefresh As New RoundedButton()
    Private ReadOnly lblCount As New Label()
    Private ReadOnly grid As New DataGridView()
    Private ReadOnly lblNote As New Label()
    Private ReadOnly debounce As New System.Windows.Forms.Timer()

    Public Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Padding = New Padding(36, 28, 36, 28)
        Dock = DockStyle.Fill
        Visible = False

        ' --- white rounded card ---------------------------------
        card.BackColor = Color.White
        card.Dock = DockStyle.Fill
        Controls.Add(card)

        ' --- header ---------------------------------------------
        picIcon.Image = AppIcons.Scaled("library_blue", 44)
        picIcon.SizeMode = PictureBoxSizeMode.CenterImage
        picIcon.BackColor = Color.White

        lblTitle.Text = "Library Catalog"
        lblTitle.Font = New Font("Segoe UI", 20.0F, FontStyle.Bold)
        lblTitle.ForeColor = BrandDark
        lblTitle.BackColor = Color.White

        lblSub.Text = "Browse and search the books available at Fisher Valley College."
        lblSub.Font = New Font("Segoe UI", 10.0F)
        lblSub.ForeColor = MutedText
        lblSub.BackColor = Color.White

        ' --- search + refresh -----------------------------------
        card.Controls.Add(searchField)
        txtSearch.Font = New Font("Segoe UI", 10.5F)
        txtSearch.PlaceholderText = "Search by title, author, or category"
        searchField.Attach(txtSearch, AppIcons.Scaled("search_gray", 18), 14, 0)

        btnRefresh.Text = "Refresh"
        btnRefresh.Kind = ButtonKind.Outline
        btnRefresh.IconImage = AppIcons.Scaled("refresh_blue", 16)
        btnRefresh.IconOnRight = False

        lblCount.Font = New Font("Segoe UI", 9.5F)
        lblCount.ForeColor = MutedText
        lblCount.BackColor = Color.White

        ' --- grid -----------------------------------------------
        StyleGrid()

        lblNote.Text = "Want to borrow a book? Log in from the Home tab."
        lblNote.Font = New Font("Segoe UI", 9.5F, FontStyle.Italic)
        lblNote.ForeColor = MutedText
        lblNote.BackColor = Color.White

        card.Controls.Add(picIcon)
        card.Controls.Add(lblTitle)
        card.Controls.Add(lblSub)
        card.Controls.Add(btnRefresh)
        card.Controls.Add(lblCount)
        card.Controls.Add(grid)
        card.Controls.Add(lblNote)

        debounce.Interval = 300
        AddHandler debounce.Tick, AddressOf Debounce_Tick
        AddHandler txtSearch.TextChanged, AddressOf Search_TextChanged
        AddHandler txtSearch.KeyDown, AddressOf Search_KeyDown
        AddHandler btnRefresh.Click, AddressOf Refresh_Click
        AddHandler card.Resize, AddressOf Card_Resize
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then debounce.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    Private Sub StyleGrid()
        grid.ReadOnly = True
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.AllowUserToResizeRows = False
        grid.RowHeadersVisible = False
        grid.MultiSelect = False
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.BackgroundColor = Color.White
        grid.BorderStyle = BorderStyle.None
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        grid.GridColor = Color.FromArgb(226, 234, 244)
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.ColumnHeadersHeight = 38
        grid.RowTemplate.Height = 34
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        With grid.ColumnHeadersDefaultCellStyle
            .BackColor = BrandDark
            .ForeColor = Color.White
            .SelectionBackColor = BrandDark
            .SelectionForeColor = Color.White
            .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            .Padding = New Padding(8, 0, 0, 0)
        End With

        With grid.DefaultCellStyle
            .Font = New Font("Segoe UI", 10.0F)
            .ForeColor = Color.FromArgb(40, 48, 58)
            .SelectionBackColor = Color.FromArgb(214, 230, 247)
            .SelectionForeColor = Color.FromArgb(16, 40, 70)
            .Padding = New Padding(8, 0, 0, 0)
        End With

        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 250, 254)
    End Sub

    ' Manual layout so everything tracks the card size.
    Private Sub Card_Resize(sender As Object, e As EventArgs)
        ApplyRoundedRegion(card, 16)

        Dim w As Integer = card.ClientSize.Width
        Dim h As Integer = card.ClientSize.Height
        If w < 200 OrElse h < 200 Then Return

        Const margin As Integer = 30
        Const btnW As Integer = 130

        picIcon.SetBounds(margin, 22, 48, 48)
        lblTitle.SetBounds(margin + 58, 20, w - margin * 2 - 58, 36)
        lblSub.SetBounds(margin + 60, 56, w - margin * 2 - 60, 22)

        searchField.SetBounds(margin, 98, w - margin * 2 - btnW - 12, 42)
        btnRefresh.SetBounds(w - margin - btnW, 98, btnW, 42)

        lblCount.SetBounds(margin, 150, w - margin * 2, 20)
        grid.SetBounds(margin, 176, w - margin * 2, Math.Max(80, h - 176 - 52))
        lblNote.SetBounds(margin, h - 38, w - margin * 2, 22)
    End Sub

    ' ------------------------------------------------------------
    ' Data
    ' ------------------------------------------------------------
    ''' <summary>Re-runs the query with whatever is in the search box.</summary>
    Public Sub Reload()
        Dim q As String = txtSearch.Text.Trim()
        Try
            Dim pattern As String = "%" & EscapeLike(q) & "%"
            Dim sql As String =
                "SELECT title AS `Title`, author AS `Author`, category AS `Category`, " &
                "year_published AS `Year`, " &
                "CONCAT(copies_available, ' of ', copies_total) AS `Copies Available` " &
                "FROM tbl_books " &
                "WHERE status = 'Available' " &
                "AND (@q = '' OR title LIKE @pattern OR author LIKE @pattern OR category LIKE @pattern) " &
                "ORDER BY title"

            Dim dt As DataTable = DBConnection.GetDataTable(
                sql, New Dictionary(Of String, Object) From {{"@q", q}, {"@pattern", pattern}})

            grid.DataSource = dt
            If grid.Columns.Contains("Title") Then grid.Columns("Title").FillWeight = 220
            If grid.Columns.Contains("Author") Then grid.Columns("Author").FillWeight = 150
            If grid.Columns.Contains("Category") Then grid.Columns("Category").FillWeight = 130
            If grid.Columns.Contains("Year") Then grid.Columns("Year").FillWeight = 50
            If grid.Columns.Contains("Copies Available") Then grid.Columns("Copies Available").FillWeight = 90

            lblCount.ForeColor = MutedText
            lblCount.Text = If(dt.Rows.Count = 1, "1 book found", dt.Rows.Count.ToString() & " books found")
        Catch ex As Exception
            grid.DataSource = Nothing
            lblCount.ForeColor = Color.FromArgb(178, 34, 34)
            lblCount.Text = "Can't reach the library database. Make sure MySQL/XAMPP is running, then click Refresh."
        End Try
    End Sub

    ' So a search for "100%" or "a_b" is treated literally.
    Private Shared Function EscapeLike(s As String) As String
        Return s.Replace("\", "\\").Replace("%", "\%").Replace("_", "\_")
    End Function

    Private Sub Search_TextChanged(sender As Object, e As EventArgs)
        debounce.Stop()
        debounce.Start()
    End Sub

    Private Sub Debounce_Tick(sender As Object, e As EventArgs)
        debounce.Stop()
        Reload()
    End Sub

    Private Sub Search_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            debounce.Stop()
            Reload()
        End If
    End Sub

    Private Sub Refresh_Click(sender As Object, e As EventArgs)
        Reload()
    End Sub

End Class
