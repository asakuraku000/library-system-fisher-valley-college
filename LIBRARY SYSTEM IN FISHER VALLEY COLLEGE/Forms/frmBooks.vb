' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmBooks (Logic)
' Books catalog — Add / Update / Delete / Search, backed by tbl_books.
' Built on top of Kahleb's shared Modules/DBConnection.vb.
' ============================================================

Imports System.Data

Public Class frmBooks

    ' 0 = no book selected (form is in "Add new" mode).
    Private selectedBookId As Integer = 0
    Private originalTotal As Integer = 0
    Private originalAvailable As Integer = 0

    Private Sub frmBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connMessage As String = ""
        If Not DBConnection.TestConnection(connMessage) Then
            MessageBox.Show(
                "Can't reach the library database yet." & Environment.NewLine & Environment.NewLine &
                connMessage & Environment.NewLine & Environment.NewLine &
                "Make sure MySQL/XAMPP is running and that library_system_db.sql has been imported, then click Refresh.",
                "Database Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        cmbCategory.Items.AddRange(New Object() {
            "Information Technology", "Computer Science", "Information Systems",
            "Education", "Business Administration", "Engineering",
            "Fiction", "Non-Fiction", "Reference", "Other"
        })

        cmbStatus.Items.Clear()
        cmbStatus.Items.AddRange(New Object() {"Available", "Damaged", "Lost", "Archived"})

        LoadBooks()
        ClearForm()
    End Sub

    ''' <summary>
    ''' Loads the book catalog (optionally filtered by search text) into the grid.
    ''' </summary>
    Private Sub LoadBooks(Optional searchText As String = "")
        Try
            Dim sql As String =
                "SELECT book_id AS `ID`, accession_number AS `Accession No.`, isbn AS `ISBN`, title AS `Title`, " &
                "author AS `Author`, publisher AS `Publisher`, year_published AS `Year Published`, category AS `Category`, " &
                "copies_total AS `Total`, copies_available AS `Available`, status AS `Status`, date_added AS `Date Added` " &
                "FROM tbl_books"

            Dim params As New Dictionary(Of String, Object)

            If Not String.IsNullOrWhiteSpace(searchText) Then
                sql &= " WHERE title LIKE @search OR author LIKE @search OR accession_number LIKE @search OR category LIKE @search OR isbn LIKE @search"
                params.Add("@search", "%" & searchText & "%")
            End If

            sql &= " ORDER BY title ASC"

            Dim dt As DataTable = DBConnection.GetDataTable(sql, params)
            dgvBooks.DataSource = dt

            If dgvBooks.Columns.Contains("ID") Then
                dgvBooks.Columns("ID").Visible = False
            End If
            If dgvBooks.Columns.Contains("Date Added") Then
                dgvBooks.Columns("Date Added").DefaultCellStyle.Format = "MMM d, yyyy"
            End If

            UpdateBooksSummary(dt)

        Catch ex As Exception
            MessageBox.Show("Unable to load the book catalog." & Environment.NewLine & ex.Message,
                             "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateBooksSummary(dt As DataTable)
        Dim totalTitles As Integer = dt.Rows.Count
        Dim totalCopies As Integer = 0

        If dt.Columns.Contains("Total") Then
            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("Total")) Then
                    totalCopies += Convert.ToInt32(row("Total"))
                End If
            Next
        End If

        lblBooksSummary.Text = $"Total titles: {totalTitles} | Total copies: {totalCopies}"
    End Sub

    Private Sub dgvBooks_SelectionChanged(sender As Object, e As EventArgs) Handles dgvBooks.SelectionChanged
        If dgvBooks.SelectedRows.Count = 0 Then
            ClearForm()
            Return
        End If

        Dim row As DataGridViewRow = dgvBooks.SelectedRows(0)

        Dim bookId As Integer
        If Not Integer.TryParse(row.Cells("ID").Value?.ToString(), bookId) Then
            ClearForm()
            Return
        End If

        selectedBookId = bookId
        txtAccession.Text = row.Cells("Accession No.").Value?.ToString()
        txtIsbn.Text = row.Cells("ISBN").Value?.ToString()
        txtTitle.Text = row.Cells("Title").Value?.ToString()
        txtAuthor.Text = row.Cells("Author").Value?.ToString()
        txtPublisher.Text = row.Cells("Publisher").Value?.ToString()
        txtYearPublished.Text = row.Cells("Year Published").Value?.ToString()
        cmbCategory.Text = row.Cells("Category").Value?.ToString()
        cmbStatus.Text = row.Cells("Status").Value?.ToString()

        Integer.TryParse(row.Cells("Total").Value?.ToString(), originalTotal)
        Integer.TryParse(row.Cells("Available").Value?.ToString(), originalAvailable)

        numCopiesTotal.Value = Math.Max(numCopiesTotal.Minimum, Math.Min(numCopiesTotal.Maximum, originalTotal))
        lblAvailableValue.Text = originalAvailable.ToString()

        UpdateFormButtonsState()
    End Sub

    ''' <summary>
    ''' Resets the input panel to "Add new book" mode.
    ''' </summary>
    Private Sub ClearForm()
        selectedBookId = 0
        originalTotal = 0
        originalAvailable = 0

        txtAccession.Clear()
        txtIsbn.Clear()
        txtTitle.Clear()
        txtAuthor.Clear()
        txtPublisher.Clear()
        txtYearPublished.Clear()
        cmbCategory.Text = ""
        cmbStatus.Text = "Available"
        numCopiesTotal.Value = 1
        lblAvailableValue.Text = "0"

        dgvBooks.ClearSelection()
        UpdateFormButtonsState()
        txtAccession.Focus()
    End Sub

    ''' <summary>
    ''' Add is only for brand-new entries; Update/Delete need an existing row selected.
    ''' </summary>
    Private Sub UpdateFormButtonsState()
        Dim hasSelection As Boolean = selectedBookId > 0
        btnAdd.Enabled = Not hasSelection
        btnUpdate.Enabled = hasSelection
        btnDelete.Enabled = hasSelection
    End Sub

    ''' <summary>
    ''' Shared required-field checks for both Add and Update.
    ''' </summary>
    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(txtAccession.Text) OrElse String.IsNullOrWhiteSpace(txtTitle.Text) Then
            MessageBox.Show("Accession No. and Title are required.", "Missing Information",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If numCopiesTotal.Value < 1 Then
            MessageBox.Show("Total Copies must be at least 1.", "Invalid Value",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' Year Published is optional, but if the admin typed something it
        ' should actually look like a year -- catches typos like "19999"
        ' or "abcd" before they hit the database.
        Dim yearText As String = txtYearPublished.Text.Trim()
        If Not String.IsNullOrWhiteSpace(yearText) Then
            Dim yearValue As Integer
            If Not Integer.TryParse(yearText, yearValue) OrElse
               yearText.Length <> 4 OrElse
               yearValue < 1900 OrElse yearValue > DateTime.Today.Year + 1 Then
                MessageBox.Show(
                    $"Year Published should be a 4-digit year between 1900 and {DateTime.Today.Year + 1}, or left blank.",
                    "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Checks tbl_books for an accession number already in use (optionally
    ''' excluding the record currently being edited), so duplicates get a
    ''' friendly message instead of a raw MySQL "unique constraint" error.
    ''' </summary>
    Private Function AccessionNumberInUse(accessionNumber As String, excludingBookId As Integer) As Boolean
        Dim sql As String = "SELECT book_id FROM tbl_books WHERE accession_number = @acc"
        Dim params As New Dictionary(Of String, Object) From {{"@acc", accessionNumber}}

        If excludingBookId > 0 Then
            sql &= " AND book_id <> @id"
            params.Add("@id", excludingBookId)
        End If

        Dim dt As DataTable = DBConnection.GetDataTable(sql, params)
        Return dt.Rows.Count > 0
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not ValidateInputs() Then Return

        Dim accession As String = txtAccession.Text.Trim()

        Try
            If AccessionNumberInUse(accession, excludingBookId:=0) Then
                MessageBox.Show("That Accession No. is already used by another book.", "Duplicate Accession No.",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim totalCopies As Integer = CInt(numCopiesTotal.Value)
            Dim isbn As String = txtIsbn.Text.Trim()
            Dim yearPublished As Object = If(String.IsNullOrWhiteSpace(txtYearPublished.Text.Trim()), CObj(DBNull.Value), CObj(Integer.Parse(txtYearPublished.Text.Trim())))
            Dim status As String = If(String.IsNullOrWhiteSpace(cmbStatus.Text), "Available", cmbStatus.Text)

            DBConnection.ExecuteNonQuery(
                "INSERT INTO tbl_books (accession_number, isbn, title, author, publisher, year_published, category, copies_total, copies_available, status) " &
                "VALUES (@acc, @isbn, @title, @author, @pub, @year, @cat, @total, @avail, @status)",
                New Dictionary(Of String, Object) From {
                    {"@acc", accession},
                    {"@isbn", If(String.IsNullOrWhiteSpace(isbn), CObj(DBNull.Value), isbn)},
                    {"@title", txtTitle.Text.Trim()},
                    {"@author", txtAuthor.Text.Trim()},
                    {"@pub", txtPublisher.Text.Trim()},
                    {"@year", yearPublished},
                    {"@cat", cmbCategory.Text.Trim()},
                    {"@total", totalCopies},
                    {"@avail", totalCopies}, ' a brand-new book starts with all copies available
                    {"@status", status}
                })

            MessageBox.Show("Book added to the catalog.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadBooks(txtSearch.Text.Trim())
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(FriendlyBookErrorMessage(ex), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If selectedBookId = 0 Then
            MessageBox.Show("Please select a book from the list first.", "No Selection",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If Not ValidateInputs() Then Return

        Dim newTotal As Integer = CInt(numCopiesTotal.Value)
        Dim borrowedCopies As Integer = originalTotal - originalAvailable

        If newTotal < borrowedCopies Then
            MessageBox.Show(
                $"Total Copies can't be less than {borrowedCopies}, since that many are currently borrowed.",
                "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim newAvailable As Integer = newTotal - borrowedCopies
        Dim accession As String = txtAccession.Text.Trim()

        Try
            If AccessionNumberInUse(accession, selectedBookId) Then
                MessageBox.Show("That Accession No. is already used by another book.", "Duplicate Accession No.",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim isbn As String = txtIsbn.Text.Trim()
            Dim yearPublished As Object = If(String.IsNullOrWhiteSpace(txtYearPublished.Text.Trim()), CObj(DBNull.Value), CObj(Integer.Parse(txtYearPublished.Text.Trim())))
            Dim status As String = If(String.IsNullOrWhiteSpace(cmbStatus.Text), "Available", cmbStatus.Text)

            DBConnection.ExecuteNonQuery(
                "UPDATE tbl_books SET accession_number = @acc, isbn = @isbn, title = @title, author = @author, " &
                "publisher = @pub, year_published = @year, category = @cat, copies_total = @total, copies_available = @avail, status = @status " &
                "WHERE book_id = @id",
                New Dictionary(Of String, Object) From {
                    {"@acc", accession},
                    {"@isbn", If(String.IsNullOrWhiteSpace(isbn), CObj(DBNull.Value), isbn)},
                    {"@title", txtTitle.Text.Trim()},
                    {"@author", txtAuthor.Text.Trim()},
                    {"@pub", txtPublisher.Text.Trim()},
                    {"@year", yearPublished},
                    {"@cat", cmbCategory.Text.Trim()},
                    {"@total", newTotal},
                    {"@avail", newAvailable},
                    {"@status", status},
                    {"@id", selectedBookId}
                })

            MessageBox.Show("Book details updated.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadBooks(txtSearch.Text.Trim())
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(FriendlyBookErrorMessage(ex), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If selectedBookId = 0 Then
            MessageBox.Show("Please select a book from the list first.", "No Selection",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim confirm As DialogResult = MessageBox.Show(
            $"Delete '{txtTitle.Text.Trim()}' from the catalog? This can't be undone.",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If confirm <> DialogResult.Yes Then Return

        Try
            DBConnection.ExecuteNonQuery(
                "DELETE FROM tbl_books WHERE book_id = @id",
                New Dictionary(Of String, Object) From {{"@id", selectedBookId}})

            MessageBox.Show("Book removed from the catalog.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadBooks(txtSearch.Text.Trim())
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(FriendlyBookErrorMessage(ex), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Turns the two most common tbl_books database errors (duplicate accession
    ''' number, or a book that still has borrow records) into a plain-language
    ''' message instead of a raw MySQL exception.
    ''' </summary>
    Private Function FriendlyBookErrorMessage(ex As Exception) As String
        Dim msg As String = ex.Message

        If msg.IndexOf("foreign key constraint", StringComparison.OrdinalIgnoreCase) >= 0 Then
            Return "This book has borrow records on file and can't be deleted." & Environment.NewLine &
                   "If it's no longer in circulation, consider setting its Total Copies to 0 instead."
        End If

        If msg.IndexOf("Duplicate entry", StringComparison.OrdinalIgnoreCase) >= 0 Then
            Return "That Accession No. is already used by another book."
        End If

        Return msg
    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub btnSearchBooks_Click(sender As Object, e As EventArgs) Handles btnSearchBooks.Click
        LoadBooks(txtSearch.Text.Trim())
    End Sub

    Private Sub btnRefreshBooks_Click(sender As Object, e As EventArgs) Handles btnRefreshBooks.Click
        txtSearch.Clear()
        LoadBooks()
        ClearForm()
    End Sub

End Class
