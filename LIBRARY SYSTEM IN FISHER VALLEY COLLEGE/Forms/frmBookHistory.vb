' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmBookHistory (admin)
' The permanent record of every book that came back (or was
' reported lost): who borrowed it, when it was borrowed and due,
' WHEN it was returned, its condition, the late fine and any
' damaged / lost penalty.
'
' It reads tbl_book_history - a separate snapshot table - so the
' history (and the penalties) stay on file even if the book or the
' borrower is edited or deleted later.
' ============================================================

Imports System.Data

Public Class frmBookHistory
    Inherits Form

    Private ReadOnly pnlHeader As New Panel()
    Private ReadOnly lblTitle As New Label()
    Private ReadOnly lblSubtitle As New Label()
    Private ReadOnly pnlFilters As New Panel()
    Private ReadOnly lblSearch As New Label()
    Private ReadOnly txtSearch As New TextBox()
    Private ReadOnly lblCondition As New Label()
    Private ReadOnly cmbCondition As New ComboBox()
    Private ReadOnly lblPeriod As New Label()
    Private ReadOnly cmbPeriod As New ComboBox()
    Private ReadOnly btnRefresh As New RoundedButton()
    Private ReadOnly dgvHistory As New DataGridView()
    Private ReadOnly lblSummary As New Label()

    Public Sub New()
        MyBase.New()
        BuildLayout()
    End Sub

    Private Sub BuildLayout()
        SuspendLayout()

        Me.Text = "Fisher Valley College Library - Book History"
        Me.Font = New Font("Segoe UI", 9.0F)
        Me.ClientSize = New Size(960, 530)

        pnlHeader.BackColor = Color.FromArgb(17, 78, 128)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 64

        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(18, 10)
        lblTitle.Text = "Book History"

        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New Font("Segoe UI", 9.0F)
        lblSubtitle.ForeColor = Color.WhiteSmoke
        lblSubtitle.Location = New Point(20, 38)
        lblSubtitle.Text = "When each book was returned, its condition, and any late fine or damaged / lost penalty"

        pnlHeader.Controls.Add(lblSubtitle)
        pnlHeader.Controls.Add(lblTitle)

        pnlFilters.Dock = DockStyle.Top
        pnlFilters.Height = 52

        lblSearch.AutoSize = True
        lblSearch.Text = "Search:"
        lblSearch.Location = New Point(20, 18)

        txtSearch.Location = New Point(72, 15)
        txtSearch.Size = New Size(240, 23)

        lblCondition.AutoSize = True
        lblCondition.Text = "Condition:"
        lblCondition.Location = New Point(332, 18)

        cmbCondition.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCondition.Location = New Point(398, 14)
        cmbCondition.Size = New Size(150, 23)
        cmbCondition.Items.AddRange(New Object() {"All", "Good", "Damaged", "Lost", "With penalty / fine"})
        cmbCondition.SelectedIndex = 0

        lblPeriod.AutoSize = True
        lblPeriod.Text = "Returned:"
        lblPeriod.Location = New Point(566, 18)

        cmbPeriod.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPeriod.Location = New Point(630, 14)
        cmbPeriod.Size = New Size(120, 23)
        cmbPeriod.Items.AddRange(New Object() {"All time", "Today", "Last 7 days", "Last 30 days"})
        cmbPeriod.SelectedIndex = 0

        btnRefresh.Kind = ButtonKind.Primary
        btnRefresh.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnRefresh.Text = "Refresh"
        btnRefresh.Location = New Point(768, 12)
        btnRefresh.Size = New Size(80, 28)

        pnlFilters.Controls.Add(lblSearch)
        pnlFilters.Controls.Add(txtSearch)
        pnlFilters.Controls.Add(lblCondition)
        pnlFilters.Controls.Add(cmbCondition)
        pnlFilters.Controls.Add(lblPeriod)
        pnlFilters.Controls.Add(cmbPeriod)
        pnlFilters.Controls.Add(btnRefresh)

        lblSummary.Dock = DockStyle.Bottom
        lblSummary.Height = 30
        lblSummary.TextAlign = ContentAlignment.MiddleLeft
        lblSummary.Padding = New Padding(20, 0, 0, 0)
        lblSummary.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        lblSummary.ForeColor = Color.FromArgb(17, 78, 128)

        dgvHistory.Dock = DockStyle.Fill
        dgvHistory.AllowUserToAddRows = False
        dgvHistory.AllowUserToDeleteRows = False
        dgvHistory.ReadOnly = True
        dgvHistory.MultiSelect = False
        dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvHistory.BackgroundColor = Color.White

        Me.Controls.Add(dgvHistory)
        Me.Controls.Add(lblSummary)
        Me.Controls.Add(pnlFilters)
        Me.Controls.Add(pnlHeader)

        AddHandler dgvHistory.CellFormatting, AddressOf dgvHistory_CellFormatting
        AddHandler btnRefresh.Click, Sub(s, e) LoadHistory()
        AddHandler txtSearch.TextChanged, Sub(s, e) LoadHistory()
        AddHandler cmbCondition.SelectedIndexChanged, Sub(s, e) LoadHistory()
        AddHandler cmbPeriod.SelectedIndexChanged, Sub(s, e) LoadHistory()

        ResumeLayout(False)
    End Sub

    Private Sub frmBookHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DBConnection.EnsureSchema()
        LoadHistory()
    End Sub

    Private Sub LoadHistory()
        Try
            Dim sql As String =
                "SELECT h.history_id AS `ID`, " &
                "h.date_returned AS `Date Returned`, " &
                "h.book_title AS `Book Title`, " &
                "h.accession_number AS `Accession No.`, " &
                "h.borrower_name AS `Borrower`, " &
                "h.id_number AS `ID Number`, " &
                "h.date_borrowed AS `Date Borrowed`, " &
                "h.due_date AS `Due Date`, " &
                "h.days_late AS `Days Late`, " &
                "h.return_condition AS `Condition`, " &
                "h.late_fine AS `Late Fine (PHP)`, " &
                "h.book_price AS `Book Price (PHP)`, " &
                "h.penalty_percent AS `Penalty %`, " &
                "h.penalty_amount AS `Penalty (PHP)`, " &
                "h.total_charge AS `Total (PHP)`, " &
                "COALESCE(h.remarks, '') AS `Remarks`, " &
                "COALESCE(h.processed_by, '') AS `Processed By` " &
                "FROM tbl_book_history h WHERE 1 = 1"

            Dim params As New Dictionary(Of String, Object)

            Select Case cmbCondition.SelectedIndex
                Case 1 : sql &= " AND h.return_condition = 'Good'"
                Case 2 : sql &= " AND h.return_condition = 'Damaged'"
                Case 3 : sql &= " AND h.return_condition = 'Lost'"
                Case 4 : sql &= " AND h.total_charge > 0"
            End Select

            Select Case cmbPeriod.SelectedIndex
                Case 1
                    sql &= " AND h.date_returned >= @since"
                    params.Add("@since", DateTime.Today)
                Case 2
                    sql &= " AND h.date_returned >= @since"
                    params.Add("@since", DateTime.Today.AddDays(-6))
                Case 3
                    sql &= " AND h.date_returned >= @since"
                    params.Add("@since", DateTime.Today.AddDays(-29))
            End Select

            Dim term As String = txtSearch.Text.Trim()
            If term.Length > 0 Then
                sql &= " AND (h.book_title LIKE @q OR h.borrower_name LIKE @q OR h.id_number LIKE @q OR h.accession_number LIKE @q)"
                params.Add("@q", "%" & term & "%")
            End If

            sql &= " ORDER BY h.date_returned DESC, h.history_id DESC"

            Dim dt As DataTable = DBConnection.GetDataTable(sql, params)
            dgvHistory.DataSource = dt

            If dgvHistory.Columns.Contains("ID") Then dgvHistory.Columns("ID").Visible = False
            FormatDate("Date Returned", "MMM d, yyyy  h:mm tt", 14)
            FormatDate("Date Borrowed", "MMM d, yyyy", 10)
            FormatDate("Due Date", "MMM d, yyyy", 10)
            FormatMoney("Late Fine (PHP)", 9)
            FormatMoney("Book Price (PHP)", 9)
            FormatMoney("Penalty (PHP)", 9)
            FormatMoney("Total (PHP)", 9)
            SetWeight("Book Title", 16)
            SetWeight("Accession No.", 9)
            SetWeight("Borrower", 13)
            SetWeight("ID Number", 9)
            SetWeight("Days Late", 6)
            SetWeight("Condition", 8)
            SetWeight("Penalty %", 7)
            SetWeight("Remarks", 12)
            SetWeight("Processed By", 10)

            Dim lateFines As Decimal = 0D
            Dim penalties As Decimal = 0D
            For Each r As DataRow In dt.Rows
                If Not IsDBNull(r("Late Fine (PHP)")) Then lateFines += Convert.ToDecimal(r("Late Fine (PHP)"))
                If Not IsDBNull(r("Penalty (PHP)")) Then penalties += Convert.ToDecimal(r("Penalty (PHP)"))
            Next

            lblSummary.Text = $"{dt.Rows.Count} record(s)   |   Late fines: PHP {lateFines:N2}   |   " &
                              $"Damaged / lost penalties: PHP {penalties:N2}   |   Total charges: PHP {(lateFines + penalties):N2}"
        Catch ex As Exception
            lblSummary.Text = "Could not load the book history: " & ex.Message
        End Try
    End Sub

    Private Sub FormatDate(columnName As String, format As String, weight As Single)
        If Not dgvHistory.Columns.Contains(columnName) Then Return
        dgvHistory.Columns(columnName).DefaultCellStyle.Format = format
        dgvHistory.Columns(columnName).FillWeight = weight
    End Sub

    Private Sub FormatMoney(columnName As String, weight As Single)
        If Not dgvHistory.Columns.Contains(columnName) Then Return
        dgvHistory.Columns(columnName).DefaultCellStyle.Format = "N2"
        dgvHistory.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvHistory.Columns(columnName).FillWeight = weight
    End Sub

    Private Sub SetWeight(columnName As String, weight As Single)
        If dgvHistory.Columns.Contains(columnName) Then dgvHistory.Columns(columnName).FillWeight = weight
    End Sub

    ' Colour the Condition cell so damaged / lost books stand out.
    Private Sub dgvHistory_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 OrElse e.Value Is Nothing Then Return
        If dgvHistory.Columns(e.ColumnIndex).Name <> "Condition" Then Return

        Select Case e.Value.ToString()
            Case "Lost"
                e.CellStyle.ForeColor = Color.Firebrick
                e.CellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            Case "Damaged"
                e.CellStyle.ForeColor = Color.DarkOrange
                e.CellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        End Select
    End Sub

End Class
