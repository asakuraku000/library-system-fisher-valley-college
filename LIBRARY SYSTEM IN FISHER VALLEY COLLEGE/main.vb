Public Class main

    ' Whichever screen (frmBooks / frmOverdueBooks / frmManageUsers) is
    ' currently embedded in PanelContentHost, if any. Tracked so we can
    ' dispose it cleanly before swapping in a different one.
    Private currentChildForm As Form

    ' "Welcome Admin!" in the blue header, right-aligned just left of the
    ' two small boxes. Created in code so main.Designer.vb stays untouched.
    Private ReadOnly lblWelcome As New Label()

    ' This form was designed on a 1506x740 canvas, which doesn't fit on
    ' a lot of screens (e.g. 1366x768 laptops). Since PanelLeft/PanelHeader/
    ' SplitContainer1/PanelContentHost are all Docked, maximizing here makes
    ' the whole layout reflow to fill whatever screen it's actually run on --
    ' this is what was causing the dashboard content to be pushed off-screen.
    '
    ' Admins now land on the Dashboard (frmDashboard) immediately after
    ' login, showing Total Books / Total Members / Currently Borrowed /
    ' Overdue counts at a glance instead of jumping straight into Book
    ' Management with no overview at all.
    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        SetupWelcome()
        StyleSidebarIcons()
        ShowInContent(New frmDashboard())
    End Sub

    Private Sub SetupWelcome()
        lblWelcome.AutoSize = False
        lblWelcome.AutoEllipsis = True
        lblWelcome.Height = 28
        lblWelcome.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblWelcome.ForeColor = Color.White
        lblWelcome.BackColor = Color.Transparent
        lblWelcome.TextAlign = ContentAlignment.MiddleRight
        lblWelcome.Text = "Welcome Admin!"
        PanelHeader.Controls.Add(lblWelcome)

        AddHandler PanelHeader.Resize, Sub(s, ev) PlaceWelcome()
        ' the boxes are anchored to the right edge, so re-place after THEY move too
        AddHandler PictureBoxYouTube.LocationChanged, Sub(s, ev) PlaceWelcome()
        PlaceWelcome()
    End Sub

    Private Sub PlaceWelcome()
        Dim rightEdge As Integer = PictureBoxYouTube.Left - 16
        Dim leftLimit As Integer = LabelTitle.Right + 20
        Dim maxWidth As Integer = Math.Max(60, rightEdge - leftLimit)
        Dim textWidth As Integer = TextRenderer.MeasureText(lblWelcome.Text, lblWelcome.Font).Width + 6
        lblWelcome.Width = Math.Min(textWidth, maxWidth)
        lblWelcome.Location = New Point(rightEdge - lblWelcome.Width, 18)
    End Sub

    ' Adds an icon in front of each sidebar label (icons are embedded in
    ' the .exe -- see Modules\AppIcons.vb -- so this works offline).
    Private Sub StyleSidebarIcons()
        Dim map As New List(Of KeyValuePair(Of Button, String)) From {
            New KeyValuePair(Of Button, String)(ButtonDashboard, "side_dashboard_white"),
            New KeyValuePair(Of Button, String)(ButtonBooks, "side_books_white"),
            New KeyValuePair(Of Button, String)(ButtonBorrower, "side_borrower_white"),
            New KeyValuePair(Of Button, String)(ButtonBorrowBooks, "side_borrow_white"),
            New KeyValuePair(Of Button, String)(ButtonReturnBooks, "side_return_white"),
            New KeyValuePair(Of Button, String)(ButtonOverdues, "side_overdue_white"),
            New KeyValuePair(Of Button, String)(ButtonManageUsers, "side_manage_white"),
            New KeyValuePair(Of Button, String)(ButtonReports, "side_reports_white"),
            New KeyValuePair(Of Button, String)(ButtonLogOut, "side_logout_white")
        }

        For Each item In map
            Dim b As Button = item.Key
            b.Image = AppIcons.Scaled(item.Value, 20)
            b.ImageAlign = ContentAlignment.MiddleLeft
            b.TextAlign = ContentAlignment.MiddleLeft
            b.TextImageRelation = TextImageRelation.ImageBeforeText
            b.Padding = New Padding(14, 0, 0, 0)
            b.Font = New Font("Segoe UI", 9.5F)
            b.Cursor = Cursors.Hand
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(28, 104, 168)
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(12, 62, 104)
        Next
    End Sub

    Private Sub PanelReportOptions_Paint(sender As Object, e As PaintEventArgs) Handles PanelReportOptions.Paint

    End Sub

    ''' <summary>
    ''' Embeds a form "inline" inside PanelContentHost instead of opening it
    ''' as its own popup window: strips its title bar/border, docks it to
    ''' fill the host panel, and hides the legacy SplitContainer1 view
    ''' (kept in the designer but no longer shown anywhere) while it's
    ''' showing. Whichever form was embedded before gets disposed first.
    ''' Works with any of the existing popup forms (frmBooks,
    ''' frmOverdueBooks, frmManageUsers, ...) unchanged. Navigating away is
    ''' handled entirely by the sidebar (there's no per-form "Close"
    ''' button anymore); see ShowDefaultView below for what's shown if an
    ''' embedded screen ever closes itself.
    ''' </summary>
    Private Sub ShowInContent(f As Form)
        If currentChildForm IsNot Nothing Then
            RemoveHandler currentChildForm.FormClosed, AddressOf ChildForm_Closed
            currentChildForm.Dispose()
            currentChildForm = Nothing
        End If

        PanelContentHost.Controls.Clear()

        f.TopLevel = False
        f.FormBorderStyle = FormBorderStyle.None
        f.Dock = DockStyle.Fill

        AddHandler f.FormClosed, AddressOf ChildForm_Closed
        currentChildForm = f

        PanelContentHost.Controls.Add(f)
        SplitContainer1.Visible = False
        PanelContentHost.Visible = True
        PanelContentHost.BringToFront()
        f.Show()
    End Sub

    ''' <summary>
    ''' Returns to the default landing screen (Dashboard / frmDashboard),
    ''' disposing whichever screen was embedded in PanelContentHost, if any.
    ''' This is what's shown whenever an embedded screen fires its own
    ''' FormClosed (see ChildForm_Closed) instead of leaving an empty panel.
    ''' </summary>
    Private Sub ShowDefaultView()
        ShowInContent(New frmDashboard())
    End Sub

    ' Opens the new Dashboard screen (Total Books / Total Members /
    ' Currently Borrowed / Overdue counts) from the sidebar button.
    Private Sub ButtonDashboard_Click(sender As Object, e As EventArgs) Handles ButtonDashboard.Click
        ShowInContent(New frmDashboard())
    End Sub

    ''' <summary>
    ''' Fires if an embedded form ever closes itself. Returns to the
    ''' default view instead of leaving an empty panel.
    ''' </summary>
    Private Sub ChildForm_Closed(sender As Object, e As EventArgs)
        ShowDefaultView()
    End Sub

    ' Added by Kahleb's part: opens the Overdue Books screen from the
    ' dashboard's existing "Overdues Books" sidebar button.
    Private Sub ButtonOverdues_Click(sender As Object, e As EventArgs) Handles ButtonOverdues.Click
        ShowInContent(New frmOverdueBooks())
    End Sub

    ' Opens the Books catalog (Add/Update/Delete/Search) from the
    ' dashboard's existing "Books" sidebar button.
    Private Sub ButtonBooks_Click(sender As Object, e As EventArgs) Handles ButtonBooks.Click
        ShowInContent(New frmBooks())
    End Sub

    ' Opens Borrower profiles (Add/Update/Delete/Search) from the
    ' dashboard's existing "Borrower" sidebar button. Same embedding
    ' pattern as Books/Overdues/Manage Users, and the same Add/Update/
    ' Delete/Search layout as frmBooks, but backed by tbl_members'
    ' student/faculty profile fields instead of the book catalog.
    Private Sub ButtonBorrower_Click(sender As Object, e As EventArgs) Handles ButtonBorrower.Click
        ShowInContent(New frmBorrower())
    End Sub

    ' Opens Manage Users (view/search accounts, change role, add or
    ' remove accounts — including creating additional admin accounts)
    ' from the dashboard's existing "Manage Users" sidebar button.
    Private Sub ButtonManageUsers_Click(sender As Object, e As EventArgs) Handles ButtonManageUsers.Click
        ShowInContent(New frmManageUsers())
    End Sub

    ' Opens Borrow Books screen from the sidebar button.
    Private Sub ButtonBorrowBooks_Click(sender As Object, e As EventArgs) Handles ButtonBorrowBooks.Click
        ShowInContent(New frmBorrowBooks())
    End Sub

    ' Opens Return Books screen from the sidebar button.
    Private Sub ButtonReturnBooks_Click(sender As Object, e As EventArgs) Handles ButtonReturnBooks.Click
        ShowInContent(New frmReturnBooks())
    End Sub

    ' Opens the Reports screen (Book Inventory / Borrowing / Return /
    ' Overdue / Member / Most Borrowed / Books by Category, with CSV
    ' export and print) from the sidebar's "Reports" button -- this used
    ' to be a dead button with no click handler at all.
    Private Sub ButtonReports_Click(sender As Object, e As EventArgs) Handles ButtonReports.Click
        ShowInContent(New frmReports())
    End Sub

    ' Logs the admin/librarian out. Closes this dashboard dialog, which
    ' sends control back to Form1's loginButton_Click -- it clears the
    ' session and re-shows the login screen.
    Private Sub ButtonLogOut_Click(sender As Object, e As EventArgs) Handles ButtonLogOut.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class
