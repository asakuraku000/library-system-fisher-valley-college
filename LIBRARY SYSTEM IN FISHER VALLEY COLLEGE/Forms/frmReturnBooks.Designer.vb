' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmReturnBooks (Designer / layout)
' Return borrowed books and calculate fines
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReturnBooks
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
        lblFineValue = New Label()
        lblFineCaption = New Label()
        lblStatusValue = New Label()
        lblStatusCaption = New Label()
        btnReturn = New Button()
        btnRefresh = New Button()
        lblSearch = New Label()
        txtSearch = New TextBox()
        btnSearch = New Button()
        dgvBorrowed = New DataGridView()
        lblSummary = New Label()
        pnlSuggestions = New Panel()
        dgvSuggestions = New DataGridView()
        pnlHeader.SuspendLayout()
        grpDetails.SuspendLayout()
        CType(dgvBorrowed, ComponentModel.ISupportInitialize).BeginInit()
        pnlSuggestions.SuspendLayout()
        CType(dgvSuggestions, ComponentModel.ISupportInitialize).BeginInit()
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
        lblSubtitle.Size = New Size(280, 15)
        lblSubtitle.TabIndex = 1
        lblSubtitle.Text = "Return borrowed books and calculate fines"
        '
        ' lblTitle
        '
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 15F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(18, 10)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(146, 28)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Return Books"
        '
        ' grpDetails
        '
        grpDetails.Controls.Add(lblFineValue)
        grpDetails.Controls.Add(lblFineCaption)
        grpDetails.Controls.Add(lblStatusValue)
        grpDetails.Controls.Add(lblStatusCaption)
        grpDetails.Controls.Add(btnReturn)
        grpDetails.Font = New Font("Segoe UI", 9F)
        grpDetails.Location = New Point(20, 80)
        grpDetails.Name = "grpDetails"
        grpDetails.Size = New Size(280, 400)
        grpDetails.TabIndex = 1
        grpDetails.TabStop = False
        grpDetails.Text = "Transaction Details"
        '
        ' lblStatusCaption
        '
        lblStatusCaption.AutoSize = True
        lblStatusCaption.Location = New Point(15, 30)
        lblStatusCaption.Name = "lblStatusCaption"
        lblStatusCaption.Size = New Size(41, 15)
        lblStatusCaption.TabIndex = 0
        lblStatusCaption.Text = "Status:"
        '
        ' lblStatusValue
        '
        lblStatusValue.AutoSize = True
        lblStatusValue.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblStatusValue.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        lblStatusValue.Location = New Point(115, 30)
        lblStatusValue.Name = "lblStatusValue"
        lblStatusValue.Size = New Size(82, 15)
        lblStatusValue.TabIndex = 1
        lblStatusValue.Text = "NO SELECTION"
        '
        ' lblFineCaption
        '
        lblFineCaption.AutoSize = True
        lblFineCaption.Location = New Point(15, 64)
        lblFineCaption.Name = "lblFineCaption"
        lblFineCaption.Size = New Size(76, 15)
        lblFineCaption.TabIndex = 2
        lblFineCaption.Text = "Fine (PHP):"
        '
        ' lblFineValue
        '
        lblFineValue.AutoSize = True
        lblFineValue.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblFineValue.ForeColor = Color.Red
        lblFineValue.Location = New Point(115, 64)
        lblFineValue.Name = "lblFineValue"
        lblFineValue.Size = New Size(31, 15)
        lblFineValue.TabIndex = 3
        lblFineValue.Text = "0.00"
        '
        ' btnReturn
        '
        btnReturn.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnReturn.FlatAppearance.BorderSize = 0
        btnReturn.FlatStyle = FlatStyle.Flat
        btnReturn.ForeColor = Color.White
        btnReturn.Location = New Point(15, 120)
        btnReturn.Name = "btnReturn"
        btnReturn.Size = New Size(250, 40)
        btnReturn.TabIndex = 4
        btnReturn.Text = "Return Selected Book"
        btnReturn.UseVisualStyleBackColor = False
        '
        ' lblSearch
        '
        lblSearch.AutoSize = True
        lblSearch.Font = New Font("Segoe UI", 9F)
        lblSearch.Location = New Point(320, 80)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(102, 15)
        lblSearch.TabIndex = 2
        lblSearch.Text = "Search Book/Name:"
        '
        ' txtSearch
        '
        txtSearch.Font = New Font("Segoe UI", 9F)
        txtSearch.Location = New Point(428, 77)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(280, 23)
        txtSearch.TabIndex = 3
        '
        ' btnSearch
        '
        btnSearch.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnSearch.FlatAppearance.BorderSize = 0
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.Font = New Font("Segoe UI", 9F)
        btnSearch.ForeColor = Color.White
        btnSearch.Location = New Point(716, 76)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(80, 26)
        btnSearch.TabIndex = 4
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = False
        '
        ' btnRefresh
        '
        btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.Font = New Font("Segoe UI", 9F)
        btnRefresh.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefresh.Location = New Point(806, 76)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(80, 26)
        btnRefresh.TabIndex = 5
        btnRefresh.Text = "Refresh"
        btnRefresh.UseVisualStyleBackColor = True
        '
        ' dgvBorrowed
        '
        dgvBorrowed.AllowUserToAddRows = False
        dgvBorrowed.AllowUserToDeleteRows = False
        dgvBorrowed.AllowUserToOrderColumns = False
        dgvBorrowed.AllowUserToResizeRows = False
        dgvBorrowed.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvBorrowed.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(CByte(245), CByte(247), CByte(250))
        dgvBorrowed.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvBorrowed.BackgroundColor = Color.White
        dgvBorrowed.BorderStyle = BorderStyle.FixedSingle
        dgvBorrowed.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        dgvBorrowed.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvBorrowed.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        dgvBorrowed.ColumnHeadersHeight = 32
        dgvBorrowed.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvBorrowed.EnableHeadersVisualStyles = False
        dgvBorrowed.GridColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        dgvBorrowed.Location = New Point(320, 114)
        dgvBorrowed.MultiSelect = False
        dgvBorrowed.Name = "dgvBorrowed"
        dgvBorrowed.ReadOnly = True
        dgvBorrowed.RowHeadersVisible = False
        dgvBorrowed.RowTemplate.Height = 26
        dgvBorrowed.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvBorrowed.Size = New Size(640, 366)
        dgvBorrowed.TabIndex = 6
        '
        ' lblSummary
        '
        lblSummary.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lblSummary.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblSummary.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        lblSummary.Location = New Point(320, 488)
        lblSummary.Name = "lblSummary"
        lblSummary.Size = New Size(620, 23)
        lblSummary.TabIndex = 7
        lblSummary.Text = "Total fines: PHP 0.00"
        lblSummary.TextAlign = ContentAlignment.MiddleLeft
        '
        ' pnlSuggestions
        '
        pnlSuggestions.BackColor = Color.White
        pnlSuggestions.BorderStyle = BorderStyle.FixedSingle
        pnlSuggestions.Controls.Add(dgvSuggestions)
        pnlSuggestions.Location = New Point(428, 101)
        pnlSuggestions.Name = "pnlSuggestions"
        pnlSuggestions.Size = New Size(458, 150)
        pnlSuggestions.TabIndex = 8
        pnlSuggestions.Visible = False
        '
        ' dgvSuggestions
        '
        dgvSuggestions.AllowUserToAddRows = False
        dgvSuggestions.AllowUserToDeleteRows = False
        dgvSuggestions.AllowUserToResizeColumns = False
        dgvSuggestions.AllowUserToResizeRows = False
        dgvSuggestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSuggestions.BackgroundColor = Color.White
        dgvSuggestions.BorderStyle = BorderStyle.None
        dgvSuggestions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        dgvSuggestions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvSuggestions.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        dgvSuggestions.ColumnHeadersHeight = 26
        dgvSuggestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvSuggestions.Dock = DockStyle.Fill
        dgvSuggestions.EnableHeadersVisualStyles = False
        dgvSuggestions.Font = New Font("Segoe UI", 9F)
        dgvSuggestions.GridColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        dgvSuggestions.MultiSelect = False
        dgvSuggestions.Name = "dgvSuggestions"
        dgvSuggestions.ReadOnly = True
        dgvSuggestions.RowHeadersVisible = False
        dgvSuggestions.RowTemplate.Height = 24
        dgvSuggestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSuggestions.TabIndex = 0
        '
        ' frmReturnBooks
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(960, 530)
        Controls.Add(lblSummary)
        Controls.Add(pnlSuggestions)
        Controls.Add(dgvBorrowed)
        Controls.Add(btnRefresh)
        Controls.Add(btnSearch)
        Controls.Add(txtSearch)
        Controls.Add(lblSearch)
        Controls.Add(grpDetails)
        Controls.Add(pnlHeader)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmReturnBooks"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fisher Valley College Library - Return Books"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        CType(dgvBorrowed, ComponentModel.ISupportInitialize).EndInit()
        pnlSuggestions.ResumeLayout(False)
        CType(dgvSuggestions, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents lblStatusCaption As Label
    Friend WithEvents lblStatusValue As Label
    Friend WithEvents lblFineCaption As Label
    Friend WithEvents lblFineValue As Label
    Friend WithEvents btnReturn As Button
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgvBorrowed As DataGridView
    Friend WithEvents lblSummary As Label
    Friend WithEvents pnlSuggestions As Panel
    Friend WithEvents dgvSuggestions As DataGridView

End Class