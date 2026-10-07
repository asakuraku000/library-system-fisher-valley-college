' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmBorrowBooks (Logic)
' Borrow books to members
' ============================================================

Imports System.Data

Public Class frmBorrowBooks

    Private Sub frmBorrowBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connMessage As String = ""
        If Not DBConnection.TestConnection(connMessage) Then
            MessageBox.Show(
                "Can't reach the library database yet." & Environment.NewLine & Environment.NewLine &
                connMessage & Environment.NewLine & Environment.NewLine &
                "Make sure MySQL/XAMPP is running and that library_system_db.sql has been imported, then click Refresh.",
                "Database Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        LoadMembers()
        LoadAvailableBooks()
    End Sub

    ''' <summary>
    ''' Set right before we set txtSearchMember/txtSearchBook.Text ourselves
    ''' (after the user picks a row from a mini table), so the resulting
    ''' TextChanged doesn't immediately reopen that popup.
    ''' </summary>
    Private suppressMemberSuggestionPopup As Boolean = False
    Private suppressBookSuggestionPopup As Boolean = False

    ''' <summary>
    ''' Queries members matching what's currently typed and shows them in
    ''' the dgvMemberSuggestions mini table under the search box.
    ''' </summary>
    Private Sub UpdateMemberSuggestionsPopup(searchText As String)
        If String.IsNullOrWhiteSpace(searchText) Then
            pnlMemberSuggestions.Visible = False
            Return
        End If

        Try
            Dim sql As String = "
                SELECT member_id, full_name AS `Name`, id_number AS `ID Number`
                FROM tbl_members
                WHERE role = 'member' AND (full_name LIKE @search OR id_number LIKE @search)
                ORDER BY full_name
                LIMIT 8"

            Dim params As New Dictionary(Of String, Object) From {
                {"@search", "%" & searchText & "%"}
            }

            Dim dt As DataTable = DBConnection.GetDataTable(sql, params)

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                pnlMemberSuggestions.Visible = False
                Return
            End If

            dgvMemberSuggestions.DataSource = dt
            If dgvMemberSuggestions.Columns.Contains("member_id") Then
                dgvMemberSuggestions.Columns("member_id").Visible = False
            End If

            pnlMemberSuggestions.BringToFront()
            pnlMemberSuggestions.Visible = True
        Catch ex As Exception
            ' Non-fatal -- typing/search still works without the mini table.
            pnlMemberSuggestions.Visible = False
        End Try
    End Sub

    ''' <summary>
    ''' Queries available books matching what's currently typed and shows
    ''' them in the dgvBookSuggestions mini table under the search box.
    ''' </summary>
    Private Sub UpdateBookSuggestionsPopup(searchText As String)
        If String.IsNullOrWhiteSpace(searchText) Then
            pnlBookSuggestions.Visible = False
            Return
        End If

        Try
            Dim sql As String = "
                SELECT book_id, title AS `Title`, author AS `Author`, copies_available AS `Available`
                FROM tbl_books
                WHERE copies_available > 0 AND status = 'Available' AND (title LIKE @search OR author LIKE @search)
                ORDER BY title
                LIMIT 8"

            Dim params As New Dictionary(Of String, Object) From {
                {"@search", "%" & searchText & "%"}
            }

            Dim dt As DataTable = DBConnection.GetDataTable(sql, params)

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                pnlBookSuggestions.Visible = False
                Return
            End If

            dgvBookSuggestions.DataSource = dt
            If dgvBookSuggestions.Columns.Contains("book_id") Then
                dgvBookSuggestions.Columns("book_id").Visible = False
            End If

            pnlBookSuggestions.BringToFront()
            pnlBookSuggestions.Visible = True
        Catch ex As Exception
            ' Non-fatal -- typing/search still works without the mini table.
            pnlBookSuggestions.Visible = False
        End Try
    End Sub

    ''' <summary>
    ''' Applies the picked mini-table row: reloads cmbMember (hidden) with
    ''' the full member list so the id is guaranteed to be there, then
    ''' selects it -- which also sets cmbMember.Text/SelectedValue for
    ''' btnBorrow_Click to use.
    ''' </summary>
    Private Sub SelectMemberSuggestion(rowIndex As Integer)
        If rowIndex < 0 OrElse rowIndex >= dgvMemberSuggestions.Rows.Count Then Return

        Dim memberId As Object = dgvMemberSuggestions.Rows(rowIndex).Cells("member_id").Value
        Dim fullName As String = ""
        If dgvMemberSuggestions.Rows(rowIndex).Cells("Name").Value IsNot Nothing Then
            fullName = dgvMemberSuggestions.Rows(rowIndex).Cells("Name").Value.ToString()
        End If
        If memberId Is Nothing OrElse String.IsNullOrEmpty(fullName) Then Return

        suppressMemberSuggestionPopup = True
        txtSearchMember.Text = fullName ' TextChanged reloads cmbMember filtered to this name
        cmbMember.SelectedValue = memberId
        lblSelectedMemberValue.Text = fullName
        pnlMemberSuggestions.Visible = False
        txtSearchMember.Focus()
        txtSearchMember.SelectionStart = txtSearchMember.Text.Length
    End Sub

    ''' <summary>
    ''' Applies the picked mini-table row: reloads cmbBook (hidden) with
    ''' the full available-books list so the id is guaranteed to be there,
    ''' then selects it -- which fires cmbBook_SelectedIndexChanged to
    ''' update the Available Copies label too.
    ''' </summary>
    Private Sub SelectBookSuggestion(rowIndex As Integer)
        If rowIndex < 0 OrElse rowIndex >= dgvBookSuggestions.Rows.Count Then Return

        Dim bookId As Object = dgvBookSuggestions.Rows(rowIndex).Cells("book_id").Value
        Dim title As String = ""
        If dgvBookSuggestions.Rows(rowIndex).Cells("Title").Value IsNot Nothing Then
            title = dgvBookSuggestions.Rows(rowIndex).Cells("Title").Value.ToString()
        End If
        If bookId Is Nothing OrElse String.IsNullOrEmpty(title) Then Return

        suppressBookSuggestionPopup = True
        txtSearchBook.Text = title ' TextChanged reloads cmbBook filtered to this title
        cmbBook.SelectedValue = bookId
        lblSelectedBookValue.Text = title
        pnlBookSuggestions.Visible = False
        txtSearchBook.Focus()
        txtSearchBook.SelectionStart = txtSearchBook.Text.Length
    End Sub

    Private Sub dgvMemberSuggestions_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMemberSuggestions.CellClick
        If e.RowIndex >= 0 Then SelectMemberSuggestion(e.RowIndex)
    End Sub

    Private Sub dgvBookSuggestions_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBookSuggestions.CellClick
        If e.RowIndex >= 0 Then SelectBookSuggestion(e.RowIndex)
    End Sub

    ''' <summary>
    ''' Mutually hide each mini table once focus moves to the other step,
    ''' and hide both once the user reaches Step 3.
    ''' </summary>
    Private Sub txtSearchMember_Enter(sender As Object, e As EventArgs) Handles txtSearchMember.Enter
        pnlBookSuggestions.Visible = False
    End Sub

    Private Sub txtSearchBook_Enter(sender As Object, e As EventArgs) Handles txtSearchBook.Enter
        pnlMemberSuggestions.Visible = False
    End Sub

    Private Sub nudQuantity_Enter(sender As Object, e As EventArgs) Handles nudQuantity.Enter
        pnlMemberSuggestions.Visible = False
        pnlBookSuggestions.Visible = False
    End Sub

    ''' <summary>
    ''' Escape dismisses a mini table; Enter picks its top row if one is showing.
    ''' </summary>
    Private Sub txtSearchMember_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchMember.KeyDown
        If e.KeyCode = Keys.Escape Then
            pnlMemberSuggestions.Visible = False
        ElseIf e.KeyCode = Keys.Enter Then
            If pnlMemberSuggestions.Visible AndAlso dgvMemberSuggestions.Rows.Count > 0 Then
                SelectMemberSuggestion(0)
            End If
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtSearchBook_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchBook.KeyDown
        If e.KeyCode = Keys.Escape Then
            pnlBookSuggestions.Visible = False
        ElseIf e.KeyCode = Keys.Enter Then
            If pnlBookSuggestions.Visible AndAlso dgvBookSuggestions.Rows.Count > 0 Then
                SelectBookSuggestion(0)
            End If
            e.SuppressKeyPress = True
        End If
    End Sub

    ''' <summary>
    ''' Loads all active members into the member dropdown, with optional filtering.
    ''' </summary>
    Private Sub LoadMembers(Optional searchText As String = "")
        Try
            Dim sql As String = "SELECT member_id, full_name, id_number FROM tbl_members WHERE role = 'member'"
            
            If Not String.IsNullOrWhiteSpace(searchText) Then
                sql &= " AND (full_name LIKE @search OR id_number LIKE @search)"
            End If
            
            sql &= " ORDER BY full_name"
            
            Dim dt As DataTable
            If Not String.IsNullOrWhiteSpace(searchText) Then
                Dim params As New Dictionary(Of String, Object) From {
                    {"@search", "%" & searchText & "%"}
                }
                dt = DBConnection.GetDataTable(sql, params)
            Else
                dt = DBConnection.GetDataTable(sql)
            End If

            cmbMember.DisplayMember = "full_name"
            cmbMember.ValueMember = "member_id"
            cmbMember.DataSource = dt

            If dt.Rows.Count > 0 Then
                cmbMember.SelectedIndex = -1
                cmbMember.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show("Could not load members: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Loads all books with available copies into the book dropdown, with optional filtering.
    ''' </summary>
    Private Sub LoadAvailableBooks(Optional searchText As String = "")
        Try
            Dim sql As String = "SELECT book_id, title, author, copies_available FROM tbl_books WHERE copies_available > 0 AND status = 'Available'"
            
            If Not String.IsNullOrWhiteSpace(searchText) Then
                sql &= " AND (title LIKE @search OR author LIKE @search)"
            End If
            
            sql &= " ORDER BY title"
            
            Dim dt As DataTable
            If Not String.IsNullOrWhiteSpace(searchText) Then
                Dim params As New Dictionary(Of String, Object) From {
                    {"@search", "%" & searchText & "%"}
                }
                dt = DBConnection.GetDataTable(sql, params)
            Else
                dt = DBConnection.GetDataTable(sql)
            End If

            cmbBook.DisplayMember = "title"
            cmbBook.ValueMember = "book_id"
            cmbBook.DataSource = dt

            If dt.Rows.Count > 0 Then
                cmbBook.SelectedIndex = -1
                cmbBook.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show("Could not load available books: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Updates the available copies label when a book is selected.
    ''' </summary>
    Private Sub cmbBook_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBook.SelectedIndexChanged
        If cmbBook.SelectedIndex >= 0 Then
            Dim row As DataRow = CType(cmbBook.SelectedItem, DataRowView).Row
            lblAvailableValue.Text = row("copies_available").ToString()
        Else
            lblAvailableValue.Text = "0"
        End If
    End Sub

    ''' <summary>
    ''' Validates the form and processes the book borrowing.
    ''' </summary>
    Private Sub btnBorrow_Click(sender As Object, e As EventArgs) Handles btnBorrow.Click
        ' Validate inputs
        If cmbMember.SelectedIndex < 0 Then
            MessageBox.Show("Please select a member.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSearchMember.Focus()
            Return
        End If

        If cmbBook.SelectedIndex < 0 Then
            MessageBox.Show("Please select a book.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSearchBook.Focus()
            Return
        End If

        If nudQuantity.Value <= 0 Then
            MessageBox.Show("Please enter a quantity greater than zero.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            nudQuantity.Focus()
            Return
        End If

        ' Check if enough copies are available
        Dim availableCopies As Integer = Convert.ToInt32(lblAvailableValue.Text)
        If nudQuantity.Value > availableCopies Then
            MessageBox.Show($"Only {availableCopies} copy(ies) available. Please reduce the quantity.", "Insufficient Copies",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            nudQuantity.Focus()
            Return
        End If

        ' Confirm the borrowing
        Dim memberName As String = cmbMember.Text
        Dim bookTitle As String = cmbBook.Text
        Dim quantity As Integer = CInt(nudQuantity.Value)

        Dim result As DialogResult = MessageBox.Show(
            $"Confirm borrowing {quantity} copy(ies) of ""{bookTitle}"" to {memberName}?" & Environment.NewLine &
            $"Date Borrowed: {dtpBorrowed.Value.ToShortDateString()}" & Environment.NewLine &
            $"Due Date: {dtpDueDate.Value.ToShortDateString()}",
            "Confirm Borrowing", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.No Then
            Return
        End If

        Try
            ' For each copy being borrowed, create a transaction record
            For i As Integer = 1 To quantity
                Dim sql As String = "
                    INSERT INTO tbl_transactions 
                    (book_id, member_id, date_borrowed, due_date, status) 
                    VALUES 
                    (@bookId, @memberId, @dateBorrowed, @dueDate, 'Borrowed')"

                Dim params As New Dictionary(Of String, Object) From {
                    {"@bookId", cmbBook.SelectedValue},
                    {"@memberId", cmbMember.SelectedValue},
                    {"@dateBorrowed", dtpBorrowed.Value.Date},
                    {"@dueDate", dtpDueDate.Value.Date}
                }

                DBConnection.ExecuteNonQuery(sql, params)
            Next

            ' Update the book's available copies
            Dim updateSql As String = "
                UPDATE tbl_books 
                SET copies_available = copies_available - @quantity 
                WHERE book_id = @bookId"

            Dim updateParams As New Dictionary(Of String, Object) From {
                {"@quantity", quantity},
                {"@bookId", cmbBook.SelectedValue}
            }

            DBConnection.ExecuteNonQuery(updateSql, updateParams)

            MessageBox.Show($"Successfully borrowed {quantity} copy(ies) of ""{bookTitle}"" to {memberName}.",
                            "Borrow Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the form
            ClearForm()
            LoadAvailableBooks()

        Catch ex As Exception
            MessageBox.Show("Could not process borrowing: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Clears the form for a new borrowing transaction.
    ''' </summary>
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    ''' <summary>
    ''' Resets all form fields to their default state.
    ''' </summary>
    Private Sub ClearForm()
        cmbMember.SelectedIndex = -1
        cmbMember.Text = ""
        txtSearchMember.Clear()
        lblSelectedMemberValue.Text = "NONE SELECTED"
        cmbBook.SelectedIndex = -1
        cmbBook.Text = ""
        txtSearchBook.Clear()
        lblSelectedBookValue.Text = "NONE SELECTED"
        lblAvailableValue.Text = "0"
        nudQuantity.Value = 1
        dtpBorrowed.Value = DateTime.Today
        dtpDueDate.Value = DateTime.Today.AddDays(14) ' Default 2-week loan period
        pnlMemberSuggestions.Visible = False
        pnlBookSuggestions.Visible = False
    End Sub

    ''' <summary>
    ''' Closes the form.
    ''' </summary>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Handles search text changes for members - filters the hidden
    ''' cmbMember and refreshes the member mini table as user types.
    ''' </summary>
    Private Sub txtSearchMember_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMember.TextChanged
        lblSelectedMemberValue.Text = "NONE SELECTED"
        LoadMembers(txtSearchMember.Text.Trim())

        If suppressMemberSuggestionPopup Then
            suppressMemberSuggestionPopup = False
            Return
        End If

        UpdateMemberSuggestionsPopup(txtSearchMember.Text.Trim())
    End Sub

    ''' <summary>
    ''' Handles search text changes for books - filters the hidden cmbBook
    ''' and refreshes the book mini table as user types.
    ''' </summary>
    Private Sub txtSearchBook_TextChanged(sender As Object, e As EventArgs) Handles txtSearchBook.TextChanged
        lblSelectedBookValue.Text = "NONE SELECTED"
        LoadAvailableBooks(txtSearchBook.Text.Trim())

        If suppressBookSuggestionPopup Then
            suppressBookSuggestionPopup = False
            Return
        End If

        UpdateBookSuggestionsPopup(txtSearchBook.Text.Trim())
    End Sub

End Class
