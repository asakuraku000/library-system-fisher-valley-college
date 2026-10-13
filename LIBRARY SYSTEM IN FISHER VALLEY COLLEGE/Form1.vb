Imports System.Data
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Text.RegularExpressions

' ============================================================
' Form1 -- ONE frame for both Log In and Create Account.
' The white card in the middle swaps between two panels
' (pnlLogin / pnlSignup) instead of opening a second form.
' Background: Images\login_bg.jpg (copied next to the .exe).
' ============================================================
Public Class Form1

    Private _bg As Image

    ' Card sizes for each mode
    Private ReadOnly LoginCardSize As New Size(780, 410)
    Private ReadOnly SignupCardSize As New Size(780, 540)

    ' Create Account layout (two columns of rounded fields + picture panel)
    Private Const SignupColW As Integer = 230
    Private Const SignupColGap As Integer = 16
    Private Const SignupFieldH As Integer = 38
    Private Const SignupLabelH As Integer = 19      ' label row above each field
    Private Const SignupRowPitch As Integer = 62
    Private Const SignupRightW As Integer = 240

    ' Create Account input rules
    Private Const ContactLength As Integer = 11     ' PH mobile number, e.g. 09123456789
    Private Const NameMaxLength As Integer = 45     ' first + last must fit full_name VARCHAR(100)
    Private _sanitizing As Boolean
    Private _eyePass As Button
    Private _eyeConfirm As Button

    ' "Remember me" just stores the last username in %AppData%\FVCLibrary\remember.txt
    Private ReadOnly RememberFile As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FVCLibrary", "remember.txt")

    ' ---- Left sidebar + pages (built in code, see BuildChrome) -------------
    Private pnlSidebar As Panel
    Private pnlPages As Panel
    Private pageLibrary As pgLibrary
    Private WithEvents navHome As SidebarButton
    Private WithEvents navLibrary As SidebarButton
    Private WithEvents navAccount As SidebarButton
    Private WithEvents navExit As SidebarButton

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True

        ' Sidebar, pages, icons, rounded buttons and padded fields come
        ' first, so everything exists before the window is maximized.
        BuildChrome()
        StyleLoginCard()
        StyleSignupCard()
        StyleButtons()

        ' Maximize so the card + background fill whatever screen it runs on.
        Me.WindowState = FormWindowState.Maximized

        LoadBackground()
        LoadRememberedUsername()
        ShowLogin()
    End Sub

    ' ------------------------------------------------------------
    ' Sidebar (Home / Library / Account / Exit) + page host
    ' ------------------------------------------------------------
    Private Sub BuildChrome()
        SuspendLayout()

        ' Header: open-book icon next to the tagline (like the mockup).
        Dim picBook As New PictureBox()
        picBook.Image = AppIcons.Scaled("book_open_white", 30)
        picBook.SizeMode = PictureBoxSizeMode.CenterImage
        picBook.BackColor = Color.Transparent
        picBook.Size = New Size(34, 34)
        picBook.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        picBook.Location = New Point(topPanel.Width - 34 - 24, (topPanel.Height - 34) \ 2)
        topPanel.Controls.Add(picBook)
        taglineLabel.Left -= 46

        ' Sidebar
        pnlSidebar = New Panel()
        pnlSidebar.Dock = DockStyle.Left
        pnlSidebar.Width = 84
        pnlSidebar.BackColor = Color.FromArgb(16, 92, 160)

        navHome = MakeNav("Home", "nav_home_white", 0)
        navLibrary = MakeNav("Library", "nav_library_white", 1)
        navAccount = MakeNav("Account", "nav_account_white", 2)
        navExit = MakeNav("Exit", "nav_exit_white", 3)

        ' Page host: the login/create-account card lives here, and so does
        ' the Library page. Transparent so the background image shows through.
        pnlPages = New Panel()
        pnlPages.Dock = DockStyle.Fill
        pnlPages.BackColor = Color.Transparent
        AddHandler pnlPages.Resize, AddressOf PagesResized

        pnlPages.Controls.Add(pnlCard)

        pageLibrary = New pgLibrary()
        pnlPages.Controls.Add(pageLibrary)

        ' Docking is applied from the last control to the first, so the
        ' header (last) takes the full top strip, then the sidebar, then
        ' the page host fills the rest.
        Controls.Add(pnlSidebar)
        Controls.Add(pnlPages)
        Controls.SetChildIndex(pnlSidebar, 0)
        Controls.SetChildIndex(pnlPages, 0)

        ResumeLayout(True)
    End Sub

    Private Function MakeNav(caption As String, iconName As String, index As Integer) As SidebarButton
        Dim b As New SidebarButton()
        b.Text = caption
        b.IconImage = AppIcons.Load(iconName)
        b.SetBounds(0, 14 + index * 78, 84, 72)
        pnlSidebar.Controls.Add(b)
        Return b
    End Function

    Private Sub SetActiveNav(active As SidebarButton)
        If navHome Is Nothing Then Return
        navHome.Selected = (active Is navHome)
        navLibrary.Selected = (active Is navLibrary)
        navAccount.Selected = (active Is navAccount)
    End Sub

    Private Sub PagesResized(sender As Object, e As EventArgs)
        CenterCard()
        pnlPages.Invalidate(True)
    End Sub

    Private Sub navHome_Click(sender As Object, e As EventArgs) Handles navHome.Click
        If Not (pnlCard.Visible AndAlso pnlLogin.Visible) Then ShowLogin()
    End Sub

    Private Sub navLibrary_Click(sender As Object, e As EventArgs) Handles navLibrary.Click
        ShowLibrary()
    End Sub

    Private Sub navAccount_Click(sender As Object, e As EventArgs) Handles navAccount.Click
        ' Don't wipe a half-filled form if they're already on it.
        If Not (pnlCard.Visible AndAlso pnlSignup.Visible) Then ShowSignup()
    End Sub

    Private Sub navExit_Click(sender As Object, e As EventArgs) Handles navExit.Click
        If MessageBox.Show("Do you want to exit the Library System?", "Exit",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub ShowLibrary()
        pnlCard.Visible = False
        pageLibrary.Visible = True
        pageLibrary.BringToFront()
        Me.AcceptButton = Nothing
        Me.CancelButton = Nothing
        SetActiveNav(navLibrary)
        pageLibrary.Reload()
    End Sub

    Private Sub pnlCard_SizeChanged(sender As Object, e As EventArgs) Handles pnlCard.SizeChanged
        ApplyRoundedRegion(pnlCard, 18)
    End Sub

    ' ------------------------------------------------------------
    ' Look & feel: padded rounded fields, icons, rounded buttons
    ' ------------------------------------------------------------
    Private Sub StyleLoginCard()
        ' Make room for the book icon above "Welcome Back" by nudging the
        ' left half of the login card down.
        Dim dy As Integer = 24
        For Each c As Control In New Control() {
                lblWelcome, lblWelcomeSub, usernameLabel, PictureBox1, usernameTextBox,
                passwordLabel, PictureBox2, passwordTextBox, btnShowPassword,
                chkRemember, lnkForgot, loginButton, createAccountButton}
            c.Top += dy
        Next

        Dim picBook As New PictureBox()
        picBook.Image = AppIcons.Scaled("book_open_blue", 34)
        picBook.SizeMode = PictureBoxSizeMode.CenterImage
        picBook.BackColor = Color.White
        picBook.SetBounds(36, 18, 40, 40)
        pnlLogin.Controls.Add(picBook)

        ' The old icon pictures are replaced by icons inside the fields.
        Dim userY As Integer = PictureBox1.Top
        Dim passY As Integer = PictureBox2.Top
        PictureBox1.Visible = False
        PictureBox2.Visible = False

        WrapTextBox(usernameTextBox, AppIcons.Scaled("user_blue", 18),
                    New Rectangle(40, userY - 1, 360, 42), 14)
        usernameTextBox.PlaceholderText = "Username"

        Dim passField As RoundedField = WrapTextBox(passwordTextBox, AppIcons.Scaled("lock_blue", 18),
                    New Rectangle(40, passY - 1, 360, 42), 14, 40)
        passwordTextBox.PlaceholderText = "Password"

        ' Show/hide password becomes an eye icon inside the field.
        passField.Controls.Add(btnShowPassword)
        btnShowPassword.Text = ""
        btnShowPassword.Image = AppIcons.Scaled("eye_gray", 18)
        btnShowPassword.ImageAlign = ContentAlignment.MiddleCenter
        btnShowPassword.BackColor = Color.White
        btnShowPassword.FlatAppearance.BorderSize = 0
        btnShowPassword.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 252)
        btnShowPassword.Cursor = Cursors.Hand
        btnShowPassword.TabStop = False
        btnShowPassword.Size = New Size(32, 32)
        btnShowPassword.Location = New Point(passField.Width - 32 - 8, (passField.Height - 32) \ 2)
        btnShowPassword.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnShowPassword.BringToFront()
    End Sub

    ' ------------------------------------------------------------
    ' Create Account card -- same look as the login card:
    ' book icon + big title, bold labels above rounded fields with
    ' icons, eye toggles on both password fields, rounded buttons,
    ' and the tinted picture panel on the right.
    ' Controls are pulled out of the two old group boxes and laid
    ' out in two columns (names / events / tab order are unchanged).
    ' ------------------------------------------------------------
    Private Sub StyleSignupCard()
        Dim x1 As Integer = 32
        Dim x2 As Integer = x1 + SignupColW + SignupColGap
        Dim y As Integer = 116

        pnlSignup.SuspendLayout()

        ' --- header: book icon, title, subtitle ---
        Dim picBook As New PictureBox()
        picBook.Image = AppIcons.Scaled("book_open_blue", 30)
        picBook.SizeMode = PictureBoxSizeMode.CenterImage
        picBook.BackColor = Color.White
        picBook.SetBounds(30, 14, 36, 36)
        pnlSignup.Controls.Add(picBook)

        lblSignupTitle.Font = New Font("Segoe UI", 20.0F, FontStyle.Bold)
        lblSignupTitle.Location = New Point(30, 46)

        Dim lblSub As New Label()
        lblSub.AutoSize = True
        lblSub.Font = New Font("Segoe UI", 10.0F)
        lblSub.ForeColor = MutedText
        lblSub.Text = "Fill in your details to register a library account"
        lblSub.Location = New Point(33, 86)
        pnlSignup.Controls.Add(lblSub)

        ' --- row 1: ID number | Username ---
        PlaceSignupLabel(lblIdNumber, "ID number", x1, y)
        PlaceSignupLabel(lblUsername, "Username", x2, y)
        PlaceSignupText(txtIdNumber, "idcard_blue", "ID number", x1, y + SignupLabelH, 0)
        PlaceSignupText(txtUsername, "user_blue", "Username", x2, y + SignupLabelH, 1)

        ' --- row 2: Password | Confirm Password (each with its own eye) ---
        y += SignupRowPitch
        PlaceSignupLabel(lblPassword, "Password", x1, y)
        PlaceSignupLabel(lblConfirm, "Confirm Password", x2, y)
        Dim fPass As RoundedField = PlaceSignupText(txtPassword, "lock_blue", "Password", x1, y + SignupLabelH, 2, 40)
        Dim fConfirm As RoundedField = PlaceSignupText(txtConfirm, "lock_blue", "Confirm password", x2, y + SignupLabelH, 3, 40)
        _eyePass = AddEyeButton(fPass, txtPassword)
        _eyeConfirm = AddEyeButton(fConfirm, txtConfirm)

        ' --- row 3: First Name | Last Name (letters only, see key handlers) ---
        y += SignupRowPitch
        PlaceSignupLabel(lblFirstName, "First Name", x1, y)
        PlaceSignupLabel(lblLastName, "Last Name", x2, y)
        PlaceSignupText(txtFirstName, "user_blue", "First name", x1, y + SignupLabelH, 4)
        PlaceSignupText(txtLastName, "user_blue", "Last name", x2, y + SignupLabelH, 5)
        txtFirstName.MaxLength = NameMaxLength
        txtLastName.MaxLength = NameMaxLength

        ' --- row 4: Course / Department | Year / Section ---
        y += SignupRowPitch
        PlaceSignupLabel(lblCourse, "Course / Department", x1, y)
        PlaceSignupLabel(lblYearSection, "Year / Section", x2, y)

        pnlSignup.Controls.Add(cmbCourse)
        cmbCourse.Font = New Font("Segoe UI", 11.0F)
        Dim fCourse As RoundedComboField = WrapComboBox(cmbCourse, AppIcons.Scaled("course_blue", 18),
                    New Rectangle(x1, y + SignupLabelH, SignupColW, SignupFieldH), 14)
        fCourse.PlaceholderText = "Select course"
        fCourse.TabIndex = 6

        PlaceSignupText(txtYearSection, "section_blue", "Year / Section", x2, y + SignupLabelH, 7)

        ' --- row 5: Contact Number (digits only) | Account Type ---
        y += SignupRowPitch
        PlaceSignupLabel(lblContact, "Contact Number", x1, y)
        PlaceSignupText(txtContact, "phone_blue", "09XXXXXXXXX", x1, y + SignupLabelH, 8)

        Dim lblType As New Label()
        pnlSignup.Controls.Add(lblType)
        lblType.AutoSize = True
        lblType.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblType.Text = "Account Type"
        lblType.Location = New Point(x2, y)

        Dim radioTop As Integer = y + SignupLabelH + (SignupFieldH - 23) \ 2
        StyleRadio(rdoStudent, x2 + 2, radioTop, 9)
        StyleRadio(rdoTeacher, x2 + 2 + 112, radioTop, 10)

        ' --- buttons + "Already have an account?" link ---
        btnCreate.SetBounds(x1, 440, 190, 42)
        btnCreate.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnCreate.TabIndex = 11
        btnCancel.SetBounds(x1 + 190 + 12, 440, 120, 42)
        btnCancel.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnCancel.TabIndex = 12
        lnkLogin.Location = New Point(34, 496)
        lnkLogin.TabIndex = 13

        ' --- the old group boxes are empty now ---
        grpAccount.Visible = False
        grpPersonal.Visible = False
        grpAccountType.Visible = False

        ' --- right side: tinted panel with the same picture + tagline as the login ---
        Dim pnlRight As New Panel()
        pnlRight.BackColor = BrandTint
        pnlRight.Dock = DockStyle.Right
        pnlRight.Width = SignupRightW

        Dim pic As New PictureBox()
        pic.Image = PictureBox3.Image
        pic.SizeMode = PictureBoxSizeMode.Zoom
        pic.SetBounds((SignupRightW - 200) \ 2, 130, 200, 200)
        pnlRight.Controls.Add(pic)

        Dim lblTag As New Label()
        lblTag.Font = New Font("Segoe UI", 10.0F, FontStyle.Italic)
        lblTag.ForeColor = BrandDark
        lblTag.TextAlign = ContentAlignment.MiddleCenter
        lblTag.Text = lblQuality.Text
        lblTag.SetBounds(0, 345, SignupRightW, 50)
        pnlRight.Controls.Add(lblTag)

        pnlSignup.Controls.Add(pnlRight)

        pnlSignup.ResumeLayout(True)
    End Sub

    ' Bold caption above a field (same style as "Username" / "Password" on the login).
    Private Sub PlaceSignupLabel(lbl As Label, caption As String, x As Integer, y As Integer)
        pnlSignup.Controls.Add(lbl)             ' leaves its old group box
        lbl.AutoSize = True
        lbl.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lbl.Text = caption
        lbl.Location = New Point(x, y)
    End Sub

    ' Moves a TextBox out of its group box into a rounded field with an icon.
    Private Function PlaceSignupText(tb As TextBox, iconName As String, placeholder As String,
                                     x As Integer, y As Integer, tabIdx As Integer,
                                     Optional rightReserve As Integer = 0) As RoundedField
        pnlSignup.Controls.Add(tb)
        tb.Font = New Font("Segoe UI", 11.0F)
        tb.PlaceholderText = placeholder
        Dim f As RoundedField = WrapTextBox(tb, AppIcons.Scaled(iconName, 18),
                                            New Rectangle(x, y, SignupColW, SignupFieldH), 14, rightReserve)
        f.TabIndex = tabIdx
        Return f
    End Function

    Private Sub StyleRadio(rdo As RadioButton, x As Integer, y As Integer, tabIdx As Integer)
        pnlSignup.Controls.Add(rdo)
        rdo.AutoSize = True
        rdo.Font = New Font("Segoe UI", 10.0F)
        rdo.BackColor = Color.White
        rdo.Cursor = Cursors.Hand
        rdo.Location = New Point(x, y)
        rdo.TabIndex = tabIdx
    End Sub

    ' Eye icon inside a password field (same look as the login's show/hide button).
    Private Function AddEyeButton(field As RoundedField, target As TextBox) As Button
        Dim b As New Button()
        b.FlatStyle = FlatStyle.Flat
        b.FlatAppearance.BorderSize = 0
        b.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 252)
        b.FlatAppearance.MouseDownBackColor = Color.FromArgb(214, 230, 247)
        b.BackColor = Color.White
        b.Image = AppIcons.Scaled("eye_gray", 18)
        b.ImageAlign = ContentAlignment.MiddleCenter
        b.Cursor = Cursors.Hand
        b.TabStop = False
        b.Size = New Size(32, 32)
        b.Location = New Point(field.Width - 32 - 8, (field.Height - 32) \ 2)
        b.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        b.Tag = target
        AddHandler b.Click, AddressOf SignupEye_Click
        field.Controls.Add(b)
        b.BringToFront()
        Return b
    End Function

    Private Sub SignupEye_Click(sender As Object, e As EventArgs)
        Dim b As Button = DirectCast(sender, Button)
        Dim tb As TextBox = DirectCast(b.Tag, TextBox)
        SetPasswordHidden(tb, b, Not tb.UseSystemPasswordChar)
        tb.Focus()
        tb.SelectionStart = tb.TextLength
    End Sub

    ' hidden = True  -> dots + plain eye;  hidden = False -> readable text + crossed-out eye
    Private Sub SetPasswordHidden(tb As TextBox, eye As Button, hidden As Boolean)
        tb.UseSystemPasswordChar = hidden
        If eye IsNot Nothing Then
            eye.Image = AppIcons.Scaled(If(hidden, "eye_gray", "eye_off_gray"), 18)
        End If
    End Sub

    Private Sub StyleButtons()
        loginButton.Kind = ButtonKind.Primary
        loginButton.IconImage = AppIcons.Scaled("arrow_right_white", 16)
        loginButton.IconOnRight = True

        createAccountButton.Kind = ButtonKind.Outline

        btnCreate.Kind = ButtonKind.Primary
        btnCreate.IconImage = AppIcons.Scaled("arrow_right_white", 16)
        btnCreate.IconOnRight = True
        btnCancel.Kind = ButtonKind.Neutral
    End Sub

    ' ------------------------------------------------------------
    ' Background (cover-fit, no stretching) + card layout
    ' ------------------------------------------------------------
    Private Sub LoadBackground()
        Try
            Dim path As String = System.IO.Path.Combine(Application.StartupPath, "Images", "login_bg.jpg")
            If File.Exists(path) Then
                Using fs As New FileStream(path, FileMode.Open, FileAccess.Read)
                    Using src As Image = Image.FromStream(fs)
                        _bg = New Bitmap(src) ' copy so the file isn't locked
                    End Using
                End Using
            End If
        Catch
            _bg = Nothing ' fall back to the gradient below
        End Try
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Dim rect As Rectangle = Me.ClientRectangle
        If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

        If _bg Is Nothing Then
            Using br As New LinearGradientBrush(rect, Color.FromArgb(222, 240, 246), Color.FromArgb(176, 222, 224), 45.0F)
                e.Graphics.FillRectangle(br, rect)
            End Using
            Return
        End If

        ' "Cover": scale until the whole window is filled, crop the overflow.
        Dim scale As Single = Math.Max(rect.Width / CSng(_bg.Width), rect.Height / CSng(_bg.Height))
        Dim w As Integer = CInt(Math.Ceiling(_bg.Width * scale))
        Dim h As Integer = CInt(Math.Ceiling(_bg.Height * scale))
        Dim x As Integer = (rect.Width - w) \ 2
        Dim y As Integer = (rect.Height - h) \ 2
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic
        e.Graphics.DrawImage(_bg, x, y, w, h)
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        CenterCard()
        Me.Invalidate(True)
    End Sub

    ' Centers the card inside the page area (the part of the window to
    ' the right of the sidebar and below the header).
    Private Sub CenterCard()
        If pnlCard Is Nothing OrElse pnlPages Is Nothing Then Return
        Dim areaW As Integer = pnlPages.ClientSize.Width
        Dim areaH As Integer = pnlPages.ClientSize.Height
        pnlCard.Left = Math.Max(0, (areaW - pnlCard.Width) \ 2)
        pnlCard.Top = Math.Max(12, (areaH - pnlCard.Height) \ 2)
    End Sub

    ' Thin light-blue outline so the white card reads against the background.
    Private Sub pnlCard_Paint(sender As Object, e As PaintEventArgs) Handles pnlCard.Paint
        Using pen As New Pen(Color.FromArgb(190, 214, 238))
            e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1)
        End Using
    End Sub

    ' ------------------------------------------------------------
    ' Switching between Log In and Create Account (same frame)
    ' ------------------------------------------------------------
    Private Sub ShowLogin()
        If pageLibrary IsNot Nothing Then pageLibrary.Visible = False
        pnlCard.Visible = True
        SetActiveNav(navHome)
        pnlSignup.Visible = False
        pnlLogin.Visible = True
        pnlCard.Size = LoginCardSize
        Me.AcceptButton = loginButton   ' Enter = Log In
        Me.CancelButton = Nothing
        CenterCard()
        If String.IsNullOrEmpty(usernameTextBox.Text) Then
            usernameTextBox.Focus()
        Else
            passwordTextBox.Focus()
        End If
    End Sub

    Private Sub ShowSignup()
        If pageLibrary IsNot Nothing Then pageLibrary.Visible = False
        pnlCard.Visible = True
        SetActiveNav(navAccount)
        ClearSignupFields()
        pnlLogin.Visible = False
        pnlSignup.Visible = True
        pnlCard.Size = SignupCardSize
        Me.AcceptButton = btnCreate     ' Enter = Create Account
        Me.CancelButton = btnCancel     ' Esc = back to Log In
        CenterCard()
        txtIdNumber.Focus()
    End Sub

    Private Sub createAccountButton_Click(sender As Object, e As EventArgs) Handles createAccountButton.Click
        ShowSignup()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ShowLogin()
    End Sub

    ' "Already have an account? Login"
    Private Sub lnkLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkLogin.LinkClicked
        ShowLogin()
    End Sub

    ' ------------------------------------------------------------
    ' LOG IN
    ' ------------------------------------------------------------
    Private Sub btnShowPassword_Click(sender As Object, e As EventArgs) Handles btnShowPassword.Click
        passwordTextBox.UseSystemPasswordChar = Not passwordTextBox.UseSystemPasswordChar
        btnShowPassword.Image = AppIcons.Scaled(If(passwordTextBox.UseSystemPasswordChar, "eye_gray", "eye_off_gray"), 18)
        passwordTextBox.Focus()
    End Sub

    Private Sub lnkForgot_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkForgot.LinkClicked
        MessageBox.Show("Please contact the library administrator to reset your password.",
                        "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Logs the user in against tbl_members.username / tbl_members.password.
    Private Sub loginButton_Click(sender As Object, e As EventArgs) Handles loginButton.Click
        Dim username As String = usernameTextBox.Text.Trim()
        Dim password As String = passwordTextBox.Text

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Login Failed",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim dt As DataTable = DBConnection.GetDataTable(
                "SELECT member_id, full_name, username, role FROM tbl_members WHERE username = @u AND password = @p",
                New Dictionary(Of String, Object) From {
                    {"@u", username},
                    {"@p", AuthHelper.HashPassword(password)}
                })

            If dt.Rows.Count = 0 Then
                ActivityLog.WriteAs(0, username, "", "", "Failed Login", $"Failed login attempt for username ""{username}""")
                MessageBox.Show("Invalid username or password.", "Login Failed",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' --- Start the session: remember who's logged in and their role ---
            AuthHelper.CurrentMemberId = Convert.ToInt32(dt.Rows(0)("member_id"))
            AuthHelper.CurrentFullName = dt.Rows(0)("full_name").ToString()
            AuthHelper.CurrentUsername = dt.Rows(0)("username").ToString()
            AuthHelper.CurrentRole = dt.Rows(0)("role").ToString()
            ActivityLog.Write("Login", $"{AuthHelper.CurrentFullName} logged in as {AuthHelper.CurrentRole}")

            If chkRemember.Checked Then
                SaveRememberedUsername(username)
            Else
                ClearRememberedUsername()
            End If

            MessageBox.Show("Welcome, " & AuthHelper.CurrentFullName & "!", "Login Successful",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.Hide()

            ' --- Route to the right dashboard based on role ---
            If AuthHelper.IsAdmin Then
                Using dash As New main()
                    dash.ShowDialog()
                End Using
            Else
                Using dash As New frmMemberDashboard()
                    dash.ShowDialog()
                End Using
            End If

            ' Logging out (or closing the dashboard some other way) brings
            ' the user back to this frame, not out of the app.
            ActivityLog.Write("Logout", $"{AuthHelper.CurrentFullName} logged out")
            AuthHelper.ClearSession()
            passwordTextBox.Clear()
            If Not chkRemember.Checked Then usernameTextBox.Clear()
            Me.Show()
            ShowLogin()

        Catch ex As Exception
            MessageBox.Show("Could not connect to the database: " & ex.Message, "Connection Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadRememberedUsername()
        Try
            If File.Exists(RememberFile) Then
                Dim saved As String = File.ReadAllText(RememberFile).Trim()
                If saved.Length > 0 Then
                    usernameTextBox.Text = saved
                    chkRemember.Checked = True
                End If
            End If
        Catch
            ' Not critical -- just start with an empty username.
        End Try
    End Sub

    Private Sub SaveRememberedUsername(username As String)
        Try
            Directory.CreateDirectory(Path.GetDirectoryName(RememberFile))
            File.WriteAllText(RememberFile, username)
        Catch
        End Try
    End Sub

    Private Sub ClearRememberedUsername()
        Try
            If File.Exists(RememberFile) Then File.Delete(RememberFile)
        Catch
        End Try
    End Sub

    ' ------------------------------------------------------------
    ' CREATE ACCOUNT
    ' ------------------------------------------------------------
    Private Sub ClearSignupFields()
        txtIdNumber.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        txtConfirm.Clear()
        txtFirstName.Clear()
        txtLastName.Clear()
        cmbCourse.SelectedIndex = -1
        txtYearSection.Clear()
        txtContact.Clear()
        rdoStudent.Checked = False
        rdoTeacher.Checked = False
        SetPasswordHidden(txtPassword, _eyePass, True)
        SetPasswordHidden(txtConfirm, _eyeConfirm, True)
    End Sub

    ' ------------------------------------------------------------
    ' CREATE ACCOUNT -- input rules
    '   Contact Number : numbers only, exactly ContactLength digits
    '   First / Last   : letters and spaces only (no numbers / special characters)
    ' Typing is filtered live (KeyPress), pasted text is cleaned
    ' (TextChanged), and everything is checked again on Create.
    ' ------------------------------------------------------------
    Private Shared Function IsAsciiDigit(c As Char) As Boolean
        Return c >= "0"c AndAlso c <= "9"c
    End Function

    Private Shared Function IsNameChar(c As Char) As Boolean
        Return Char.IsLetter(c) OrElse c = " "c
    End Function

    ' Trim + collapse repeated spaces ("Juan   Dela  Cruz" -> "Juan Dela Cruz").
    Private Shared Function CleanName(raw As String) As String
        Return Regex.Replace(raw.Trim(), "\s+", " ")
    End Function

    Private Shared Function IsValidName(name As String) As Boolean
        Return name.Length > 0 AndAlso Regex.IsMatch(name, "^\p{L}+( \p{L}+)*$")
    End Function

    Private Sub txtContact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContact.KeyPress
        If Char.IsControl(e.KeyChar) Then Return                  ' backspace, Ctrl+V, ...
        If Not IsAsciiDigit(e.KeyChar) Then
            e.Handled = True
        ElseIf txtContact.TextLength >= ContactLength AndAlso txtContact.SelectionLength = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub NameBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFirstName.KeyPress, txtLastName.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Not IsNameChar(e.KeyChar) Then e.Handled = True
    End Sub

    ' Pasting (Ctrl+V / right-click) bypasses KeyPress, so clean the text here too.
    Private Sub txtContact_TextChanged(sender As Object, e As EventArgs) Handles txtContact.TextChanged
        KeepOnly(txtContact, AddressOf IsAsciiDigit, ContactLength)
    End Sub

    Private Sub txtFirstName_TextChanged(sender As Object, e As EventArgs) Handles txtFirstName.TextChanged
        KeepOnly(txtFirstName, AddressOf IsNameChar, NameMaxLength)
    End Sub

    Private Sub txtLastName_TextChanged(sender As Object, e As EventArgs) Handles txtLastName.TextChanged
        KeepOnly(txtLastName, AddressOf IsNameChar, NameMaxLength)
    End Sub

    Private Sub KeepOnly(tb As TextBox, allowed As Func(Of Char, Boolean), maxLen As Integer)
        If _sanitizing Then Return

        Dim sb As New System.Text.StringBuilder(tb.TextLength)
        For Each ch As Char In tb.Text
            If allowed(ch) AndAlso sb.Length < maxLen Then sb.Append(ch)
        Next
        If sb.Length = tb.TextLength Then Return                   ' nothing to remove

        _sanitizing = True
        Try
            Dim caret As Integer = tb.SelectionStart
            Dim removed As Integer = tb.TextLength - sb.Length
            tb.Text = sb.ToString()
            tb.SelectionStart = Math.Max(0, Math.Min(tb.TextLength, caret - removed))
        Finally
            _sanitizing = False
        End Try
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Dim firstName As String = CleanName(txtFirstName.Text)
        Dim lastName As String = CleanName(txtLastName.Text)
        Dim contact As String = txtContact.Text.Trim()

        ' --- Required-field validation ---
        If String.IsNullOrWhiteSpace(txtIdNumber.Text) OrElse
           String.IsNullOrWhiteSpace(txtUsername.Text) OrElse
           String.IsNullOrWhiteSpace(txtPassword.Text) OrElse
           String.IsNullOrWhiteSpace(txtConfirm.Text) OrElse
           firstName.Length = 0 OrElse
           lastName.Length = 0 OrElse
           contact.Length = 0 Then
            MessageBox.Show("Please fill out all required fields (ID number, Username, Password, First Name, Last Name, Contact Number).",
                             "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

        ' --- Names: letters only (no numbers, no special characters) ---
        If Not IsValidName(firstName) Then
            MessageBox.Show("First Name can only contain letters. Numbers and special characters are not allowed.",
                             "Invalid First Name", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFirstName.Focus()
            Return
        End If

        If Not IsValidName(lastName) Then
            MessageBox.Show("Last Name can only contain letters. Numbers and special characters are not allowed.",
                             "Invalid Last Name", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtLastName.Focus()
            Return
        End If

        If cmbCourse.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a Course / Department.", "Missing Information",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbCourse.Focus()
            Return
        End If

        ' --- Contact number: required, numbers only ---
        If Not Regex.IsMatch(contact, "^[0-9]+$") Then
            MessageBox.Show("Contact Number can only contain numbers.", "Invalid Contact Number",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContact.Focus()
            Return
        End If

        If contact.Length <> ContactLength Then
            MessageBox.Show($"Contact Number must be {ContactLength} digits (example: 09123456789).", "Invalid Contact Number",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContact.Focus()
            Return
        End If

        If Not rdoStudent.Checked AndAlso Not rdoTeacher.Checked Then
            MessageBox.Show("Please select an Account Type (Student or Teacher).", "Missing Information",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' --- Make sure tbl_members has the account_type column (older DBs don't) ---
            DBConnection.EnsureMemberColumns()

            ' --- Duplicate ID number / Username check ---
            Dim typedId As String = txtIdNumber.Text.Trim()
            Dim typedUser As String = txtUsername.Text.Trim()

            Dim existing As DataTable = DBConnection.GetDataTable(
                "SELECT id_number, username FROM tbl_members WHERE username = @u OR id_number = @id",
                New Dictionary(Of String, Object) From {
                    {"@u", typedUser},
                    {"@id", typedId}
                })

            If existing.Rows.Count > 0 Then
                ' Say exactly WHICH one is already taken, instead of guessing.
                Dim idTaken As Boolean = False
                Dim userTaken As Boolean = False
                For Each r As DataRow In existing.Rows
                    If String.Equals(r("id_number").ToString().Trim(), typedId, StringComparison.OrdinalIgnoreCase) Then idTaken = True
                    If Not IsDBNull(r("username")) AndAlso
                       String.Equals(r("username").ToString().Trim(), typedUser, StringComparison.OrdinalIgnoreCase) Then userTaken = True
                Next

                Dim dupMessage As String
                If idTaken AndAlso userTaken Then
                    dupMessage = $"The ID number ""{typedId}"" and the username ""{typedUser}"" are both already registered. Please log in instead."
                ElseIf idTaken Then
                    dupMessage = $"The ID number ""{typedId}"" is already registered to an existing account. Please log in with that account, or check that you typed your ID number correctly."
                ElseIf userTaken Then
                    dupMessage = $"The username ""{typedUser}"" is already taken. Please choose a different username."
                Else
                    dupMessage = "That ID number or Username is already registered. Please log in instead, or use a different username."
                End If

                MessageBox.Show(dupMessage, "Account Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' --- Save the new account ---
            Dim fullName As String = firstName & " " & lastName
            Dim accountType As String = If(rdoTeacher.Checked, "Teacher", "Student")

            ' Self-registration is always role 'member' -- role is never read
            ' from this form. Admin accounts are created directly in the
            ' database. Account Type (Student/Teacher) is just who they are.
            DBConnection.ExecuteNonQuery(
                "INSERT INTO tbl_members (id_number, full_name, course, year_level, contact_number, username, password, role, account_type) " &
                "VALUES (@id, @name, @course, @year, @contact, @user, @pass, 'member', @type)",
                New Dictionary(Of String, Object) From {
                    {"@id", txtIdNumber.Text.Trim()},
                    {"@name", fullName},
                    {"@course", cmbCourse.SelectedItem.ToString()},
                    {"@year", txtYearSection.Text.Trim()},
                    {"@contact", contact},
                    {"@user", txtUsername.Text.Trim()},
                    {"@pass", AuthHelper.HashPassword(txtPassword.Text)},
                    {"@type", accountType}
                })

            ActivityLog.WriteAs(0, txtUsername.Text.Trim(), fullName, "member", "Create Account", $"New {accountType} account registered: {fullName} (ID {txtIdNumber.Text.Trim()})")
            MessageBox.Show("Account created successfully! You can now log in.", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Back to Log In with the new username already filled in.
            Dim newUser As String = txtUsername.Text.Trim()
            ClearSignupFields()
            usernameTextBox.Text = newUser
            passwordTextBox.Clear()
            ShowLogin()

        Catch ex As Exception
            MessageBox.Show("Could not create the account: " & ex.Message, "Database Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
