' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmMemberBorrowed  (member sidebar > My Borrowed Books)
'
' The logged-in member's CURRENT loans (not yet returned):
' accession no., title, author, date borrowed, due date, how many
' days are left / overdue, status (Borrowed / Due Soon / Overdue)
' and the fine so far (overdue days x tbl_settings.fine_per_day).
'
' Above the grid: a one-line summary (how many borrowed, how many
' overdue, total fine, and how many more books they can borrow
' based on tbl_settings.max_books_per_member).
' Toolbar: "Overdue only" toggle + Refresh.
'
' Read-only -- borrowing/returning is still done by the librarian
' (admin Borrow Books / Return Books screens).
' Built entirely in code; uses the shared DBConnection + AuthHelper.
' ============================================================

Imports System.Data
Imports System.Globalization

Public Class frmMemberBorrowed
    Inherits frmMemberPageBase

    Private ReadOnly pnlToolbar As New FlowLayoutPanel()
    Private ReadOnly chkOverdueOnly As New CheckBox()
    Private ReadOnly btnRefresh As New RoundedButton()
    Private ReadOnly lblSummary As New Label()
    Private ReadOnly grid As New DataGridView()
    Private ReadOnly lblNote As New Label()

    Public Sub New()
        MyBase.New("My Borrowed Books", "Fisher Valley College Library — Books You Currently Have")
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

        chkOverdueOnly.AutoSize = True
        chkOverdueOnly.Text = "Overdue only"
        chkOverdueOnly.Font = New Font("Segoe UI", 9.5F)
        chkOverdueOnly.Margin = New Padding(0, 6, 18, 0)

        btnRefresh.Size = New Size(90, 30)
        btnRefresh.Text = "Refresh"
        btnRefresh.Kind = ButtonKind.Outline
        btnRefresh.Font = New Font("Segoe UI", 9.0F)
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.BackColor = Color.FromArgb(17, 78, 128)
        btnRefresh.ForeColor = Color.White
        btnRefresh.Cursor = Cursors.Hand
        btnRefresh.Margin = New Padding(0, 1, 0, 0)

        pnlToolbar.Controls.Add(chkOverdueOnly)
        pnlToolbar.Controls.Add(btnRefresh)

        ' --- summary line (under the toolbar) ---
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

        AddHandler chkOverdueOnly.CheckedChanged, Sub(s, e) LoadLoans()
        AddHandler btnRefresh.Click, Sub(s, e) LoadLoans()

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
    Private Sub frmMemberBorrowed_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLoans()
    End Sub

    ''' <summary>
    ''' Loads the member's open loans. Status: Overdue (past due date),
    ''' Due Soon (due today or within 2 days) or Borrowed. The summary is
    ''' always about ALL open loans, even when "Overdue only" filters the grid.
    ''' </summary>
    Private Sub LoadLoans()
        Try
            Dim rate As Decimal = GetSettingDecimal("fine_per_day", 5D)
            Dim maxBooks As Integer = CInt(GetSettingDecimal("max_books_per_member", 3D))

            Dim sql As String =
                "SELECT b.accession_number AS `Accession No.`, b.title AS `Book Title`, b.author AS `Author`, " &
                "t.date_borrowed AS `Borrowed`, t.due_date AS `Due Date`, " &
                "CASE WHEN t.due_date < CURDATE() " &
                "     THEN CONCAT(DATEDIFF(CURDATE(), t.due_date), ' day(s) overdue') " &
                "     WHEN t.due_date = CURDATE() THEN 'Due today' " &
                "     ELSE CONCAT(DATEDIFF(t.due_date, CURDATE()), ' day(s) left') END AS `Remaining`, " &
                "CASE WHEN t.due_date < CURDATE() THEN 'Overdue' " &
                "     WHEN DATEDIFF(t.due_date, CURDATE()) <= 2 THEN 'Due Soon' " &
                "     ELSE 'Borrowed' END AS `Status`, " &
                "GREATEST(DATEDIFF(CURDATE(), t.due_date), 0) * @rate AS `Fine (PHP)` " &
                "FROM tbl_transactions t " &
                "INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                "WHERE t.member_id = @id AND t.date_returned IS NULL " &
                "ORDER BY t.due_date ASC"

            Dim all As DataTable = DBConnection.GetDataTable(sql,
                New Dictionary(Of String, Object) From {
                    {"@id", AuthHelper.CurrentMemberId},
                    {"@rate", rate}
                })

            ' --- summary from ALL open loans ---
            Dim overdueCount As Integer = 0
            Dim totalFine As Decimal = 0D
            For Each r As DataRow In all.Rows
                If r("Status").ToString() = "Overdue" Then overdueCount += 1
                totalFine += Convert.ToDecimal(r("Fine (PHP)"), CultureInfo.InvariantCulture)
            Next
            ShowSummary(all.Rows.Count, overdueCount, totalFine, maxBooks)

            ' --- grid (optionally only the overdue ones) ---
            Dim view As DataTable = all
            If chkOverdueOnly.Checked Then
                view = all.Clone()
                For Each r As DataRow In all.Rows
                    If r("Status").ToString() = "Overdue" Then view.ImportRow(r)
                Next
            End If

            grid.DataSource = view
            FormatColumns()

            lblNote.Text = $"Fine: PHP {rate:N2} per overdue day. Return your books at the library counter."

        Catch ex As Exception
            lblSummary.Text = ""
            MessageBox.Show("Could not load your borrowed books: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowSummary(total As Integer, overdueCount As Integer, totalFine As Decimal, maxBooks As Integer)
        If total = 0 Then
            lblSummary.ForeColor = Color.FromArgb(17, 78, 128)
            lblSummary.Text = "You have no books borrowed right now."
            Return
        End If

        Dim canBorrow As Integer = Math.Max(0, maxBooks - total)
        Dim msg As String = $"{total} book(s) borrowed"

        If overdueCount = 0 Then
            lblSummary.ForeColor = Color.FromArgb(17, 78, 128)
            msg &= " — none overdue."
        Else
            lblSummary.ForeColor = Color.Firebrick
            msg &= $" — {overdueCount} overdue, fine so far PHP {totalFine:N2}."
        End If

        msg &= $"  You can borrow {canBorrow} more (limit {maxBooks})."
        lblSummary.Text = msg
    End Sub

    Private Sub FormatColumns()
        For Each n As String In {"Borrowed", "Due Date"}
            If grid.Columns.Contains(n) Then grid.Columns(n).DefaultCellStyle.Format = "MMM d, yyyy"
        Next
        If grid.Columns.Contains("Fine (PHP)") Then grid.Columns("Fine (PHP)").DefaultCellStyle.Format = "N2"

        SetWeight("Accession No.", 80)
        SetWeight("Book Title", 200)
        SetWeight("Author", 130)
        SetWeight("Borrowed", 85)
        SetWeight("Due Date", 85)
        SetWeight("Remaining", 100)
        SetWeight("Status", 75)
        SetWeight("Fine (PHP)", 70)
    End Sub

    Private Sub SetWeight(colName As String, weight As Single)
        If grid.Columns.Contains(colName) Then grid.Columns(colName).FillWeight = weight
    End Sub

    ''' <summary>Colours Status / Remaining: red = Overdue, orange = Due Soon.</summary>
    Private Sub grid_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)
        If Not grid.Columns.Contains("Status") Then Return
        For Each r As DataGridViewRow In grid.Rows
            Dim c As Color = Color.Empty
            Select Case Convert.ToString(r.Cells("Status").Value)
                Case "Overdue"
                    c = Color.Firebrick
                Case "Due Soon"
                    c = Color.FromArgb(237, 140, 0)
            End Select

            If Not c.IsEmpty Then
                r.Cells("Status").Style.ForeColor = c
                If grid.Columns.Contains("Remaining") Then r.Cells("Remaining").Style.ForeColor = c
            End If
        Next
    End Sub

    ''' <summary>Reads a number from tbl_settings; returns the default if it can't.</summary>
    Private Function GetSettingDecimal(key As String, defaultValue As Decimal) As Decimal
        Try
            Dim dt As DataTable = DBConnection.GetDataTable(
                "SELECT setting_value FROM tbl_settings WHERE setting_key = @k",
                New Dictionary(Of String, Object) From {{"@k", key}})
            Dim v As Decimal
            If dt.Rows.Count > 0 AndAlso Decimal.TryParse(dt.Rows(0)("setting_value").ToString(),
                    NumberStyles.Any, CultureInfo.InvariantCulture, v) Then
                Return v
            End If
        Catch ex As Exception
            ' fall through to the default
        End Try
        Return defaultValue
    End Function

End Class
