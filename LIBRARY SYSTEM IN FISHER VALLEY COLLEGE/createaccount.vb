Imports System.Data

Public Class createaccount
    Private Sub createaccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Same fixed-size issue as Form1 -- maximize so it fits the screen
        ' it's actually run on instead of opening at a hardcoded 1506x740.
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub pnlMain_Paint(sender As Object, e As PaintEventArgs) Handles pnlMain.Paint

    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        ' --- Required-field validation ---
        If String.IsNullOrWhiteSpace(txtStudentID.Text) OrElse
           String.IsNullOrWhiteSpace(txtUsername.Text) OrElse
           String.IsNullOrWhiteSpace(txtPassword.Text) OrElse
           String.IsNullOrWhiteSpace(txtConfirm.Text) OrElse
           String.IsNullOrWhiteSpace(txtFirstName.Text) OrElse
           String.IsNullOrWhiteSpace(txtLastName.Text) Then
            MessageBox.Show("Please fill out all required fields (Student ID, Username, Password, First Name, Last Name).",
                             "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If cmbCourse.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a Course / Department.", "Missing Information",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If txtPassword.Text <> txtConfirm.Text Then
            MessageBox.Show("Password and Confirm Password do not match.", "Password Mismatch",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtConfirm.Focus()
            Return
        End If

        If txtPassword.Text.Length < 6 Then
            MessageBox.Show("Password must be at least 6 characters long.", "Weak Password",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Return
        End If

        Try
            ' --- Duplicate Student ID / Username check ---
            Dim existing As DataTable = DBConnection.GetDataTable(
                "SELECT member_id FROM tbl_members WHERE username = @u OR id_number = @id",
                New Dictionary(Of String, Object) From {
                    {"@u", txtUsername.Text.Trim()},
                    {"@id", txtStudentID.Text.Trim()}
                })

            If existing.Rows.Count > 0 Then
                MessageBox.Show("That Student ID or Username is already registered. Please log in instead, or use a different username.",
                                 "Account Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' --- Save the new account ---
            Dim fullName As String = txtFirstName.Text.Trim() & " " & txtLastName.Text.Trim()

            ' This public "Create Account" screen is for students/faculty
            ' registering themselves, so role is always hard-coded to
            ' 'member' here — it's never taken from the form. Admin
            ' accounts are created directly in the database (see the
            ' seed row / notes in Database/library_system_db.sql), never
            ' through self-service sign-up.
            DBConnection.ExecuteNonQuery(
                "INSERT INTO tbl_members (id_number, full_name, course, year_level, contact_number, username, password, role) " &
                "VALUES (@id, @name, @course, @year, @contact, @user, @pass, 'member')",
                New Dictionary(Of String, Object) From {
                    {"@id", txtStudentID.Text.Trim()},
                    {"@name", fullName},
                    {"@course", cmbCourse.SelectedItem.ToString()},
                    {"@year", txtYearSection.Text.Trim()},
                    {"@contact", txtContact.Text.Trim()},
                    {"@user", txtUsername.Text.Trim()},
                    {"@pass", AuthHelper.HashPassword(txtPassword.Text)}
                })

            MessageBox.Show("Account created successfully! You can now log in.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Could not create the account: " & ex.Message, "Database Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' "Already have an account? Login" link — sends the user back to Form1.
    Private Sub lnkLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkLogin.LinkClicked
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class