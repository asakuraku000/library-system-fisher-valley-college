' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmMemberBrowseBooks  (member sidebar > Browse Books)
'
' Read-only book catalog for members. Same header strip as the
' other member pages (frmMemberPageBase), plus:
'   - live search (title / author / ISBN / publisher / accession no.)
'   - Category filter (from tbl_categories)
'   - "Available only" toggle
'   - Refresh button
'   - double-click a row to see the full book details
'
' Members can only LOOK here -- Add / Update / Delete stay in the
' admin Books screen. Archived books are hidden from members.
' Built entirely in code; uses the shared DBConnection module.
' ============================================================

Imports System.Data

Public Class frmMemberBrowseBooks
    Inherits frmMemberPageBase

    Private ReadOnly pnlToolbar As New FlowLayoutPanel()
    Private ReadOnly txtSearch As New TextBox()
    Private ReadOnly cboCategory As New ComboBox()
    Private ReadOnly chkAvailableOnly As New CheckBox()
    Private ReadOnly btnRefresh As New Button()
    Private ReadOnly grid As New DataGridView()
    Private ReadOnly lblCount As New Label()
    Private ReadOnly debounce As New System.Windows.Forms.Timer()

    ' True while the category list is being refilled, so changing the
    ' combo's items doesn't trigger extra book reloads.
    Private loadingFilters As Boolean = False

    Public Sub New()
        MyBase.New("Browse Books", "Fisher Valley College Library — Book Catalog")
        BuildUi()
    End Sub

    ' ------------------------------------------------------------
    ' UI
    ' ------------------------------------------------------------
    Private Sub BuildUi()
        SuspendLayout()

        ' --- toolbar: Search | Category | Available only | Refresh ---
        pnlToolbar.Dock = DockStyle.Top
        pnlToolbar.Height = 48
        pnlToolbar.WrapContents = False
        pnlToolbar.FlowDirection = FlowDirection.LeftToRight

        txtSearch.Width = 300
        txtSearch.Font = New Font("Segoe UI", 10.0F)
        txtSearch.Margin = New Padding(0, 2, 18, 0)

        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList
        cboCategory.Width = 220
        cboCategory.Font = New Font("Segoe UI", 10.0F)
        cboCategory.Margin = New Padding(0, 2, 18, 0)
        cboCategory.Items.Add("All Categories")
        cboCategory.SelectedIndex = 0

        chkAvailableOnly.AutoSize = True
        chkAvailableOnly.Text = "Available only"
        chkAvailableOnly.Font = New Font("Segoe UI", 9.5F)
        chkAvailableOnly.Margin = New Padding(0, 6, 18, 0)

        btnRefresh.Size = New Size(90, 30)
        btnRefresh.Text = "Refresh"
        btnRefresh.Font = New Font("Segoe UI", 9.0F)
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.BackColor = Color.FromArgb(17, 78, 128)
        btnRefresh.ForeColor = Color.White
        btnRefresh.Cursor = Cursors.Hand
        btnRefresh.Margin = New Padding(0, 1, 0, 0)

        pnlToolbar.Controls.Add(MakeToolbarLabel("Search:"))
        pnlToolbar.Controls.Add(txtSearch)
        pnlToolbar.Controls.Add(MakeToolbarLabel("Category:"))
        pnlToolbar.Controls.Add(cboCategory)
        pnlToolbar.Controls.Add(chkAvailableOnly)
        pnlToolbar.Controls.Add(btnRefresh)

        ' --- grid ---
        StyleGrid(grid)

        ' --- footer count ---
        lblCount.AutoSize = False
        lblCount.Dock = DockStyle.Bottom
        lblCount.Height = 26
        lblCount.TextAlign = ContentAlignment.BottomLeft
        lblCount.Font = New Font("Segoe UI", 9.0F)
        lblCount.ForeColor = Color.FromArgb(90, 90, 90)
        lblCount.Text = ""

        ' Fill first, toolbar last (the last control added docks first = top).
        pnlBody.Controls.Add(grid)
        pnlBody.Controls.Add(lblCount)
        pnlBody.Controls.Add(pnlToolbar)

        ' --- events ---
        debounce.Interval = 300
        AddHandler debounce.Tick, Sub(s, e) OnDebounceTick()
        AddHandler txtSearch.TextChanged, Sub(s, e) RestartDebounce()
        AddHandler cboCategory.SelectedIndexChanged, Sub(s, e) OnFilterChanged()
        AddHandler chkAvailableOnly.CheckedChanged, Sub(s, e) OnFilterChanged()
        AddHandler btnRefresh.Click, Sub(s, e) RefreshAll()
        AddHandler grid.CellDoubleClick, AddressOf grid_CellDoubleClick
        AddHandler grid.DataBindingComplete, AddressOf grid_DataBindingComplete
        AddHandler Disposed, Sub(s, e) debounce.Dispose()

        ResumeLayout(True)
    End Sub

    Private Function MakeToolbarLabel(caption As String) As Label
        Dim l As New Label()
        l.AutoSize = True
        l.Text = caption
        l.Font = New Font("Segoe UI", 9.5F)
        l.ForeColor = Color.FromArgb(40, 40, 40)
        l.Margin = New Padding(0, 7, 6, 0)
        Return l
    End Function

    Private Sub StyleGrid(g As DataGridView)
        g.Dock = DockStyle.Fill
        g.Font = New Font("Segoe UI", 9.0F)
        g.AllowUserToAddRows = False
        g.AllowUserToDeleteRows = False
        g.AllowUserToResizeRows = False
        g.ReadOnly = True
        g.MultiSelect = False
        g.RowHeadersVisible = False
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        g.BackgroundColor = Color.White
        g.BorderStyle = BorderStyle.FixedSingle
        g.GridColor = Color.FromArgb(225, 225, 225)
        g.RowTemplate.Height = 26
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250)
        g.EnableHeadersVisualStyles = False
        g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 78, 128)
        g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        g.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        g.ColumnHeadersHeight = 32
    End Sub

    ' ------------------------------------------------------------
    ' Load + filters
    ' ------------------------------------------------------------
    Private Sub frmMemberBrowseBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategories()
        LoadBooks()
    End Sub

    Private Sub RefreshAll()
        LoadCategories()
        LoadBooks()
    End Sub

    Private Sub RestartDebounce()
        debounce.Stop()
        debounce.Start()
    End Sub

    Private Sub OnDebounceTick()
        debounce.Stop()
        LoadBooks()
    End Sub

    Private Sub OnFilterChanged()
        If loadingFilters Then Return
        LoadBooks()
    End Sub

    ''' <summary>
    ''' Fills the Category dropdown from tbl_categories (the same master list
    ''' the admin Books screen uses), keeping the current choice if it still exists.
    ''' </summary>
    Private Sub LoadCategories()
        loadingFilters = True
        Try
            Dim keep As String = If(cboCategory.SelectedIndex > 0, cboCategory.Text, "")

            Dim dt As DataTable = DBConnection.GetDataTable(
                "SELECT category_name FROM tbl_categories ORDER BY category_name")

            cboCategory.Items.Clear()
            cboCategory.Items.Add("All Categories")
            For Each r As DataRow In dt.Rows
                cboCategory.Items.Add(r("category_name").ToString())
            Next

            Dim idx As Integer = If(keep = "", 0, cboCategory.Items.IndexOf(keep))
            cboCategory.SelectedIndex = If(idx < 0, 0, idx)
        Catch ex As Exception
            ' Non-fatal: the books grid still works with "All Categories".
            If cboCategory.Items.Count = 0 Then cboCategory.Items.Add("All Categories")
            If cboCategory.SelectedIndex < 0 Then cboCategory.SelectedIndex = 0
        Finally
            loadingFilters = False
        End Try
    End Sub

    ''' <summary>
    ''' Loads the catalog with the current search / category / availability
    ''' filters. Status for members: Available (a copy is on the shelf),
    ''' Unavailable (all copies borrowed), or Damaged / Lost.
    ''' </summary>
    Private Sub LoadBooks()
        Try
            Dim q As String = txtSearch.Text.Trim()
            Dim cat As String = If(cboCategory.SelectedIndex > 0, cboCategory.Text, "")

            Dim sql As String =
                "SELECT accession_number AS `Accession No.`, isbn AS `ISBN`, title AS `Title`, " &
                "author AS `Author`, publisher AS `Publisher`, year_published AS `Year`, " &
                "category AS `Category`, " &
                "CONCAT(copies_available, ' / ', copies_total) AS `Copies Available`, " &
                "CASE WHEN status <> 'Available' THEN status " &
                "     WHEN copies_available > 0 THEN 'Available' " &
                "     ELSE 'Unavailable' END AS `Status` " &
                "FROM tbl_books " &
                "WHERE status <> 'Archived' " &
                "AND (@q = '' OR title LIKE @like OR author LIKE @like OR isbn LIKE @like " &
                "     OR publisher LIKE @like OR accession_number LIKE @like) " &
                "AND (@cat = '' OR category = @cat) " &
                "AND (@avail = 0 OR (status = 'Available' AND copies_available > 0)) " &
                "ORDER BY title ASC"

            Dim dt As DataTable = DBConnection.GetDataTable(sql,
                New Dictionary(Of String, Object) From {
                    {"@q", q},
                    {"@like", "%" & q & "%"},
                    {"@cat", cat},
                    {"@avail", If(chkAvailableOnly.Checked, 1, 0)}
                })

            grid.DataSource = dt
            SetColumnWeights()

            Dim filtered As Boolean = (q <> "" OrElse cat <> "" OrElse chkAvailableOnly.Checked)
            lblCount.Text = If(dt.Rows.Count = 0,
                If(filtered, "No books match your search.", "The catalog is empty."),
                $"Showing {dt.Rows.Count} book(s)" & If(filtered, " (filtered)", "") &
                " — double-click a row for details.")

        Catch ex As Exception
            MessageBox.Show("Could not load the book catalog: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Gives the Title / Author columns more room than the short ones.</summary>
    Private Sub SetColumnWeights()
        SetWeight("Accession No.", 75)
        SetWeight("ISBN", 95)
        SetWeight("Title", 190)
        SetWeight("Author", 120)
        SetWeight("Publisher", 100)
        SetWeight("Year", 45)
        SetWeight("Category", 100)
        SetWeight("Copies Available", 75)
        SetWeight("Status", 75)
    End Sub

    Private Sub SetWeight(colName As String, weight As Single)
        If grid.Columns.Contains(colName) Then grid.Columns(colName).FillWeight = weight
    End Sub

    ''' <summary>Colours the Status cell: green = Available, red = Unavailable, grey = Damaged / Lost.</summary>
    Private Sub grid_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)
        If Not grid.Columns.Contains("Status") Then Return
        For Each r As DataGridViewRow In grid.Rows
            Select Case Convert.ToString(r.Cells("Status").Value)
                Case "Available"
                    r.Cells("Status").Style.ForeColor = Color.FromArgb(46, 125, 50)
                Case "Unavailable"
                    r.Cells("Status").Style.ForeColor = Color.Firebrick
                Case Else
                    r.Cells("Status").Style.ForeColor = Color.FromArgb(120, 120, 120)
            End Select
        Next
    End Sub

    ''' <summary>Double-click a row: show that book's full details.</summary>
    Private Sub grid_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return

        Dim r As DataGridViewRow = grid.Rows(e.RowIndex)
        Dim msg As String =
            "Title: " & CellText(r, "Title") & Environment.NewLine &
            "Author: " & CellText(r, "Author") & Environment.NewLine &
            "Publisher: " & CellText(r, "Publisher") & Environment.NewLine &
            "Year: " & CellText(r, "Year") & Environment.NewLine &
            "Category: " & CellText(r, "Category") & Environment.NewLine &
            "ISBN: " & CellText(r, "ISBN") & Environment.NewLine &
            "Accession No.: " & CellText(r, "Accession No.") & Environment.NewLine & Environment.NewLine &
            "Copies available: " & CellText(r, "Copies Available") & Environment.NewLine &
            "Status: " & CellText(r, "Status")

        MessageBox.Show(msg, "Book Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function CellText(r As DataGridViewRow, colName As String) As String
        Dim v As String = Convert.ToString(r.Cells(colName).Value)
        Return If(String.IsNullOrWhiteSpace(v), "—", v)
    End Function

End Class
