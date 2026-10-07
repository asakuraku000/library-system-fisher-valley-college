' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmManageUsers (Logic)
' Admin-only account management: list/search every tbl_members
' record, add a new account (member OR admin), change a user's
' role, or delete an account. This is the intended way to create
' additional admin accounts — self-registration (createaccount.vb)
' always creates 'member' accounts only.
' Built on top of Kahleb's shared Modules/DBConnection.vb.
' ============================================================

Imports System.Data

Public Class frmManageUsers

    ' 0 = no user selected (form is in "Add new" mode).
    Private selectedMemberId As Integer = 0

    Private Sub frmManageUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Defense-in-depth: this screen should only ever be reachable from
        ' the admin dashboard, but guard it directly too in case another
        ' form ever opens it without going through that route.
        If Not AuthHelper.IsAdmin Then
            MessageBox.Show("Only admin accounts can manage users.", "Access Denied",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        Dim connMessage As String = ""
        If Not DBConnection.TestConnection(connMessage) Then
            MessageBox.Show(
                "Can't reach the library database yet." & Environment.NewLine & Environment.NewLine &
                connMessage & Environment.NewLine & Environment.NewLine &
                "Make sure MySQL/XAMPP is running and that library_system_db.sql has been imported, then click Refresh.",
                "Database Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        cmbRole.Items.Clear()
        cmbRole.Items.AddRange(New Object() {"member", "admin"})
        cmbRole.SelectedIndex = 0

        LoadUsers()
        ClearForm()
    End Sub

    ''' <summary>
    ''' Loads the account list (optionally filtered by search text) into the grid.
    ''' </summary>
    Private Sub LoadUsers(Optional searchText As String = "")
        Try
            Dim sql As String =
                "SELECT member_id AS `ID`, id_number AS `ID Number`, full_name AS `Full Name`, " &
                "username AS `Username`, role AS `Role`, date_registered AS `Date Registered` " &
                "FROM tbl_members"

            Dim params As New Dictionary(Of String, Object)

            If Not String.IsNullOrWhiteSpace(searchText) Then
                sql &= " WHERE full_name LIKE @search OR username LIKE @search OR id_number LIKE @search OR role LIKE @search"
                params.Add("@search", "%" & searchText & "%")
            End If

            sql &= " ORDER BY role DESC, full_name ASC"

            Dim dt As DataTable = DBConnection.GetDataTable(sql, params)
            dgvUsers.DataSource = dt

            If dgvUsers.Columns.Contains("ID") Then
                dgvUsers.Columns("ID").Visible = False
            End If

            Dim adminCount As Integer = 0
            For Each row As DataRow In dt.Rows
                If row("Role").ToString() = "admin" Then adminCount += 1
            Next
            lblUsersSummary.Text = $"Total users: {dt.Rows.Count} | Admins: {adminCount} | Members: {dt.Rows.Count - adminCount}"

        Catch ex As Exception
            MessageBox.Show("Could not load users: " & ex.Message, "Database Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Counts how many accounts currently have role = 'admin'. Used to stop
    ''' the last admin account from being demoted or deleted, which would
    ''' lock everyone out of Manage Users / Books / Overdue Books.
    ''' </summary>
    Private Function CountAdmins() As Integer
        Dim dt As DataTable = DBConnection.GetDataTable("SELECT COUNT(*) AS cnt FROM tbl_members WHERE role = 'admin'")
        Return Convert.ToInt32(dt.Rows(0)("cnt"))
    End Function

    Private Sub dgvUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsers.CellClick
        If e.RowIndex < 0 Then Return

        Dim row As DataGridViewRow = dgvUsers.Rows(e.RowIndex)
        selectedMemberId = Convert.ToInt32(row.Cells("ID").Value)
        txtIdNumber.Text = row.Cells("ID Number").Value.ToString()
        txtFullName.Text = row.Cells("Full Name").Value.ToString()
        txtUsername.Text = row.Cells("Username").Value.ToString()
        txtPassword.Text = ""
        cmbRole.SelectedItem = row.Cells("Role").Value.ToString()
    End Sub

    Private Sub btnSearchUsers_Click(sender As Object, e As EventArgs) Handles btnSearchUsers.Click
        LoadUsers(txtSearch.Text.Trim())
    End Sub

    Private Sub btnRefreshUsers_Click(sender As Object, e As EventArgs) Handles btnRefreshUsers.Click
        txtSearch.Clear()
        LoadUsers()
        ClearForm()
    End Sub

    ''' <summary>
    ''' Shared required-field / format checks for both Add and Update.
    ''' </summary>
    Private Function ValidateForm(isNewUser As Boolean) As Boolean
        If String.IsNullOrWhiteSpace(txtIdNumber.Text) OrElse
           String.IsNullOrWhiteSpace(txtFullName.Text) OrElse
           String.IsNullOrWhiteSpace(txtUsername.Text) Then
            MessageBox.Show("ID Number, Full Name, and Username are required.", "Missing Information",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If cmbRole.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a Role.", "Missing Information",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' A brand-new account needs a password right away since that's
        ' what lets it log in at all. An existing account can leave the
        ' password field blank to keep whatever it already has.
        If isNewUser AndAlso String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please set a password for this new account.", "Missing Information",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If Not String.IsNullOrWhiteSpace(txtPassword.Text) AndAlso txtPassword.Text.Length < 6 Then
            MessageBox.Show("Password must be at least 6 characters long.", "Weak Password",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        If Not ValidateForm(isNewUser:=True) Then Return

        Try
            Dim existing As DataTable = DBConnection.GetDataTable(
                "SELECT member_id FROM tbl_members WHERE username = @u OR id_number = @id",
                New Dictionary(Of String, Object) From {
                    {"@u", txtUsername.Text.Trim()},
                    {"@id", txtIdNumber.Text.Trim()}
                })

            If existing.Rows.Count > 0 Then
                MessageBox.Show("That ID Number or Username is already registered.", "Account Already Exists",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            DBConnection.ExecuteNonQuery(
                "INSERT INTO tbl_members (id_number, full_name, username, password, role) " &
                "VALUES (@id, @name, @user, @pass, @role)",
                New Dictionary(Of String, Object) From {
                    {"@id", txtIdNumber.Text.Trim()},
                    {"@name", txtFullName.Text.Trim()},
                    {"@user", txtUsername.Text.Trim()},
                    {"@pass", AuthHelper.HashPassword(txtPassword.Text)},
                    {"@role", cmbRole.SelectedItem.ToString()}
                })

            MessageBox.Show("Account created successfully.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUsers(txtSearch.Text.Trim())
            ClearForm()

        Catch ex As Exception
            MessageBox.Show("Could not create the account: " & ex.Message, "Database Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdateUser_Click(sender As Object, e As EventArgs) Handles btnUpdateUser.Click
        If selectedMemberId = 0 Then
            MessageBox.Show("Select a user from the list first.", "No User Selected",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not ValidateForm(isNewUser:=False) Then Return

        Dim newRole As String = cmbRole.SelectedItem.ToString()

        ' Guard against locking everyone out: if this is the last admin
        ' account, don't allow demoting it to 'member'.
        If selectedMemberId = AuthHelper.CurrentMemberId AndAlso newRole <> "admin" Then
            MessageBox.Show("You can't remove your own admin role while logged in.", "Not Allowed",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            If newRole <> "admin" AndAlso CountAdmins() <= 1 Then
                Dim currentRoleDt As DataTable = DBConnection.GetDataTable(
                    "SELECT role FROM tbl_members WHERE member_id = @id",
                    New Dictionary(Of String, Object) From {{"@id", selectedMemberId}})
                If currentRoleDt.Rows.Count > 0 AndAlso currentRoleDt.Rows(0)("role").ToString() = "admin" Then
                    MessageBox.Show("Can't demote the last remaining admin account.", "Not Allowed",
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            Dim existing As DataTable = DBConnection.GetDataTable(
                "SELECT member_id FROM tbl_members WHERE (username = @u OR id_number = @id) AND member_id <> @mid",
                New Dictionary(Of String, Object) From {
                    {"@u", txtUsername.Text.Trim()},
                    {"@id", txtIdNumber.Text.Trim()},
                    {"@mid", selectedMemberId}
                })

            If existing.Rows.Count > 0 Then
                MessageBox.Show("That ID Number or Username is already used by another account.", "Duplicate",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sql As String =
                "UPDATE tbl_members SET id_number = @id, full_name = @name, username = @user, role = @role"
            Dim params As New Dictionary(Of String, Object) From {
                {"@id", txtIdNumber.Text.Trim()},
                {"@name", txtFullName.Text.Trim()},
                {"@user", txtUsername.Text.Trim()},
                {"@role", newRole},
                {"@mid", selectedMemberId}
            }

            ' Only touch the password column if the admin actually typed a
            ' new one — otherwise leave the existing hash untouched.
            If Not String.IsNullOrWhiteSpace(txtPassword.Text) Then
                sql &= ", password = @pass"
                params.Add("@pass", AuthHelper.HashPassword(txtPassword.Text))
            End If

            sql &= " WHERE member_id = @mid"

            DBConnection.ExecuteNonQuery(sql, params)

            ' If the admin just updated their own account, keep the
            ' session's display name/role in sync.
            If selectedMemberId = AuthHelper.CurrentMemberId Then
                AuthHelper.CurrentFullName = txtFullName.Text.Trim()
                AuthHelper.CurrentUsername = txtUsername.Text.Trim()
                AuthHelper.CurrentRole = newRole
            End If

            MessageBox.Show("Account updated successfully.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUsers(txtSearch.Text.Trim())
            ClearForm()

        Catch ex As Exception
            MessageBox.Show("Could not update the account: " & ex.Message, "Database Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs) Handles btnDeleteUser.Click
        If selectedMemberId = 0 Then
            MessageBox.Show("Select a user from the list first.", "No User Selected",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If selectedMemberId = AuthHelper.CurrentMemberId Then
            MessageBox.Show("You can't delete the account you're currently logged in as.", "Not Allowed",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim roleDt As DataTable = DBConnection.GetDataTable(
                "SELECT role, full_name FROM tbl_members WHERE member_id = @id",
                New Dictionary(Of String, Object) From {{"@id", selectedMemberId}})

            If roleDt.Rows.Count = 0 Then Return

            If roleDt.Rows(0)("role").ToString() = "admin" AndAlso CountAdmins() <= 1 Then
                MessageBox.Show("Can't delete the last remaining admin account.", "Not Allowed",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim confirm As DialogResult = MessageBox.Show(
                $"Delete the account ""{roleDt.Rows(0)("full_name")}""? This cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If confirm <> DialogResult.Yes Then Return

            DBConnection.ExecuteNonQuery(
                "DELETE FROM tbl_members WHERE member_id = @id",
                New Dictionary(Of String, Object) From {{"@id", selectedMemberId}})

            MessageBox.Show("Account deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUsers(txtSearch.Text.Trim())
            ClearForm()

        Catch ex As Exception
            ' Most likely cause: this member still has rows in
            ' tbl_transactions (a foreign key), so MySQL refuses the delete.
            MessageBox.Show(
                "Could not delete this account: " & ex.Message & Environment.NewLine & Environment.NewLine &
                "If this user has borrowing history, they can't be deleted while those records exist.",
                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearUser_Click(sender As Object, e As EventArgs) Handles btnClearUser.Click
        ClearForm()
    End Sub

    Private Sub ClearForm()
        selectedMemberId = 0
        txtIdNumber.Clear()
        txtFullName.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        If cmbRole.Items.Count > 0 Then cmbRole.SelectedIndex = 0
        dgvUsers.ClearSelection()
    End Sub

End Class
