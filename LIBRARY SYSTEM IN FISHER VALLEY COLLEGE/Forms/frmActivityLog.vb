' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmActivityLog (admin)
' Shows what users did and WHEN: logins / logouts, borrowing and
' returning, and every add / edit / delete. Filter by role, action,
' period or search text. Refreshes itself every 10 seconds.
' Data comes from tbl_activity_log (written by ActivityLog.vb).
' ============================================================

Imports System.Data

Public Class frmActivityLog
    Inherits Form

    Private ReadOnly pnlHeader As New Panel()
    Private ReadOnly lblTitle As New Label()
    Private ReadOnly lblSubtitle As New Label()
    Private ReadOnly pnlFilters As New Panel()
    Private ReadOnly lblSearch As New Label()
    Private ReadOnly txtSearch As New TextBox()
    Private ReadOnly lblRole As New Label()
    Private ReadOnly cmbRole As New ComboBox()
    Private ReadOnly lblAction As New Label()
    Private ReadOnly cmbAction As New ComboBox()
    Private ReadOnly lblPeriod As New Label()
    Private ReadOnly cmbPeriod As New ComboBox()
    Private ReadOnly btnRefresh As New RoundedButton()
    Private ReadOnly chkAuto As New CheckBox()
    Private ReadOnly dgvLog As New DataGridView()
    Private ReadOnly pnlPresence As New Panel()
    Private ReadOnly lblPresence As New Label()
    Private ReadOnly dgvPresence As New DataGridView()
    Private ReadOnly lblSummary As New Label()
    Private ReadOnly tmrRefresh As New System.Windows.Forms.Timer()

    Public Sub New()
        MyBase.New()
        BuildLayout()
    End Sub

    Private Sub BuildLayout()
        SuspendLayout()

        Me.Text = "Fisher Valley College Library - Activity Log"
        Me.Font = New Font("Segoe UI", 9.0F)
        Me.ClientSize = New Size(960, 690)

        ' ---- header (same look as the other admin screens) ----
        pnlHeader.BackColor = Color.FromArgb(17, 78, 128)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 64

        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(18, 10)
        lblTitle.Text = "Activity Log"

        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New Font("Segoe UI", 9.0F)
        lblSubtitle.ForeColor = Color.WhiteSmoke
        lblSubtitle.Location = New Point(20, 38)
        lblSubtitle.Text = "See what users did and when - logins, borrowing, returns, edits and deletions"

        pnlHeader.Controls.Add(lblSubtitle)
        pnlHeader.Controls.Add(lblTitle)

        ' ---- filter bar ----
        pnlFilters.Dock = DockStyle.Top
        pnlFilters.Height = 52

        lblSearch.AutoSize = True
        lblSearch.Text = "Search:"
        lblSearch.Location = New Point(20, 18)

        txtSearch.Location = New Point(72, 15)
        txtSearch.Size = New Size(190, 23)

        lblRole.AutoSize = True
        lblRole.Text = "Role:"
        lblRole.Location = New Point(282, 18)

        cmbRole.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRole.Location = New Point(318, 14)
        cmbRole.Size = New Size(90, 23)
        cmbRole.Items.AddRange(New Object() {"All", "Admin", "Member"})
        cmbRole.SelectedIndex = 0

        lblAction.AutoSize = True
        lblAction.Text = "Action:"
        lblAction.Location = New Point(424, 18)

        cmbAction.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAction.Location = New Point(470, 14)
        cmbAction.Size = New Size(150, 23)
        cmbAction.Items.AddRange(New Object() {
            "All actions", "Login", "Logout", "Failed Login", "Create Account",
            "Borrow Book", "Return Book", "Book Lost",
            "Add Book", "Edit Book", "Delete Book",
            "Add Borrower", "Edit Borrower", "Delete Borrower",
            "Add User", "Edit User", "Delete User",
            "Update Profile", "Change Password", "View Page"
        })
        cmbAction.SelectedIndex = 0

        lblPeriod.AutoSize = True
        lblPeriod.Text = "Period:"
        lblPeriod.Location = New Point(636, 18)

        cmbPeriod.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPeriod.Location = New Point(684, 14)
        cmbPeriod.Size = New Size(110, 23)
        cmbPeriod.Items.AddRange(New Object() {"Today", "Last 7 days", "Last 30 days", "All time"})
        cmbPeriod.SelectedIndex = 1

        btnRefresh.Kind = ButtonKind.Primary
        btnRefresh.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnRefresh.Text = "Refresh"
        btnRefresh.Location = New Point(808, 12)
        btnRefresh.Size = New Size(80, 28)

        chkAuto.AutoSize = True
        chkAuto.Text = "Auto-refresh"
        chkAuto.Checked = True
        chkAuto.Location = New Point(900, 17)

        pnlFilters.Controls.Add(lblSearch)
        pnlFilters.Controls.Add(txtSearch)
        pnlFilters.Controls.Add(lblRole)
        pnlFilters.Controls.Add(cmbRole)
        pnlFilters.Controls.Add(lblAction)
        pnlFilters.Controls.Add(cmbAction)
        pnlFilters.Controls.Add(lblPeriod)
        pnlFilters.Controls.Add(cmbPeriod)
        pnlFilters.Controls.Add(btnRefresh)
        pnlFilters.Controls.Add(chkAuto)

        ' ---- summary + grid ----
        lblSummary.Dock = DockStyle.Bottom
        lblSummary.Height = 30
        lblSummary.TextAlign = ContentAlignment.MiddleLeft
        lblSummary.Padding = New Padding(20, 0, 0, 0)
        lblSummary.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        lblSummary.ForeColor = Color.FromArgb(17, 78, 128)

        dgvLog.Dock = DockStyle.Fill
        dgvLog.AllowUserToAddRows = False
        dgvLog.AllowUserToDeleteRows = False
        dgvLog.ReadOnly = True
        dgvLog.MultiSelect = False
        dgvLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvLog.BackgroundColor = Color.White

        ' ---- who is online / last active (like Messenger's "Active 1 min ago") ----
        lblPresence.Dock = DockStyle.Top
        lblPresence.Height = 26
        lblPresence.TextAlign = ContentAlignment.MiddleLeft
        lblPresence.Padding = New Padding(20, 0, 0, 0)
        lblPresence.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblPresence.ForeColor = Color.FromArgb(17, 78, 128)
        lblPresence.Text = "User Status - who is online and when each user was last active"

        dgvPresence.Dock = DockStyle.Fill
        dgvPresence.AllowUserToAddRows = False
        dgvPresence.AllowUserToDeleteRows = False
        dgvPresence.ReadOnly = True
        dgvPresence.MultiSelect = False
        dgvPresence.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPresence.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPresence.BackgroundColor = Color.White
        dgvPresence.RowHeadersVisible = False

        pnlPresence.Dock = DockStyle.Top
        pnlPresence.Height = 190
        pnlPresence.Controls.Add(dgvPresence)
        pnlPresence.Controls.Add(lblPresence)

        ' Fill control first, then bottom, then the top bars (docking order)
        Me.Controls.Add(dgvLog)
        Me.Controls.Add(lblSummary)
        Me.Controls.Add(pnlPresence)
        Me.Controls.Add(pnlFilters)
        Me.Controls.Add(pnlHeader)

        tmrRefresh.Interval = 10000

        AddHandler btnRefresh.Click, Sub(s, e)
                                         LoadPresence()
                                         LoadLog()
                                     End Sub
        AddHandler txtSearch.TextChanged, Sub(s, e) LoadLog()
        AddHandler cmbRole.SelectedIndexChanged, Sub(s, e) LoadLog()
        AddHandler cmbAction.SelectedIndexChanged, Sub(s, e) LoadLog()
        AddHandler cmbPeriod.SelectedIndexChanged, Sub(s, e) LoadLog()
        AddHandler chkAuto.CheckedChanged, Sub(s, e) tmrRefresh.Enabled = chkAuto.Checked
        AddHandler tmrRefresh.Tick, AddressOf OnAutoRefresh

        ResumeLayout(False)
    End Sub

    Private Sub frmActivityLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DBConnection.EnsureSchema()
        LoadPresence()
        LoadLog()
        tmrRefresh.Enabled = chkAuto.Checked
    End Sub

    Private Sub OnAutoRefresh(sender As Object, e As EventArgs)
        LoadPresence()

        ' Don't yank the list away while the admin is scrolled down reading older entries.
        If dgvLog.FirstDisplayedScrollingRowIndex > 0 Then Return
        LoadLog()
    End Sub

    ''' <summary>
    ''' Fills the User Status table: every account that can log in, with
    ''' "Active now" / "Active 1 min ago" / "Active 3 hrs ago" and the last login time.
    ''' </summary>
    Private Sub LoadPresence()
        Try
            Dim sql As String =
                "SELECT full_name AS `User`, username AS `Username`, role AS `Role`, " &
                Presence.SqlStatusColumns & " " &
                "FROM tbl_members WHERE username IS NOT NULL AND username <> '' " &
                "ORDER BY (last_active IS NULL), last_active DESC, full_name ASC"

            Dim dt As DataTable = DBConnection.GetDataTable(sql)
            Presence.AddStatusColumns(dt)
            dgvPresence.DataSource = dt

            For Each row As DataGridViewRow In dgvPresence.Rows
                Dim status As String = Convert.ToString(row.Cells("Last Active").Value)
                If status = "Active now" Then
                    row.Cells("Last Active").Style.ForeColor = Color.FromArgb(46, 125, 50)
                    row.Cells("Last Active").Style.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
                End If
            Next

            lblPresence.Text = "User Status - who is online and when each user was last active"
        Catch ex As Exception
            lblPresence.Text = "User Status - could not load: " & ex.Message
        End Try
    End Sub

    Private Sub LoadLog()
        Try
            Dim sql As String =
                "SELECT l.log_id AS `ID`, l.created_at AS `Date & Time`, " &
                "COALESCE(l.full_name, l.username, '(unknown)') AS `User`, " &
                "COALESCE(l.username, '-') AS `Username`, " &
                "COALESCE(l.role, '-') AS `Role`, " &
                "l.action AS `Action`, COALESCE(l.details, '') AS `Details` " &
                "FROM tbl_activity_log l WHERE 1 = 1"

            Dim params As New Dictionary(Of String, Object)

            Select Case cmbPeriod.SelectedIndex
                Case 0
                    sql &= " AND l.created_at >= @since"
                    params.Add("@since", DateTime.Today)
                Case 1
                    sql &= " AND l.created_at >= @since"
                    params.Add("@since", DateTime.Today.AddDays(-6))
                Case 2
                    sql &= " AND l.created_at >= @since"
                    params.Add("@since", DateTime.Today.AddDays(-29))
            End Select

            If cmbRole.SelectedIndex > 0 Then
                sql &= " AND LOWER(l.role) = @role"
                params.Add("@role", cmbRole.SelectedItem.ToString().ToLower())
            End If

            If cmbAction.SelectedIndex > 0 Then
                sql &= " AND l.action = @action"
                params.Add("@action", cmbAction.SelectedItem.ToString())
            End If

            Dim term As String = txtSearch.Text.Trim()
            If term.Length > 0 Then
                sql &= " AND (l.full_name LIKE @q OR l.username LIKE @q OR l.action LIKE @q OR l.details LIKE @q)"
                params.Add("@q", "%" & term & "%")
            End If

            sql &= " ORDER BY l.created_at DESC, l.log_id DESC LIMIT 1000"

            Dim dt As DataTable = DBConnection.GetDataTable(sql, params)
            dgvLog.DataSource = dt

            If dgvLog.Columns.Contains("ID") Then dgvLog.Columns("ID").Visible = False
            If dgvLog.Columns.Contains("Date & Time") Then
                dgvLog.Columns("Date & Time").DefaultCellStyle.Format = "MMM d, yyyy  h:mm:ss tt"
                dgvLog.Columns("Date & Time").FillWeight = 22
            End If
            If dgvLog.Columns.Contains("User") Then dgvLog.Columns("User").FillWeight = 18
            If dgvLog.Columns.Contains("Username") Then dgvLog.Columns("Username").FillWeight = 13
            If dgvLog.Columns.Contains("Role") Then dgvLog.Columns("Role").FillWeight = 9
            If dgvLog.Columns.Contains("Action") Then dgvLog.Columns("Action").FillWeight = 14
            If dgvLog.Columns.Contains("Details") Then dgvLog.Columns("Details").FillWeight = 50

            lblSummary.Text = $"{dt.Rows.Count} activit{If(dt.Rows.Count = 1, "y", "ies")} shown" &
                              If(dt.Rows.Count >= 1000, " (latest 1000)", "")
        Catch ex As Exception
            lblSummary.Text = "Could not load the activity log: " & ex.Message
        End Try
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            tmrRefresh.Stop()
            tmrRefresh.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

End Class
