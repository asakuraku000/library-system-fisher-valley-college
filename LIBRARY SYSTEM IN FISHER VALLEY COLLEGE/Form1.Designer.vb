<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        topPanel = New Panel()
        PictureBoxLogo = New PictureBox()
        titleLabel = New Label()
        subtitleLabel = New Label()
        taglineLabel = New Label()
        pnlCard = New Panel()
        pnlLogin = New Panel()
        pnlLoginRight = New Panel()
        PictureBox3 = New PictureBox()
        lblQuality = New Label()
        lblWelcome = New Label()
        lblWelcomeSub = New Label()
        usernameLabel = New Label()
        PictureBox1 = New PictureBox()
        usernameTextBox = New TextBox()
        passwordLabel = New Label()
        PictureBox2 = New PictureBox()
        passwordTextBox = New TextBox()
        btnShowPassword = New Button()
        chkRemember = New CheckBox()
        lnkForgot = New LinkLabel()
        loginButton = New RoundedButton()
        createAccountButton = New RoundedButton()
        pnlSignup = New Panel()
        lblSignupTitle = New Label()
        grpAccount = New GroupBox()
        lblIdNumber = New Label()
        txtIdNumber = New TextBox()
        lblUsername = New Label()
        txtUsername = New TextBox()
        lblPassword = New Label()
        txtPassword = New TextBox()
        lblConfirm = New Label()
        txtConfirm = New TextBox()
        grpPersonal = New GroupBox()
        lblFirstName = New Label()
        txtFirstName = New TextBox()
        lblLastName = New Label()
        txtLastName = New TextBox()
        lblCourse = New Label()
        cmbCourse = New ComboBox()
        lblYearSection = New Label()
        txtYearSection = New TextBox()
        lblContact = New Label()
        txtContact = New TextBox()
        grpAccountType = New GroupBox()
        rdoStudent = New RadioButton()
        rdoTeacher = New RadioButton()
        lnkLogin = New LinkLabel()
        btnCreate = New RoundedButton()
        btnCancel = New RoundedButton()
        topPanel.SuspendLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).BeginInit()
        pnlCard.SuspendLayout()
        pnlLogin.SuspendLayout()
        pnlLoginRight.SuspendLayout()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        pnlSignup.SuspendLayout()
        grpAccount.SuspendLayout()
        grpPersonal.SuspendLayout()
        grpAccountType.SuspendLayout()
        SuspendLayout()
        '
        ' topPanel
        '
        topPanel.BackColor = Color.FromArgb(CByte(16), CByte(92), CByte(160))
        topPanel.Controls.Add(PictureBoxLogo)
        topPanel.Controls.Add(titleLabel)
        topPanel.Controls.Add(subtitleLabel)
        topPanel.Controls.Add(taglineLabel)
        topPanel.Dock = DockStyle.Top
        topPanel.Location = New Point(0, 0)
        topPanel.Name = "topPanel"
        topPanel.Size = New Size(1506, 72)
        topPanel.TabIndex = 5
        '
        ' PictureBoxLogo
        '
        PictureBoxLogo.BackColor = Color.WhiteSmoke
        PictureBoxLogo.Image = My.Resources.Resources._4EBBE486_D879_4CF0_AA27_34B66C33211E_
        PictureBoxLogo.Location = New Point(0, 0)
        PictureBoxLogo.Name = "PictureBoxLogo"
        PictureBoxLogo.Size = New Size(85, 72)
        PictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBoxLogo.TabIndex = 6
        PictureBoxLogo.TabStop = False
        '
        ' titleLabel
        '
        titleLabel.AutoSize = True
        titleLabel.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        titleLabel.ForeColor = Color.White
        titleLabel.Location = New Point(100, 10)
        titleLabel.Name = "titleLabel"
        titleLabel.Size = New Size(283, 28)
        titleLabel.TabIndex = 1
        titleLabel.Text = "The Fisher Valley College, Inc."
        '
        ' subtitleLabel
        '
        subtitleLabel.AutoSize = True
        subtitleLabel.Font = New Font("Segoe UI", 9.0F)
        subtitleLabel.ForeColor = Color.FromArgb(CByte(205), CByte(228), CByte(250))
        subtitleLabel.Location = New Point(103, 42)
        subtitleLabel.Name = "subtitleLabel"
        subtitleLabel.Size = New Size(100, 15)
        subtitleLabel.TabIndex = 2
        subtitleLabel.Text = "L I B R A R Y   S Y S T E M"
        '
        ' taglineLabel
        '
        taglineLabel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        taglineLabel.AutoSize = True
        taglineLabel.Font = New Font("Segoe UI", 10.0F, FontStyle.Italic)
        taglineLabel.ForeColor = Color.White
        taglineLabel.Location = New Point(1290, 26)
        taglineLabel.Name = "taglineLabel"
        taglineLabel.Size = New Size(180, 19)
        taglineLabel.TabIndex = 3
        taglineLabel.Text = "Books Build Better Tomorrow"
        '
        ' pnlCard
        '
        pnlCard.BackColor = Color.White
        pnlCard.Controls.Add(pnlLogin)
        pnlCard.Controls.Add(pnlSignup)
        pnlCard.Location = New Point(363, 165)
        pnlCard.Name = "pnlCard"
        pnlCard.Size = New Size(780, 410)
        pnlCard.TabIndex = 0
        '
        ' pnlLogin
        '
        pnlLogin.Controls.Add(pnlLoginRight)
        pnlLogin.Controls.Add(lblWelcome)
        pnlLogin.Controls.Add(lblWelcomeSub)
        pnlLogin.Controls.Add(usernameLabel)
        pnlLogin.Controls.Add(PictureBox1)
        pnlLogin.Controls.Add(usernameTextBox)
        pnlLogin.Controls.Add(passwordLabel)
        pnlLogin.Controls.Add(PictureBox2)
        pnlLogin.Controls.Add(passwordTextBox)
        pnlLogin.Controls.Add(btnShowPassword)
        pnlLogin.Controls.Add(chkRemember)
        pnlLogin.Controls.Add(lnkForgot)
        pnlLogin.Controls.Add(loginButton)
        pnlLogin.Controls.Add(createAccountButton)
        pnlLogin.Dock = DockStyle.Fill
        pnlLogin.Location = New Point(0, 0)
        pnlLogin.Name = "pnlLogin"
        pnlLogin.Size = New Size(780, 410)
        pnlLogin.TabIndex = 0
        '
        ' pnlLoginRight
        '
        pnlLoginRight.BackColor = Color.FromArgb(CByte(232), CByte(242), CByte(252))
        pnlLoginRight.Controls.Add(PictureBox3)
        pnlLoginRight.Controls.Add(lblQuality)
        pnlLoginRight.Dock = DockStyle.Right
        pnlLoginRight.Location = New Point(440, 0)
        pnlLoginRight.Name = "pnlLoginRight"
        pnlLoginRight.Size = New Size(340, 410)
        pnlLoginRight.TabIndex = 20
        '
        ' PictureBox3
        '
        PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), Image)
        PictureBox3.Location = New Point(45, 40)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(250, 250)
        PictureBox3.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox3.TabIndex = 0
        PictureBox3.TabStop = False
        '
        ' lblQuality
        '
        lblQuality.Font = New Font("Segoe UI", 10.0F, FontStyle.Italic)
        lblQuality.ForeColor = Color.FromArgb(CByte(16), CByte(92), CByte(160))
        lblQuality.Location = New Point(0, 305)
        lblQuality.Name = "lblQuality"
        lblQuality.Size = New Size(340, 50)
        lblQuality.TabIndex = 1
        lblQuality.Text = "Quality Education" & vbCrLf & "for a Brighter Tomorrow"
        lblQuality.TextAlign = ContentAlignment.MiddleCenter
        '
        ' lblWelcome
        '
        lblWelcome.AutoSize = True
        lblWelcome.Font = New Font("Segoe UI", 22.0F, FontStyle.Bold)
        lblWelcome.ForeColor = Color.FromArgb(CByte(16), CByte(92), CByte(160))
        lblWelcome.Location = New Point(36, 28)
        lblWelcome.Name = "lblWelcome"
        lblWelcome.Size = New Size(210, 41)
        lblWelcome.TabIndex = 0
        lblWelcome.Text = "Welcome Back"
        '
        ' lblWelcomeSub
        '
        lblWelcomeSub.AutoSize = True
        lblWelcomeSub.Font = New Font("Segoe UI", 10.0F)
        lblWelcomeSub.ForeColor = Color.FromArgb(CByte(100), CByte(110), CByte(120))
        lblWelcomeSub.Location = New Point(40, 74)
        lblWelcomeSub.Name = "lblWelcomeSub"
        lblWelcomeSub.Size = New Size(244, 19)
        lblWelcomeSub.TabIndex = 1
        lblWelcomeSub.Text = "Sign in to access your library account"
        '
        ' usernameLabel
        '
        usernameLabel.AutoSize = True
        usernameLabel.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        usernameLabel.Location = New Point(40, 116)
        usernameLabel.Name = "usernameLabel"
        usernameLabel.Size = New Size(66, 17)
        usernameLabel.TabIndex = 2
        usernameLabel.Text = "Username"
        '
        ' PictureBox1
        '
        PictureBox1.Image = My.Resources.Resources._F05D3536_EF6F_453B_A65C_746E448B028D_
        PictureBox1.Location = New Point(40, 138)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(30, 30)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 3
        PictureBox1.TabStop = False
        '
        ' usernameTextBox
        '
        usernameTextBox.Font = New Font("Segoe UI", 11.0F)
        usernameTextBox.Location = New Point(78, 140)
        usernameTextBox.Name = "usernameTextBox"
        usernameTextBox.Size = New Size(322, 27)
        usernameTextBox.TabIndex = 0
        '
        ' passwordLabel
        '
        passwordLabel.AutoSize = True
        passwordLabel.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        passwordLabel.Location = New Point(40, 184)
        passwordLabel.Name = "passwordLabel"
        passwordLabel.Size = New Size(63, 17)
        passwordLabel.TabIndex = 5
        passwordLabel.Text = "Password"
        '
        ' PictureBox2
        '
        PictureBox2.Image = My.Resources.Resources._B6325D80_0E9D_46BB_9B7C_027AB01B1AEE_
        PictureBox2.Location = New Point(40, 206)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(30, 30)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 6
        PictureBox2.TabStop = False
        '
        ' passwordTextBox
        '
        passwordTextBox.Font = New Font("Segoe UI", 11.0F)
        passwordTextBox.Location = New Point(78, 208)
        passwordTextBox.Name = "passwordTextBox"
        passwordTextBox.Size = New Size(260, 27)
        passwordTextBox.TabIndex = 1
        passwordTextBox.UseSystemPasswordChar = True
        '
        ' btnShowPassword
        '
        btnShowPassword.BackColor = Color.White
        btnShowPassword.FlatAppearance.BorderColor = Color.FromArgb(CByte(24), CByte(110), CByte(190))
        btnShowPassword.FlatStyle = FlatStyle.Flat
        btnShowPassword.Font = New Font("Segoe UI", 9.0F)
        btnShowPassword.ForeColor = Color.FromArgb(CByte(24), CByte(110), CByte(190))
        btnShowPassword.Location = New Point(344, 207)
        btnShowPassword.Name = "btnShowPassword"
        btnShowPassword.Size = New Size(56, 29)
        btnShowPassword.TabIndex = 2
        btnShowPassword.TabStop = False
        btnShowPassword.Text = "Show"
        btnShowPassword.UseVisualStyleBackColor = False
        '
        ' chkRemember
        '
        chkRemember.AutoSize = True
        chkRemember.Font = New Font("Segoe UI", 9.5F)
        chkRemember.Location = New Point(40, 258)
        chkRemember.Name = "chkRemember"
        chkRemember.Size = New Size(110, 21)
        chkRemember.TabIndex = 3
        chkRemember.Text = "Remember me"
        chkRemember.UseVisualStyleBackColor = True
        '
        ' lnkForgot
        '
        lnkForgot.AutoSize = True
        lnkForgot.Font = New Font("Segoe UI", 9.5F)
        lnkForgot.Location = New Point(292, 259)
        lnkForgot.Name = "lnkForgot"
        lnkForgot.Size = New Size(108, 17)
        lnkForgot.TabIndex = 4
        lnkForgot.TabStop = True
        lnkForgot.Text = "Forgot Password?"
        '
        ' loginButton
        '
        loginButton.BackColor = Color.FromArgb(CByte(24), CByte(110), CByte(190))
        loginButton.FlatAppearance.BorderSize = 0
        loginButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(16), CByte(92), CByte(160))
        loginButton.FlatStyle = FlatStyle.Flat
        loginButton.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        loginButton.ForeColor = Color.White
        loginButton.Location = New Point(40, 306)
        loginButton.Name = "loginButton"
        loginButton.Size = New Size(170, 42)
        loginButton.TabIndex = 5
        loginButton.Text = "Log In"
        loginButton.UseVisualStyleBackColor = False
        '
        ' createAccountButton
        '
        createAccountButton.BackColor = Color.White
        createAccountButton.FlatAppearance.BorderColor = Color.FromArgb(CByte(24), CByte(110), CByte(190))
        createAccountButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(232), CByte(242), CByte(252))
        createAccountButton.FlatStyle = FlatStyle.Flat
        createAccountButton.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        createAccountButton.ForeColor = Color.FromArgb(CByte(24), CByte(110), CByte(190))
        createAccountButton.Location = New Point(230, 306)
        createAccountButton.Name = "createAccountButton"
        createAccountButton.Size = New Size(170, 42)
        createAccountButton.TabIndex = 6
        createAccountButton.Text = "Create Account"
        createAccountButton.UseVisualStyleBackColor = False
        '
        ' pnlSignup
        '
        pnlSignup.Controls.Add(lblSignupTitle)
        pnlSignup.Controls.Add(grpAccount)
        pnlSignup.Controls.Add(grpPersonal)
        pnlSignup.Controls.Add(lnkLogin)
        pnlSignup.Controls.Add(btnCreate)
        pnlSignup.Controls.Add(btnCancel)
        pnlSignup.Dock = DockStyle.Fill
        pnlSignup.Location = New Point(0, 0)
        pnlSignup.Name = "pnlSignup"
        pnlSignup.Size = New Size(780, 410)
        pnlSignup.TabIndex = 1
        pnlSignup.Visible = False
        '
        ' lblSignupTitle
        '
        lblSignupTitle.AutoSize = True
        lblSignupTitle.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        lblSignupTitle.ForeColor = Color.FromArgb(CByte(16), CByte(92), CByte(160))
        lblSignupTitle.Location = New Point(22, 10)
        lblSignupTitle.Name = "lblSignupTitle"
        lblSignupTitle.Size = New Size(160, 30)
        lblSignupTitle.TabIndex = 0
        lblSignupTitle.Text = "Create Account"
        '
        ' grpAccount
        '
        grpAccount.BackColor = Color.White
        grpAccount.Controls.Add(lblIdNumber)
        grpAccount.Controls.Add(txtIdNumber)
        grpAccount.Controls.Add(lblUsername)
        grpAccount.Controls.Add(txtUsername)
        grpAccount.Controls.Add(lblPassword)
        grpAccount.Controls.Add(txtPassword)
        grpAccount.Controls.Add(lblConfirm)
        grpAccount.Controls.Add(txtConfirm)
        grpAccount.Font = New Font("Segoe UI", 10.0F)
        grpAccount.Location = New Point(24, 50)
        grpAccount.Name = "grpAccount"
        grpAccount.Size = New Size(552, 172)
        grpAccount.TabIndex = 1
        grpAccount.TabStop = False
        grpAccount.Text = "Account Credentials"
        '
        ' lblIdNumber
        '
        lblIdNumber.AutoSize = True
        lblIdNumber.Location = New Point(16, 34)
        lblIdNumber.Name = "lblIdNumber"
        lblIdNumber.Size = New Size(74, 19)
        lblIdNumber.TabIndex = 0
        lblIdNumber.Text = "ID number"
        '
        ' txtIdNumber
        '
        txtIdNumber.Location = New Point(170, 30)
        txtIdNumber.Name = "txtIdNumber"
        txtIdNumber.Size = New Size(360, 25)
        txtIdNumber.TabIndex = 0
        '
        ' lblUsername
        '
        lblUsername.AutoSize = True
        lblUsername.Location = New Point(16, 68)
        lblUsername.Name = "lblUsername"
        lblUsername.Size = New Size(71, 19)
        lblUsername.TabIndex = 2
        lblUsername.Text = "Username"
        '
        ' txtUsername
        '
        txtUsername.Location = New Point(170, 64)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(360, 25)
        txtUsername.TabIndex = 1
        '
        ' lblPassword
        '
        lblPassword.AutoSize = True
        lblPassword.Location = New Point(16, 102)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(67, 19)
        lblPassword.TabIndex = 4
        lblPassword.Text = "Password"
        '
        ' txtPassword
        '
        txtPassword.Location = New Point(170, 98)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(360, 25)
        txtPassword.TabIndex = 2
        txtPassword.UseSystemPasswordChar = True
        '
        ' lblConfirm
        '
        lblConfirm.AutoSize = True
        lblConfirm.Location = New Point(16, 136)
        lblConfirm.Name = "lblConfirm"
        lblConfirm.Size = New Size(120, 19)
        lblConfirm.TabIndex = 6
        lblConfirm.Text = "Confirm Password"
        '
        ' txtConfirm
        '
        txtConfirm.Location = New Point(170, 132)
        txtConfirm.Name = "txtConfirm"
        txtConfirm.Size = New Size(360, 25)
        txtConfirm.TabIndex = 3
        txtConfirm.UseSystemPasswordChar = True
        '
        ' grpPersonal
        '
        grpPersonal.BackColor = Color.White
        grpPersonal.Controls.Add(lblFirstName)
        grpPersonal.Controls.Add(txtFirstName)
        grpPersonal.Controls.Add(lblLastName)
        grpPersonal.Controls.Add(txtLastName)
        grpPersonal.Controls.Add(lblCourse)
        grpPersonal.Controls.Add(cmbCourse)
        grpPersonal.Controls.Add(lblYearSection)
        grpPersonal.Controls.Add(txtYearSection)
        grpPersonal.Controls.Add(lblContact)
        grpPersonal.Controls.Add(txtContact)
        grpPersonal.Controls.Add(grpAccountType)
        grpPersonal.Font = New Font("Segoe UI", 10.0F)
        grpPersonal.Location = New Point(24, 232)
        grpPersonal.Name = "grpPersonal"
        grpPersonal.Size = New Size(552, 206)
        grpPersonal.TabIndex = 2
        grpPersonal.TabStop = False
        grpPersonal.Text = "Personal Information"
        '
        ' lblFirstName
        '
        lblFirstName.AutoSize = True
        lblFirstName.Location = New Point(16, 34)
        lblFirstName.Name = "lblFirstName"
        lblFirstName.Size = New Size(75, 19)
        lblFirstName.TabIndex = 0
        lblFirstName.Text = "First Name"
        '
        ' txtFirstName
        '
        txtFirstName.Location = New Point(170, 30)
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(190, 25)
        txtFirstName.TabIndex = 0
        '
        ' lblLastName
        '
        lblLastName.AutoSize = True
        lblLastName.Location = New Point(16, 68)
        lblLastName.Name = "lblLastName"
        lblLastName.Size = New Size(74, 19)
        lblLastName.TabIndex = 2
        lblLastName.Text = "Last Name"
        '
        ' txtLastName
        '
        txtLastName.Location = New Point(170, 64)
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(190, 25)
        txtLastName.TabIndex = 1
        '
        ' lblCourse
        '
        lblCourse.AutoSize = True
        lblCourse.Location = New Point(16, 102)
        lblCourse.Name = "lblCourse"
        lblCourse.Size = New Size(139, 19)
        lblCourse.TabIndex = 4
        lblCourse.Text = "Course / Department"
        '
        ' cmbCourse
        '
        cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCourse.Items.AddRange(New Object() {"BSCS", "BSBA", "BSED"})
        cmbCourse.Location = New Point(170, 98)
        cmbCourse.Name = "cmbCourse"
        cmbCourse.Size = New Size(190, 25)
        cmbCourse.TabIndex = 2
        '
        ' lblYearSection
        '
        lblYearSection.AutoSize = True
        lblYearSection.Location = New Point(16, 136)
        lblYearSection.Name = "lblYearSection"
        lblYearSection.Size = New Size(97, 19)
        lblYearSection.TabIndex = 6
        lblYearSection.Text = "Year / Section"
        '
        ' txtYearSection
        '
        txtYearSection.Location = New Point(170, 132)
        txtYearSection.Name = "txtYearSection"
        txtYearSection.Size = New Size(190, 25)
        txtYearSection.TabIndex = 3
        '
        ' lblContact
        '
        lblContact.AutoSize = True
        lblContact.Location = New Point(16, 170)
        lblContact.Name = "lblContact"
        lblContact.Size = New Size(134, 19)
        lblContact.TabIndex = 8
        lblContact.Text = "Contact / Phone No."
        '
        ' txtContact
        '
        txtContact.Location = New Point(170, 166)
        txtContact.Name = "txtContact"
        txtContact.Size = New Size(190, 25)
        txtContact.TabIndex = 4
        '
        ' grpAccountType
        '
        grpAccountType.Controls.Add(rdoStudent)
        grpAccountType.Controls.Add(rdoTeacher)
        grpAccountType.Location = New Point(384, 26)
        grpAccountType.Name = "grpAccountType"
        grpAccountType.Size = New Size(150, 84)
        grpAccountType.TabIndex = 5
        grpAccountType.TabStop = False
        grpAccountType.Text = "Account Type"
        '
        ' rdoStudent
        '
        rdoStudent.AutoSize = True
        rdoStudent.Location = New Point(16, 26)
        rdoStudent.Name = "rdoStudent"
        rdoStudent.Size = New Size(71, 23)
        rdoStudent.TabIndex = 0
        rdoStudent.Text = "Student"
        rdoStudent.UseVisualStyleBackColor = True
        '
        ' rdoTeacher
        '
        rdoTeacher.AutoSize = True
        rdoTeacher.Location = New Point(16, 52)
        rdoTeacher.Name = "rdoTeacher"
        rdoTeacher.Size = New Size(71, 23)
        rdoTeacher.TabIndex = 1
        rdoTeacher.Text = "Teacher"
        rdoTeacher.UseVisualStyleBackColor = True
        '
        ' lnkLogin
        '
        lnkLogin.AutoSize = True
        lnkLogin.Font = New Font("Segoe UI", 9.5F)
        lnkLogin.Location = New Point(24, 461)
        lnkLogin.Name = "lnkLogin"
        lnkLogin.Size = New Size(185, 17)
        lnkLogin.TabIndex = 3
        lnkLogin.TabStop = True
        lnkLogin.Text = "Already have an account? Login"
        '
        ' btnCreate
        '
        btnCreate.BackColor = Color.FromArgb(CByte(24), CByte(110), CByte(190))
        btnCreate.FlatAppearance.BorderSize = 0
        btnCreate.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(16), CByte(92), CByte(160))
        btnCreate.FlatStyle = FlatStyle.Flat
        btnCreate.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnCreate.ForeColor = Color.White
        btnCreate.Location = New Point(326, 452)
        btnCreate.Name = "btnCreate"
        btnCreate.Size = New Size(140, 36)
        btnCreate.TabIndex = 4
        btnCreate.Text = "Create Account"
        btnCreate.UseVisualStyleBackColor = False
        '
        ' btnCancel
        '
        btnCancel.BackColor = Color.White
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(CByte(160), CByte(170), CByte(180))
        btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(240), CByte(244), CByte(248))
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI", 9.5F)
        btnCancel.Location = New Point(474, 452)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(102, 36)
        btnCancel.TabIndex = 5
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
        '
        ' Form1
        '
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1506, 740)
        Controls.Add(pnlCard)
        Controls.Add(topPanel)
        MinimumSize = New Size(900, 640)
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Library System - The Fisher Valley College, Inc."
        topPanel.ResumeLayout(False)
        topPanel.PerformLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).EndInit()
        pnlCard.ResumeLayout(False)
        pnlLogin.ResumeLayout(False)
        pnlLogin.PerformLayout()
        pnlLoginRight.ResumeLayout(False)
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        pnlSignup.ResumeLayout(False)
        pnlSignup.PerformLayout()
        grpAccount.ResumeLayout(False)
        grpAccount.PerformLayout()
        grpPersonal.ResumeLayout(False)
        grpPersonal.PerformLayout()
        grpAccountType.ResumeLayout(False)
        grpAccountType.PerformLayout()
        ResumeLayout(False)
    End Sub

    Private WithEvents topPanel As Panel
    Private WithEvents PictureBoxLogo As PictureBox
    Private WithEvents titleLabel As Label
    Private WithEvents subtitleLabel As Label
    Private WithEvents taglineLabel As Label
    Private WithEvents pnlCard As Panel
    Private WithEvents pnlLogin As Panel
    Private WithEvents pnlLoginRight As Panel
    Friend WithEvents PictureBox3 As PictureBox
    Private WithEvents lblQuality As Label
    Private WithEvents lblWelcome As Label
    Private WithEvents lblWelcomeSub As Label
    Private WithEvents usernameLabel As Label
    Friend WithEvents PictureBox1 As PictureBox
    Private WithEvents usernameTextBox As TextBox
    Private WithEvents passwordLabel As Label
    Friend WithEvents PictureBox2 As PictureBox
    Private WithEvents passwordTextBox As TextBox
    Private WithEvents btnShowPassword As Button
    Private WithEvents chkRemember As CheckBox
    Private WithEvents lnkForgot As LinkLabel
    Private WithEvents loginButton As RoundedButton
    Private WithEvents createAccountButton As RoundedButton
    Private WithEvents pnlSignup As Panel
    Private WithEvents lblSignupTitle As Label
    Friend WithEvents grpAccount As GroupBox
    Friend WithEvents lblIdNumber As Label
    Friend WithEvents txtIdNumber As TextBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblConfirm As Label
    Friend WithEvents txtConfirm As TextBox
    Friend WithEvents grpPersonal As GroupBox
    Friend WithEvents lblFirstName As Label
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents lblLastName As Label
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents lblCourse As Label
    Friend WithEvents cmbCourse As ComboBox
    Friend WithEvents lblYearSection As Label
    Friend WithEvents txtYearSection As TextBox
    Friend WithEvents lblContact As Label
    Friend WithEvents txtContact As TextBox
    Friend WithEvents grpAccountType As GroupBox
    Friend WithEvents rdoStudent As RadioButton
    Friend WithEvents rdoTeacher As RadioButton
    Friend WithEvents lnkLogin As LinkLabel
    Friend WithEvents btnCreate As RoundedButton
    Friend WithEvents btnCancel As RoundedButton

End Class
