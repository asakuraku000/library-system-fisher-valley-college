<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class createaccount
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        grpAccount = New GroupBox()
        lblStudentID = New Label()
        txtStudentID = New TextBox()
        lblUsername = New Label()
        txtUsername = New TextBox()
        lblPassword = New Label()
        txtPassword = New TextBox()
        lblConfirm = New Label()
        txtConfirm = New TextBox()
        lblFirstName = New Label()
        txtFirstName = New TextBox()
        btnCreate = New Button()
        lblLastName = New Label()
        btnCancel = New Button()
        txtLastName = New TextBox()
        lblCourse = New Label()
        cmbCourse = New ComboBox()
        lnkLogin = New LinkLabel()
        lblYearSection = New Label()
        txtYearSection = New TextBox()
        lblContact = New Label()
        grpPersonal = New GroupBox()
        txtContact = New TextBox()
        pnlSidebar = New Panel()
        lblTitle = New Label()
        pnlTop = New Panel()
        PictureBoxLogo = New PictureBox()
        pnlMain = New Panel()
        grpAccount.SuspendLayout()
        grpPersonal.SuspendLayout()
        pnlTop.SuspendLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).BeginInit()
        pnlMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' grpAccount
        ' 
        grpAccount.BackColor = Color.White
        grpAccount.Controls.Add(lblStudentID)
        grpAccount.Controls.Add(txtStudentID)
        grpAccount.Controls.Add(lblUsername)
        grpAccount.Controls.Add(txtUsername)
        grpAccount.Controls.Add(lblPassword)
        grpAccount.Controls.Add(txtPassword)
        grpAccount.Controls.Add(lblConfirm)
        grpAccount.Controls.Add(txtConfirm)
        grpAccount.Font = New Font("Segoe UI", 10F)
        grpAccount.Location = New Point(522, 130)
        grpAccount.Name = "grpAccount"
        grpAccount.Size = New Size(520, 170)
        grpAccount.TabIndex = 1
        grpAccount.TabStop = False
        grpAccount.Text = "Account Credentials"
        ' 
        ' lblStudentID
        ' 
        lblStudentID.AutoSize = True
        lblStudentID.Location = New Point(18, 34)
        lblStudentID.Name = "lblStudentID"
        lblStudentID.Size = New Size(75, 19)
        lblStudentID.TabIndex = 0
        lblStudentID.Text = "Student ID"
        ' 
        ' txtStudentID
        ' 
        txtStudentID.Location = New Point(150, 30)
        txtStudentID.Name = "txtStudentID"
        txtStudentID.Size = New Size(320, 25)
        txtStudentID.TabIndex = 1
        ' 
        ' lblUsername
        ' 
        lblUsername.AutoSize = True
        lblUsername.Location = New Point(18, 68)
        lblUsername.Name = "lblUsername"
        lblUsername.Size = New Size(71, 19)
        lblUsername.TabIndex = 2
        lblUsername.Text = "Username"
        ' 
        ' txtUsername
        ' 
        txtUsername.Location = New Point(150, 64)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(320, 25)
        txtUsername.TabIndex = 3
        ' 
        ' lblPassword
        ' 
        lblPassword.AutoSize = True
        lblPassword.Location = New Point(18, 102)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(67, 19)
        lblPassword.TabIndex = 4
        lblPassword.Text = "Password"
        ' 
        ' txtPassword
        ' 
        txtPassword.Location = New Point(150, 98)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(320, 25)
        txtPassword.TabIndex = 5
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' lblConfirm
        ' 
        lblConfirm.AutoSize = True
        lblConfirm.Location = New Point(18, 136)
        lblConfirm.Name = "lblConfirm"
        lblConfirm.Size = New Size(120, 19)
        lblConfirm.TabIndex = 6
        lblConfirm.Text = "Confirm Password"
        ' 
        ' txtConfirm
        ' 
        txtConfirm.Location = New Point(150, 132)
        txtConfirm.Name = "txtConfirm"
        txtConfirm.Size = New Size(320, 25)
        txtConfirm.TabIndex = 7
        txtConfirm.UseSystemPasswordChar = True
        ' 
        ' lblFirstName
        ' 
        lblFirstName.AutoSize = True
        lblFirstName.Location = New Point(18, 34)
        lblFirstName.Name = "lblFirstName"
        lblFirstName.Size = New Size(75, 19)
        lblFirstName.TabIndex = 0
        lblFirstName.Text = "First Name"
        ' 
        ' txtFirstName
        ' 
        txtFirstName.Location = New Point(150, 30)
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(320, 25)
        txtFirstName.TabIndex = 1
        ' 
        ' btnCreate
        ' 
        btnCreate.BackColor = Color.FromArgb(CByte(24), CByte(120), CByte(210))
        btnCreate.FlatStyle = FlatStyle.Flat
        btnCreate.ForeColor = Color.White
        btnCreate.Location = New Point(374, 119)
        btnCreate.Name = "btnCreate"
        btnCreate.Size = New Size(130, 32)
        btnCreate.TabIndex = 2
        btnCreate.Text = "Create Account"
        btnCreate.UseVisualStyleBackColor = False
        ' 
        ' lblLastName
        ' 
        lblLastName.AutoSize = True
        lblLastName.Location = New Point(18, 68)
        lblLastName.Name = "lblLastName"
        lblLastName.Size = New Size(74, 19)
        lblLastName.TabIndex = 2
        lblLastName.Text = "Last Name"
        ' 
        ' btnCancel
        ' 
        btnCancel.FlatStyle = FlatStyle.System
        btnCancel.Location = New Point(374, 157)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(130, 32)
        btnCancel.TabIndex = 3
        btnCancel.Text = "Cancel"
        ' 
        ' txtLastName
        ' 
        txtLastName.Location = New Point(150, 64)
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(320, 25)
        txtLastName.TabIndex = 3
        ' 
        ' lblCourse
        ' 
        lblCourse.AutoSize = True
        lblCourse.Location = New Point(18, 102)
        lblCourse.Name = "lblCourse"
        lblCourse.Size = New Size(139, 19)
        lblCourse.TabIndex = 4
        lblCourse.Text = "Course / Department"
        ' 
        ' cmbCourse
        ' 
        cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCourse.Items.AddRange(New Object() {"BSCS", "BSBA", "BSED"})
        cmbCourse.Location = New Point(150, 98)
        cmbCourse.Name = "cmbCourse"
        cmbCourse.Size = New Size(200, 25)
        cmbCourse.TabIndex = 5
        ' 
        ' lnkLogin
        ' 
        lnkLogin.AutoSize = True
        lnkLogin.Location = New Point(522, 527)
        lnkLogin.Name = "lnkLogin"
        lnkLogin.Size = New Size(175, 15)
        lnkLogin.TabIndex = 50
        lnkLogin.TabStop = True
        lnkLogin.Text = "Already have an account? Login"
        ' 
        ' lblYearSection
        ' 
        lblYearSection.AutoSize = True
        lblYearSection.Location = New Point(18, 136)
        lblYearSection.Name = "lblYearSection"
        lblYearSection.Size = New Size(87, 19)
        lblYearSection.TabIndex = 6
        lblYearSection.Text = "Year & Section"
        ' 
        ' txtYearSection
        ' 
        txtYearSection.Location = New Point(150, 132)
        txtYearSection.Name = "txtYearSection"
        txtYearSection.Size = New Size(200, 25)
        txtYearSection.TabIndex = 7
        ' 
        ' lblContact
        ' 
        lblContact.AutoSize = True
        lblContact.Location = New Point(18, 170)
        lblContact.Name = "lblContact"
        lblContact.Size = New Size(134, 19)
        lblContact.TabIndex = 8
        lblContact.Text = "Contact / Phone No."
        ' 
        ' grpPersonal
        ' 
        grpPersonal.BackColor = Color.White
        grpPersonal.Controls.Add(lblFirstName)
        grpPersonal.Controls.Add(txtFirstName)
        grpPersonal.Controls.Add(btnCreate)
        grpPersonal.Controls.Add(lblLastName)
        grpPersonal.Controls.Add(btnCancel)
        grpPersonal.Controls.Add(txtLastName)
        grpPersonal.Controls.Add(lblCourse)
        grpPersonal.Controls.Add(cmbCourse)
        grpPersonal.Controls.Add(lblYearSection)
        grpPersonal.Controls.Add(txtYearSection)
        grpPersonal.Controls.Add(lblContact)
        grpPersonal.Controls.Add(txtContact)
        grpPersonal.Font = New Font("Segoe UI", 10F)
        grpPersonal.Location = New Point(522, 314)
        grpPersonal.Name = "grpPersonal"
        grpPersonal.Size = New Size(520, 210)
        grpPersonal.TabIndex = 0
        grpPersonal.TabStop = False
        grpPersonal.Text = "Personal Information"
        ' 
        ' txtContact
        ' 
        txtContact.Location = New Point(150, 166)
        txtContact.Name = "txtContact"
        txtContact.Size = New Size(200, 25)
        txtContact.TabIndex = 9
        ' 
        ' pnlSidebar
        ' 
        pnlSidebar.BackColor = Color.FromArgb(CByte(10), CByte(80), CByte(140))
        pnlSidebar.Dock = DockStyle.Left
        pnlSidebar.Location = New Point(0, 64)
        pnlSidebar.Name = "pnlSidebar"
        pnlSidebar.Size = New Size(85, 676)
        pnlSidebar.TabIndex = 4
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(91, 9)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(167, 30)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Library System"
        ' 
        ' pnlTop
        ' 
        pnlTop.BackColor = Color.FromArgb(CByte(24), CByte(120), CByte(210))
        pnlTop.Controls.Add(PictureBoxLogo)
        pnlTop.Controls.Add(lblTitle)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(0, 0)
        pnlTop.Name = "pnlTop"
        pnlTop.Size = New Size(1506, 64)
        pnlTop.TabIndex = 5
        ' 
        ' PictureBoxLogo
        ' 
        PictureBoxLogo.BackColor = Color.WhiteSmoke
        PictureBoxLogo.Image = My.Resources.Resources._4EBBE486_D879_4CF0_AA27_34B66C33211E_1
        PictureBoxLogo.Location = New Point(0, 0)
        PictureBoxLogo.Name = "PictureBoxLogo"
        PictureBoxLogo.Size = New Size(85, 64)
        PictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBoxLogo.TabIndex = 3
        PictureBoxLogo.TabStop = False
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.WhiteSmoke
        pnlMain.Controls.Add(grpPersonal)
        pnlMain.Controls.Add(grpAccount)
        pnlMain.Controls.Add(lnkLogin)
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(24)
        pnlMain.Size = New Size(1506, 740)
        pnlMain.TabIndex = 3
        ' 
        ' createaccount
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1506, 740)
        Controls.Add(pnlSidebar)
        Controls.Add(pnlTop)
        Controls.Add(pnlMain)
        Name = "createaccount"
        Text = "createaccount"
        grpAccount.ResumeLayout(False)
        grpAccount.PerformLayout()
        grpPersonal.ResumeLayout(False)
        grpPersonal.PerformLayout()
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).EndInit()
        pnlMain.ResumeLayout(False)
        pnlMain.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents grpAccount As GroupBox
    Friend WithEvents lblStudentID As Label
    Friend WithEvents txtStudentID As TextBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblConfirm As Label
    Friend WithEvents txtConfirm As TextBox
    Friend WithEvents lblFirstName As Label
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents btnCreate As Button
    Friend WithEvents lblLastName As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents lblCourse As Label
    Friend WithEvents cmbCourse As ComboBox
    Friend WithEvents lnkLogin As LinkLabel
    Friend WithEvents lblYearSection As Label
    Friend WithEvents txtYearSection As TextBox
    Friend WithEvents lblContact As Label
    Friend WithEvents grpPersonal As GroupBox
    Friend WithEvents txtContact As TextBox
    Friend WithEvents pnlSidebar As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlTop As Panel
    Friend WithEvents pnlMain As Panel
    Private WithEvents PictureBoxLogo As PictureBox
End Class
