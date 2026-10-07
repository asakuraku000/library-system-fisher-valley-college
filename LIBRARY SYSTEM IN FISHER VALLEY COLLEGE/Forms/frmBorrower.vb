' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmBorrower (Logic)
' Borrower profiles — Add / Update / Delete / Search, backed by
' tbl_members' profile columns (id_number, full_name, course,
' year_level, contact_number, email).
'
' This is scoped to role = 'member' on purpose: it's the "student/
' faculty borrower" record shown on the Dashboard's Student
' Information lookup, not a login account. Username/password/role
' management stays in frmManageUsers, which owns the account side
' of the same tbl_members table.
'
' Built on top of Kahleb's shared Modules/DBConnection.vb, and
' patterned directly after frmBooks.vb (same Add/Update/Delete/
' Search/Clear flow) so the two screens behave the same way.
' ============================================================

Imports System.Data

Public Class frmBorrower

    ' 0 = no borrower selected (form is in "Add new" mode).
    Private selectedMemberId As Integer = 0

    Private Sub frmBorrower_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connMessage As String = ""
        If Not DBConnection.TestConnection(connMessage) Then
            MessageBox.Show(
                "Can't reach the library database yet." & Environment.NewLine & Environment.NewLine &
                connMessage & Environment.NewLine & Environment.NewLine &
                "Make sure MySQL/XAMPP is running and that library_system_db.sql has been imported, then click Refresh.",
                "Database Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        LoadBorrowers()
        ClearForm()
    End Sub

    ''' <summary>
    ''' Loads the borrower list (optionally filtered by search text) into the grid.
    ''' Only role = 'member' rows are shown here — admin/staff accounts are
    ''' managed separately in Manage Users.
    ''' </summary>
    Private Sub LoadBorrowers(Optional searchText As String = "")
        Try
            Dim sql As String =
                "SELECT member_id AS `ID`, id_number AS `ID Number`, full_name AS `Full Name`, " &
                "course AS `Course`, year_level AS `Year Level`, contact_number AS `Contact Number`, " &
                "email AS `Email`, address AS `Address`, membership_status AS `Membership Status`, " &
                "date_registered AS `Date Registered` " &
                "FROM tbl_members WHERE role = 'member'"

            Dim params As New Dictionary(Of String, Object)

            If Not String.IsNullOrWhiteSpace(searchText) Then
                sql &= " AND (full_name LIKE @search OR id_number LIKE @search OR course LIKE @search OR email LIKE @search)"
                params.Add("@search", "%" & searchText & "%")
            End If

            sql &= " ORDER BY full_name ASC"

            Dim dt As DataTable = DBConnection.GetDataTable(sql, params)
            dgvBorrowers.DataSource = dt

            If dgvBorrowers.Columns.Contains("ID") Then
                dgvBorrowers.Columns("ID").Visible = False
            End If
            If dgvBorrowers.Columns.Contains("Date Registered") Then
                dgvBorrowers.Columns("Date Registered").DefaultCellStyle.Format = "MMM d, yyyy"
            End If

            lblBorrowersSummary.Text = $"Total borrowers: {dt.Rows.Count}"

        Catch ex As Exception
            MessageBox.Show("Unable to load the borrower list." & Environment.NewLine & ex.Message,
                             "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvBorrowers_SelectionChanged(sender As Object, e As EventArgs) Handles dgvBorrowers.SelectionChanged
        If dgvBorrowers.SelectedRows.Count = 0 Then
            ClearForm()
            Return
        End If

        Dim row As DataGridViewRow = dgvBorrowers.SelectedRows(0)

        Dim memberId As Integer
        If Not Integer.TryParse(row.Cells("ID").Value?.ToString(), memberId) Then
            ClearForm()
            Return
        End If

        selectedMemberId = memberId
        txtIdNumber.Text = row.Cells("ID Number").Value?.ToString()
        txtFullName.Text = row.Cells("Full Name").Value?.ToString()
        cmbCourse.Text = row.Cells("Course").Value?.ToString()
        cmbYearLevel.Text = row.Cells("Year Level").Value?.ToString()
        txtContact.Text = row.Cells("Contact Number").Value?.ToString()
        txtEmail.Text = row.Cells("Email").Value?.ToString()
        txtAddress.Text = row.Cells("Address").Value?.ToString()

        Dim membershipStatus As String = row.Cells("Membership Status").Value?.ToString()
        cmbMembershipStatus.Text = If(String.IsNullOrEmpty(membershipStatus), "Active", membershipStatus)

        Dim dateRegistered As Object = row.Cells("Date Registered").Value
        lblDateRegisteredValue.Text = If(IsDBNull(dateRegistered) OrElse dateRegistered Is Nothing,
                                          "—", Convert.ToDateTime(dateRegistered).ToString("MMM d, yyyy"))

        UpdateFormButtonsState()
    End Sub

    ''' <summary>
    ''' Resets the input panel to "Add new borrower" mode.
    ''' </summary>
    Private Sub ClearForm()
        selectedMemberId = 0

        txtIdNumber.Clear()
        txtFullName.Clear()
        cmbCourse.Text = ""
        cmbYearLevel.Text = ""
        txtContact.Clear()
        txtEmail.Clear()
        txtAddress.Clear()
        cmbMembershipStatus.SelectedIndex = 0 ' "Active" — default for a newly added borrower
        lblDateRegisteredValue.Text = "—"

        dgvBorrowers.ClearSelection()
        UpdateFormButtonsState()
        txtIdNumber.Focus()
    End Sub

    ''' <summary>
    ''' Add is only for brand-new entries; Update/Delete need an existing row selected.
    ''' </summary>
    Private Sub UpdateFormButtonsState()
        Dim hasSelection As Boolean = selectedMemberId > 0
        btnAdd.Enabled = Not hasSelection
        btnUpdate.Enabled = hasSelection
        btnDelete.Enabled = hasSelection
    End Sub

    ''' <summary>
    ''' Shared required-field checks for both Add and Update.
    ''' </summary>
    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(txtIdNumber.Text) OrElse String.IsNullOrWhiteSpace(txtFullName.Text) Then
            MessageBox.Show("ID Number and Full Name are required.", "Missing Information",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Checks tbl_members for an ID Number already in use (optionally
    ''' excluding the record currently being edited), so duplicates get a
    ''' friendly message instead of a raw MySQL "unique constraint" error.
    ''' </summary>
    Private Function IdNumberInUse(idNumber As String, excludingMemberId As Integer) As Boolean
        Dim sql As String = "SELECT member_id FROM tbl_members WHERE id_number = @id"
        Dim params As New Dictionary(Of String, Object) From {{"@id", idNumber}}

        If excludingMemberId > 0 Then
            sql &= " AND member_id <> @mid"
            params.Add("@mid", excludingMemberId)
        End If

        Dim dt As DataTable = DBConnection.GetDataTable(sql, params)
        Return dt.Rows.Count > 0
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not ValidateInputs() Then Return

        Dim idNumber As String = txtIdNumber.Text.Trim()

        Try
            If IdNumberInUse(idNumber, excludingMemberId:=0) Then
                MessageBox.Show("That ID Number is already used by another borrower.", "Duplicate ID Number",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            DBConnection.ExecuteNonQuery(
                "INSERT INTO tbl_members (id_number, full_name, course, year_level, contact_number, email, address, membership_status, role) " &
                "VALUES (@id, @name, @course, @year, @contact, @email, @address, @status, 'member')",
                New Dictionary(Of String, Object) From {
                    {"@id", idNumber},
                    {"@name", txtFullName.Text.Trim()},
                    {"@course", cmbCourse.Text.Trim()},
                    {"@year", cmbYearLevel.Text.Trim()},
                    {"@contact", txtContact.Text.Trim()},
                    {"@email", txtEmail.Text.Trim()},
                    {"@address", txtAddress.Text.Trim()},
                    {"@status", If(String.IsNullOrWhiteSpace(cmbMembershipStatus.Text), "Active", cmbMembershipStatus.Text)}
                })

            MessageBox.Show("Borrower added.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadBorrowers(txtSearch.Text.Trim())
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(FriendlyBorrowerErrorMessage(ex), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If selectedMemberId = 0 Then
            MessageBox.Show("Please select a borrower from the list first.", "No Selection",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If Not ValidateInputs() Then Return

        Dim idNumber As String = txtIdNumber.Text.Trim()

        Try
            If IdNumberInUse(idNumber, selectedMemberId) Then
                MessageBox.Show("That ID Number is already used by another borrower.", "Duplicate ID Number",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            DBConnection.ExecuteNonQuery(
                "UPDATE tbl_members SET id_number = @id, full_name = @name, course = @course, " &
                "year_level = @year, contact_number = @contact, email = @email, address = @address, " &
                "membership_status = @status " &
                "WHERE member_id = @mid AND role = 'member'",
                New Dictionary(Of String, Object) From {
                    {"@id", idNumber},
                    {"@name", txtFullName.Text.Trim()},
                    {"@course", cmbCourse.Text.Trim()},
                    {"@year", cmbYearLevel.Text.Trim()},
                    {"@contact", txtContact.Text.Trim()},
                    {"@email", txtEmail.Text.Trim()},
                    {"@address", txtAddress.Text.Trim()},
                    {"@status", If(String.IsNullOrWhiteSpace(cmbMembershipStatus.Text), "Active", cmbMembershipStatus.Text)},
                    {"@mid", selectedMemberId}
                })

            MessageBox.Show("Borrower details updated.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadBorrowers(txtSearch.Text.Trim())
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(FriendlyBorrowerErrorMessage(ex), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If selectedMemberId = 0 Then
            MessageBox.Show("Please select a borrower from the list first.", "No Selection",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim confirm As DialogResult = MessageBox.Show(
            $"Delete '{txtFullName.Text.Trim()}' from the borrower list? This can't be undone.",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If confirm <> DialogResult.Yes Then Return

        Try
            DBConnection.ExecuteNonQuery(
                "DELETE FROM tbl_members WHERE member_id = @id AND role = 'member'",
                New Dictionary(Of String, Object) From {{"@id", selectedMemberId}})

            MessageBox.Show("Borrower removed.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadBorrowers(txtSearch.Text.Trim())
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(FriendlyBorrowerErrorMessage(ex), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Turns the two most common tbl_members database errors (duplicate ID
    ''' number, or a borrower that still has transaction history) into a
    ''' plain-language message instead of a raw MySQL exception.
    ''' </summary>
    Private Function FriendlyBorrowerErrorMessage(ex As Exception) As String
        Dim msg As String = ex.Message

        If msg.IndexOf("foreign key constraint", StringComparison.OrdinalIgnoreCase) >= 0 Then
            Return "This borrower has borrow/return records on file and can't be deleted." & Environment.NewLine &
                   "If they're no longer active, consider leaving the record as-is instead."
        End If

        If msg.IndexOf("Duplicate entry", StringComparison.OrdinalIgnoreCase) >= 0 Then
            Return "That ID Number is already used by another borrower."
        End If

        Return msg
    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub btnSearchBorrowers_Click(sender As Object, e As EventArgs) Handles btnSearchBorrowers.Click
        LoadBorrowers(txtSearch.Text.Trim())
    End Sub

    Private Sub btnRefreshBorrowers_Click(sender As Object, e As EventArgs) Handles btnRefreshBorrowers.Click
        txtSearch.Clear()
        LoadBorrowers()
        ClearForm()
    End Sub

    ''' <summary>
    ''' Opens the selected borrower's full borrow/return history in a small
    ''' popup — this is the direct "View Member Borrowing History" view
    ''' from the borrower's own record, instead of the old workaround of
    ''' going to Reports > Borrowing/Return Report and searching by name.
    ''' </summary>
    Private Sub btnViewHistory_Click(sender As Object, e As EventArgs) Handles btnViewHistory.Click
        If selectedMemberId = 0 Then
            MessageBox.Show("Please select a borrower from the list first.", "No Selection",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ShowBorrowingHistory(selectedMemberId, txtFullName.Text.Trim())
    End Sub

    ''' <summary>
    ''' Builds and shows a read-only borrow/return history grid for one
    ''' member (tbl_transactions joined to tbl_books), newest first.
    ''' Built entirely in code (no separate Designer/.resx) so it stays
    ''' self-contained in this file.
    ''' </summary>
    Private Sub ShowBorrowingHistory(memberId As Integer, memberName As String)
        Dim dt As DataTable

        Try
            dt = DBConnection.GetDataTable(
                "SELECT b.accession_number AS `Accession No.`, b.title AS `Title`, " &
                "t.date_borrowed AS `Date Borrowed`, t.due_date AS `Due Date`, " &
                "t.date_returned AS `Date Returned`, t.status AS `Status`, " &
                "t.fine_amount AS `Fine (PHP)`, IF(t.fine_paid = 1, 'Yes', 'No') AS `Fine Paid` " &
                "FROM tbl_transactions t " &
                "INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                "WHERE t.member_id = @mid " &
                "ORDER BY t.date_borrowed DESC",
                New Dictionary(Of String, Object) From {{"@mid", memberId}})

        Catch ex As Exception
            MessageBox.Show("Unable to load borrowing history." & Environment.NewLine & ex.Message,
                             "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Using dlg As New Form()
            dlg.Text = $"Borrowing History — {memberName}"
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.Size = New Size(760, 460)
            dlg.MinimumSize = New Size(500, 300)
            dlg.Font = New Font("Segoe UI", 9F)
            dlg.Icon = Nothing

            Dim lblSummary As New Label() With {
                .Dock = DockStyle.Top,
                .Height = 30,
                .Padding = New Padding(10, 8, 10, 0),
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(17, 78, 128),
                .Text = $"{dt.Rows.Count} transaction(s) on file for {memberName}"
            }

            Dim grid As New DataGridView() With {
                .Dock = DockStyle.Fill,
                .DataSource = dt,
                .ReadOnly = True,
                .AllowUserToAddRows = False,
                .AllowUserToDeleteRows = False,
                .AllowUserToOrderColumns = False,
                .AllowUserToResizeRows = False,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                .BackgroundColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .MultiSelect = False,
                .RowHeadersVisible = False,
                .EnableHeadersVisualStyles = False
            }
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 78, 128)
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250)

            If grid.Columns.Contains("Fine (PHP)") Then
                grid.Columns("Fine (PHP)").DefaultCellStyle.Format = "N2"
            End If

            Dim btnClose As New Button() With {
                .Dock = DockStyle.Bottom,
                .Height = 40,
                .Text = "Close",
                .DialogResult = DialogResult.OK,
                .FlatStyle = FlatStyle.Flat,
                .BackColor = Color.FromArgb(17, 78, 128),
                .ForeColor = Color.White
            }

            dlg.Controls.Add(grid)
            dlg.Controls.Add(lblSummary)
            dlg.Controls.Add(btnClose)
            dlg.AcceptButton = btnClose

            dlg.ShowDialog(Me)
        End Using
    End Sub

End Class
