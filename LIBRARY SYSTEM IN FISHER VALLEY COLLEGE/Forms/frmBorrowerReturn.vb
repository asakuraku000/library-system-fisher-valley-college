' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmBorrowerReturn (dialog opened by the "Return Books"
' button on the Borrower screen - it replaces the old Delete button)
'
' Shows every book the selected borrower still has, and lets the
' admin return them right from the borrower's record:
'   - Return Selected Book : Good / Damaged-Torn / Lost, with the
'                            penalty worked out from the book's price
'   - Return All (Good)    : returns every borrowed book at once
' Every return is saved to the Book History (date returned,
' condition, fine, penalty) and to the Activity Log.
' Built in code (no Designer / .resx).
' ============================================================

Imports System.Data

Public Class frmBorrowerReturn
    Inherits Form

    Private ReadOnly memberId As Integer
    Private ReadOnly memberName As String

    Private ReadOnly pnlHeader As New Panel()
    Private ReadOnly lblTitle As New Label()
    Private ReadOnly lblSubtitle As New Label()
    Private ReadOnly dgvLoans As New DataGridView()
    Private ReadOnly pnlBottom As New Panel()
    Private ReadOnly lblCondition As New Label()
    Private ReadOnly cmbCondition As New ComboBox()
    Private ReadOnly lblPenaltyPct As New Label()
    Private ReadOnly numPenaltyPct As New NumericUpDown()
    Private ReadOnly lblRemarks As New Label()
    Private ReadOnly txtRemarks As New TextBox()
    Private ReadOnly lblSummary As New Label()
    Private ReadOnly btnReturnSelected As New Button()
    Private ReadOnly btnReturnAll As New Button()
    Private ReadOnly btnClose As New Button()

    Private finePerDay As Decimal = 5D
    Private damagedPctDefault As Decimal = 50D
    Private lostPctDefault As Decimal = 100D
    Private loading As Boolean = False

    Public Sub New(memberId As Integer, memberName As String)
        MyBase.New()
        Me.memberId = memberId
        Me.memberName = memberName
        BuildLayout()
    End Sub

    Private Sub BuildLayout()
        SuspendLayout()

        Me.Text = "Return Books - " & memberName
        Me.Font = New Font("Segoe UI", 9.0F)
        Me.ClientSize = New Size(860, 560)
        Me.MinimumSize = New Size(860, 560)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ShowInTaskbar = False

        ' ---- header ----
        pnlHeader.BackColor = Color.FromArgb(17, 78, 128)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 64

        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(18, 10)
        lblTitle.Text = "Return Books - " & memberName

        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New Font("Segoe UI", 9.0F)
        lblSubtitle.ForeColor = Color.WhiteSmoke
        lblSubtitle.Location = New Point(20, 38)
        lblSubtitle.Text = "Books this borrower has not returned yet"

        pnlHeader.Controls.Add(lblSubtitle)
        pnlHeader.Controls.Add(lblTitle)

        ' ---- grid ----
        dgvLoans.Dock = DockStyle.Fill
        dgvLoans.AllowUserToAddRows = False
        dgvLoans.AllowUserToDeleteRows = False
        dgvLoans.ReadOnly = True
        dgvLoans.MultiSelect = False
        dgvLoans.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvLoans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvLoans.BackgroundColor = Color.White
        dgvLoans.RowHeadersVisible = False
        UiStyle.StyleGrid(dgvLoans)

        ' ---- bottom panel ----
        pnlBottom.Dock = DockStyle.Bottom
        pnlBottom.Height = 150

        lblCondition.AutoSize = True
        lblCondition.Text = "Condition:"
        lblCondition.Location = New Point(20, 18)

        cmbCondition.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCondition.Location = New Point(90, 14)
        cmbCondition.Size = New Size(170, 23)
        cmbCondition.Items.AddRange(New Object() {"Good", "Damaged / Torn", "Lost / Not returned"})
        cmbCondition.SelectedIndex = 0

        lblPenaltyPct.AutoSize = True
        lblPenaltyPct.Text = "Penalty %:"
        lblPenaltyPct.Location = New Point(280, 18)

        numPenaltyPct.Location = New Point(350, 14)
        numPenaltyPct.Size = New Size(70, 23)
        numPenaltyPct.DecimalPlaces = 0
        numPenaltyPct.Minimum = 0
        numPenaltyPct.Maximum = 100
        numPenaltyPct.Value = 0
        numPenaltyPct.Enabled = False

        lblRemarks.AutoSize = True
        lblRemarks.Text = "Remarks:"
        lblRemarks.Location = New Point(440, 18)

        txtRemarks.Location = New Point(505, 14)
        txtRemarks.Size = New Size(335, 23)
        txtRemarks.MaxLength = 250

        lblSummary.AutoSize = False
        lblSummary.Location = New Point(20, 54)
        lblSummary.Size = New Size(820, 24)
        lblSummary.TextAlign = ContentAlignment.MiddleLeft
        lblSummary.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblSummary.ForeColor = Color.FromArgb(17, 78, 128)

        StyleButton(btnReturnSelected, "Return Selected Book", Color.FromArgb(46, 125, 50))
        btnReturnSelected.Location = New Point(20, 94)
        btnReturnSelected.Size = New Size(200, 36)

        StyleButton(btnReturnAll, "Return All (Good condition)", Color.FromArgb(17, 78, 128))
        btnReturnAll.Location = New Point(232, 94)
        btnReturnAll.Size = New Size(230, 36)

        StyleButton(btnClose, "Close", Color.FromArgb(110, 110, 110))
        btnClose.Location = New Point(720, 94)
        btnClose.Size = New Size(120, 36)

        pnlBottom.Controls.Add(lblCondition)
        pnlBottom.Controls.Add(cmbCondition)
        pnlBottom.Controls.Add(lblPenaltyPct)
        pnlBottom.Controls.Add(numPenaltyPct)
        pnlBottom.Controls.Add(lblRemarks)
        pnlBottom.Controls.Add(txtRemarks)
        pnlBottom.Controls.Add(lblSummary)
        pnlBottom.Controls.Add(btnReturnSelected)
        pnlBottom.Controls.Add(btnReturnAll)
        pnlBottom.Controls.Add(btnClose)

        ' Fill control first, then bottom, then the top bar (docking order)
        Me.Controls.Add(dgvLoans)
        Me.Controls.Add(pnlBottom)
        Me.Controls.Add(pnlHeader)

        AddHandler cmbCondition.SelectedIndexChanged, AddressOf OnConditionChanged
        AddHandler numPenaltyPct.ValueChanged, Sub(s, e) UpdateSummary()
        AddHandler dgvLoans.SelectionChanged, Sub(s, e) UpdateSummary()
        AddHandler btnReturnSelected.Click, AddressOf OnReturnSelected
        AddHandler btnReturnAll.Click, AddressOf OnReturnAll
        AddHandler btnClose.Click, Sub(s, e) Me.Close()

        ResumeLayout(False)
    End Sub

    Private Sub StyleButton(b As Button, caption As String, back As Color)
        b.Text = caption
        b.FlatStyle = FlatStyle.Flat
        b.FlatAppearance.BorderSize = 0
        b.BackColor = back
        b.ForeColor = Color.White
        b.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        b.Cursor = Cursors.Hand
        b.UseVisualStyleBackColor = False
    End Sub

    Private Sub frmBorrowerReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSettings()
        LoadLoans()
    End Sub

    Private Sub LoadSettings()
        Try
            DBConnection.EnsureSchema()

            Dim v As Decimal
            If Decimal.TryParse(DBConnection.ReadSetting("fine_per_day", "5.00"),
                                Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, v) Then
                finePerDay = Math.Max(0D, v)
            End If
            If Decimal.TryParse(DBConnection.ReadSetting("damaged_penalty_percent", "50"),
                                Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, v) Then
                damagedPctDefault = Math.Max(0D, Math.Min(100D, v))
            End If
            If Decimal.TryParse(DBConnection.ReadSetting("lost_penalty_percent", "100"),
                                Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, v) Then
                lostPctDefault = Math.Max(0D, Math.Min(100D, v))
            End If
        Catch
        End Try
    End Sub

    Private Sub LoadLoans()
        Dim count As Integer = 0

        Try
            loading = True

            Dim sql As String =
                "SELECT t.transaction_id AS `ID`, b.accession_number AS `Accession No.`, b.title AS `Book Title`, " &
                "t.date_borrowed AS `Date Borrowed`, t.due_date AS `Due Date`, " &
                "GREATEST(DATEDIFF(CURDATE(), t.due_date), 0) AS `Days Overdue`, " &
                "GREATEST(DATEDIFF(CURDATE(), t.due_date), 0) * @fpd AS `Late Fine (PHP)`, " &
                "COALESCE(b.price, 0) AS `Price (PHP)` " &
                "FROM tbl_transactions t " &
                "INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                "WHERE t.member_id = @mid AND t.date_returned IS NULL " &
                "ORDER BY t.due_date ASC"

            Dim dt As DataTable = DBConnection.GetDataTable(sql,
                New Dictionary(Of String, Object) From {{"@mid", memberId}, {"@fpd", finePerDay}})

            dgvLoans.DataSource = dt
            count = dt.Rows.Count

            If dgvLoans.Columns.Contains("ID") Then dgvLoans.Columns("ID").Visible = False
            If dgvLoans.Columns.Contains("Date Borrowed") Then dgvLoans.Columns("Date Borrowed").DefaultCellStyle.Format = "MMM d, yyyy"
            If dgvLoans.Columns.Contains("Due Date") Then dgvLoans.Columns("Due Date").DefaultCellStyle.Format = "MMM d, yyyy"
            If dgvLoans.Columns.Contains("Late Fine (PHP)") Then dgvLoans.Columns("Late Fine (PHP)").DefaultCellStyle.Format = "N2"
            If dgvLoans.Columns.Contains("Price (PHP)") Then dgvLoans.Columns("Price (PHP)").DefaultCellStyle.Format = "N2"

            lblSubtitle.Text = If(count = 0,
                "This borrower has no unreturned books.",
                $"{count} book(s) not yet returned - pick a book, set its condition, then return it")
        Catch ex As Exception
            MessageBox.Show("Could not load the borrowed books." & Environment.NewLine & ex.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            loading = False
        End Try

        btnReturnSelected.Enabled = count > 0
        btnReturnAll.Enabled = count > 0
        UpdateSummary()
    End Sub

    Private Function SelectedLoan() As DataGridViewRow
        If dgvLoans.SelectedRows.Count = 0 Then Return Nothing
        Return dgvLoans.SelectedRows(0)
    End Function

    Private Function CellDecimal(row As DataGridViewRow, columnName As String) As Decimal
        Dim v As Object = row.Cells(columnName).Value
        If v Is Nothing OrElse IsDBNull(v) Then Return 0D
        Return Convert.ToDecimal(v)
    End Function

    Private Sub OnConditionChanged(sender As Object, e As EventArgs)
        Dim idx As Integer = cmbCondition.SelectedIndex
        numPenaltyPct.Enabled = idx > 0

        If idx = 1 Then
            numPenaltyPct.Value = damagedPctDefault
        ElseIf idx = 2 Then
            numPenaltyPct.Value = lostPctDefault
        Else
            numPenaltyPct.Value = 0
        End If

        UpdateSummary()
    End Sub

    Private Sub UpdateSummary()
        If loading Then Return

        Dim row As DataGridViewRow = SelectedLoan()
        If row Is Nothing Then
            lblSummary.Text = "Select a book to return."
            Return
        End If

        Dim fine As Decimal = CellDecimal(row, "Late Fine (PHP)")
        Dim price As Decimal = CellDecimal(row, "Price (PHP)")
        Dim idx As Integer = cmbCondition.SelectedIndex
        Dim pct As Decimal = If(idx = 0, 0D, numPenaltyPct.Value)
        Dim penalty As Decimal = If(idx = 0, 0D, Math.Round(price * pct / 100D, 2))

        lblSummary.Text = $"Late fine: PHP {fine:N2}   |   Book price: PHP {price:N2}   |   Penalty: PHP {penalty:N2}   |   TOTAL: PHP {(fine + penalty):N2}"
    End Sub

    Private Sub OnReturnSelected(sender As Object, e As EventArgs)
        Dim row As DataGridViewRow = SelectedLoan()
        If row Is Nothing Then
            MessageBox.Show("Please select a borrowed book to return.", "Selection Required",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim transactionId As Integer = Convert.ToInt32(row.Cells("ID").Value)
        Dim bookTitle As String = Convert.ToString(row.Cells("Book Title").Value)
        Dim fine As Decimal = CellDecimal(row, "Late Fine (PHP)")
        Dim price As Decimal = CellDecimal(row, "Price (PHP)")
        Dim idx As Integer = cmbCondition.SelectedIndex
        Dim pct As Decimal = If(idx = 0, 0D, numPenaltyPct.Value)
        Dim penalty As Decimal = If(idx = 0, 0D, Math.Round(price * pct / 100D, 2))

        Dim what As String = If(idx = 2, "as LOST (not returned)", If(idx = 1, "as DAMAGED / TORN", "in good condition"))
        Dim message As String = $"Return ""{bookTitle}"" borrowed by {memberName} {what}?" & Environment.NewLine
        If fine > 0D Then message &= $"Late fine: PHP {fine:N2}" & Environment.NewLine
        If penalty > 0D Then message &= $"Penalty ({pct:0.##}% of PHP {price:N2}): PHP {penalty:N2}" & Environment.NewLine
        If fine + penalty > 0D Then message &= $"TOTAL TO PAY: PHP {(fine + penalty):N2}" & Environment.NewLine
        message &= Environment.NewLine & "Continue?"

        If MessageBox.Show(message, "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Dim errMsg As String = ""
        If ProcessReturn(transactionId, bookTitle, fine, price, idx, pct, txtRemarks.Text.Trim(), errMsg) Then
            MessageBox.Show(If(idx = 2, $"""{bookTitle}"" has been recorded as lost.", $"Successfully returned ""{bookTitle}""."),
                            "Return Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRemarks.Clear()
            cmbCondition.SelectedIndex = 0
            LoadLoans()
        Else
            MessageBox.Show("Could not process the return: " & errMsg, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub OnReturnAll(sender As Object, e As EventArgs)
        If dgvLoans.Rows.Count = 0 Then Return

        Dim totalFine As Decimal = 0D
        For Each row As DataGridViewRow In dgvLoans.Rows
            totalFine += CellDecimal(row, "Late Fine (PHP)")
        Next

        Dim message As String =
            $"Return all {dgvLoans.Rows.Count} book(s) borrowed by {memberName} in GOOD condition?" & Environment.NewLine &
            $"Total late fines: PHP {totalFine:N2}" & Environment.NewLine & Environment.NewLine &
            "(For a damaged or lost book, return it one by one instead.)"

        If MessageBox.Show(message, "Confirm Return All", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Dim total As Integer = dgvLoans.Rows.Count
        Dim done As Integer = 0
        Dim lastError As String = ""

        For Each row As DataGridViewRow In dgvLoans.Rows
            Dim errMsg As String = ""
            If ProcessReturn(Convert.ToInt32(row.Cells("ID").Value), Convert.ToString(row.Cells("Book Title").Value),
                             CellDecimal(row, "Late Fine (PHP)"), CellDecimal(row, "Price (PHP)"), 0, 0D, "", errMsg) Then
                done += 1
            Else
                lastError = errMsg
            End If
        Next

        LoadLoans()

        If done = total Then
            MessageBox.Show($"All {total} book(s) were returned.", "Return Successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show($"Returned {done} of {total} book(s)." & Environment.NewLine & lastError, "Return Incomplete",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ''' <summary>
    ''' Saves one return the same way the Return Books screen does: the loan is closed
    ''' (date, condition, fine, penalty), the stock is updated, and the Book History
    ''' and Activity Log get a row.
    ''' </summary>
    Private Function ProcessReturn(transactionId As Integer, bookTitle As String, fineAmount As Decimal,
                                   bookPrice As Decimal, condIdx As Integer, penaltyPct As Decimal,
                                   remarks As String, ByRef errMsg As String) As Boolean
        Try
            DBConnection.EnsureSchema()

            Dim condition As String = If(condIdx = 2, "Lost", If(condIdx = 1, "Damaged", "Good"))
            Dim pct As Decimal = If(condIdx = 0, 0D, penaltyPct)
            Dim penaltyAmount As Decimal = If(condIdx = 0, 0D, Math.Round(bookPrice * pct / 100D, 2))
            Dim processedBy As String = If(String.IsNullOrWhiteSpace(AuthHelper.CurrentFullName), AuthHelper.CurrentUsername, AuthHelper.CurrentFullName)

            Dim updateSql As String =
                "UPDATE tbl_transactions SET " &
                "date_returned = @dateReturned, " &
                "returned_at = @returnedAt, " &
                "status = CASE WHEN @dateReturned > due_date THEN 'Overdue' ELSE 'Returned' END, " &
                "fine_amount = @fineAmount, " &
                "fine_paid = 0, " &
                "return_condition = @cond, " &
                "penalty_percent = @pct, " &
                "penalty_amount = @penalty, " &
                "remarks = @remarks, " &
                "processed_by = @by " &
                "WHERE transaction_id = @transactionId AND date_returned IS NULL"

            Dim rowsAffected As Integer = DBConnection.ExecuteNonQuery(updateSql,
                New Dictionary(Of String, Object) From {
                    {"@dateReturned", DateTime.Today},
                    {"@returnedAt", DateTime.Now},
                    {"@fineAmount", fineAmount},
                    {"@cond", condition},
                    {"@pct", pct},
                    {"@penalty", penaltyAmount},
                    {"@remarks", If(String.IsNullOrEmpty(remarks), CObj(DBNull.Value), CObj(remarks))},
                    {"@by", If(String.IsNullOrWhiteSpace(processedBy), CObj(DBNull.Value), CObj(processedBy))},
                    {"@transactionId", transactionId}
                })

            If rowsAffected = 0 Then
                errMsg = "This book was already returned."
                Return False
            End If

            ' A returned (or damaged) copy goes back on the shelf; a lost copy is gone for good.
            Dim bookUpdateSql As String
            If condIdx = 2 Then
                bookUpdateSql =
                    "UPDATE tbl_books SET copies_total = GREATEST(copies_total - 1, 0) " &
                    "WHERE book_id = (SELECT book_id FROM tbl_transactions WHERE transaction_id = @transactionId)"
            Else
                bookUpdateSql =
                    "UPDATE tbl_books SET copies_available = copies_available + 1 " &
                    "WHERE book_id = (SELECT book_id FROM tbl_transactions WHERE transaction_id = @transactionId)"
            End If
            DBConnection.ExecuteNonQuery(bookUpdateSql,
                New Dictionary(Of String, Object) From {{"@transactionId", transactionId}})

            DBConnection.RecordBookHistory(transactionId)
            ActivityLog.Write(If(condIdx = 2, "Book Lost", "Return Book"),
                              $"""{bookTitle}"" - {memberName} - condition: {condition}; late fine PHP {fineAmount:N2}; penalty PHP {penaltyAmount:N2} (returned from the Borrower screen)")

            Return True
        Catch ex As Exception
            errMsg = ex.Message
            Return False
        End Try
    End Function

End Class
