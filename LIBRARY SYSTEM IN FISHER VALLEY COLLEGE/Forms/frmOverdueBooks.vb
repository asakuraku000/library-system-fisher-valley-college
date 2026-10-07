' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmOverdueBooks (Logic)
' Part: Kahleb — Overdue Books
' ============================================================

Imports System.Data

Public Class frmOverdueBooks

    Private Const FINE_PER_DAY As Decimal = 5.0D ' PHP per day overdue; keep in sync with the SQL view

    Private Sub frmOverdueBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Check the DB connection first so a bad/unreachable MySQL setup shows a
        ' clear one-line message instead of a raw ADO.NET exception dialog.
        Dim connMessage As String = ""
        If Not DBConnection.TestConnection(connMessage) Then
            MessageBox.Show(
                "Can't reach the library database yet." & Environment.NewLine & Environment.NewLine &
                connMessage & Environment.NewLine & Environment.NewLine &
                "Make sure MySQL/XAMPP is running and that library_system_db.sql has been imported, then click Refresh.",
                "Database Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        LoadOverdueBooks()
    End Sub

    ''' <summary>
    ''' Loads overdue books (optionally filtered by search text) from the
    ''' vw_overdue_books view into the grid.
    ''' </summary>
    Private Sub LoadOverdueBooks(Optional searchText As String = "")
        Try
            Dim sql As String =
                "SELECT transaction_id AS `ID`, accession_number AS `Accession No.`, title AS `Book Title`, " &
                "borrower_name AS `Borrower`, id_number AS `ID Number`, contact_number AS `Contact No.`, " &
                "date_borrowed AS `Date Borrowed`, due_date AS `Due Date`, days_overdue AS `Days Overdue`, " &
                "computed_fine AS `Fine (PHP)` FROM vw_overdue_books"

            Dim params As New Dictionary(Of String, Object)

            If Not String.IsNullOrWhiteSpace(searchText) Then
                sql &= " WHERE title LIKE @search OR borrower_name LIKE @search OR accession_number LIKE @search"
                params.Add("@search", "%" & searchText & "%")
            End If

            sql &= " ORDER BY days_overdue DESC"

            Dim dt As DataTable = DBConnection.GetDataTable(sql, params)
            dgvOverdue.DataSource = dt

            If dgvOverdue.Columns.Contains("Fine (PHP)") Then
                dgvOverdue.Columns("Fine (PHP)").DefaultCellStyle.Format = "N2"
            End If
            If dgvOverdue.Columns.Contains("ID") Then
                dgvOverdue.Columns("ID").Visible = False
            End If

            HighlightSeverelyOverdueRows()
            UpdateSummary(dt)
            UpdateActionButtonsState()

        Catch ex As Exception
            MessageBox.Show("Unable to load overdue books." & Environment.NewLine & ex.Message,
                             "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Bolds and colors rows red when overdue by more than 7 days so they
    ''' stand out for follow-up. Guarded so a missing/renamed column (e.g. if
    ''' the view changes) just skips the styling instead of crashing.
    ''' </summary>
    Private Sub HighlightSeverelyOverdueRows()
        If Not dgvOverdue.Columns.Contains("Days Overdue") Then Return

        For Each row As DataGridViewRow In dgvOverdue.Rows
            Dim cellValue As Object = row.Cells("Days Overdue").Value
            Dim daysOverdue As Integer
            If cellValue IsNot Nothing AndAlso Not IsDBNull(cellValue) AndAlso Integer.TryParse(cellValue.ToString(), daysOverdue) Then
                If daysOverdue > 7 Then
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(178, 34, 34)
                    row.DefaultCellStyle.Font = New Font(dgvOverdue.Font, FontStyle.Bold)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Refreshes the "Total overdue / Total fines" summary label.
    ''' </summary>
    Private Sub UpdateSummary(dt As DataTable)
        Dim totalOverdue As Integer = dt.Rows.Count
        Dim totalFines As Decimal = 0D

        If dt.Columns.Contains("Fine (PHP)") Then
            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("Fine (PHP)")) Then
                    totalFines += Convert.ToDecimal(row("Fine (PHP)"))
                End If
            Next
        End If

        If totalOverdue = 0 Then
            lblSummary.Text = "No overdue books right now — nice and caught up!"
        Else
            lblSummary.Text = $"Total overdue: {totalOverdue} | Total fines: PHP {totalFines:N2}"
        End If
    End Sub

    ''' <summary>
    ''' Enables "Mark as Returned" only when there's actually a row to act on.
    ''' </summary>
    Private Sub UpdateActionButtonsState()
        btnMarkReturned.Enabled = dgvOverdue.Rows.Count > 0
    End Sub

    Private Sub dgvOverdue_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOverdue.SelectionChanged
        btnMarkReturned.Enabled = dgvOverdue.SelectedRows.Count > 0
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadOverdueBooks(txtSearch.Text.Trim())
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadOverdueBooks()
    End Sub

    ''' <summary>
    ''' Marks the selected transaction as returned today and locks in the
    ''' final fine based on how many days it was overdue.
    ''' </summary>
    Private Sub btnMarkReturned_Click(sender As Object, e As EventArgs) Handles btnMarkReturned.Click
        If dgvOverdue.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a record first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim selected As DataGridViewRow = dgvOverdue.SelectedRows(0)

        Dim transactionId As Integer
        If Not Integer.TryParse(selected.Cells("ID").Value?.ToString(), transactionId) Then
            MessageBox.Show("Couldn't read that record's ID. Please refresh and try again.",
                             "Unexpected Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim bookTitle As String = If(selected.Cells("Book Title").Value?.ToString(), "(untitled)")

        Dim daysOverdue As Integer
        Integer.TryParse(selected.Cells("Days Overdue").Value?.ToString(), daysOverdue)

        Dim confirm As DialogResult = MessageBox.Show(
            $"Mark '{bookTitle}' as returned?",
            "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirm <> DialogResult.Yes Then Return

        Try
            Dim sql As String = "UPDATE tbl_transactions SET date_returned = @today, status = 'Returned', fine_amount = @fine WHERE transaction_id = @id"

            Dim finalFine As Decimal = daysOverdue * FINE_PER_DAY

            Dim params As New Dictionary(Of String, Object) From {
                {"@today", DateTime.Today},
                {"@fine", finalFine},
                {"@id", transactionId}
            }

            DBConnection.ExecuteNonQuery(sql, params)

            MessageBox.Show("Book marked as returned. Fine recorded: PHP " & finalFine.ToString("N2"),
                             "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadOverdueBooks(txtSearch.Text.Trim())

        Catch ex As Exception
            MessageBox.Show("Failed to update record." & Environment.NewLine & ex.Message,
                             "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
