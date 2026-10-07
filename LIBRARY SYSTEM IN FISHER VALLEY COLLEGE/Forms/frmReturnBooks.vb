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
                    END AS `Fine (PHP)`
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

        ' Confirm the return
        Dim message As String = $"Return ""{bookTitle}"" borrowed by {borrowerName}?" & Environment.NewLine
        If daysOverdue > 0 Then
            message &= $"OVERDUE by {daysOverdue} day(s)" & Environment.NewLine &
                      $"Fine: PHP {fineAmount:N2}" & Environment.NewLine & Environment.NewLine
        End If
        message &= "Mark this book as returned?"

        Dim result As DialogResult = MessageBox.Show(message, "Confirm Return",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.No Then
            Return
        End If

        Try
            ' Update the transaction record
            Dim updateSql As String = "
                UPDATE tbl_transactions 
                SET 
                    date_returned = @dateReturned,
                    status = CASE 
                                WHEN @dateReturned > due_date THEN 'Overdue' 
                                ELSE 'Returned' 
                             END,
                    fine_amount = @fineAmount,
                    fine_paid = 0
                WHERE transaction_id = @transactionId"

            Dim updateParams As New Dictionary(Of String, Object) From {
                {"@dateReturned", DateTime.Today},
                {"@fineAmount", fineAmount},
                {"@transactionId", transactionId}
            }

            Dim rowsAffected As Integer = DBConnection.ExecuteNonQuery(updateSql, updateParams)
            If rowsAffected = 0 Then
                MessageBox.Show("Could not update transaction record.", "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Update the book's available copies
            Dim bookUpdateSql As String = "
                UPDATE tbl_books 
                SET copies_available = copies_available + 1 
                WHERE book_id = (
                    SELECT book_id FROM tbl_transactions WHERE transaction_id = @transactionId
                )"

            Dim bookUpdateParams As New Dictionary(Of String, Object) From {
                {"@transactionId", transactionId}
            }

            DBConnection.ExecuteNonQuery(bookUpdateSql, bookUpdateParams)

            Dim successMessage As String = $"Successfully returned ""{bookTitle}"" by {borrowerName}."
            If daysOverdue > 0 Then
                successMessage &= Environment.NewLine & $"Fine of PHP {fineAmount:N2} has been recorded."
            End If

            MessageBox.Show(successMessage, "Return Successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the grid (the mini table re-queries live, no refresh needed)
            LoadBorrowedBooks()

        Catch ex As Exception
            MessageBox.Show("Could not process return: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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