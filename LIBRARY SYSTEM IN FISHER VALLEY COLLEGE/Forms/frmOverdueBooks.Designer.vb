' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmOverdueBooks (Designer / layout)
' Part: Kahleb — Overdue Books
'
' NOTE (fix): this file previously skipped the .Name property on
' every control and the <DesignerGenerated()> attribute that the
' Windows Forms Designer needs to serialize/re-open a form. That
' mismatch is what throws "Object reference not set to an instance
' of an object" the moment you double-click frmOverdueBooks in
' Solution Explorer to open it in the visual designer. Every
' control below now has a real .Name (matching main.Designer.vb /
' createaccount.Designer.vb), so the designer can load it normally.
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOverdueBooks
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
        lblSearch = New Label()
        txtSearch = New TextBox()
        btnSearch = New Button()
        btnRefresh = New Button()
        dgvOverdue = New DataGridView()
        lblSummary = New Label()
        btnMarkReturned = New Button()
        pnlHeader.SuspendLayout()
        CType(dgvOverdue, ComponentModel.ISupportInitialize).BeginInit()
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
        pnlHeader.Size = New Size(900, 64)
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
        lblTitle.Size = New Size(160, 28)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Overdue Books"
        '
        ' lblSearch
        '
        lblSearch.AutoSize = True
        lblSearch.Font = New Font("Segoe UI", 9F)
        lblSearch.Location = New Point(20, 80)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(133, 15)
        lblSearch.TabIndex = 1
        lblSearch.Text = "Search borrower/book:"
        '
        ' txtSearch
        '
        txtSearch.Font = New Font("Segoe UI", 9F)
        txtSearch.Location = New Point(160, 76)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(260, 23)
        txtSearch.TabIndex = 2
        '
        ' btnSearch
        '
        btnSearch.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnSearch.FlatAppearance.BorderSize = 0
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.Font = New Font("Segoe UI", 9F)
        btnSearch.ForeColor = Color.White
        btnSearch.Location = New Point(430, 75)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(90, 26)
        btnSearch.TabIndex = 3
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = False
        '
        ' btnRefresh
        '
        btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.Font = New Font("Segoe UI", 9F)
        btnRefresh.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefresh.Location = New Point(530, 75)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(90, 26)
        btnRefresh.TabIndex = 4
        btnRefresh.Text = "Refresh"
        btnRefresh.UseVisualStyleBackColor = True
        '
        ' dgvOverdue
        '
        dgvOverdue.AllowUserToAddRows = False
        dgvOverdue.AllowUserToDeleteRows = False
        dgvOverdue.AllowUserToResizeRows = False
        dgvOverdue.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(CByte(245), CByte(247), CByte(250))
        dgvOverdue.BackgroundColor = Color.White
        dgvOverdue.BorderStyle = BorderStyle.FixedSingle
        dgvOverdue.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        dgvOverdue.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvOverdue.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        dgvOverdue.ColumnHeadersHeight = 32
        dgvOverdue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvOverdue.EnableHeadersVisualStyles = False
        dgvOverdue.GridColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        dgvOverdue.AllowUserToOrderColumns = False
        dgvOverdue.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvOverdue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvOverdue.Location = New Point(20, 114)
        dgvOverdue.MultiSelect = False
        dgvOverdue.Name = "dgvOverdue"
        dgvOverdue.ReadOnly = True
        dgvOverdue.RowHeadersVisible = False
        dgvOverdue.RowTemplate.Height = 26
        dgvOverdue.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOverdue.Size = New Size(860, 320)
        dgvOverdue.TabIndex = 5
        '
        ' lblSummary
        '
        lblSummary.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblSummary.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblSummary.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        lblSummary.Location = New Point(20, 446)
        lblSummary.Name = "lblSummary"
        lblSummary.Size = New Size(560, 23)
        lblSummary.TabIndex = 6
        lblSummary.Text = "Total overdue: 0 | Total fines: PHP 0.00"
        lblSummary.TextAlign = ContentAlignment.MiddleLeft
        '
        ' btnMarkReturned
        '
        btnMarkReturned.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnMarkReturned.BackColor = Color.FromArgb(CByte(46), CByte(125), CByte(50))
        btnMarkReturned.FlatAppearance.BorderSize = 0
        btnMarkReturned.FlatStyle = FlatStyle.Flat
        btnMarkReturned.Font = New Font("Segoe UI", 9F)
        btnMarkReturned.ForeColor = Color.White
        btnMarkReturned.Location = New Point(600, 441)
        btnMarkReturned.Name = "btnMarkReturned"
        btnMarkReturned.Size = New Size(150, 32)
        btnMarkReturned.TabIndex = 7
        btnMarkReturned.Text = "Mark as Returned"
        btnMarkReturned.UseVisualStyleBackColor = False
        '
        ' frmOverdueBooks
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(900, 493)
        Controls.Add(dgvOverdue)
        Controls.Add(btnRefresh)
        Controls.Add(btnSearch)
        Controls.Add(txtSearch)
        Controls.Add(lblSearch)
        Controls.Add(lblSummary)
        Controls.Add(btnMarkReturned)
        Controls.Add(pnlHeader)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmOverdueBooks"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fisher Valley College Library - Overdue Books"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        CType(dgvOverdue, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgvOverdue As DataGridView
    Friend WithEvents lblSummary As Label
    Friend WithEvents btnMarkReturned As Button
End Class
