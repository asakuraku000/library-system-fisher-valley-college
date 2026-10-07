' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmBorrowBooks (Designer / layout)
' Borrow books to members
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBorrowBooks
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    Private components As System.ComponentModel.IContainer

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

    'NOTE: The following procedure is required by the Windows Form Designer.
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlHeader = New Panel()
        lblSubtitle = New Label()
        lblTitle = New Label()
        grpMemberSelection = New GroupBox()
        lblSearchMember = New Label()
        txtSearchMember = New TextBox()
        cmbMember = New ComboBox()
        lblMember = New Label()
        lblSelectedMemberValue = New Label()
        grpBookSelection = New GroupBox()
        lblSearchBook = New Label()
        txtSearchBook = New TextBox()
        lblAvailableValue = New Label()
        lblAvailableCaption = New Label()
        cmbBook = New ComboBox()
        lblBook = New Label()
        lblSelectedBookValue = New Label()
        grpBorrowDetails = New GroupBox()
        lblQuantity = New Label()
        nudQuantity = New NumericUpDown()
        lblBorrowedDate = New Label()
        dtpBorrowed = New DateTimePicker()
        lblDueDate = New Label()
        dtpDueDate = New DateTimePicker()
        btnBorrow = New Button()
        btnClear = New Button()
        btnCancel = New Button()
        pnlMemberSuggestions = New Panel()
        dgvMemberSuggestions = New DataGridView()
        pnlBookSuggestions = New Panel()
        dgvBookSuggestions = New DataGridView()
        pnlHeader.SuspendLayout()
        grpMemberSelection.SuspendLayout()
        grpBookSelection.SuspendLayout()
        grpBorrowDetails.SuspendLayout()
        CType(nudQuantity, ComponentModel.ISupportInitialize).BeginInit()
        pnlMemberSuggestions.SuspendLayout()
        CType(dgvMemberSuggestions, ComponentModel.ISupportInitialize).BeginInit()
        pnlBookSuggestions.SuspendLayout()
        CType(dgvBookSuggestions, ComponentModel.ISupportInitialize).BeginInit()
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
        lblSubtitle.Size = New Size(230, 15)
        lblSubtitle.TabIndex = 1
        lblSubtitle.Text = "Borrow books to members quickly"
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
        lblTitle.Text = "Borrow Books"
        '
        ' grpMemberSelection
        '
        grpMemberSelection.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpMemberSelection.Controls.Add(lblSearchMember)
        grpMemberSelection.Controls.Add(txtSearchMember)
        grpMemberSelection.Controls.Add(cmbMember)
        grpMemberSelection.Controls.Add(lblMember)
        grpMemberSelection.Controls.Add(lblSelectedMemberValue)
        grpMemberSelection.Font = New Font("Segoe UI", 9F)
        grpMemberSelection.Location = New Point(20, 80)
        grpMemberSelection.Name = "grpMemberSelection"
        grpMemberSelection.Size = New Size(860, 100)
        grpMemberSelection.TabIndex = 1
        grpMemberSelection.TabStop = False
        grpMemberSelection.Text = "Step 1: Select Member"
        '
        ' lblSearchMember
        '
        lblSearchMember.AutoSize = True
        lblSearchMember.Location = New Point(15, 30)
        lblSearchMember.Name = "lblSearchMember"
        lblSearchMember.Size = New Size(80, 15)
        lblSearchMember.TabIndex = 14
        lblSearchMember.Text = "Find Member:"
        '
        ' txtSearchMember
        '
        txtSearchMember.Location = New Point(110, 27)
        txtSearchMember.Name = "txtSearchMember"
        txtSearchMember.Size = New Size(300, 23)
        txtSearchMember.TabIndex = 15
        '
        ' cmbMember
        '
        cmbMember.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMember.FormattingEnabled = True
        cmbMember.Location = New Point(110, 60)
        cmbMember.Name = "cmbMember"
        cmbMember.Size = New Size(300, 23)
        cmbMember.TabIndex = 2
        cmbMember.Visible = False
        '
        ' lblMember
        '
        lblMember.AutoSize = True
        lblMember.Location = New Point(20, 63)
        lblMember.Name = "lblMember"
        lblMember.Size = New Size(97, 15)
        lblMember.TabIndex = 1
        lblMember.Text = "Selected Member:"
        '
        ' lblSelectedMemberValue
        '
        lblSelectedMemberValue.AutoSize = True
        lblSelectedMemberValue.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblSelectedMemberValue.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        lblSelectedMemberValue.Location = New Point(180, 63)
        lblSelectedMemberValue.Name = "lblSelectedMemberValue"
        lblSelectedMemberValue.Size = New Size(93, 15)
        lblSelectedMemberValue.TabIndex = 18
        lblSelectedMemberValue.Text = "NONE SELECTED"
        '
        ' grpBookSelection
        '
        grpBookSelection.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpBookSelection.Controls.Add(lblSearchBook)
        grpBookSelection.Controls.Add(txtSearchBook)
        grpBookSelection.Controls.Add(lblAvailableValue)
        grpBookSelection.Controls.Add(lblAvailableCaption)
        grpBookSelection.Controls.Add(cmbBook)
        grpBookSelection.Controls.Add(lblBook)
        grpBookSelection.Controls.Add(lblSelectedBookValue)
        grpBookSelection.Font = New Font("Segoe UI", 9F)
        grpBookSelection.Location = New Point(20, 190)
        grpBookSelection.Name = "grpBookSelection"
        grpBookSelection.Size = New Size(860, 130)
        grpBookSelection.TabIndex = 2
        grpBookSelection.TabStop = False
        grpBookSelection.Text = "Step 2: Select Book"
        '
        ' lblSearchBook
        '
        lblSearchBook.AutoSize = True
        lblSearchBook.Location = New Point(15, 30)
        lblSearchBook.Name = "lblSearchBook"
        lblSearchBook.Size = New Size(73, 15)
        lblSearchBook.TabIndex = 16
        lblSearchBook.Text = "Find Book:"
        '
        ' txtSearchBook
        '
        txtSearchBook.Location = New Point(110, 27)
        txtSearchBook.Name = "txtSearchBook"
        txtSearchBook.Size = New Size(300, 23)
        txtSearchBook.TabIndex = 17
        '
        ' lblAvailableValue
        '
        lblAvailableValue.AutoSize = True
        lblAvailableValue.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblAvailableValue.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        lblAvailableValue.Location = New Point(145, 104)
        lblAvailableValue.Name = "lblAvailableValue"
        lblAvailableValue.Size = New Size(14, 15)
        lblAvailableValue.TabIndex = 12
        lblAvailableValue.Text = "0"
        '
        ' lblAvailableCaption
        '
        lblAvailableCaption.AutoSize = True
        lblAvailableCaption.Location = New Point(15, 104)
        lblAvailableCaption.Name = "lblAvailableCaption"
        lblAvailableCaption.Size = New Size(102, 15)
        lblAvailableCaption.TabIndex = 11
        lblAvailableCaption.Text = "Available Copies:"
        '
        ' cmbBook
        '
        cmbBook.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBook.FormattingEnabled = True
        cmbBook.Location = New Point(110, 60)
        cmbBook.Name = "cmbBook"
        cmbBook.Size = New Size(300, 23)
        cmbBook.TabIndex = 4
        cmbBook.Visible = False
        '
        ' lblBook
        '
        lblBook.AutoSize = True
        lblBook.Location = New Point(20, 63)
        lblBook.Name = "lblBook"
        lblBook.Size = New Size(88, 15)
        lblBook.TabIndex = 3
        lblBook.Text = "Selected Book:"
        '
        ' lblSelectedBookValue
        '
        lblSelectedBookValue.AutoSize = True
        lblSelectedBookValue.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblSelectedBookValue.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        lblSelectedBookValue.Location = New Point(180, 63)
        lblSelectedBookValue.Name = "lblSelectedBookValue"
        lblSelectedBookValue.Size = New Size(93, 15)
        lblSelectedBookValue.TabIndex = 19
        lblSelectedBookValue.Text = "NONE SELECTED"
        '
        ' grpBorrowDetails
        '
        grpBorrowDetails.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpBorrowDetails.Controls.Add(lblQuantity)
        grpBorrowDetails.Controls.Add(nudQuantity)
        grpBorrowDetails.Controls.Add(lblBorrowedDate)
        grpBorrowDetails.Controls.Add(dtpBorrowed)
        grpBorrowDetails.Controls.Add(lblDueDate)
        grpBorrowDetails.Controls.Add(dtpDueDate)
        grpBorrowDetails.Font = New Font("Segoe UI", 9F)
        grpBorrowDetails.Location = New Point(20, 330)
        grpBorrowDetails.Name = "grpBorrowDetails"
        grpBorrowDetails.Size = New Size(860, 110)
        grpBorrowDetails.TabIndex = 3
        grpBorrowDetails.TabStop = False
        grpBorrowDetails.Text = "Step 3: Borrowing Details"
        '
        ' lblQuantity
        '
        lblQuantity.AutoSize = True
        lblQuantity.Location = New Point(15, 35)
        lblQuantity.Name = "lblQuantity"
        lblQuantity.Size = New Size(53, 15)
        lblQuantity.TabIndex = 9
        lblQuantity.Text = "Quantity:"
        '
        ' nudQuantity
        '
        nudQuantity.Location = New Point(110, 32)
        nudQuantity.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        nudQuantity.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudQuantity.Name = "nudQuantity"
        nudQuantity.Size = New Size(80, 23)
        nudQuantity.TabIndex = 10
        nudQuantity.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        ' lblBorrowedDate
        '
        lblBorrowedDate.AutoSize = True
        lblBorrowedDate.Location = New Point(250, 35)
        lblBorrowedDate.Name = "lblBorrowedDate"
        lblBorrowedDate.Size = New Size(89, 15)
        lblBorrowedDate.TabIndex = 5
        lblBorrowedDate.Text = "Date Borrowed:"
        '
        ' dtpBorrowed
        '
        dtpBorrowed.Format = DateTimePickerFormat.Short
        dtpBorrowed.Location = New Point(360, 32)
        dtpBorrowed.Name = "dtpBorrowed"
        dtpBorrowed.Size = New Size(120, 23)
        dtpBorrowed.TabIndex = 6
        '
        ' lblDueDate
        '
        lblDueDate.AutoSize = True
        lblDueDate.Location = New Point(530, 35)
        lblDueDate.Name = "lblDueDate"
        lblDueDate.Size = New Size(59, 15)
        lblDueDate.TabIndex = 7
        lblDueDate.Text = "Due Date:"
        '
        ' dtpDueDate
        '
        dtpDueDate.Format = DateTimePickerFormat.Short
        dtpDueDate.Location = New Point(640, 32)
        dtpDueDate.Name = "dtpDueDate"
        dtpDueDate.Size = New Size(120, 23)
        dtpDueDate.TabIndex = 8
        '
        ' btnBorrow
        '
        btnBorrow.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnBorrow.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnBorrow.FlatAppearance.BorderSize = 0
        btnBorrow.FlatStyle = FlatStyle.Flat
        btnBorrow.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnBorrow.ForeColor = Color.White
        btnBorrow.Location = New Point(20, 460)
        btnBorrow.Name = "btnBorrow"
        btnBorrow.Size = New Size(120, 35)
        btnBorrow.TabIndex = 2
        btnBorrow.Text = "Confirm Borrow"
        btnBorrow.UseVisualStyleBackColor = False
        '
        ' btnClear
        '
        btnClear.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnClear.BackColor = Color.FromArgb(CByte(100), CByte(100), CByte(100))
        btnClear.FlatAppearance.BorderSize = 0
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.Font = New Font("Segoe UI", 9F)
        btnClear.ForeColor = Color.White
        btnClear.Location = New Point(150, 460)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(100, 35)
        btnClear.TabIndex = 3
        btnClear.Text = "Clear"
        btnClear.UseVisualStyleBackColor = False
        '
        ' btnCancel
        '
        btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCancel.BackColor = Color.FromArgb(CByte(180), CByte(30), CByte(30))
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI", 9F)
        btnCancel.ForeColor = Color.White
        btnCancel.Location = New Point(760, 460)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(120, 35)
        btnCancel.TabIndex = 4
        btnCancel.Text = "Close"
        btnCancel.UseVisualStyleBackColor = False
        '
        ' pnlMemberSuggestions
        '
        pnlMemberSuggestions.BackColor = Color.White
        pnlMemberSuggestions.BorderStyle = BorderStyle.FixedSingle
        pnlMemberSuggestions.Controls.Add(dgvMemberSuggestions)
        pnlMemberSuggestions.Location = New Point(133, 134)
        pnlMemberSuggestions.Name = "pnlMemberSuggestions"
        pnlMemberSuggestions.Size = New Size(370, 150)
        pnlMemberSuggestions.TabIndex = 20
        pnlMemberSuggestions.Visible = False
        '
        ' dgvMemberSuggestions
        '
        dgvMemberSuggestions.AllowUserToAddRows = False
        dgvMemberSuggestions.AllowUserToDeleteRows = False
        dgvMemberSuggestions.AllowUserToResizeColumns = False
        dgvMemberSuggestions.AllowUserToResizeRows = False
        dgvMemberSuggestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMemberSuggestions.BackgroundColor = Color.White
        dgvMemberSuggestions.BorderStyle = BorderStyle.None
        dgvMemberSuggestions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        dgvMemberSuggestions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvMemberSuggestions.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        dgvMemberSuggestions.ColumnHeadersHeight = 26
        dgvMemberSuggestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvMemberSuggestions.Dock = DockStyle.Fill
        dgvMemberSuggestions.EnableHeadersVisualStyles = False
        dgvMemberSuggestions.Font = New Font("Segoe UI", 9F)
        dgvMemberSuggestions.GridColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        dgvMemberSuggestions.MultiSelect = False
        dgvMemberSuggestions.Name = "dgvMemberSuggestions"
        dgvMemberSuggestions.ReadOnly = True
        dgvMemberSuggestions.RowHeadersVisible = False
        dgvMemberSuggestions.RowTemplate.Height = 24
        dgvMemberSuggestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMemberSuggestions.TabIndex = 0
        '
        ' pnlBookSuggestions
        '
        pnlBookSuggestions.BackColor = Color.White
        pnlBookSuggestions.BorderStyle = BorderStyle.FixedSingle
        pnlBookSuggestions.Controls.Add(dgvBookSuggestions)
        pnlBookSuggestions.Location = New Point(133, 244)
        pnlBookSuggestions.Name = "pnlBookSuggestions"
        pnlBookSuggestions.Size = New Size(370, 150)
        pnlBookSuggestions.TabIndex = 21
        pnlBookSuggestions.Visible = False
        '
        ' dgvBookSuggestions
        '
        dgvBookSuggestions.AllowUserToAddRows = False
        dgvBookSuggestions.AllowUserToDeleteRows = False
        dgvBookSuggestions.AllowUserToResizeColumns = False
        dgvBookSuggestions.AllowUserToResizeRows = False
        dgvBookSuggestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvBookSuggestions.BackgroundColor = Color.White
        dgvBookSuggestions.BorderStyle = BorderStyle.None
        dgvBookSuggestions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        dgvBookSuggestions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvBookSuggestions.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        dgvBookSuggestions.ColumnHeadersHeight = 26
        dgvBookSuggestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvBookSuggestions.Dock = DockStyle.Fill
        dgvBookSuggestions.EnableHeadersVisualStyles = False
        dgvBookSuggestions.Font = New Font("Segoe UI", 9F)
        dgvBookSuggestions.GridColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        dgvBookSuggestions.MultiSelect = False
        dgvBookSuggestions.Name = "dgvBookSuggestions"
        dgvBookSuggestions.ReadOnly = True
        dgvBookSuggestions.RowHeadersVisible = False
        dgvBookSuggestions.RowTemplate.Height = 24
        dgvBookSuggestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvBookSuggestions.TabIndex = 0
        '
        ' frmBorrowBooks
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(900, 510)
        Controls.Add(btnCancel)
        Controls.Add(btnClear)
        Controls.Add(btnBorrow)
        Controls.Add(grpBorrowDetails)
        Controls.Add(grpBookSelection)
        Controls.Add(grpMemberSelection)
        Controls.Add(pnlHeader)
        Controls.Add(pnlMemberSuggestions)
        Controls.Add(pnlBookSuggestions)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmBorrowBooks"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fisher Valley College Library - Borrow Books"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        grpMemberSelection.ResumeLayout(False)
        grpMemberSelection.PerformLayout()
        grpBookSelection.ResumeLayout(False)
        grpBookSelection.PerformLayout()
        grpBorrowDetails.ResumeLayout(False)
        grpBorrowDetails.PerformLayout()
        CType(nudQuantity, ComponentModel.ISupportInitialize).EndInit()
        pnlMemberSuggestions.ResumeLayout(False)
        CType(dgvMemberSuggestions, ComponentModel.ISupportInitialize).EndInit()
        pnlBookSuggestions.ResumeLayout(False)
        CType(dgvBookSuggestions, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents grpMemberSelection As GroupBox
    Friend WithEvents lblSearchMember As Label
    Friend WithEvents txtSearchMember As TextBox
    Friend WithEvents cmbMember As ComboBox
    Friend WithEvents lblMember As Label
    Friend WithEvents lblSelectedMemberValue As Label
    Friend WithEvents grpBookSelection As GroupBox
    Friend WithEvents lblSearchBook As Label
    Friend WithEvents txtSearchBook As TextBox
    Friend WithEvents lblAvailableValue As Label
    Friend WithEvents lblAvailableCaption As Label
    Friend WithEvents cmbBook As ComboBox
    Friend WithEvents lblBook As Label
    Friend WithEvents lblSelectedBookValue As Label
    Friend WithEvents grpBorrowDetails As GroupBox
    Friend WithEvents lblQuantity As Label
    Friend WithEvents nudQuantity As NumericUpDown
    Friend WithEvents lblBorrowedDate As Label
    Friend WithEvents dtpBorrowed As DateTimePicker
    Friend WithEvents lblDueDate As Label
    Friend WithEvents dtpDueDate As DateTimePicker
    Friend WithEvents btnBorrow As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents pnlMemberSuggestions As Panel
    Friend WithEvents dgvMemberSuggestions As DataGridView
    Friend WithEvents pnlBookSuggestions As Panel
    Friend WithEvents dgvBookSuggestions As DataGridView

End Class
