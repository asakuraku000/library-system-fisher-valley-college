' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmMemberHistory  (member sidebar > Borrowing History)
'
' EVERY transaction of the logged-in member (AuthHelper.CurrentMemberId)
' from tbl_transactions joined with tbl_books, newest first:
' accession no., title, author, date borrowed, due date, date
' returned, status and fine.
'
'   Status   Borrowed       = open loan, not yet due
'            Overdue        = open loan past its due date
'            Returned       = returned on or before the due date
'            Returned Late  = returned after the due date
'   Fine     returned loans -> tbl_transactions.fine_amount (saved
'            by the librarian when the book was returned)
'            open loans     -> overdue days x tbl_settings.fine_per_day
'
' Toolbar: status filter, search (title / author / accession no.),
' Refresh. Above the grid: a one-line summary of the full history.
' Read-only. Built in code; uses the shared DBConnection + AuthHelper.
' ============================================================

Imports System.Data
Imports System.Globalization

Public Class frmMemberHistory
    Inherits frmMemberPageBase

    Private ReadOnly pnlToolbar As New FlowLayoutPanel()
    Private ReadOnly lblFilter As New Label()
    Private ReadOnly cboStatus As New ComboBox()
    Private ReadOnly txtSearch As New TextBox()
    Private ReadOnly btnRefresh As New Button()
    Private ReadOnly lblSummary As New Label()
    Private ReadOnly grid As New DataGridView()
    Private ReadOnly lblNote As New Label()

    ' Everything the member ever borrowed; the grid shows a filtered view of it.
    Private historyTable As DataTable

    Public Sub New()
        MyBase.New("Borrowing History", "Fisher Valley College Library — Your Past Transactions")
        BuildUi()
    End Sub

    ' ------------------------------------------------------------
    ' UI
    ' ------------------------------------------------------------
    Private Sub BuildUi()
        SuspendLayout()

        ' --- toolbar ---
        pnlToolbar.Dock = DockStyle.Top
        pnlToolbar.Height = 44
        pnlToolbar.WrapContents = False
        pnlToolbar.FlowDirection = FlowDirection.LeftToRight

        lblFilter.AutoSize = True
        lblFilter.Text = "Show:"
        lblFilter.Font = New Font("Segoe UI", 9.5F)
        lblFilter.Margin = New Padding(0, 8, 6, 0)

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cboStatus.Font = New Font("Segoe UI", 9.5F)
        cboStatus.Width = 170
        cboStatus.Margin = New Padding(0, 5, 18, 0)
        cboStatus.Items.AddRange(New Object() {
            "All transactions", "Not yet returned", "Overdue", "Returned", "Returned late"})
        cboStatus.SelectedIndex = 0

        txtSearch.Font = New Font("Segoe UI", 9.5F)
        txtSearch.Width = 260
        txtSearch.PlaceholderText = "Search title, author or accession no."
        txtSearch.Margin = New Padding(0, 5, 18, 0)

        btnRefresh.Size = New Size(90, 30)
        btnRefresh.Text = "Refresh"
        btnRefresh.Font = New Font("Segoe UI", 9.0F)
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.BackColor = Color.FromArgb(17, 78, 128)
        btnRefresh.ForeColor = Color.White
        btnRefresh.Cursor = Cursors.Hand
        btnRefresh.Margin = New Padding(0, 3, 0, 0)

        pnlToolbar.Controls.Add(lblFilter)
        pnlToolbar.Controls.Add(cboStatus)
        pnlToolbar.Controls.Add(txtSearch)
        pnlToolbar.Controls.Add(btnRefresh)

        ' --- summary line ---
        lblSummary.AutoSize = False
        lblSummary.Dock = DockStyle.Top
        lblSummary.Height = 34
        lblSummary.TextAlign = ContentAlignment.MiddleLeft
        lblSummary.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblSummary.ForeColor = Color.FromArgb(17, 78, 128)
        lblSummary.Text = ""

        ' --- grid ---
        StyleGrid(grid)
        AddHandler grid.DataBindingComplete, AddressOf grid_DataBindingComplete

        ' --- footer note ---
        lblNote.AutoSize = False
        lblNote.Dock = DockStyle.Bottom
        lblNote.Height = 26
        lblNote.TextAlign = ContentAlignment.BottomLeft
        lblNote.Font = New Font("Segoe UI", 9.0F)
        lblNote.ForeColor = Color.FromArgb(90, 90, 90)
        lblNote.Text = ""

        ' Fill first; then footer; then summary; toolbar last (= very top).
        pnlBody.Controls.Add(grid)
        pnlBody.Controls.Add(lblNote)
        pnlBody.Controls.Add(lblSummary)
        pnlBody.Controls.Add(pnlToolbar)

        AddHandler btnRefresh.Click, Sub(s, e) LoadHistory()
        AddHandler cboStatus.SelectedIndexChanged, Sub(s, e) ApplyFilter()
        AddHandler txtSearch.TextChanged, Sub(s, e) ApplyFilter()

        ResumeLayout(True)
    End Sub

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
    ' Data
    ' ------------------------------------------------------------
    Private Sub frmMemberHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadHistory()
    End Sub

    ''' <summary>Reads the member's full history from the database, then applies the filter.</summary>
    Private Sub LoadHistory()
        Try
            Dim rate As Decimal = GetFinePerDay()

            Dim sql As String =
                "SELECT b.accession_number AS `Accession No.`, b.title AS `Book Title`, b.author AS `Author`, " &
                "t.date_borrowed AS `Borrowed`, t.due_date AS `Due Date`, t.date_returned AS `Returned`, " &
                "CASE WHEN t.date_returned IS NOT NULL THEN " &
                "       CASE WHEN t.date_returned > t.due_date THEN 'Returned Late' ELSE 'Returned' END " &
                "     WHEN t.due_date < CURDATE() THEN 'Overdue' " &
                "     ELSE 'Borrowed' END AS `Status`, " &
                "CASE WHEN t.date_returned IS NOT NULL THEN t.fine_amount " &
                "     ELSE GREATEST(DATEDIFF(CURDATE(), t.due_date), 0) * @rate END AS `Fine (PHP)`, " &
                "CASE WHEN t.date_returned IS NOT NULL AND t.fine_amount > 0 " &
                "     THEN IF(t.fine_paid = 1, 'Paid', 'Unpaid') ELSE '-' END AS `Fine Status` " &
                "FROM tbl_transactions t " &
                "INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                "WHERE t.member_id = @id " &
                "ORDER BY t.date_borrowed DESC, t.transaction_id DESC"

            historyTable = DBConnection.GetDataTable(sql,
                New Dictionary(Of String, Object) From {
                    {"@id", AuthHelper.CurrentMemberId},
                    {"@rate", rate}
                })

            ShowSummary()
            lblNote.Text = $"Fine: PHP {rate:N2} per overdue day. Fines for returned books are the amounts recorded by the librarian."
            ApplyFilter()

        Catch ex As Exception
            historyTable = Nothing
            grid.DataSource = Nothing
            lblSummary.Text = ""
            MessageBox.Show("Could not load your borrowing history: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>One line about the WHOLE history (not affected by the filter).</summary>
    Private Sub ShowSummary()
        If historyTable Is Nothing OrElse historyTable.Rows.Count = 0 Then
            lblSummary.Text = "You have no borrowing history yet."
            Return
        End If

        Dim returned As Integer = 0
        Dim late As Integer = 0
        Dim notReturned As Integer = 0
        Dim totalFine As Decimal = 0D

        For Each r As DataRow In historyTable.Rows
            Select Case r("Status").ToString()
                Case "Returned"
                    returned += 1
                Case "Returned Late"
                    returned += 1
                    late += 1
                Case Else
                    notReturned += 1
            End Select
            totalFine += Convert.ToDecimal(r("Fine (PHP)"), CultureInfo.InvariantCulture)
        Next

        lblSummary.Text =
            $"{historyTable.Rows.Count} transaction(s): {returned} returned ({late} late), " &
            $"{notReturned} not yet returned. Total fines: PHP {totalFine:N2}."
    End Sub

    ''' <summary>Shows the rows that match the status dropdown + the search box.</summary>
    Private Sub ApplyFilter()
        If historyTable Is Nothing Then Return

        Dim parts As New List(Of String)

        Select Case cboStatus.SelectedIndex
            Case 1
                parts.Add("[Status] IN ('Borrowed', 'Overdue')")
            Case 2
                parts.Add("[Status] = 'Overdue'")
            Case 3
                parts.Add("[Status] IN ('Returned', 'Returned Late')")
            Case 4
                parts.Add("[Status] = 'Returned Late'")
        End Select

        Dim q As String = txtSearch.Text.Trim()
        If q.Length > 0 Then
            Dim safe As String = EscapeLike(q)
            parts.Add($"([Book Title] LIKE '%{safe}%' OR [Author] LIKE '%{safe}%' OR [Accession No.] LIKE '%{safe}%')")
        End If

        Dim view As New DataView(historyTable)
        view.RowFilter = String.Join(" AND ", parts)

        grid.DataSource = view
        FormatColumns()
    End Sub

    ''' <summary>Escapes a search term for DataView.RowFilter LIKE.</summary>
    Private Function EscapeLike(text As String) As String
        Return text.Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]").Replace("'", "''")
    End Function

    Private Sub FormatColumns()
        For Each n As String In {"Borrowed", "Due Date", "Returned"}
            If grid.Columns.Contains(n) Then grid.Columns(n).DefaultCellStyle.Format = "MMM d, yyyy"
        Next
        If grid.Columns.Contains("Fine (PHP)") Then grid.Columns("Fine (PHP)").DefaultCellStyle.Format = "N2"

        SetWeight("Accession No.", 80)
        SetWeight("Book Title", 210)
        SetWeight("Author", 130)
        SetWeight("Borrowed", 85)
        SetWeight("Due Date", 85)
        SetWeight("Returned", 85)
        SetWeight("Status", 95)
        SetWeight("Fine (PHP)", 70)
        SetWeight("Fine Status", 70)
    End Sub

    Private Sub SetWeight(colName As String, weight As Single)
        If grid.Columns.Contains(colName) Then grid.Columns(colName).FillWeight = weight
    End Sub

    ''' <summary>Colours the Status cell: red = Overdue, orange = Returned Late, green = Returned.</summary>
    Private Sub grid_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)
        If Not grid.Columns.Contains("Status") Then Return
        For Each r As DataGridViewRow In grid.Rows
            Select Case Convert.ToString(r.Cells("Status").Value)
                Case "Overdue"
                    r.Cells("Status").Style.ForeColor = Color.Firebrick
                Case "Returned Late"
                    r.Cells("Status").Style.ForeColor = Color.FromArgb(237, 140, 0)
                Case "Returned"
                    r.Cells("Status").Style.ForeColor = Color.SeaGreen
            End Select
        Next
    End Sub

    ''' <summary>Fine per overdue day from tbl_settings; 5.00 if it can't be read.</summary>
    Private Function GetFinePerDay() As Decimal
        Try
            Dim dt As DataTable = DBConnection.GetDataTable(
                "SELECT setting_value FROM tbl_settings WHERE setting_key = 'fine_per_day'")
            Dim v As Decimal
            If dt.Rows.Count > 0 AndAlso Decimal.TryParse(dt.Rows(0)("setting_value").ToString(),
                    NumberStyles.Any, CultureInfo.InvariantCulture, v) Then
                Return v
            End If
        Catch ex As Exception
            ' fall through to the default
        End Try
        Return 5D
    End Function

End Class
