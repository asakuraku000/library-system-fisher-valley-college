' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmDashboard (Designer / layout)
'
' Admin landing screen — replaces the old removed Dashboard.
' Shows Total Books / Total Members / Currently Borrowed / Overdue
' counts as summary cards, plus a "Recently Overdue" mini-list.
' Embedded into main's PanelContentHost the same way frmBooks /
' frmOverdueBooks / frmManageUsers are (see main.vb ShowInContent).
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDashboard
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
        btnRefreshDashboard = New Button()

        pnlCardBooks = New Panel()
        lblBooksAccent = New Label()
        lblTotalBooksValue = New Label()
        lblTotalBooksCaption = New Label()

        pnlCardMembers = New Panel()
        lblMembersAccent = New Label()
        lblTotalMembersValue = New Label()
        lblTotalMembersCaption = New Label()

        pnlCardBorrowed = New Panel()
        lblBorrowedAccent = New Label()
        lblCurrentlyBorrowedValue = New Label()
        lblCurrentlyBorrowedCaption = New Label()

        pnlCardOverdue = New Panel()
        lblOverdueAccent = New Label()
        lblOverdueValue = New Label()
        lblOverdueCaption = New Label()

        lblBookStatusTitle = New Label()
        dgvBookStatus = New DataGridView()

        tabRecentActivity = New TabControl()
        tabPageOverdue = New TabPage()
        dgvRecentOverdue = New DataGridView()
        tabPageTransactions = New TabPage()
        dgvRecentTransactions = New DataGridView()

        lblLastUpdated = New Label()

        pnlHeader.SuspendLayout()
        pnlCardBooks.SuspendLayout()
        pnlCardMembers.SuspendLayout()
        pnlCardBorrowed.SuspendLayout()
        pnlCardOverdue.SuspendLayout()
        CType(dgvBookStatus, ComponentModel.ISupportInitialize).BeginInit()
        tabRecentActivity.SuspendLayout()
        tabPageOverdue.SuspendLayout()
        tabPageTransactions.SuspendLayout()
        CType(dgvRecentOverdue, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvRecentTransactions, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' pnlHeader
        '
        pnlHeader.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        pnlHeader.Controls.Add(btnRefreshDashboard)
        pnlHeader.Controls.Add(lblSubtitle)
        pnlHeader.Controls.Add(lblTitle)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New Size(1180, 64)
        pnlHeader.TabIndex = 0
        '
        ' lblTitle
        '
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 15F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(18, 10)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(120, 28)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Dashboard"
        '
        ' lblSubtitle
        '
        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New Font("Segoe UI", 9F)
        lblSubtitle.ForeColor = Color.WhiteSmoke
        lblSubtitle.Location = New Point(20, 38)
        lblSubtitle.Name = "lblSubtitle"
        lblSubtitle.Size = New Size(260, 15)
        lblSubtitle.TabIndex = 1
        lblSubtitle.Text = "Fisher Valley College Library — Admin Overview"
        '
        ' btnRefreshDashboard
        '
        btnRefreshDashboard.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRefreshDashboard.FlatAppearance.BorderColor = Color.White
        btnRefreshDashboard.FlatStyle = FlatStyle.Flat
        btnRefreshDashboard.Font = New Font("Segoe UI", 9F)
        btnRefreshDashboard.ForeColor = Color.White
        btnRefreshDashboard.Location = New Point(1050, 17)
        btnRefreshDashboard.Name = "btnRefreshDashboard"
        btnRefreshDashboard.Size = New Size(110, 30)
        btnRefreshDashboard.TabIndex = 2
        btnRefreshDashboard.Text = "Refresh"
        btnRefreshDashboard.UseVisualStyleBackColor = False
        '
        ' pnlCardBooks
        '
        pnlCardBooks.BackColor = Color.White
        pnlCardBooks.BorderStyle = BorderStyle.FixedSingle
        pnlCardBooks.Controls.Add(lblTotalBooksCaption)
        pnlCardBooks.Controls.Add(lblTotalBooksValue)
        pnlCardBooks.Controls.Add(lblBooksAccent)
        pnlCardBooks.Location = New Point(20, 84)
        pnlCardBooks.Name = "pnlCardBooks"
        pnlCardBooks.Size = New Size(260, 110)
        pnlCardBooks.TabIndex = 1
        '
        ' lblBooksAccent
        '
        lblBooksAccent.BackColor = Color.FromArgb(CByte(10), CByte(120), CByte(200))
        lblBooksAccent.Dock = DockStyle.Top
        lblBooksAccent.Location = New Point(0, 0)
        lblBooksAccent.Name = "lblBooksAccent"
        lblBooksAccent.Size = New Size(258, 6)
        lblBooksAccent.TabIndex = 0
        '
        ' lblTotalBooksValue
        '
        lblTotalBooksValue.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTotalBooksValue.ForeColor = Color.FromArgb(CByte(10), CByte(120), CByte(200))
        lblTotalBooksValue.Location = New Point(16, 20)
        lblTotalBooksValue.Name = "lblTotalBooksValue"
        lblTotalBooksValue.Size = New Size(228, 40)
        lblTotalBooksValue.TabIndex = 1
        lblTotalBooksValue.Text = "0"
        lblTotalBooksValue.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblTotalBooksCaption
        '
        lblTotalBooksCaption.Font = New Font("Segoe UI", 10F)
        lblTotalBooksCaption.ForeColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        lblTotalBooksCaption.Location = New Point(16, 66)
        lblTotalBooksCaption.Name = "lblTotalBooksCaption"
        lblTotalBooksCaption.Size = New Size(228, 28)
        lblTotalBooksCaption.TabIndex = 2
        lblTotalBooksCaption.Text = "Total Books (titles)"
        '
        ' pnlCardMembers
        '
        pnlCardMembers.BackColor = Color.White
        pnlCardMembers.BorderStyle = BorderStyle.FixedSingle
        pnlCardMembers.Controls.Add(lblTotalMembersCaption)
        pnlCardMembers.Controls.Add(lblTotalMembersValue)
        pnlCardMembers.Controls.Add(lblMembersAccent)
        pnlCardMembers.Location = New Point(304, 84)
        pnlCardMembers.Name = "pnlCardMembers"
        pnlCardMembers.Size = New Size(260, 110)
        pnlCardMembers.TabIndex = 2
        '
        ' lblMembersAccent
        '
        lblMembersAccent.BackColor = Color.FromArgb(CByte(46), CByte(125), CByte(50))
        lblMembersAccent.Dock = DockStyle.Top
        lblMembersAccent.Location = New Point(0, 0)
        lblMembersAccent.Name = "lblMembersAccent"
        lblMembersAccent.Size = New Size(258, 6)
        lblMembersAccent.TabIndex = 0
        '
        ' lblTotalMembersValue
        '
        lblTotalMembersValue.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTotalMembersValue.ForeColor = Color.FromArgb(CByte(46), CByte(125), CByte(50))
        lblTotalMembersValue.Location = New Point(16, 20)
        lblTotalMembersValue.Name = "lblTotalMembersValue"
        lblTotalMembersValue.Size = New Size(228, 40)
        lblTotalMembersValue.TabIndex = 1
        lblTotalMembersValue.Text = "0"
        lblTotalMembersValue.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblTotalMembersCaption
        '
        lblTotalMembersCaption.Font = New Font("Segoe UI", 10F)
        lblTotalMembersCaption.ForeColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        lblTotalMembersCaption.Location = New Point(16, 66)
        lblTotalMembersCaption.Name = "lblTotalMembersCaption"
        lblTotalMembersCaption.Size = New Size(228, 28)
        lblTotalMembersCaption.TabIndex = 2
        lblTotalMembersCaption.Text = "Total Members"
        '
        ' pnlCardBorrowed
        '
        pnlCardBorrowed.BackColor = Color.White
        pnlCardBorrowed.BorderStyle = BorderStyle.FixedSingle
        pnlCardBorrowed.Controls.Add(lblCurrentlyBorrowedCaption)
        pnlCardBorrowed.Controls.Add(lblCurrentlyBorrowedValue)
        pnlCardBorrowed.Controls.Add(lblBorrowedAccent)
        pnlCardBorrowed.Location = New Point(588, 84)
        pnlCardBorrowed.Name = "pnlCardBorrowed"
        pnlCardBorrowed.Size = New Size(260, 110)
        pnlCardBorrowed.TabIndex = 3
        '
        ' lblBorrowedAccent
        '
        lblBorrowedAccent.BackColor = Color.FromArgb(CByte(237), CByte(140), CByte(0))
        lblBorrowedAccent.Dock = DockStyle.Top
        lblBorrowedAccent.Location = New Point(0, 0)
        lblBorrowedAccent.Name = "lblBorrowedAccent"
        lblBorrowedAccent.Size = New Size(258, 6)
        lblBorrowedAccent.TabIndex = 0
        '
        ' lblCurrentlyBorrowedValue
        '
        lblCurrentlyBorrowedValue.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblCurrentlyBorrowedValue.ForeColor = Color.FromArgb(CByte(237), CByte(140), CByte(0))
        lblCurrentlyBorrowedValue.Location = New Point(16, 20)
        lblCurrentlyBorrowedValue.Name = "lblCurrentlyBorrowedValue"
        lblCurrentlyBorrowedValue.Size = New Size(228, 40)
        lblCurrentlyBorrowedValue.TabIndex = 1
        lblCurrentlyBorrowedValue.Text = "0"
        lblCurrentlyBorrowedValue.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblCurrentlyBorrowedCaption
        '
        lblCurrentlyBorrowedCaption.Font = New Font("Segoe UI", 10F)
        lblCurrentlyBorrowedCaption.ForeColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        lblCurrentlyBorrowedCaption.Location = New Point(16, 66)
        lblCurrentlyBorrowedCaption.Name = "lblCurrentlyBorrowedCaption"
        lblCurrentlyBorrowedCaption.Size = New Size(228, 28)
        lblCurrentlyBorrowedCaption.TabIndex = 2
        lblCurrentlyBorrowedCaption.Text = "Currently Borrowed"
        '
        ' pnlCardOverdue
        '
        pnlCardOverdue.BackColor = Color.White
        pnlCardOverdue.BorderStyle = BorderStyle.FixedSingle
        pnlCardOverdue.Controls.Add(lblOverdueCaption)
        pnlCardOverdue.Controls.Add(lblOverdueValue)
        pnlCardOverdue.Controls.Add(lblOverdueAccent)
        pnlCardOverdue.Location = New Point(872, 84)
        pnlCardOverdue.Name = "pnlCardOverdue"
        pnlCardOverdue.Size = New Size(260, 110)
        pnlCardOverdue.TabIndex = 4
        '
        ' lblOverdueAccent
        '
        lblOverdueAccent.BackColor = Color.FromArgb(CByte(178), CByte(34), CByte(34))
        lblOverdueAccent.Dock = DockStyle.Top
        lblOverdueAccent.Location = New Point(0, 0)
        lblOverdueAccent.Name = "lblOverdueAccent"
        lblOverdueAccent.Size = New Size(258, 6)
        lblOverdueAccent.TabIndex = 0
        '
        ' lblOverdueValue
        '
        lblOverdueValue.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblOverdueValue.ForeColor = Color.FromArgb(CByte(178), CByte(34), CByte(34))
        lblOverdueValue.Location = New Point(16, 20)
        lblOverdueValue.Name = "lblOverdueValue"
        lblOverdueValue.Size = New Size(228, 40)
        lblOverdueValue.TabIndex = 1
        lblOverdueValue.Text = "0"
        lblOverdueValue.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblOverdueCaption
        '
        lblOverdueCaption.Font = New Font("Segoe UI", 10F)
        lblOverdueCaption.ForeColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        lblOverdueCaption.Location = New Point(16, 66)
        lblOverdueCaption.Name = "lblOverdueCaption"
        lblOverdueCaption.Size = New Size(228, 28)
        lblOverdueCaption.TabIndex = 2
        lblOverdueCaption.Text = "Overdue Books"
        '
        ' lblBookStatusTitle
        '
        lblBookStatusTitle.AutoSize = True
        lblBookStatusTitle.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblBookStatusTitle.ForeColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        lblBookStatusTitle.Location = New Point(20, 204)
        lblBookStatusTitle.Name = "lblBookStatusTitle"
        lblBookStatusTitle.Size = New Size(150, 20)
        lblBookStatusTitle.TabIndex = 5
        lblBookStatusTitle.Text = "Book Status"
        '
        ' dgvBookStatus
        '
        dgvBookStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        dgvBookStatus.AllowUserToAddRows = False
        dgvBookStatus.AllowUserToDeleteRows = False
        dgvBookStatus.AllowUserToResizeRows = False
        dgvBookStatus.ReadOnly = True
        dgvBookStatus.RowHeadersVisible = False
        dgvBookStatus.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvBookStatus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvBookStatus.BackgroundColor = Color.White
        dgvBookStatus.BorderStyle = BorderStyle.FixedSingle
        dgvBookStatus.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        dgvBookStatus.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvBookStatus.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        dgvBookStatus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvBookStatus.EnableHeadersVisualStyles = False
        dgvBookStatus.RowTemplate.Height = 24
        dgvBookStatus.Font = New Font("Segoe UI", 9F)
        dgvBookStatus.Location = New Point(20, 228)
        dgvBookStatus.Name = "dgvBookStatus"
        dgvBookStatus.Size = New Size(1140, 104)
        dgvBookStatus.TabIndex = 6
        '
        ' tabRecentActivity
        '
        tabRecentActivity.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tabRecentActivity.Controls.Add(tabPageOverdue)
        tabRecentActivity.Controls.Add(tabPageTransactions)
        tabRecentActivity.Font = New Font("Segoe UI", 9F)
        tabRecentActivity.Location = New Point(20, 346)
        tabRecentActivity.Name = "tabRecentActivity"
        tabRecentActivity.SelectedIndex = 0
        tabRecentActivity.Size = New Size(1140, 380)
        tabRecentActivity.TabIndex = 7
        '
        ' tabPageOverdue
        '
        tabPageOverdue.Controls.Add(dgvRecentOverdue)
        tabPageOverdue.Location = New Point(4, 24)
        tabPageOverdue.Name = "tabPageOverdue"
        tabPageOverdue.Padding = New Padding(8)
        tabPageOverdue.Size = New Size(1132, 352)
        tabPageOverdue.TabIndex = 0
        tabPageOverdue.Text = "Recently Overdue (Top 5)"
        tabPageOverdue.UseVisualStyleBackColor = True
        '
        ' dgvRecentOverdue
        '
        dgvRecentOverdue.AllowUserToAddRows = False
        dgvRecentOverdue.AllowUserToDeleteRows = False
        dgvRecentOverdue.ReadOnly = True
        dgvRecentOverdue.RowHeadersVisible = False
        dgvRecentOverdue.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRecentOverdue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvRecentOverdue.Font = New Font("Segoe UI", 9F)
        dgvRecentOverdue.Dock = DockStyle.Fill
        dgvRecentOverdue.Location = New Point(8, 8)
        dgvRecentOverdue.Name = "dgvRecentOverdue"
        dgvRecentOverdue.Size = New Size(1116, 336)
        dgvRecentOverdue.TabIndex = 0
        '
        ' tabPageTransactions
        '
        tabPageTransactions.Controls.Add(dgvRecentTransactions)
        tabPageTransactions.Location = New Point(4, 24)
        tabPageTransactions.Name = "tabPageTransactions"
        tabPageTransactions.Padding = New Padding(8)
        tabPageTransactions.Size = New Size(1132, 352)
        tabPageTransactions.TabIndex = 1
        tabPageTransactions.Text = "Recent Transactions (Top 10)"
        tabPageTransactions.UseVisualStyleBackColor = True
        '
        ' dgvRecentTransactions
        '
        dgvRecentTransactions.AllowUserToAddRows = False
        dgvRecentTransactions.AllowUserToDeleteRows = False
        dgvRecentTransactions.ReadOnly = True
        dgvRecentTransactions.RowHeadersVisible = False
        dgvRecentTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRecentTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvRecentTransactions.Font = New Font("Segoe UI", 9F)
        dgvRecentTransactions.Dock = DockStyle.Fill
        dgvRecentTransactions.Location = New Point(8, 8)
        dgvRecentTransactions.Name = "dgvRecentTransactions"
        dgvRecentTransactions.Size = New Size(1116, 336)
        dgvRecentTransactions.TabIndex = 0
        '
        ' lblLastUpdated
        '
        lblLastUpdated.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblLastUpdated.AutoSize = True
        lblLastUpdated.Font = New Font("Segoe UI", 8F)
        lblLastUpdated.ForeColor = Color.FromArgb(CByte(120), CByte(120), CByte(120))
        lblLastUpdated.Location = New Point(20, 734)
        lblLastUpdated.Name = "lblLastUpdated"
        lblLastUpdated.Size = New Size(120, 15)
        lblLastUpdated.TabIndex = 8
        lblLastUpdated.Text = "Last updated: —"
        '
        ' frmDashboard
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1180, 766)
        Controls.Add(lblLastUpdated)
        Controls.Add(tabRecentActivity)
        Controls.Add(dgvBookStatus)
        Controls.Add(lblBookStatusTitle)
        Controls.Add(pnlCardOverdue)
        Controls.Add(pnlCardBorrowed)
        Controls.Add(pnlCardMembers)
        Controls.Add(pnlCardBooks)
        Controls.Add(pnlHeader)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmDashboard"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fisher Valley College Library - Dashboard"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        pnlCardBooks.ResumeLayout(False)
        pnlCardMembers.ResumeLayout(False)
        pnlCardBorrowed.ResumeLayout(False)
        pnlCardOverdue.ResumeLayout(False)
        CType(dgvBookStatus, ComponentModel.ISupportInitialize).EndInit()
        tabRecentActivity.ResumeLayout(False)
        tabPageOverdue.ResumeLayout(False)
        CType(dgvRecentOverdue, ComponentModel.ISupportInitialize).EndInit()
        tabPageTransactions.ResumeLayout(False)
        CType(dgvRecentTransactions, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents btnRefreshDashboard As Button

    Friend WithEvents pnlCardBooks As Panel
    Friend WithEvents lblBooksAccent As Label
    Friend WithEvents lblTotalBooksValue As Label
    Friend WithEvents lblTotalBooksCaption As Label

    Friend WithEvents pnlCardMembers As Panel
    Friend WithEvents lblMembersAccent As Label
    Friend WithEvents lblTotalMembersValue As Label
    Friend WithEvents lblTotalMembersCaption As Label

    Friend WithEvents pnlCardBorrowed As Panel
    Friend WithEvents lblBorrowedAccent As Label
    Friend WithEvents lblCurrentlyBorrowedValue As Label
    Friend WithEvents lblCurrentlyBorrowedCaption As Label

    Friend WithEvents pnlCardOverdue As Panel
    Friend WithEvents lblOverdueAccent As Label
    Friend WithEvents lblOverdueValue As Label
    Friend WithEvents lblOverdueCaption As Label

    Friend WithEvents lblBookStatusTitle As Label
    Friend WithEvents dgvBookStatus As DataGridView

    Friend WithEvents tabRecentActivity As TabControl
    Friend WithEvents tabPageOverdue As TabPage
    Friend WithEvents dgvRecentOverdue As DataGridView
    Friend WithEvents tabPageTransactions As TabPage
    Friend WithEvents dgvRecentTransactions As DataGridView

    Friend WithEvents lblLastUpdated As Label
End Class
