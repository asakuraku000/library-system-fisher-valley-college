' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmManageUsers (Designer / layout)
' Admin-only screen (opened from main's "Manage Users" button):
' view/search every account, change a user's role, add new
' accounts (including admin accounts), or remove an account.
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmManageUsers
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer.
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlHeader = New Panel()
        lblSubtitle = New Label()
        lblTitle = New Label()
        grpDetails = New GroupBox()
        lblPasswordHint = New Label()
        cmbRole = New ComboBox()
        lblRole = New Label()
        txtPassword = New TextBox()
        lblPassword = New Label()
        txtUsername = New TextBox()
        lblUsername = New Label()
        txtFullName = New TextBox()
        lblFullName = New Label()
        txtIdNumber = New TextBox()
        lblIdNumber = New Label()
        btnClearUser = New RoundedButton()
        btnDeleteUser = New RoundedButton()
        btnUpdateUser = New RoundedButton()
        btnAddUser = New RoundedButton()
        lblSearch = New Label()
        txtSearch = New TextBox()
        btnSearchUsers = New RoundedButton()
        btnRefreshUsers = New RoundedButton()
        dgvUsers = New DataGridView()
        lblUsersSummary = New Label()
        pnlHeader.SuspendLayout()
        grpDetails.SuspendLayout()
        CType(dgvUsers, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' pnlHeader
        '
        pnlHeader.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        pnlHeader.Controls.Add(lblSubtitle)
        pnlHeader.Controls.Add(lblTitle)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New Size(960, 64)
        pnlHeader.TabIndex = 0
        '
        ' lblSubtitle
        '
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New Font("Segoe UI", 9F)
        lblSubtitle.ForeColor = Color.WhiteSmoke
        lblSubtitle.Location = New Point(20, 38)
        lblSubtitle.Name = "lblSubtitle"
        lblSubtitle.Size = New Size(180, 15)
        lblSubtitle.TabIndex = 1
        lblSubtitle.Text = "Fisher Valley College Library"
        '
        ' lblTitle
        '
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 15F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(18, 10)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(190, 28)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Manage Users"
        '
        ' grpDetails
        '
        grpDetails.Controls.Add(lblPasswordHint)
        grpDetails.Controls.Add(cmbRole)
        grpDetails.Controls.Add(lblRole)
        grpDetails.Controls.Add(txtPassword)
        grpDetails.Controls.Add(lblPassword)
        grpDetails.Controls.Add(txtUsername)
        grpDetails.Controls.Add(lblUsername)
        grpDetails.Controls.Add(txtFullName)
        grpDetails.Controls.Add(lblFullName)
        grpDetails.Controls.Add(txtIdNumber)
        grpDetails.Controls.Add(lblIdNumber)
        grpDetails.Controls.Add(btnClearUser)
        grpDetails.Controls.Add(btnDeleteUser)
        grpDetails.Controls.Add(btnUpdateUser)
        grpDetails.Controls.Add(btnAddUser)
        grpDetails.Font = New Font("Segoe UI", 9F)
        grpDetails.Location = New Point(20, 80)
        grpDetails.Name = "grpDetails"
        grpDetails.Size = New Size(280, 400)
        grpDetails.TabIndex = 1
        grpDetails.TabStop = False
        grpDetails.Text = "Account Details"
        '
        ' lblIdNumber
        '
        lblIdNumber.AutoSize = True
        lblIdNumber.Location = New Point(15, 30)
        lblIdNumber.Name = "lblIdNumber"
        lblIdNumber.Size = New Size(65, 15)
        lblIdNumber.TabIndex = 0
        lblIdNumber.Text = "ID Number:"
        '
        ' txtIdNumber
        '
        txtIdNumber.Location = New Point(115, 27)
        txtIdNumber.Name = "txtIdNumber"
        txtIdNumber.Size = New Size(150, 23)
        txtIdNumber.TabIndex = 1
        '
        ' lblFullName
        '
        lblFullName.AutoSize = True
        lblFullName.Location = New Point(15, 64)
        lblFullName.Name = "lblFullName"
        lblFullName.Size = New Size(64, 15)
        lblFullName.TabIndex = 2
        lblFullName.Text = "Full Name:"
        '
        ' txtFullName
        '
        txtFullName.Location = New Point(115, 61)
        txtFullName.Name = "txtFullName"
        txtFullName.Size = New Size(150, 23)
        txtFullName.TabIndex = 3
        '
        ' lblUsername
        '
        lblUsername.AutoSize = True
        lblUsername.Location = New Point(15, 98)
        lblUsername.Name = "lblUsername"
        lblUsername.Size = New Size(70, 15)
        lblUsername.TabIndex = 4
        lblUsername.Text = "Username:"
        '
        ' txtUsername
        '
        txtUsername.Location = New Point(115, 95)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(150, 23)
        txtUsername.TabIndex = 5
        '
        ' lblPassword
        '
        lblPassword.AutoSize = True
        lblPassword.Location = New Point(15, 132)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(63, 15)
        lblPassword.TabIndex = 6
        lblPassword.Text = "Password:"
        '
        ' txtPassword
        '
        txtPassword.Location = New Point(115, 129)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "*"c
        txtPassword.Size = New Size(150, 23)
        txtPassword.TabIndex = 7
        '
        ' lblPasswordHint
        '
        lblPasswordHint.AutoSize = True
        lblPasswordHint.Font = New Font("Segoe UI", 7.5F, FontStyle.Italic)
        lblPasswordHint.ForeColor = Color.Gray
        lblPasswordHint.Location = New Point(115, 154)
        lblPasswordHint.Name = "lblPasswordHint"
        lblPasswordHint.Size = New Size(150, 13)
        lblPasswordHint.TabIndex = 8
        lblPasswordHint.Text = "Blank = keep current password"
        '
        ' lblRole
        '
        lblRole.AutoSize = True
        lblRole.Location = New Point(15, 188)
        lblRole.Name = "lblRole"
        lblRole.Size = New Size(35, 15)
        lblRole.TabIndex = 9
        lblRole.Text = "Role:"
        '
        ' cmbRole
        '
        cmbRole.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRole.Location = New Point(115, 185)
        cmbRole.Name = "cmbRole"
        cmbRole.Size = New Size(150, 23)
        cmbRole.TabIndex = 10
        '
        ' btnAddUser
        '
        btnAddUser.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnAddUser.FlatAppearance.BorderSize = 0
        btnAddUser.FlatStyle = FlatStyle.Flat
        btnAddUser.ForeColor = Color.White
        btnAddUser.Location = New Point(15, 280)
        btnAddUser.Name = "btnAddUser"
        btnAddUser.Kind = ButtonKind.Primary
        btnAddUser.Size = New Size(120, 32)
        btnAddUser.TabIndex = 11
        btnAddUser.Text = "Add User"
        btnAddUser.UseVisualStyleBackColor = False
        '
        ' btnUpdateUser
        '
        btnUpdateUser.BackColor = Color.FromArgb(CByte(46), CByte(125), CByte(50))
        btnUpdateUser.FlatAppearance.BorderSize = 0
        btnUpdateUser.FlatStyle = FlatStyle.Flat
        btnUpdateUser.ForeColor = Color.White
        btnUpdateUser.Location = New Point(145, 280)
        btnUpdateUser.Name = "btnUpdateUser"
        btnUpdateUser.Kind = ButtonKind.Custom
        btnUpdateUser.Size = New Size(120, 32)
        btnUpdateUser.TabIndex = 12
        btnUpdateUser.Text = "Update"
        btnUpdateUser.UseVisualStyleBackColor = False
        '
        ' btnDeleteUser
        '
        btnDeleteUser.BackColor = Color.FromArgb(CByte(178), CByte(34), CByte(34))
        btnDeleteUser.FlatAppearance.BorderSize = 0
        btnDeleteUser.FlatStyle = FlatStyle.Flat
        btnDeleteUser.ForeColor = Color.White
        btnDeleteUser.Location = New Point(15, 320)
        btnDeleteUser.Name = "btnDeleteUser"
        btnDeleteUser.Kind = ButtonKind.Custom
        btnDeleteUser.Size = New Size(120, 32)
        btnDeleteUser.TabIndex = 13
        btnDeleteUser.Text = "Delete"
        btnDeleteUser.UseVisualStyleBackColor = False
        '
        ' btnClearUser
        '
        btnClearUser.FlatStyle = FlatStyle.Flat
        btnClearUser.Location = New Point(145, 320)
        btnClearUser.Name = "btnClearUser"
        btnClearUser.Kind = ButtonKind.Neutral
        btnClearUser.Size = New Size(120, 32)
        btnClearUser.TabIndex = 14
        btnClearUser.Text = "Clear Form"
        btnClearUser.UseVisualStyleBackColor = True
        '
        ' lblSearch
        '
        lblSearch.AutoSize = True
        lblSearch.Font = New Font("Segoe UI", 9F)
        lblSearch.Location = New Point(320, 80)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(51, 15)
        lblSearch.TabIndex = 2
        lblSearch.Text = "Search:"
        '
        ' txtSearch
        '
        txtSearch.Font = New Font("Segoe UI", 9F)
        txtSearch.Location = New Point(378, 77)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(300, 23)
        txtSearch.TabIndex = 3
        '
        ' btnSearchUsers
        '
        btnSearchUsers.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnSearchUsers.FlatAppearance.BorderSize = 0
        btnSearchUsers.FlatStyle = FlatStyle.Flat
        btnSearchUsers.Font = New Font("Segoe UI", 9F)
        btnSearchUsers.ForeColor = Color.White
        btnSearchUsers.Location = New Point(688, 76)
        btnSearchUsers.Name = "btnSearchUsers"
        btnSearchUsers.Kind = ButtonKind.Primary
        btnSearchUsers.Size = New Size(80, 26)
        btnSearchUsers.TabIndex = 4
        btnSearchUsers.Text = "Search"
        btnSearchUsers.UseVisualStyleBackColor = False
        '
        ' btnRefreshUsers
        '
        btnRefreshUsers.FlatAppearance.BorderColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefreshUsers.FlatStyle = FlatStyle.Flat
        btnRefreshUsers.Font = New Font("Segoe UI", 9F)
        btnRefreshUsers.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefreshUsers.Location = New Point(778, 76)
        btnRefreshUsers.Name = "btnRefreshUsers"
        btnRefreshUsers.Kind = ButtonKind.Outline
        btnRefreshUsers.Size = New Size(80, 26)
        btnRefreshUsers.TabIndex = 5
        btnRefreshUsers.Text = "Refresh"
        btnRefreshUsers.UseVisualStyleBackColor = True
        '
        ' dgvUsers
        '
        dgvUsers.AllowUserToAddRows = False
        dgvUsers.AllowUserToDeleteRows = False
        dgvUsers.AllowUserToOrderColumns = False
        dgvUsers.AllowUserToResizeRows = False
        dgvUsers.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(CByte(245), CByte(247), CByte(250))
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvUsers.BackgroundColor = Color.White
        dgvUsers.BorderStyle = BorderStyle.FixedSingle
        dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvUsers.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        dgvUsers.ColumnHeadersHeight = 32
        dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvUsers.EnableHeadersVisualStyles = False
        dgvUsers.GridColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        dgvUsers.Location = New Point(320, 114)
        dgvUsers.MultiSelect = False
        dgvUsers.Name = "dgvUsers"
        dgvUsers.ReadOnly = True
        dgvUsers.RowHeadersVisible = False
        dgvUsers.RowTemplate.Height = 26
        dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvUsers.Size = New Size(620, 366)
        dgvUsers.TabIndex = 6
        '
        ' lblUsersSummary
        '
        lblUsersSummary.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblUsersSummary.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblUsersSummary.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        lblUsersSummary.Location = New Point(320, 488)
        lblUsersSummary.Name = "lblUsersSummary"
        lblUsersSummary.Size = New Size(500, 23)
        lblUsersSummary.TabIndex = 7
        lblUsersSummary.Text = "Total users: 0 | Admins: 0 | Members: 0"
        lblUsersSummary.TextAlign = ContentAlignment.MiddleLeft
        '
        ' frmManageUsers
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(960, 530)
        Controls.Add(lblUsersSummary)
        Controls.Add(dgvUsers)
        Controls.Add(btnRefreshUsers)
        Controls.Add(btnSearchUsers)
        Controls.Add(txtSearch)
        Controls.Add(lblSearch)
        Controls.Add(grpDetails)
        Controls.Add(pnlHeader)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmManageUsers"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fisher Valley College Library - Manage Users"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        CType(dgvUsers, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents lblIdNumber As Label
    Friend WithEvents txtIdNumber As TextBox
    Friend WithEvents lblFullName As Label
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblPasswordHint As Label
    Friend WithEvents lblRole As Label
    Friend WithEvents cmbRole As ComboBox
    Friend WithEvents btnAddUser As RoundedButton
    Friend WithEvents btnUpdateUser As RoundedButton
    Friend WithEvents btnDeleteUser As RoundedButton
    Friend WithEvents btnClearUser As RoundedButton
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearchUsers As RoundedButton
    Friend WithEvents btnRefreshUsers As RoundedButton
    Friend WithEvents dgvUsers As DataGridView
    Friend WithEvents lblUsersSummary As Label
End Class
