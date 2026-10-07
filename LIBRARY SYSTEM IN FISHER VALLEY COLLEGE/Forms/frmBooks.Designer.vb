' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmBooks (Designer / layout)
' Books catalog — Add / Update / Delete / Search
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBooks
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
        btnClear = New Button()
        btnDelete = New Button()
        btnUpdate = New Button()
        btnAdd = New Button()
        lblAvailableValue = New Label()
        lblAvailableCaption = New Label()
        numCopiesTotal = New NumericUpDown()
        lblCopiesTotal = New Label()
        lblStatus = New Label()
        cmbStatus = New ComboBox()
        lblCategory = New Label()
        cmbCategory = New ComboBox()
        lblYearPublished = New Label()
        txtYearPublished = New TextBox()
        lblPublisher = New Label()
        txtPublisher = New TextBox()
        lblAuthor = New Label()
        txtAuthor = New TextBox()
        lblTitleField = New Label()
        txtTitle = New TextBox()
        lblIsbn = New Label()
        txtIsbn = New TextBox()
        lblAccession = New Label()
        txtAccession = New TextBox()
        lblSearch = New Label()
        txtSearch = New TextBox()
        btnSearchBooks = New Button()
        btnRefreshBooks = New Button()
        dgvBooks = New DataGridView()
        lblBooksSummary = New Label()
        pnlHeader.SuspendLayout()
        grpDetails.SuspendLayout()
        CType(numCopiesTotal, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvBooks, ComponentModel.ISupportInitialize).BeginInit()
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
        lblTitle.Text = "Books Management"
        '
        ' grpDetails
        '
        grpDetails.Controls.Add(btnClear)
        grpDetails.Controls.Add(btnDelete)
        grpDetails.Controls.Add(btnUpdate)
        grpDetails.Controls.Add(btnAdd)
        grpDetails.Controls.Add(lblAvailableValue)
        grpDetails.Controls.Add(lblAvailableCaption)
        grpDetails.Controls.Add(numCopiesTotal)
        grpDetails.Controls.Add(lblCopiesTotal)
        grpDetails.Controls.Add(cmbStatus)
        grpDetails.Controls.Add(lblStatus)
        grpDetails.Controls.Add(cmbCategory)
        grpDetails.Controls.Add(lblCategory)
        grpDetails.Controls.Add(txtYearPublished)
        grpDetails.Controls.Add(lblYearPublished)
        grpDetails.Controls.Add(txtPublisher)
        grpDetails.Controls.Add(lblPublisher)
        grpDetails.Controls.Add(txtAuthor)
        grpDetails.Controls.Add(lblAuthor)
        grpDetails.Controls.Add(txtTitle)
        grpDetails.Controls.Add(lblTitleField)
        grpDetails.Controls.Add(txtIsbn)
        grpDetails.Controls.Add(lblIsbn)
        grpDetails.Controls.Add(txtAccession)
        grpDetails.Controls.Add(lblAccession)
        grpDetails.Font = New Font("Segoe UI", 9F)
        grpDetails.Location = New Point(20, 80)
        grpDetails.Name = "grpDetails"
        grpDetails.Size = New Size(280, 480)
        grpDetails.TabIndex = 1
        grpDetails.TabStop = False
        grpDetails.Text = "Book Details"
        '
        ' lblAccession
        '
        lblAccession.AutoSize = True
        lblAccession.Location = New Point(15, 30)
        lblAccession.Name = "lblAccession"
        lblAccession.Size = New Size(87, 15)
        lblAccession.TabIndex = 0
        lblAccession.Text = "Accession No.:"
        '
        ' txtAccession
        '
        txtAccession.Location = New Point(115, 27)
        txtAccession.Name = "txtAccession"
        txtAccession.Size = New Size(150, 23)
        txtAccession.TabIndex = 1
        '
        ' lblIsbn
        '
        lblIsbn.AutoSize = True
        lblIsbn.Location = New Point(15, 64)
        lblIsbn.Name = "lblIsbn"
        lblIsbn.Size = New Size(37, 15)
        lblIsbn.TabIndex = 2
        lblIsbn.Text = "ISBN:"
        '
        ' txtIsbn
        '
        txtIsbn.Location = New Point(115, 61)
        txtIsbn.Name = "txtIsbn"
        txtIsbn.Size = New Size(150, 23)
        txtIsbn.TabIndex = 3
        '
        ' lblTitleField
        '
        lblTitleField.AutoSize = True
        lblTitleField.Location = New Point(15, 98)
        lblTitleField.Name = "lblTitleField"
        lblTitleField.Size = New Size(32, 15)
        lblTitleField.TabIndex = 4
        lblTitleField.Text = "Title:"
        '
        ' txtTitle
        '
        txtTitle.Location = New Point(115, 95)
        txtTitle.Name = "txtTitle"
        txtTitle.Size = New Size(150, 23)
        txtTitle.TabIndex = 5
        '
        ' lblAuthor
        '
        lblAuthor.AutoSize = True
        lblAuthor.Location = New Point(15, 132)
        lblAuthor.Name = "lblAuthor"
        lblAuthor.Size = New Size(45, 15)
        lblAuthor.TabIndex = 6
        lblAuthor.Text = "Author:"
        '
        ' txtAuthor
        '
        txtAuthor.Location = New Point(115, 129)
        txtAuthor.Name = "txtAuthor"
        txtAuthor.Size = New Size(150, 23)
        txtAuthor.TabIndex = 7
        '
        ' lblPublisher
        '
        lblPublisher.AutoSize = True
        lblPublisher.Location = New Point(15, 166)
        lblPublisher.Name = "lblPublisher"
        lblPublisher.Size = New Size(60, 15)
        lblPublisher.TabIndex = 8
        lblPublisher.Text = "Publisher:"
        '
        ' txtPublisher
        '
        txtPublisher.Location = New Point(115, 163)
        txtPublisher.Name = "txtPublisher"
        txtPublisher.Size = New Size(150, 23)
        txtPublisher.TabIndex = 9
        '
        ' lblYearPublished
        '
        lblYearPublished.AutoSize = True
        lblYearPublished.Location = New Point(15, 200)
        lblYearPublished.Name = "lblYearPublished"
        lblYearPublished.Size = New Size(97, 15)
        lblYearPublished.TabIndex = 10
        lblYearPublished.Text = "Year Published:"
        '
        ' txtYearPublished
        '
        txtYearPublished.Location = New Point(115, 197)
        txtYearPublished.MaxLength = 4
        txtYearPublished.Name = "txtYearPublished"
        txtYearPublished.Size = New Size(150, 23)
        txtYearPublished.TabIndex = 11
        '
        ' lblCategory
        '
        lblCategory.AutoSize = True
        lblCategory.Location = New Point(15, 234)
        lblCategory.Name = "lblCategory"
        lblCategory.Size = New Size(58, 15)
        lblCategory.TabIndex = 12
        lblCategory.Text = "Category:"
        '
        ' cmbCategory
        '
        cmbCategory.DropDownStyle = ComboBoxStyle.DropDown
        cmbCategory.Location = New Point(115, 231)
        cmbCategory.Name = "cmbCategory"
        cmbCategory.Size = New Size(150, 23)
        cmbCategory.TabIndex = 13
        '
        ' lblStatus
        '
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(15, 268)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(46, 15)
        lblStatus.TabIndex = 14
        lblStatus.Text = "Status:"
        '
        ' cmbStatus
        '
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus.Location = New Point(115, 265)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(150, 23)
        cmbStatus.TabIndex = 15
        '
        ' lblCopiesTotal
        '
        lblCopiesTotal.AutoSize = True
        lblCopiesTotal.Location = New Point(15, 302)
        lblCopiesTotal.Name = "lblCopiesTotal"
        lblCopiesTotal.Size = New Size(76, 15)
        lblCopiesTotal.TabIndex = 16
        lblCopiesTotal.Text = "Total Copies:"
        '
        ' numCopiesTotal
        '
        numCopiesTotal.Location = New Point(115, 299)
        numCopiesTotal.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        numCopiesTotal.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        numCopiesTotal.Name = "numCopiesTotal"
        numCopiesTotal.Size = New Size(80, 23)
        numCopiesTotal.TabIndex = 17
        numCopiesTotal.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        ' lblAvailableCaption
        '
        lblAvailableCaption.AutoSize = True
        lblAvailableCaption.Location = New Point(15, 336)
        lblAvailableCaption.Name = "lblAvailableCaption"
        lblAvailableCaption.Size = New Size(59, 15)
        lblAvailableCaption.TabIndex = 18
        lblAvailableCaption.Text = "Available:"
        '
        ' lblAvailableValue
        '
        lblAvailableValue.AutoSize = True
        lblAvailableValue.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblAvailableValue.Location = New Point(115, 336)
        lblAvailableValue.Name = "lblAvailableValue"
        lblAvailableValue.Size = New Size(14, 15)
        lblAvailableValue.TabIndex = 19
        lblAvailableValue.Text = "0"
        '
        ' btnAdd
        '
        btnAdd.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.ForeColor = Color.White
        btnAdd.Location = New Point(15, 382)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(120, 32)
        btnAdd.TabIndex = 20
        btnAdd.Text = "Add Book"
        btnAdd.UseVisualStyleBackColor = False
        '
        ' btnUpdate
        '
        btnUpdate.BackColor = Color.FromArgb(CByte(46), CByte(125), CByte(50))
        btnUpdate.FlatAppearance.BorderSize = 0
        btnUpdate.FlatStyle = FlatStyle.Flat
        btnUpdate.ForeColor = Color.White
        btnUpdate.Location = New Point(145, 382)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(120, 32)
        btnUpdate.TabIndex = 21
        btnUpdate.Text = "Update"
        btnUpdate.UseVisualStyleBackColor = False
        '
        ' btnDelete
        '
        btnDelete.BackColor = Color.FromArgb(CByte(178), CByte(34), CByte(34))
        btnDelete.FlatAppearance.BorderSize = 0
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.ForeColor = Color.White
        btnDelete.Location = New Point(15, 422)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(120, 32)
        btnDelete.TabIndex = 22
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = False
        '
        ' btnClear
        '
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.Location = New Point(145, 422)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(120, 32)
        btnClear.TabIndex = 23
        btnClear.Text = "Clear Form"
        btnClear.UseVisualStyleBackColor = True
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
        ' btnSearchBooks
        '
        btnSearchBooks.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnSearchBooks.FlatAppearance.BorderSize = 0
        btnSearchBooks.FlatStyle = FlatStyle.Flat
        btnSearchBooks.Font = New Font("Segoe UI", 9F)
        btnSearchBooks.ForeColor = Color.White
        btnSearchBooks.Location = New Point(688, 76)
        btnSearchBooks.Name = "btnSearchBooks"
        btnSearchBooks.Size = New Size(80, 26)
        btnSearchBooks.TabIndex = 4
        btnSearchBooks.Text = "Search"
        btnSearchBooks.UseVisualStyleBackColor = False
        '
        ' btnRefreshBooks
        '
        btnRefreshBooks.FlatAppearance.BorderColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefreshBooks.FlatStyle = FlatStyle.Flat
        btnRefreshBooks.Font = New Font("Segoe UI", 9F)
        btnRefreshBooks.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefreshBooks.Location = New Point(778, 76)
        btnRefreshBooks.Name = "btnRefreshBooks"
        btnRefreshBooks.Size = New Size(80, 26)
        btnRefreshBooks.TabIndex = 5
        btnRefreshBooks.Text = "Refresh"
        btnRefreshBooks.UseVisualStyleBackColor = True
        '
        ' dgvBooks
        '
        dgvBooks.AllowUserToAddRows = False
        dgvBooks.AllowUserToDeleteRows = False
        dgvBooks.AllowUserToOrderColumns = False
        dgvBooks.AllowUserToResizeRows = False
        dgvBooks.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvBooks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(CByte(245), CByte(247), CByte(250))
        dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvBooks.BackgroundColor = Color.White
        dgvBooks.BorderStyle = BorderStyle.FixedSingle
        dgvBooks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        dgvBooks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvBooks.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        dgvBooks.ColumnHeadersHeight = 32
        dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvBooks.EnableHeadersVisualStyles = False
        dgvBooks.GridColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        dgvBooks.Location = New Point(320, 114)
        dgvBooks.MultiSelect = False
        dgvBooks.Name = "dgvBooks"
        dgvBooks.ReadOnly = True
        dgvBooks.RowHeadersVisible = False
        dgvBooks.RowTemplate.Height = 26
        dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvBooks.Size = New Size(620, 446)
        dgvBooks.TabIndex = 6
        '
        ' lblBooksSummary
        '
        lblBooksSummary.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblBooksSummary.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblBooksSummary.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        lblBooksSummary.Location = New Point(320, 568)
        lblBooksSummary.Name = "lblBooksSummary"
        lblBooksSummary.Size = New Size(500, 23)
        lblBooksSummary.TabIndex = 7
        lblBooksSummary.Text = "Total titles: 0 | Total copies: 0"
        lblBooksSummary.TextAlign = ContentAlignment.MiddleLeft
        '
        ' frmBooks
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(960, 610)
        Controls.Add(lblBooksSummary)
        Controls.Add(dgvBooks)
        Controls.Add(btnRefreshBooks)
        Controls.Add(btnSearchBooks)
        Controls.Add(txtSearch)
        Controls.Add(lblSearch)
        Controls.Add(grpDetails)
        Controls.Add(pnlHeader)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmBooks"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fisher Valley College Library - Books"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        CType(numCopiesTotal, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvBooks, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents lblAccession As Label
    Friend WithEvents txtAccession As TextBox
    Friend WithEvents lblIsbn As Label
    Friend WithEvents txtIsbn As TextBox
    Friend WithEvents lblTitleField As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents lblAuthor As Label
    Friend WithEvents txtAuthor As TextBox
    Friend WithEvents lblPublisher As Label
    Friend WithEvents txtPublisher As TextBox
    Friend WithEvents lblYearPublished As Label
    Friend WithEvents txtYearPublished As TextBox
    Friend WithEvents lblCategory As Label
    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents lblCopiesTotal As Label
    Friend WithEvents numCopiesTotal As NumericUpDown
    Friend WithEvents lblAvailableCaption As Label
    Friend WithEvents lblAvailableValue As Label
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearchBooks As Button
    Friend WithEvents btnRefreshBooks As Button
    Friend WithEvents dgvBooks As DataGridView
    Friend WithEvents lblBooksSummary As Label
End Class
