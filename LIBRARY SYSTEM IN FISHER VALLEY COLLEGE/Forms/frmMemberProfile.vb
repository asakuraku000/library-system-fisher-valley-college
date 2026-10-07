' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmMemberProfile  (member sidebar > My Profile)
'
' The logged-in member's own row in tbl_members:
'
'   Account Information  (read-only -- managed by the librarian)
'       ID number, full name, account type, course, year level,
'       username, membership status, member since
'   Contact Details      (member can edit + Save)
'       contact number, email, address
'   Change Password      (member can change their own)
'       current password is verified first; new password >= 6
'       characters (same rule as Create Account), stored as the
'       same SHA-256 hash AuthHelper.HashPassword uses at login.
'
' Built in code; uses the shared DBConnection + AuthHelper.
' ============================================================

Imports System.Data
Imports System.Text.RegularExpressions

Public Class frmMemberProfile
    Inherits frmMemberPageBase

    Private Const RowH As Integer = 34
    Private ReadOnly Navy As Color = Color.FromArgb(17, 78, 128)
    Private ReadOnly Blue As Color = Color.FromArgb(10, 120, 200)

    ' --- read-only account info ---
    Private ReadOnly lblIdNumber As Label = MakeValueLabel()
    Private ReadOnly lblFullName As Label = MakeValueLabel()
    Private ReadOnly lblAccountType As Label = MakeValueLabel()
    Private ReadOnly lblCourse As Label = MakeValueLabel()
    Private ReadOnly lblYearLevel As Label = MakeValueLabel()
    Private ReadOnly lblUsername As Label = MakeValueLabel()
    Private ReadOnly lblStatus As Label = MakeValueLabel()
    Private ReadOnly lblSince As Label = MakeValueLabel()

    ' --- editable contact details ---
    Private ReadOnly txtContact As New TextBox()
    Private ReadOnly txtEmail As New TextBox()
    Private ReadOnly txtAddress As New TextBox()
    Private ReadOnly btnSaveContact As New RoundedButton()

    ' --- change password ---
    Private ReadOnly txtCurrentPw As New TextBox()
    Private ReadOnly txtNewPw As New TextBox()
    Private ReadOnly txtConfirmPw As New TextBox()
    Private ReadOnly chkShowPw As New CheckBox()
    Private ReadOnly btnChangePw As New RoundedButton()

    Public Sub New()
        MyBase.New("My Profile", "Fisher Valley College Library — Your Account")
        BuildUi()
    End Sub

    ' ------------------------------------------------------------
    ' UI
    ' ------------------------------------------------------------
    Private Sub BuildUi()
        SuspendLayout()

        ' two equal columns; each column stacks its cards top-down
        Dim tlp As New TableLayoutPanel()
        tlp.Dock = DockStyle.Fill
        tlp.ColumnCount = 2
        tlp.RowCount = 1
        tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))

        Dim pnlLeft As New Panel()
        pnlLeft.Dock = DockStyle.Fill
        pnlLeft.AutoScroll = True
        pnlLeft.Padding = New Padding(0, 0, 8, 0)

        Dim pnlRight As New Panel()
        pnlRight.Dock = DockStyle.Fill
        pnlRight.AutoScroll = True
        pnlRight.Padding = New Padding(8, 0, 0, 0)

        ' ---------- card 1: account information (read-only) ----------
        Dim acctTable As TableLayoutPanel = Nothing
        Dim cardAccount As Panel = MakeCard("Account Information", 8 * RowH + 110, acctTable)
        AddRow(acctTable, "ID Number", lblIdNumber)
        AddRow(acctTable, "Full Name", lblFullName)
        AddRow(acctTable, "Account Type", lblAccountType)
        AddRow(acctTable, "Course", lblCourse)
        AddRow(acctTable, "Year Level", lblYearLevel)
        AddRow(acctTable, "Username", lblUsername)
        AddRow(acctTable, "Membership Status", lblStatus)
        AddRow(acctTable, "Member Since", lblSince)

        ' note along the bottom of the card (added after the Fill table)
        Dim lblAcctNote As New Label()
        lblAcctNote.AutoSize = False
        lblAcctNote.Dock = DockStyle.Bottom
        lblAcctNote.Height = 40
        lblAcctNote.TextAlign = ContentAlignment.MiddleLeft
        lblAcctNote.Font = New Font("Segoe UI", 8.5F, FontStyle.Italic)
        lblAcctNote.ForeColor = Color.FromArgb(110, 110, 110)
        lblAcctNote.Text = "To change your name, course, year level or username, please ask the library administrator."
        cardAccount.Controls.Add(lblAcctNote)

        ' ---------- card 2: contact details (editable) ----------
        Dim contactTable As TableLayoutPanel = Nothing
        Dim cardContact As Panel = MakeCard("Contact Details", 3 * RowH + 44 + 70, contactTable)
        StyleTextBox(txtContact)
        StyleTextBox(txtEmail)
        StyleTextBox(txtAddress)
        txtContact.MaxLength = 20
        txtEmail.MaxLength = 100
        txtAddress.MaxLength = 255
        AddRow(contactTable, "Contact Number", txtContact)
        AddRow(contactTable, "Email", txtEmail)
        AddRow(contactTable, "Address", txtAddress)
        StylePrimaryButton(btnSaveContact, "Save Changes", 140)
        AddButtonRow(contactTable, btnSaveContact)

        ' ---------- card 3: change password ----------
        Dim pwTable As TableLayoutPanel = Nothing
        Dim cardPassword As Panel = MakeCard("Change Password", 3 * RowH + 30 + 44 + 70, pwTable)
        StyleTextBox(txtCurrentPw)
        StyleTextBox(txtNewPw)
        StyleTextBox(txtConfirmPw)
        txtCurrentPw.UseSystemPasswordChar = True
        txtNewPw.UseSystemPasswordChar = True
        txtConfirmPw.UseSystemPasswordChar = True
        txtCurrentPw.MaxLength = 100
        txtNewPw.MaxLength = 100
        txtConfirmPw.MaxLength = 100
        AddRow(pwTable, "Current Password", txtCurrentPw)
        AddRow(pwTable, "New Password", txtNewPw)
        AddRow(pwTable, "Confirm New Password", txtConfirmPw)

        chkShowPw.AutoSize = True
        chkShowPw.Text = "Show passwords"
        chkShowPw.Font = New Font("Segoe UI", 9.0F)
        chkShowPw.Margin = New Padding(0, 6, 0, 0)
        AddRow(pwTable, "", chkShowPw, 30)

        StylePrimaryButton(btnChangePw, "Change Password", 140)
        AddButtonRow(pwTable, btnChangePw)

        ' Last added docks first (= on top). Left column: just one card.
        pnlLeft.Controls.Add(cardAccount)

        pnlRight.Controls.Add(cardPassword)
        pnlRight.Controls.Add(MakeSpacer(14))
        pnlRight.Controls.Add(cardContact)

        tlp.Controls.Add(pnlLeft, 0, 0)
        tlp.Controls.Add(pnlRight, 1, 0)
        pnlBody.Controls.Add(tlp)

        AddHandler btnSaveContact.Click, AddressOf btnSaveContact_Click
        AddHandler btnChangePw.Click, AddressOf btnChangePw_Click
        AddHandler chkShowPw.CheckedChanged,
            Sub(s, e)
                Dim hide As Boolean = Not chkShowPw.Checked
                txtCurrentPw.UseSystemPasswordChar = hide
                txtNewPw.UseSystemPasswordChar = hide
                txtConfirmPw.UseSystemPasswordChar = hide
            End Sub

        ResumeLayout(True)
    End Sub

    ' ---------- small UI helpers ----------
    Private Shared Function MakeValueLabel() As Label
        Dim l As New Label()
        l.AutoSize = False
        l.Dock = DockStyle.Fill
        l.TextAlign = ContentAlignment.MiddleLeft
        l.AutoEllipsis = True
        l.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        l.ForeColor = Color.FromArgb(40, 40, 40)
        l.Text = "—"
        Return l
    End Function

    ''' <summary>
    ''' White card: coloured accent bar, a title, and a 2-column table
    ''' (caption | field) that fills the rest. Docked to the top of its column.
    ''' </summary>
    Private Function MakeCard(title As String, cardHeight As Integer, ByRef table As TableLayoutPanel) As Panel
        Dim card As New Panel()
        card.Dock = DockStyle.Top
        card.Height = cardHeight
        card.BackColor = Color.White
        card.BorderStyle = BorderStyle.FixedSingle
        card.Padding = New Padding(18, 0, 18, 10)

        Dim bar As New Panel()
        bar.Dock = DockStyle.Top
        bar.Height = 5
        bar.BackColor = Blue

        Dim lblT As New Label()
        lblT.AutoSize = False
        lblT.Dock = DockStyle.Top
        lblT.Height = 40
        lblT.TextAlign = ContentAlignment.MiddleLeft
        lblT.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        lblT.ForeColor = Navy
        lblT.Text = title

        table = New TableLayoutPanel()
        table.Dock = DockStyle.Fill
        table.ColumnCount = 2
        table.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150.0F))
        table.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))

        ' fill first, then title, then the accent bar last (= on top)
        card.Controls.Add(table)
        card.Controls.Add(lblT)
        card.Controls.Add(bar)
        Return card
    End Function

    Private Sub AddRow(t As TableLayoutPanel, caption As String, ctl As Control, Optional height As Integer = RowH)
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, CSng(height)))
        t.RowCount = t.RowStyles.Count
        Dim idx As Integer = t.RowStyles.Count - 1

        Dim lbl As New Label()
        lbl.AutoSize = False
        lbl.Dock = DockStyle.Fill
        lbl.TextAlign = ContentAlignment.MiddleLeft
        lbl.Font = New Font("Segoe UI", 9.5F)
        lbl.ForeColor = Color.FromArgb(90, 90, 90)
        lbl.Text = caption

        t.Controls.Add(lbl, 0, idx)
        t.Controls.Add(ctl, 1, idx)
    End Sub

    Private Sub AddButtonRow(t As TableLayoutPanel, btn As Button)
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 44.0F))
        t.RowCount = t.RowStyles.Count
        t.Controls.Add(btn, 1, t.RowStyles.Count - 1)
    End Sub

    Private Sub StyleTextBox(tb As TextBox)
        tb.Font = New Font("Segoe UI", 9.5F)
        tb.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tb.Margin = New Padding(0, 5, 0, 0)
    End Sub

    Private Sub StylePrimaryButton(b As Button, caption As String, width As Integer)
        b.Size = New Size(width, 32)
        b.Text = caption
        b.Font = New Font("Segoe UI", 9.0F)
        b.FlatStyle = FlatStyle.Flat
        b.FlatAppearance.BorderSize = 0
        b.FlatAppearance.MouseOverBackColor = Color.FromArgb(28, 104, 168)
        b.FlatAppearance.MouseDownBackColor = Color.FromArgb(12, 62, 104)
        b.BackColor = Navy
        b.ForeColor = Color.White
        b.Cursor = Cursors.Hand
        b.Margin = New Padding(0, 8, 0, 0)
    End Sub

    Private Function MakeSpacer(h As Integer) As Panel
        Dim p As New Panel()
        p.Dock = DockStyle.Top
        p.Height = h
        Return p
    End Function

    ' ------------------------------------------------------------
    ' Data
    ' ------------------------------------------------------------
    Private Sub frmMemberProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProfile()
    End Sub

    ''' <summary>
    ''' Reads the member's row. SELECT * on purpose: older copies of the
    ''' database may not have every column (e.g. account_type), so each
    ''' field is read only if it exists.
    ''' </summary>
    Private Sub LoadProfile()
        Try
            Dim dt As DataTable = DBConnection.GetDataTable(
                "SELECT * FROM tbl_members WHERE member_id = @id",
                New Dictionary(Of String, Object) From {{"@id", AuthHelper.CurrentMemberId}})

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Your account could not be found. Please log in again.", "My Profile",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim r As DataRow = dt.Rows(0)

            lblIdNumber.Text = OrDash(Field(r, "id_number"))
            lblFullName.Text = OrDash(Field(r, "full_name"))

            Dim accType As String = Field(r, "account_type")
            If accType = "" Then accType = "Member"
            lblAccountType.Text = accType

            lblCourse.Text = OrDash(Field(r, "course"))
            lblYearLevel.Text = OrDash(Field(r, "year_level"))
            lblUsername.Text = OrDash(Field(r, "username"))

            Dim st As String = Field(r, "membership_status")
            lblStatus.Text = OrDash(st)
            Select Case st
                Case "Active"
                    lblStatus.ForeColor = Color.SeaGreen
                Case "Inactive", "Suspended"
                    lblStatus.ForeColor = Color.Firebrick
                Case Else
                    lblStatus.ForeColor = Color.FromArgb(40, 40, 40)
            End Select

            lblSince.Text = "—"
            If dt.Columns.Contains("date_registered") AndAlso Not IsDBNull(r("date_registered")) Then
                lblSince.Text = Convert.ToDateTime(r("date_registered")).ToString("MMM d, yyyy")
            End If

            txtContact.Text = Field(r, "contact_number")
            txtEmail.Text = Field(r, "email")
            txtAddress.Text = Field(r, "address")

        Catch ex As Exception
            MessageBox.Show("Could not load your profile: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Function Field(r As DataRow, column As String) As String
        If r.Table.Columns.Contains(column) AndAlso Not IsDBNull(r(column)) Then
            Return r(column).ToString().Trim()
        End If
        Return ""
    End Function

    Private Shared Function OrDash(value As String) As String
        Return If(String.IsNullOrWhiteSpace(value), "—", value)
    End Function

    ' ------------------------------------------------------------
    ' Save contact details
    ' ------------------------------------------------------------
    Private Sub btnSaveContact_Click(sender As Object, e As EventArgs)
        Dim contact As String = txtContact.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim address As String = txtAddress.Text.Trim()

        If contact <> "" AndAlso Not Regex.IsMatch(contact, "^[0-9+\-\s()]{7,20}$") Then
            MessageBox.Show("Please enter a valid contact number (digits, spaces, + - ( ) only).",
                            "Invalid Contact Number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContact.Focus()
            Return
        End If

        If email <> "" AndAlso Not Regex.IsMatch(email, "^[^@\s]+@[^@\s]+\.[^@\s]+$") Then
            MessageBox.Show("Please enter a valid email address.",
                            "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmail.Focus()
            Return
        End If

        Try
            DBConnection.ExecuteNonQuery(
                "UPDATE tbl_members SET contact_number = @c, email = @e, address = @a WHERE member_id = @id",
                New Dictionary(Of String, Object) From {
                    {"@c", OrNull(contact)},
                    {"@e", OrNull(email)},
                    {"@a", OrNull(address)},
                    {"@id", AuthHelper.CurrentMemberId}
                })

            MessageBox.Show("Your contact details were saved.", "My Profile",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Could not save your contact details: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Function OrNull(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then Return DBNull.Value
        Return value
    End Function

    ' ------------------------------------------------------------
    ' Change password
    ' ------------------------------------------------------------
    Private Sub btnChangePw_Click(sender As Object, e As EventArgs)
        If txtCurrentPw.Text = "" OrElse txtNewPw.Text = "" OrElse txtConfirmPw.Text = "" Then
            MessageBox.Show("Please fill out all three password fields.", "Change Password",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If txtNewPw.Text.Length < 6 Then
            MessageBox.Show("Your new password must be at least 6 characters long.", "Change Password",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNewPw.Focus()
            Return
        End If

        If txtNewPw.Text <> txtConfirmPw.Text Then
            MessageBox.Show("New Password and Confirm New Password do not match.", "Change Password",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtConfirmPw.Focus()
            Return
        End If

        If txtNewPw.Text = txtCurrentPw.Text Then
            MessageBox.Show("Your new password must be different from your current one.", "Change Password",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNewPw.Focus()
            Return
        End If

        Try
            ' verify the current password the same way login does
            Dim check As DataTable = DBConnection.GetDataTable(
                "SELECT member_id FROM tbl_members WHERE member_id = @id AND password = @p",
                New Dictionary(Of String, Object) From {
                    {"@id", AuthHelper.CurrentMemberId},
                    {"@p", AuthHelper.HashPassword(txtCurrentPw.Text)}
                })

            If check.Rows.Count = 0 Then
                MessageBox.Show("Your current password is incorrect.", "Change Password",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtCurrentPw.Focus()
                txtCurrentPw.SelectAll()
                Return
            End If

            DBConnection.ExecuteNonQuery(
                "UPDATE tbl_members SET password = @p WHERE member_id = @id",
                New Dictionary(Of String, Object) From {
                    {"@p", AuthHelper.HashPassword(txtNewPw.Text)},
                    {"@id", AuthHelper.CurrentMemberId}
                })

            txtCurrentPw.Clear()
            txtNewPw.Clear()
            txtConfirmPw.Clear()
            chkShowPw.Checked = False

            MessageBox.Show("Your password was changed. Use the new password the next time you log in.",
                            "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Could not change your password: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
