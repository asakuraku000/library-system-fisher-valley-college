' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmMemberHome  (sidebar > Dashboard, member side)
'
' Member version of the admin frmDashboard: four summary cards
' on top, a one-line summary, then a tabbed "My Current Loans" /
' "Recent Activity" view. Same look as the admin dashboard, but
' every number is for the LOGGED-IN member only
' (AuthHelper.CurrentMemberId).
'
'   Books Available     tbl_books (status = Available, copies left)
'   Currently Borrowed  tbl_transactions (date_returned IS NULL)
'   Overdue Books       open loans past due_date
'   Fines So Far        overdue days x tbl_settings.fine_per_day
'                       (the old member screen hard-coded 5.00)
'
' Built entirely in code (no Designer file), embedded into
' frmMemberDashboard's content host. Uses the shared DBConnection.
' ============================================================

Imports System.Data
Imports System.Globalization

Public Class frmMemberHome
    Inherits Form

    ' --- Header ---
    Private ReadOnly pnlHeader As New Panel()
    Private ReadOnly lblTitle As New Label()
    Private ReadOnly lblSubtitle As New Label()
    Private ReadOnly btnRefresh As New Button()

    ' --- Cards ---
    Private ReadOnly tlpCards As New TableLayoutPanel()
    Private ReadOnly lblBooksAvailableValue As New Label()
    Private ReadOnly lblBorrowedValue As New Label()
    Private ReadOnly lblOverdueValue As New Label()
    Private ReadOnly lblFinesValue As New Label()

    ' --- Summary line + tabs ---
    Private ReadOnly lblSummary As New Label()
    Private ReadOnly pnlTabsHost As New Panel()
    Private ReadOnly tabs As New TabControl()
    Private ReadOnly tabLoans As New TabPage()
    Private ReadOnly tabActivity As New TabPage()
    Private ReadOnly dgvLoans As New DataGridView()
    Private ReadOnly dgvActivity As New DataGridView()
    Private ReadOnly lblLastUpdated As New Label()

    Public Sub New()
        MyBase.New()
        BuildUi()
    End Sub

    ' ------------------------------------------------------------
    ' UI
    ' ------------------------------------------------------------
    Private Sub BuildUi()
        SuspendLayout()

        Text = "Dashboard"
        Font = New Font("Segoe UI", 9.0F)
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(1000, 700)

        ' --- header (dark blue strip, same as frmDashboard) ---
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 64
        pnlHeader.BackColor = Color.FromArgb(17, 78, 128)

        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(18, 10)
        lblTitle.Text = "Dashboard"

        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New Font("Segoe UI", 9.0F)
        lblSubtitle.ForeColor = Color.WhiteSmoke
        lblSubtitle.Location = New Point(20, 38)
        lblSubtitle.Text = "Fisher Valley College Library — My Overview"

        btnRefresh.Size = New Size(110, 30)
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderColor = Color.White
        btnRefresh.Font = New Font("Segoe UI", 9.0F)
        btnRefresh.ForeColor = Color.White
        btnRefresh.Cursor = Cursors.Hand
        btnRefresh.Text = "Refresh"
        AddHandler btnRefresh.Click, Sub(s, e) LoadHome()

        pnlHeader.Controls.Add(btnRefresh)
        pnlHeader.Controls.Add(lblSubtitle)
        pnlHeader.Controls.Add(lblTitle)
        ' keep the Refresh button pinned to the right edge
        AddHandler pnlHeader.Resize, Sub(s, e) PlaceRefreshButton()
        PlaceRefreshButton()

        ' --- 4 summary cards (equal columns, so they stretch to any screen) ---
        tlpCards.Dock = DockStyle.Top
        tlpCards.Height = 126
        tlpCards.Padding = New Padding(12, 16, 12, 0)
        tlpCards.ColumnCount = 4
        tlpCards.RowCount = 1
        For i As Integer = 1 To 4
            tlpCards.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        Next
        tlpCards.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))

        tlpCards.Controls.Add(MakeCard(Color.FromArgb(10, 120, 200), "Books Available", lblBooksAvailableValue), 0, 0)
        tlpCards.Controls.Add(MakeCard(Color.FromArgb(237, 140, 0), "Currently Borrowed", lblBorrowedValue), 1, 0)
        tlpCards.Controls.Add(MakeCard(Color.FromArgb(178, 34, 34), "Overdue Books", lblOverdueValue), 2, 0)
        tlpCards.Controls.Add(MakeCard(Color.FromArgb(106, 27, 154), "Fines So Far (PHP)", lblFinesValue), 3, 0)

        ' --- one-line summary under the cards ---
        lblSummary.AutoSize = False
        lblSummary.Dock = DockStyle.Top
        lblSummary.Height = 46
        lblSummary.Padding = New Padding(20, 10, 20, 0)
        lblSummary.TextAlign = ContentAlignment.MiddleLeft
        lblSummary.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblSummary.ForeColor = Color.FromArgb(17, 78, 128)
        lblSummary.Text = ""

        ' --- tabs: My Current Loans / Recent Activity ---
        StyleGrid(dgvLoans)
        StyleGrid(dgvActivity)
        AddHandler dgvLoans.DataBindingComplete, AddressOf dgvLoans_DataBindingComplete

        tabLoans.Text = "My Current Loans"
        tabLoans.Padding = New Padding(8)
        tabLoans.UseVisualStyleBackColor = True
        tabLoans.Controls.Add(dgvLoans)

        tabActivity.Text = "Recent Activity (Top 10)"
        tabActivity.Padding = New Padding(8)
        tabActivity.UseVisualStyleBackColor = True
        tabActivity.Controls.Add(dgvActivity)

        tabs.Dock = DockStyle.Fill
        tabs.Font = New Font("Segoe UI", 9.0F)
        tabs.Controls.Add(tabLoans)
        tabs.Controls.Add(tabActivity)

        pnlTabsHost.Dock = DockStyle.Fill
        pnlTabsHost.Padding = New Padding(20, 0, 20, 8)
        pnlTabsHost.Controls.Add(tabs)

        ' --- "last updated" footer ---
        lblLastUpdated.AutoSize = False
        lblLastUpdated.Dock = DockStyle.Bottom
        lblLastUpdated.Height = 24
        lblLastUpdated.Padding = New Padding(20, 0, 0, 0)
        lblLastUpdated.TextAlign = ContentAlignment.MiddleLeft
        lblLastUpdated.Font = New Font("Segoe UI", 8.0F)
        lblLastUpdated.ForeColor = Color.FromArgb(120, 120, 120)
        lblLastUpdated.Text = "Last updated: —"

        ' Fill first, edge-docked controls after it. The LAST one added docks
        ' first, so: header (very top) > cards > summary > footer > tabs (rest).
        Controls.Add(pnlTabsHost)
        Controls.Add(lblLastUpdated)
        Controls.Add(lblSummary)
        Controls.Add(tlpCards)
        Controls.Add(pnlHeader)

        ResumeLayout(True)
    End Sub

    Private Sub PlaceRefreshButton()
        btnRefresh.Location = New Point(Math.Max(0, pnlHeader.ClientSize.Width - btnRefresh.Width - 20), 17)
    End Sub

    ''' <summary>
    ''' One white summary card with a coloured accent bar, a big number and a
    ''' caption (same look as the admin dashboard cards). Docked children, so
    ''' it resizes cleanly.
    ''' </summary>
    Private Function MakeCard(accent As Color, caption As String, valueLabel As Label) As Panel
        Dim card As New Panel()
        card.Dock = DockStyle.Fill
        card.Margin = New Padding(8, 0, 8, 0)
        card.BackColor = Color.White
        card.BorderStyle = BorderStyle.FixedSingle

        Dim lblCaption As New Label()
        lblCaption.AutoSize = False
        lblCaption.Dock = DockStyle.Fill
        lblCaption.Padding = New Padding(16, 2, 8, 0)
        lblCaption.Font = New Font("Segoe UI", 10.0F)
        lblCaption.ForeColor = Color.FromArgb(90, 90, 90)
        lblCaption.TextAlign = ContentAlignment.TopLeft
        lblCaption.Text = caption

        valueLabel.AutoSize = False
        valueLabel.Dock = DockStyle.Top
        valueLabel.Height = 56
        valueLabel.Padding = New Padding(16, 10, 0, 0)
        valueLabel.Font = New Font("Segoe UI", 24.0F, FontStyle.Bold)
        valueLabel.ForeColor = accent
        valueLabel.TextAlign = ContentAlignment.MiddleLeft
        valueLabel.Text = "0"

        Dim bar As New Panel()
        bar.Dock = DockStyle.Top
        bar.Height = 6
        bar.BackColor = accent

        ' fill first, then value, then the accent bar last (so it's on top)
        card.Controls.Add(lblCaption)
        card.Controls.Add(valueLabel)
        card.Controls.Add(bar)
        Return card
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
        g.BorderStyle = BorderStyle.None
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
    Private Sub frmMemberHome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connMessage As String = ""
        If Not DBConnection.TestConnection(connMessage) Then
            MessageBox.Show(
                "Can't reach the library database yet." & Environment.NewLine & Environment.NewLine &
                connMessage & Environment.NewLine & Environment.NewLine &
                "Make sure MySQL/XAMPP is running and that library_system_db.sql has been imported, then click Refresh.",
                "Database Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        LoadHome()
    End Sub

    ''' <summary>
    ''' Refreshes every card and both grids. Each piece has its own
    ''' Try/Catch so one failing query doesn't blank out the rest.
    ''' </summary>
    Private Sub LoadHome()
        LoadBooksAvailable()
        LoadMyLoans()
        LoadMyActivity()
        lblLastUpdated.Text = "Last updated: " & DateTime.Now.ToString("MMM d, yyyy h:mm tt")
    End Sub

    ''' <summary>Books Available card: titles that can be borrowed right now.</summary>
    Private Sub LoadBooksAvailable()
        Try
            Dim dt As DataTable = DBConnection.GetDataTable(
                "SELECT COUNT(*) AS cnt FROM tbl_books WHERE status = 'Available' AND copies_available > 0")
            lblBooksAvailableValue.Text = If(dt.Rows.Count > 0, dt.Rows(0)("cnt").ToString(), "0")
        Catch ex As Exception
            lblBooksAvailableValue.Text = "—"
            ShowLoadError("Books Available", ex)
        End Try
    End Sub

    ''' <summary>
    ''' My Current Loans grid (open loans of the logged-in member) -- and from the
    ''' same rows: the Currently Borrowed / Overdue / Fines cards + summary line,
    ''' so the cards can never disagree with the grid.
    ''' </summary>
    Private Sub LoadMyLoans()
        Try
            Dim rate As Decimal = GetFinePerDay()

            Dim sql As String =
                "SELECT b.accession_number AS `Accession No.`, b.title AS `Book Title`, b.author AS `Author`, " &
                "t.date_borrowed AS `Borrowed`, t.due_date AS `Due Date`, " &
                "CASE WHEN t.due_date < CURDATE() THEN 'Overdue' ELSE 'Borrowed' END AS `Status`, " &
                "GREATEST(DATEDIFF(CURDATE(), t.due_date), 0) AS `Days Overdue`, " &
                "GREATEST(DATEDIFF(CURDATE(), t.due_date), 0) * @rate AS `Fine (PHP)` " &
                "FROM tbl_transactions t " &
                "INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                "WHERE t.member_id = @id AND t.date_returned IS NULL " &
                "ORDER BY t.due_date ASC"

            Dim dt As DataTable = DBConnection.GetDataTable(sql,
                New Dictionary(Of String, Object) From {
                    {"@id", AuthHelper.CurrentMemberId},
                    {"@rate", rate}
                })

            dgvLoans.DataSource = dt
            FormatDateColumns(dgvLoans, "Borrowed", "Due Date")
            If dgvLoans.Columns.Contains("Fine (PHP)") Then
                dgvLoans.Columns("Fine (PHP)").DefaultCellStyle.Format = "N2"
            End If

            Dim overdueCount As Integer = 0
            Dim totalFine As Decimal = 0D
            For Each row As DataRow In dt.Rows
                If row("Status").ToString() = "Overdue" Then overdueCount += 1
                totalFine += Convert.ToDecimal(row("Fine (PHP)"), CultureInfo.InvariantCulture)
            Next

            lblBorrowedValue.Text = dt.Rows.Count.ToString()
            lblOverdueValue.Text = overdueCount.ToString()
            lblFinesValue.Text = totalFine.ToString("N2")

            If dt.Rows.Count = 0 Then
                lblSummary.Text = "You have no books borrowed right now."
            ElseIf overdueCount = 0 Then
                lblSummary.Text = $"{dt.Rows.Count} book(s) borrowed — none overdue."
            Else
                lblSummary.Text = $"{dt.Rows.Count} book(s) borrowed — {overdueCount} overdue. Please return them at the library."
            End If

        Catch ex As Exception
            lblBorrowedValue.Text = "—"
            lblOverdueValue.Text = "—"
            lblFinesValue.Text = "—"
            lblSummary.Text = ""
            ShowLoadError("My Current Loans", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Recent Activity tab: my latest borrow + return events (top 10),
    ''' same query shape as the admin "Recent Transactions" but only mine.
    ''' </summary>
    Private Sub LoadMyActivity()
        Try
            Dim sql As String =
                "SELECT * FROM (" &
                "  SELECT 'Borrowed' AS `Activity`, b.accession_number AS `Accession No.`, b.title AS `Book Title`, " &
                "  t.date_borrowed AS `Date` " &
                "  FROM tbl_transactions t " &
                "  INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                "  WHERE t.member_id = @id " &
                "  UNION ALL " &
                "  SELECT 'Returned' AS `Activity`, b.accession_number AS `Accession No.`, b.title AS `Book Title`, " &
                "  t.date_returned AS `Date` " &
                "  FROM tbl_transactions t " &
                "  INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                "  WHERE t.member_id = @id AND t.date_returned IS NOT NULL " &
                ") AS activity " &
                "ORDER BY `Date` DESC " &
                "LIMIT 10"

            Dim dt As DataTable = DBConnection.GetDataTable(sql,
                New Dictionary(Of String, Object) From {{"@id", AuthHelper.CurrentMemberId}})

            dgvActivity.DataSource = dt
            FormatDateColumns(dgvActivity, "Date")

        Catch ex As Exception
            ShowLoadError("Recent Activity", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Fine per overdue day from tbl_settings ('fine_per_day'); falls back to
    ''' 5.00 (the DB default) if the row/table can't be read.
    ''' </summary>
    Private Function GetFinePerDay() As Decimal
        Try
            Dim dt As DataTable = DBConnection.GetDataTable(
                "SELECT setting_value FROM tbl_settings WHERE setting_key = 'fine_per_day'")
            Dim rate As Decimal
            If dt.Rows.Count > 0 AndAlso Decimal.TryParse(dt.Rows(0)("setting_value").ToString(),
                    NumberStyles.Any, CultureInfo.InvariantCulture, rate) Then
                Return rate
            End If
        Catch ex As Exception
            ' fall through to the default below
        End Try
        Return 5D
    End Function

    Private Sub FormatDateColumns(g As DataGridView, ParamArray names As String())
        For Each n As String In names
            If g.Columns.Contains(n) Then
                g.Columns(n).DefaultCellStyle.Format = "MMM d, yyyy"
            End If
        Next
    End Sub

    ''' <summary>Paints the Status cell red for overdue loans.</summary>
    Private Sub dgvLoans_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)
        If Not dgvLoans.Columns.Contains("Status") Then Return
        For Each r As DataGridViewRow In dgvLoans.Rows
            If Convert.ToString(r.Cells("Status").Value) = "Overdue" Then
                r.Cells("Status").Style.ForeColor = Color.Firebrick
            End If
        Next
    End Sub

    Private Sub ShowLoadError(section As String, ex As Exception)
        MessageBox.Show(
            $"Couldn't load '{section}'." & Environment.NewLine & ex.Message,
            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Class
