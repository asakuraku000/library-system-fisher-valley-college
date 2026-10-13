' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmReturnBooks (Logic)
' Return borrowed books and calculate fines
' ============================================================

Imports System.Data

Public Class frmReturnBooks

    Private Const FINE_PER_DAY As Decimal = 5.0D ' PHP per day overdue

    Private Sub frmReturnBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connMessage As String = ""
        If Not DBConnection.TestConnection(connMessage) Then
            MessageBox.Show(
                "Can't reach the library database yet." & Environment.NewLine & Environment.NewLine &
                connMessage & Environment.NewLine & Environment.NewLine &
                "Make sure MySQL/XAMPP is running and that library_system_db.sql has been imported, then click Refresh.",
                "Database Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        BuildPenaltyControls()
        LoadBorrowedBooks()
    End Sub

    ''' <summary>
    ''' Set right before we set txtSearch.Text ourselves (after the user
    ''' picks a row from the mini table), so the resulting TextChanged
    ''' doesn't immediately reopen the popup.
    ''' </summary>
    Private suppressSuggestionPopup As Boolean = False

    ''' <summary>
    ''' Queries borrower names and book titles (from books still out on
    ''' loan) matching what's currently typed, and shows them in the
    ''' dgvSuggestions mini table under the search box. Hides the mini
    ''' table when the box is empty or nothing matches.
    ''' </summary>
    Private Sub UpdateSuggestionsPopup(searchText As String)
        If String.IsNullOrWhiteSpace(searchText) Then
            pnlSuggestions.Visible = False
            Return
        End If

        Try
            Dim sql As String = "
                SELECT 'Borrower' AS `Type`, m.full_name AS `Match`
                FROM tbl_transactions t
                INNER JOIN tbl_members m ON t.member_id = m.member_id
                WHERE t.date_returned IS NULL AND m.full_name LIKE @search
                GROUP BY m.full_name
                UNION
                SELECT 'Book' AS `Type`, b.title AS `Match`
                FROM tbl_transactions t
                INNER JOIN tbl_books b ON t.book_id = b.book_id
                WHERE t.date_returned IS NULL AND b.title LIKE @search
                GROUP BY b.title
                ORDER BY `Type`, `Match`
                LIMIT 8"

            Dim params As New Dictionary(Of String, Object) From {
                {"@search", "%" & searchText & "%"}
            }

            Dim dt As DataTable = DBConnection.GetDataTable(sql, params)

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                pnlSuggestions.Visible = False
                Return
            End If

            dgvSuggestions.DataSource = dt

            If dgvSuggestions.Columns.Contains("Type") Then
                dgvSuggestions.Columns("Type").HeaderText = "Type"
                dgvSuggestions.Columns("Type").FillWeight = 35
            End If
            If dgvSuggestions.Columns.Contains("Match") Then
                dgvSuggestions.Columns("Match").HeaderText = "Name / Title"
                dgvSuggestions.Columns("Match").FillWeight = 65
            End If

            pnlSuggestions.BringToFront()
            pnlSuggestions.Visible = True
        Catch ex As Exception
            ' Non-fatal -- typing/search still works without the mini table.
            pnlSuggestions.Visible = False
        End Try
    End Sub

    ''' <summary>
    ''' Fills the search box with the row the user picked from the mini
    ''' table and applies it as the filter.
    ''' </summary>
    Private Sub dgvSuggestions_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSuggestions.CellClick
        If e.RowIndex < 0 Then Return

        Dim selectedValue As String = ""
        If dgvSuggestions.Rows(e.RowIndex).Cells("Match").Value IsNot Nothing Then
            selectedValue = dgvSuggestions.Rows(e.RowIndex).Cells("Match").Value.ToString()
        End If
        If String.IsNullOrEmpty(selectedValue) Then Return

        suppressSuggestionPopup = True
        txtSearch.Text = selectedValue
        txtSearch.SelectionStart = txtSearch.Text.Length
        pnlSuggestions.Visible = False
        txtSearch.Focus()
    End Sub

    ''' <summary>
    ''' Hides the mini table once the user moves on to the results grid.
    ''' </summary>
    Private Sub dgvBorrowed_Enter(sender As Object, e As EventArgs) Handles dgvBorrowed.Enter
        pnlSuggestions.Visible = False
    End Sub

    ''' <summary>
    ''' Loads all currently borrowed books into the grid.
    ''' </summary>
    ''' <param name="searchText">Optional text to search for in book title, borrower name, accession number, or ID number.</param>
    Private Sub LoadBorrowedBooks(Optional searchText As String = "")
        Try
            Dim sql As String = "
                SELECT 
                    t.transaction_id AS `ID`,
                    b.title AS `Book Title`,
                    b.accession_number AS `Accession No.`,
                    m.full_name AS `Borrower`,
                    m.id_number AS `ID Number`,
                    t.date_borrowed AS `Date Borrowed`,
                    t.due_date AS `Due Date`,
                    DATEDIFF(CURDATE(), t.due_date) AS `Days Overdue`,
                    CASE 
                        WHEN DATEDIFF(CURDATE(), t.due_date) > 0 
                        THEN DATEDIFF(CURDATE(), t.due_date) * @finePerDay
                        ELSE 0 
                    END AS `Fine (PHP)`,
                    COALESCE(b.price, 0) AS `Price (PHP)`
                FROM tbl_transactions t
                INNER JOIN tbl_books b ON t.book_id = b.book_id
                INNER JOIN tbl_members m ON t.member_id = m.member_id
                WHERE t.date_returned IS NULL"

            If Not String.IsNullOrWhiteSpace(searchText) Then
                sql &= " AND (b.title LIKE @search OR m.full_name LIKE @search OR b.accession_number LIKE @search OR m.id_number LIKE @search)"
            End If

            sql &= " ORDER BY t.date_borrowed DESC"

            Dim params As New Dictionary(Of String, Object) From {
                {"@finePerDay", FINE_PER_DAY},
                {"@search", "%" & searchText & "%"}
            }

        Dim dt As DataTable = DBConnection.GetDataTable(sql, params)
        If dt IsNot Nothing Then
            dgvBorrowed.DataSource = dt

            ' Hide the transaction_id column
            If dgvBorrowed.Columns IsNot Nothing AndAlso dgvBorrowed.Columns.Count > 0 Then
                If dgvBorrowed.Columns.Contains("ID") Then
                    dgvBorrowed.Columns("ID").Visible = False
                End If
            End If

            ' Set columns to fill available width
            dgvBorrowed.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                
            ' Update summary
            UpdateSummary(dt)
        End If

    Catch ex As Exception
        MessageBox.Show("Could not load borrowed books: " & ex.Message, "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

''' <summary>
''' Updates the summary label with total fines.
''' </summary>
Private Sub UpdateSummary(dt As DataTable)
    Try
        Dim totalFine As Decimal = 0D
            
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("Fine (PHP)")) Then
                    totalFine += Convert.ToDecimal(row("Fine (PHP)"))
                End If
            Next
        End If

        lblSummary.Text = "Total fines: PHP " & totalFine.ToString("N2")
    Catch ex As Exception
        lblSummary.Text = "Total fines: PHP 0.00"
    End Try
End Sub

''' <summary>
''' Handles when a row is selected in the grid to show fine details.
''' </summary>
Private Sub dgvBorrowed_SelectionChanged(sender As Object, e As EventArgs) Handles dgvBorrowed.SelectionChanged
    If dgvBorrowed IsNot Nothing AndAlso dgvBorrowed.SelectedRows IsNot Nothing AndAlso dgvBorrowed.SelectedRows.Count > 0 Then
        Dim row As DataGridViewRow = dgvBorrowed.SelectedRows(0)
        If row IsNot Nothing Then
            ' Get fine value safely
            Dim fineValue As Decimal = 0D
            If row.Cells("Fine (PHP)").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("Fine (PHP)").Value) Then
                fineValue = Convert.ToDecimal(row.Cells("Fine (PHP)").Value)
            End If
            lblFineValue.Text = fineValue.ToString("N2")
                
            ' Get days overdue safely
            Dim daysOverdue As Integer = 0
            If row.Cells("Days Overdue").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("Days Overdue").Value) Then
                daysOverdue = Convert.ToInt32(row.Cells("Days Overdue").Value)
            End If
                
            lblStatusValue.Text = If(daysOverdue > 0, "OVERDUE", "CURRENT")
            lblStatusValue.ForeColor = If(daysOverdue > 0, Color.Red, Color.Green)
        Else
            lblFineValue.Text = "0.00"
            lblStatusValue.Text = "NO SELECTION"
            lblStatusValue.ForeColor = Color.Black
        End If
    Else
        lblFineValue.Text = "0.00"
        lblStatusValue.Text = "NO SELECTION"
        lblStatusValue.ForeColor = Color.Black
    End If
End Sub

    ''' <summary>
    ''' Processes the return of the selected book.
    ''' </summary>
    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        If dgvBorrowed IsNot Nothing AndAlso dgvBorrowed.SelectedRows IsNot Nothing AndAlso dgvBorrowed.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a borrowed book to return.", "Selection Required",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If dgvBorrowed Is Nothing OrElse dgvBorrowed.SelectedRows Is Nothing OrElse dgvBorrowed.SelectedRows.Count = 0 Then
            Return ' Should not happen due to check above, but just in case
        End If

        Dim row As DataGridViewRow = dgvBorrowed.SelectedRows(0)
        If row Is Nothing Then
            Return
        End If

        Dim transactionId As Integer = 0
        Dim bookTitle As String = ""
        Dim borrowerName As String = ""
        Dim daysOverdue As Integer = 0
        Dim fineAmount As Decimal = 0D

        Try
            If row.Cells("ID") IsNot Nothing AndAlso row.Cells("ID").Value IsNot Nothing Then
                transactionId = Convert.ToInt32(row.Cells("ID").Value)
            End If

            If row.Cells("Book Title") IsNot Nothing AndAlso row.Cells("Book Title").Value IsNot Nothing Then
                bookTitle = row.Cells("Book Title").Value.ToString()
            End If

            If row.Cells("Borrower") IsNot Nothing AndAlso row.Cells("Borrower").Value IsNot Nothing Then
                borrowerName = row.Cells("Borrower").Value.ToString()
            End If

            If row.Cells("Days Overdue") IsNot Nothing AndAlso row.Cells("Days Overdue").Value IsNot Nothing Then
                daysOverdue = Convert.ToInt32(row.Cells("Days Overdue").Value)
            End If

            If row.Cells("Fine (PHP)") IsNot Nothing AndAlso row.Cells("Fine (PHP)").Value IsNot Nothing Then
                fineAmount = Convert.ToDecimal(row.Cells("Fine (PHP)").Value)
            End If
        Catch ex As Exception
            MessageBox.Show("Error reading selected row data: " & ex.Message, "Data Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' --- Condition / penalty chosen in the Transaction Details panel ---
        Dim condIdx As Integer = cmbCondition.SelectedIndex
        Dim condition As String = If(condIdx = 2, "Lost", If(condIdx = 1, "Damaged", "Good"))
        Dim bookPrice As Decimal = SelectedValueDecimal("Price (PHP)")
        Dim penaltyPct As Decimal = If(condIdx = 0, 0D, numPenaltyPct.Value)
        Dim penaltyAmount As Decimal = If(condIdx = 0, 0D, Math.Round(bookPrice * penaltyPct / 100D, 2))
        Dim totalCharge As Decimal = fineAmount + penaltyAmount
        Dim remarks As String = txtRemarks.Text.Trim()

        If condIdx > 0 AndAlso bookPrice <= 0D Then
            Dim noPrice As DialogResult = MessageBox.Show(
                "This book has no price on file, so the penalty will be PHP 0.00." & Environment.NewLine & Environment.NewLine &
                "Set its price in Books first, or continue without a penalty?",
                "No Book Price", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If noPrice = DialogResult.No Then Return
        End If

        ' Confirm the return
        Dim message As String
        If condIdx = 2 Then
            message = $"Record ""{bookTitle}"" borrowed by {borrowerName} as LOST (not returned)?" & Environment.NewLine
        ElseIf condIdx = 1 Then
            message = $"Return ""{bookTitle}"" borrowed by {borrowerName} as DAMAGED / TORN?" & Environment.NewLine
        Else
            message = $"Return ""{bookTitle}"" borrowed by {borrowerName}?" & Environment.NewLine
        End If

        If daysOverdue > 0 Then
            message &= $"OVERDUE by {daysOverdue} day(s) - late fine: PHP {fineAmount:N2}" & Environment.NewLine
        End If
        If penaltyAmount > 0D Then
            message &= $"Penalty ({penaltyPct:0.##}% of PHP {bookPrice:N2}): PHP {penaltyAmount:N2}" & Environment.NewLine
        End If
        If totalCharge > 0D Then
            message &= $"TOTAL TO PAY: PHP {totalCharge:N2}" & Environment.NewLine
        End If
        message &= Environment.NewLine & "Continue?"

        Dim result As DialogResult = MessageBox.Show(message, "Confirm Return",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.No Then
            Return
        End If

        Try
            DBConnection.EnsureSchema()

            ' Update the transaction record
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
                "WHERE transaction_id = @transactionId"

            Dim processedBy As String = If(String.IsNullOrWhiteSpace(AuthHelper.CurrentFullName), AuthHelper.CurrentUsername, AuthHelper.CurrentFullName)

            Dim updateParams As New Dictionary(Of String, Object) From {
                {"@dateReturned", DateTime.Today},
                {"@returnedAt", DateTime.Now},
                {"@fineAmount", fineAmount},
                {"@cond", condition},
                {"@pct", penaltyPct},
                {"@penalty", penaltyAmount},
                {"@remarks", If(remarks.Length = 0, CObj(DBNull.Value), CObj(remarks))},
                {"@by", If(String.IsNullOrWhiteSpace(processedBy), CObj(DBNull.Value), CObj(processedBy))},
                {"@transactionId", transactionId}
            }

            Dim rowsAffected As Integer = DBConnection.ExecuteNonQuery(updateSql, updateParams)
            If rowsAffected = 0 Then
                MessageBox.Show("Could not update transaction record.", "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Update the book's stock: a returned (or damaged) copy goes back on the shelf;
            ' a lost copy is gone for good, so the total number of copies drops by one.
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

            Dim bookUpdateParams As New Dictionary(Of String, Object) From {
                {"@transactionId", transactionId}
            }

            DBConnection.ExecuteNonQuery(bookUpdateSql, bookUpdateParams)

            ' Permanent history (own table) + activity log for the admin
            DBConnection.RecordBookHistory(transactionId)
            ActivityLog.Write(If(condIdx = 2, "Book Lost", "Return Book"),
                              $"""{bookTitle}"" - {borrowerName} - condition: {condition}; late fine PHP {fineAmount:N2}; penalty PHP {penaltyAmount:N2}")

            Dim successMessage As String
            If condIdx = 2 Then
                successMessage = $"""{bookTitle}"" by {borrowerName} has been recorded as lost."
            Else
                successMessage = $"Successfully returned ""{bookTitle}"" by {borrowerName}."
            End If
            If totalCharge > 0D Then
                successMessage &= Environment.NewLine & $"Charges of PHP {totalCharge:N2} have been recorded."
            End If

            MessageBox.Show(successMessage, "Return Successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the grid (the mini table re-queries live, no refresh needed)
            txtRemarks.Clear()
            cmbCondition.SelectedIndex = 0
            LoadBorrowedBooks()

        Catch ex As Exception
            MessageBox.Show("Could not process return: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ------------------------------------------------------------
    ' Condition / penalty controls (Good, Damaged / Torn, Lost)
    ' The penalty is a percent of the book's price (Books screen).
    ' Default percents come from tbl_settings and can be changed per case.
    ' ------------------------------------------------------------
    Private cmbCondition As ComboBox
    Private numPenaltyPct As NumericUpDown
    Private lblPriceValue As Label
    Private lblPenaltyValue As Label
    Private lblTotalValue As Label
    Private txtRemarks As TextBox
    Private damagedPctDefault As Decimal = 50D
    Private lostPctDefault As Decimal = 100D

    Private Sub BuildPenaltyControls()
        DBConnection.EnsureSchema()

        Dim v As Decimal
        If Decimal.TryParse(DBConnection.ReadSetting("damaged_penalty_percent", "50"),
                            Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, v) Then
            damagedPctDefault = Math.Max(0D, Math.Min(100D, v))
        End If
        If Decimal.TryParse(DBConnection.ReadSetting("lost_penalty_percent", "100"),
                            Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, v) Then
            lostPctDefault = Math.Max(0D, Math.Min(100D, v))
        End If

        lblSubtitle.Text = "Return borrowed books, record damaged or lost books, and calculate fines and penalties"

        AddCaption("Condition:", 98)
        cmbCondition = New ComboBox()
        cmbCondition.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCondition.Location = New Point(115, 95)
        cmbCondition.Size = New Size(150, 23)
        cmbCondition.Items.AddRange(New Object() {"Good", "Damaged / Torn", "Lost / Not returned"})
        cmbCondition.SelectedIndex = 0
        grpDetails.Controls.Add(cmbCondition)

        AddCaption("Book Price:", 132)
        lblPriceValue = AddValue(132)

        AddCaption("Penalty %:", 166)
        numPenaltyPct = New NumericUpDown()
        numPenaltyPct.Location = New Point(115, 163)
        numPenaltyPct.Size = New Size(70, 23)
        numPenaltyPct.DecimalPlaces = 0
        numPenaltyPct.Minimum = 0
        numPenaltyPct.Maximum = 100
        numPenaltyPct.Value = 0
        numPenaltyPct.Enabled = False
        grpDetails.Controls.Add(numPenaltyPct)

        AddCaption("Penalty (PHP):", 200)
        lblPenaltyValue = AddValue(200)

        AddCaption("Total (PHP):", 234)
        lblTotalValue = AddValue(234)
        lblTotalValue.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)

        AddCaption("Remarks:", 268)
        txtRemarks = New TextBox()
        txtRemarks.Location = New Point(115, 265)
        txtRemarks.Size = New Size(150, 23)
        grpDetails.Controls.Add(txtRemarks)
        UiStyle.WrapField(txtRemarks)

        btnReturn.Top = 322

        AddHandler cmbCondition.SelectedIndexChanged, AddressOf OnConditionChanged
        AddHandler numPenaltyPct.ValueChanged, Sub(s, e) RefreshPenaltyPreview()

        RefreshPenaltyPreview()
    End Sub

    Private Sub AddCaption(text As String, y As Integer)
        Dim l As New Label()
        l.AutoSize = True
        l.Text = text
        l.Font = lblFineCaption.Font
        l.ForeColor = lblFineCaption.ForeColor
        l.Location = New Point(15, y)
        grpDetails.Controls.Add(l)
    End Sub

    Private Function AddValue(y As Integer) As Label
        Dim l As New Label()
        l.AutoSize = True
        l.Text = "0.00"
        l.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        l.ForeColor = Color.FromArgb(17, 78, 128)
        l.Location = New Point(115, y)
        grpDetails.Controls.Add(l)
        Return l
    End Function

    Private Sub OnConditionChanged(sender As Object, e As EventArgs)
        Select Case cmbCondition.SelectedIndex
            Case 1
                numPenaltyPct.Enabled = True
                numPenaltyPct.Value = damagedPctDefault
            Case 2
                numPenaltyPct.Enabled = True
                numPenaltyPct.Value = lostPctDefault
            Case Else
                numPenaltyPct.Value = 0
                numPenaltyPct.Enabled = False
        End Select
        RefreshPenaltyPreview()
    End Sub

    Private Sub RefreshPenaltyPreview()
        If cmbCondition Is Nothing OrElse lblTotalValue Is Nothing Then Return

        Dim price As Decimal = SelectedValueDecimal("Price (PHP)")
        Dim fine As Decimal = SelectedValueDecimal("Fine (PHP)")
        Dim penalty As Decimal = If(cmbCondition.SelectedIndex = 0, 0D, Math.Round(price * numPenaltyPct.Value / 100D, 2))

        lblPriceValue.Text = price.ToString("N2")
        lblPenaltyValue.Text = penalty.ToString("N2")
        lblTotalValue.Text = (fine + penalty).ToString("N2")
        lblTotalValue.ForeColor = If(fine + penalty > 0D, Color.Red, Color.FromArgb(17, 78, 128))
    End Sub

    Private Function SelectedValueDecimal(columnName As String) As Decimal
        If dgvBorrowed Is Nothing OrElse dgvBorrowed.SelectedRows.Count = 0 Then Return 0D
        If Not dgvBorrowed.Columns.Contains(columnName) Then Return 0D

        Dim v As Object = dgvBorrowed.SelectedRows(0).Cells(columnName).Value
        If v Is Nothing OrElse IsDBNull(v) Then Return 0D
        Return Convert.ToDecimal(v)
    End Function

    ' A different row means a fresh decision: start from "Good" again.
    Private Sub dgvBorrowed_SelectionChanged_Penalty(sender As Object, e As EventArgs) Handles dgvBorrowed.SelectionChanged
        If cmbCondition Is Nothing Then Return
        If cmbCondition.SelectedIndex <> 0 Then
            cmbCondition.SelectedIndex = 0
        Else
            RefreshPenaltyPreview()
        End If
    End Sub

    ''' <summary>
    ''' Refreshes the list of borrowed books.
    ''' </summary>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        pnlSuggestions.Visible = False
        LoadBorrowedBooks()
    End Sub

    ''' <summary>
    ''' Searches for borrowed books based on the search text.
    ''' </summary>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        pnlSuggestions.Visible = False
        LoadBorrowedBooks(txtSearch.Text.Trim())
    End Sub

    ''' <summary>
    ''' Live-filters the grid as the user types, and refreshes the mini
    ''' table of matching borrower names / book titles below the box.
    ''' </summary>
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadBorrowedBooks(txtSearch.Text.Trim())

        If suppressSuggestionPopup Then
            suppressSuggestionPopup = False
            Return
        End If

        UpdateSuggestionsPopup(txtSearch.Text.Trim())
    End Sub

    ''' <summary>
    ''' Handles the Enter key (trigger search) and Escape key (dismiss the
    ''' mini table) in the search box.
    ''' </summary>
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            pnlSuggestions.Visible = False
            btnSearch.PerformClick()
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Escape Then
            pnlSuggestions.Visible = False
        End If
    End Sub

End Class