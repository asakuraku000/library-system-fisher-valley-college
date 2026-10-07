' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmMemberDashboard (Logic)
'
' Member version of the admin dashboard (main.vb). Same layout:
' blue header on top, dark-blue sidebar on the left, and a content
' host where each sidebar page is embedded "inline" (no popup
' windows). The whole screen is built in code, so there is no
' big Designer file to keep in sync.
'
' Sidebar (same wording as the member menu):
'   Dashboard           -> frmMemberHome         (live data)
'   Browse Books        -> frmMemberBrowseBooks  (live data)
'   My Borrowed Books   -> frmMemberBorrowed     (live data)
'   Borrowing History   -> frmMemberHistory      (live data)
'   My Profile          -> frmMemberProfile      (view + edit)
' The page that is open stays highlighted in the sidebar.
'   Logout              -> closes this dialog; Form1 clears the
'                          session and shows the login screen again
'
' Still opened by Form1 exactly like before:
'   Using dash As New frmMemberDashboard() : dash.ShowDialog()
' so Form1.vb needs NO changes.
' ============================================================

Imports System.Data

Public Class frmMemberDashboard

    ' --- Palette (same colours the admin dashboard uses) ---------------
    Private ReadOnly HeaderBlue As Color = Color.FromArgb(10, 120, 200)
    Private ReadOnly SidebarBlue As Color = Color.FromArgb(17, 78, 128)
    Private ReadOnly SidebarHover As Color = Color.FromArgb(28, 104, 168)
    Private ReadOnly SidebarPressed As Color = Color.FromArgb(12, 62, 104)
    ' Page you're on right now: same blue as the hover colour, kept lit.
    Private ReadOnly SidebarActive As Color = Color.FromArgb(28, 104, 168)
    Private Const SidebarWidth As Integer = 210

    ' --- Layout pieces ------------------------------------------------
    Private ReadOnly pnlLeft As New Panel()
    Private ReadOnly pnlHeader As New Panel()
    Private ReadOnly pnlContentHost As New Panel()
    Private ReadOnly picLogo As New PictureBox()
    Private ReadOnly picYouTube As New PictureBox()
    Private ReadOnly picFb As New PictureBox()
    Private ReadOnly lblTitle As New Label()
    Private ReadOnly lblWelcome As New Label()

    ' --- Sidebar buttons ----------------------------------------------
    Private btnDashboard As Button
    Private btnBrowseBooks As Button
    Private btnMyBorrowed As Button
    Private btnHistory As Button
    Private btnProfile As Button
    Private btnLogOut As Button

    ' The sidebar button of the page that is open (kept highlighted).
    Private activeButton As Button

    ' Whichever page is currently embedded in pnlContentHost. Tracked so
    ' it can be disposed cleanly before another one is swapped in.
    Private currentChildForm As Form

    Public Sub New()
        InitializeComponent()
        BuildShell()
    End Sub

    ' ------------------------------------------------------------
    ' Build the header + sidebar + content host
    ' ------------------------------------------------------------
    Private Sub BuildShell()
        SuspendLayout()

        Text = "Fisher Valley College Library - Member"
        BackColor = Color.WhiteSmoke

        ' --- content host (fills whatever is left) ---
        pnlContentHost.Dock = DockStyle.Fill
        pnlContentHost.BackColor = Color.WhiteSmoke

        ' --- header ---
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 64
        pnlHeader.BackColor = HeaderBlue

        ' Same header pieces as the admin dashboard (main.Designer.vb):
        ' 80x64 logo at the far left, "Library System" title, and the two
        ' small 28x28 boxes (YouTube / Facebook) at the far right.
        picLogo.BackColor = Color.WhiteSmoke
        picLogo.Location = New Point(0, 0)
        picLogo.Size = New Size(80, 64)
        picLogo.SizeMode = PictureBoxSizeMode.StretchImage
        picLogo.TabStop = False
        picLogo.Image = LoadAdminLogo()

        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 20.0F)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(84, 12)
        lblTitle.Text = "Library System"

        picYouTube.BackColor = Color.WhiteSmoke
        picYouTube.Size = New Size(28, 28)
        picYouTube.TabStop = False

        picFb.BackColor = Color.WhiteSmoke
        picFb.Size = New Size(28, 28)
        picFb.TabStop = False

        ' "Welcome, <name>" sits just left of the two boxes, right-aligned.
        ' Its width follows the text (long names grow to the LEFT, never
        ' past the title; if there's truly no room it shows "..." instead).
        lblWelcome.AutoSize = False
        lblWelcome.AutoEllipsis = True
        lblWelcome.Height = 28
        lblWelcome.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblWelcome.ForeColor = Color.White
        lblWelcome.TextAlign = ContentAlignment.MiddleRight
        lblWelcome.Text = "Welcome, " & AuthHelper.CurrentFullName

        pnlHeader.Controls.Add(lblWelcome)
        pnlHeader.Controls.Add(picFb)
        pnlHeader.Controls.Add(picYouTube)
        pnlHeader.Controls.Add(picLogo)
        pnlHeader.Controls.Add(lblTitle)

        ' keep the two boxes pinned to the right edge (48 / 82 px from it, like admin)
        AddHandler pnlHeader.Resize, Sub(s, e) PlaceHeaderIcons()
        PlaceHeaderIcons()

        ' --- sidebar ---
        pnlLeft.Dock = DockStyle.Left
        pnlLeft.Width = SidebarWidth
        pnlLeft.BackColor = SidebarBlue

        btnDashboard = MakeNavButton("Dashboard", "side_dashboard_white", Sub(s, e) GoDashboard())
        btnBrowseBooks = MakeNavButton("Browse Books", "side_books_white", Sub(s, e) GoBrowseBooks())
        btnMyBorrowed = MakeNavButton("My Borrowed Books", "side_borrow_white", Sub(s, e) GoMyBorrowed())
        btnHistory = MakeNavButton("Borrowing History", "side_return_white", Sub(s, e) GoHistory())
        btnProfile = MakeNavButton("My Profile", "nav_account_white", Sub(s, e) GoProfile())
        btnLogOut = MakeNavButton("Logout", "side_logout_white", Sub(s, e) DoLogOut())
        btnLogOut.Dock = DockStyle.Bottom

        ' Added bottom-to-top on purpose: the LAST control added is docked
        ' FIRST, so Dashboard ends up on top. (Same trick main.Designer.vb uses.)
        pnlLeft.Controls.Add(btnLogOut)
        pnlLeft.Controls.Add(btnProfile)
        pnlLeft.Controls.Add(btnHistory)
        pnlLeft.Controls.Add(btnMyBorrowed)
        pnlLeft.Controls.Add(btnBrowseBooks)
        pnlLeft.Controls.Add(btnDashboard)

        ' Fill first, then the header, then the sidebar last (docks first,
        ' so the sidebar runs the full height like in the admin dashboard).
        Controls.Add(pnlContentHost)
        Controls.Add(pnlHeader)
        Controls.Add(pnlLeft)

        ResumeLayout(True)
    End Sub

    Private Sub PlaceHeaderIcons()
        picFb.Location = New Point(Math.Max(0, pnlHeader.ClientSize.Width - 48), 18)
        picYouTube.Location = New Point(Math.Max(0, pnlHeader.ClientSize.Width - 82), 18)

        ' Greeting: right edge sits 16 px left of the YouTube box; the width
        ' is just the text (capped so it can't run into the title).
        Dim rightEdge As Integer = picYouTube.Left - 16
        Dim leftLimit As Integer = lblTitle.Right + 20
        Dim maxWidth As Integer = Math.Max(60, rightEdge - leftLimit)
        Dim textWidth As Integer = TextRenderer.MeasureText(lblWelcome.Text, lblWelcome.Font).Width + 6
        lblWelcome.Width = Math.Min(textWidth, maxWidth)
        lblWelcome.Location = New Point(rightEdge - lblWelcome.Width, 18)
    End Sub

    ''' <summary>
    ''' Loads the same logo image the admin dashboard uses (from My.Resources).
    ''' Falls back to the built-in book icon only if it can't be loaded.
    ''' </summary>
    Private Function LoadAdminLogo() As Image
        Try
            ' exact same resource main.Designer.vb uses for PictureBoxLogo
            Return My.Resources.Resources._A0DF6743_89CC_4595_A3E3_7962AA7C6ABC_
        Catch ex As Exception
            Return AppIcons.Scaled("book_open_blue", 48)
        End Try
    End Function

    ''' <summary>
    ''' One sidebar item: icon + label, flat, left-aligned, with the same
    ''' hover / pressed colours as the admin sidebar.
    ''' </summary>
    Private Function MakeNavButton(caption As String, iconName As String, onClick As EventHandler) As Button
        Dim b As New Button()
        b.Name = "btnNav" & caption.Replace(" ", "")
        b.Text = caption
        b.Dock = DockStyle.Top
        b.Height = 48
        b.FlatStyle = FlatStyle.Flat
        b.FlatAppearance.BorderSize = 0
        b.FlatAppearance.MouseOverBackColor = SidebarHover
        b.FlatAppearance.MouseDownBackColor = SidebarPressed
        b.BackColor = SidebarBlue
        b.ForeColor = Color.White
        b.Font = New Font("Segoe UI", 9.5F)
        b.Image = AppIcons.Scaled(iconName, 20)
        b.ImageAlign = ContentAlignment.MiddleLeft
        b.TextAlign = ContentAlignment.MiddleLeft
        b.TextImageRelation = TextImageRelation.ImageBeforeText
        b.Padding = New Padding(14, 0, 0, 0)
        b.Cursor = Cursors.Hand
        b.TabStop = False
        AddHandler b.Click, onClick
        ' white strip on the left edge of the ACTIVE button
        AddHandler b.Paint,
            Sub(sender, e)
                If sender Is activeButton Then
                    Using br As New SolidBrush(Color.White)
                        e.Graphics.FillRectangle(br, 0, 0, 4, b.Height)
                    End Using
                End If
            End Sub
        Return b
    End Function

    ' ------------------------------------------------------------
    ' Startup: maximize (so it fits 1366x768 laptops too) and land on
    ' the Dashboard, just like the admin side.
    ' ------------------------------------------------------------
    Private Sub frmMemberDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WindowState = FormWindowState.Maximized
        GoDashboard()
    End Sub

    ' ------------------------------------------------------------
    ' Navigation
    ' ------------------------------------------------------------
    Private Sub GoDashboard()
        Navigate(btnDashboard, New frmMemberHome())
    End Sub

    Private Sub GoBrowseBooks()
        Navigate(btnBrowseBooks, New frmMemberBrowseBooks())
    End Sub

    Private Sub GoMyBorrowed()
        Navigate(btnMyBorrowed, New frmMemberBorrowed())
    End Sub

    Private Sub GoHistory()
        Navigate(btnHistory, New frmMemberHistory())
    End Sub

    Private Sub GoProfile()
        Navigate(btnProfile, New frmMemberProfile())
    End Sub

    Private Sub Navigate(target As Button, page As Form)
        SetActive(target)
        ShowInContent(page)
    End Sub

    ''' <summary>
    ''' Keeps the button of the open page highlighted (lit background, bold
    ''' text, white strip on the left) and puts every other one back to normal.
    ''' Hover / pressed colours still work on top of it, like the admin sidebar.
    ''' </summary>
    Private Sub SetActive(target As Button)
        activeButton = target
        For Each b As Button In New Button() {btnDashboard, btnBrowseBooks, btnMyBorrowed, btnHistory, btnProfile}
            Dim isActive As Boolean = (b Is target)
            b.BackColor = If(isActive, SidebarActive, SidebarBlue)
            b.Font = New Font("Segoe UI", 9.5F, If(isActive, FontStyle.Bold, FontStyle.Regular))
            b.Invalidate()
        Next
    End Sub

    ''' <summary>
    ''' Embeds a form "inline" inside pnlContentHost (same idea as
    ''' main.ShowInContent): strips its title bar, docks it to fill the
    ''' host, and disposes whichever page was open before.
    ''' </summary>
    Private Sub ShowInContent(f As Form)
        If currentChildForm IsNot Nothing Then
            RemoveHandler currentChildForm.FormClosed, AddressOf ChildForm_Closed
            currentChildForm.Dispose()
            currentChildForm = Nothing
        End If

        pnlContentHost.Controls.Clear()

        f.TopLevel = False
        f.FormBorderStyle = FormBorderStyle.None
        f.Dock = DockStyle.Fill

        AddHandler f.FormClosed, AddressOf ChildForm_Closed
        currentChildForm = f

        pnlContentHost.Controls.Add(f)
        f.Show()
    End Sub

    ''' <summary>
    ''' If an embedded page ever closes itself, go back to the Dashboard
    ''' instead of leaving an empty panel.
    ''' </summary>
    Private Sub ChildForm_Closed(sender As Object, e As EventArgs)
        GoDashboard()
    End Sub

    ' ------------------------------------------------------------
    ' Logout -- same behaviour as the admin dashboard: close this
    ' dialog and Form1 clears the session + shows the login screen.
    ' ------------------------------------------------------------
    Private Sub DoLogOut()
        DialogResult = DialogResult.OK
        Close()
    End Sub

End Class
