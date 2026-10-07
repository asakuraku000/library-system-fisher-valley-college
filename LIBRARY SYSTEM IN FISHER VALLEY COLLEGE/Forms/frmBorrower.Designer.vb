' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmBorrower (Designer / layout)
' Borrower profiles — Add / Update / Delete / Search
' Layout deliberately mirrors frmBooks.Designer.vb (same header
' style, GroupBox size, row spacing, search bar, grid and summary
' label positions) so it looks and behaves like the same family
' of screen once embedded in PanelContentHost.
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBorrower
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
        btnViewHistory = New RoundedButton()
        btnClear = New RoundedButton()
        btnDelete = New RoundedButton()
        btnUpdate = New RoundedButton()
        btnAdd = New RoundedButton()
        lblDateRegisteredValue = New Label()
        lblDateRegisteredCaption = New Label()
        cmbMembershipStatus = New ComboBox()
        lblMembershipStatus = New Label()
        txtAddress = New TextBox()
        lblAddress = New Label()
        txtEmail = New TextBox()
        lblEmail = New Label()
        txtContact = New TextBox()
        lblContact = New Label()
        cmbYearLevel = New ComboBox()
        lblYearLevel = New Label()
        cmbCourse = New ComboBox()
        lblCourse = New Label()
        txtFullName = New TextBox()
        lblFullName = New Label()
        txtIdNumber = New TextBox()
        lblIdNumber = New Label()
        lblSearch = New Label()
        txtSearch = New TextBox()
        btnSearchBorrowers = New RoundedButton()
        btnRefreshBorrowers = New RoundedButton()
        dgvBorrowers = New DataGridView()
        lblBorrowersSummary = New Label()
        pnlHeader.SuspendLayout()
        grpDetails.SuspendLayout()
        CType(dgvBorrowers, ComponentModel.ISupportInitialize).BeginInit()
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
        lblTitle.Size = New Size(210, 28)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Borrower Management"
        '
        ' grpDetails
        '
        grpDetails.Controls.Add(btnViewHistory)
        grpDetails.Controls.Add(btnClear)
        grpDetails.Controls.Add(btnDelete)
        grpDetails.Controls.Add(btnUpdate)
        grpDetails.Controls.Add(btnAdd)
        grpDetails.Controls.Add(lblDateRegisteredValue)
        grpDetails.Controls.Add(lblDateRegisteredCaption)
        grpDetails.Controls.Add(cmbMembershipStatus)
        grpDetails.Controls.Add(lblMembershipStatus)
        grpDetails.Controls.Add(txtAddress)
        grpDetails.Controls.Add(lblAddress)
        grpDetails.Controls.Add(txtEmail)
        grpDetails.Controls.Add(lblEmail)
        grpDetails.Controls.Add(txtContact)
        grpDetails.Controls.Add(lblContact)
        grpDetails.Controls.Add(cmbYearLevel)
        grpDetails.Controls.Add(lblYearLevel)
        grpDetails.Controls.Add(cmbCourse)
        grpDetails.Controls.Add(lblCourse)
        grpDetails.Controls.Add(txtFullName)
        grpDetails.Controls.Add(lblFullName)
        grpDetails.Controls.Add(txtIdNumber)
        grpDetails.Controls.Add(lblIdNumber)
        grpDetails.Font = New Font("Segoe UI", 9F)
        grpDetails.Location = New Point(20, 80)
        grpDetails.Name = "grpDetails"
        grpDetails.Size = New Size(280, 470)
        grpDetails.TabIndex = 1
        grpDetails.TabStop = False
        grpDetails.Text = "Borrower Details"
        '
        ' lblIdNumber
        '
        lblIdNumber.AutoSize = True
        lblIdNumber.Location = New Point(15, 30)
        lblIdNumber.Name = "lblIdNumber"
        lblIdNumber.Size = New Size(68, 15)
        lblIdNumber.TabIndex = 0
        lblIdNumber.Text = "ID Number:"
        '
        ' txtIdNumber
        '
        txtIdNumber.Location = New Point(115, 27)
        txtIdNumber.Name = "txtIdNumber"
        txtIdNumber.Size = New Size(150, 23)
        txtIdNumber.TabIndex = 1
        '
        ' lblFullName
        '
        lblFullName.AutoSize = True
        lblFullName.Location = New Point(15, 64)
        lblFullName.Name = "lblFullName"
        lblFullName.Size = New Size(63, 15)
        lblFullName.TabIndex = 2
        lblFullName.Text = "Full Name:"
        '
        ' txtFullName
        '
        txtFullName.Location = New Point(115, 61)
        txtFullName.Name = "txtFullName"
        txtFullName.Size = New Size(150, 23)
        txtFullName.TabIndex = 3
        '
        ' lblCourse
        '
        lblCourse.AutoSize = True
        lblCourse.Location = New Point(15, 98)
        lblCourse.Name = "lblCourse"
        lblCourse.Size = New Size(47, 15)
        lblCourse.TabIndex = 4
        lblCourse.Text = "Course:"
        '
        ' cmbCourse
        '
        cmbCourse.DropDownStyle = ComboBoxStyle.DropDown
        cmbCourse.Items.AddRange(New Object() {"BSCS", "BSIT", "BSED"})
        cmbCourse.Location = New Point(115, 95)
        cmbCourse.Name = "cmbCourse"
        cmbCourse.Size = New Size(150, 23)
        cmbCourse.TabIndex = 5
        '
        ' lblYearLevel
        '
        lblYearLevel.AutoSize = True
        lblYearLevel.Location = New Point(15, 132)
        lblYearLevel.Name = "lblYearLevel"
        lblYearLevel.Size = New Size(66, 15)
        lblYearLevel.TabIndex = 6
        lblYearLevel.Text = "Year Level:"
        '
        ' cmbYearLevel
        '
        cmbYearLevel.DropDownStyle = ComboBoxStyle.DropDown
        cmbYearLevel.Items.AddRange(New Object() {"1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year", "Faculty/Staff"})
        cmbYearLevel.Location = New Point(115, 129)
        cmbYearLevel.Name = "cmbYearLevel"
        cmbYearLevel.Size = New Size(150, 23)
        cmbYearLevel.TabIndex = 7
        '
        ' lblContact
        '
        lblContact.AutoSize = True
        lblContact.Location = New Point(15, 166)
        lblContact.Name = "lblContact"
        lblContact.Size = New Size(85, 15)
        lblContact.TabIndex = 8
        lblContact.Text = "Contact Number:"
        '
        ' txtContact
        '
        txtContact.Location = New Point(115, 163)
        txtContact.Name = "txtContact"
        txtContact.Size = New Size(150, 23)
        txtContact.TabIndex = 9
        '
        ' lblEmail
        '
        lblEmail.AutoSize = True
        lblEmail.Location = New Point(15, 200)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New Size(39, 15)
        lblEmail.TabIndex = 10
        lblEmail.Text = "Email:"
        '
        ' txtEmail
        '
        txtEmail.Location = New Point(115, 197)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(150, 23)
        txtEmail.TabIndex = 11
        '
        ' lblAddress
        '
        lblAddress.AutoSize = True
        lblAddress.Location = New Point(15, 234)
        lblAddress.Name = "lblAddress"
        lblAddress.Size = New Size(53, 15)
        lblAddress.TabIndex = 12
        lblAddress.Text = "Address:"
        '
        ' txtAddress
        '
        txtAddress.Location = New Point(115, 231)
        txtAddress.Name = "txtAddress"
        txtAddress.Size = New Size(150, 23)
        txtAddress.TabIndex = 13
        '
        ' lblMembershipStatus
        '
        lblMembershipStatus.AutoSize = True
        lblMembershipStatus.Location = New Point(15, 268)
        lblMembershipStatus.Name = "lblMembershipStatus"
        lblMembershipStatus.Size = New Size(102, 15)
        lblMembershipStatus.TabIndex = 14
        lblMembershipStatus.Text = "Membership Status:"
        '
        ' cmbMembershipStatus
        '
        cmbMembershipStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMembershipStatus.Items.AddRange(New Object() {"Active", "Inactive", "Suspended"})
        cmbMembershipStatus.Location = New Point(115, 265)
        cmbMembershipStatus.Name = "cmbMembershipStatus"
        cmbMembershipStatus.Size = New Size(150, 23)
        cmbMembershipStatus.TabIndex = 15
        '
        ' lblDateRegisteredCaption
        '
        lblDateRegisteredCaption.AutoSize = True
        lblDateRegisteredCaption.Location = New Point(15, 302)
        lblDateRegisteredCaption.Name = "lblDateRegisteredCaption"
        lblDateRegisteredCaption.Size = New Size(94, 15)
        lblDateRegisteredCaption.TabIndex = 16
        lblDateRegisteredCaption.Text = "Date Registered:"
        '
        ' lblDateRegisteredValue
        '
        lblDateRegisteredValue.AutoSize = True
        lblDateRegisteredValue.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblDateRegisteredValue.Location = New Point(115, 302)
        lblDateRegisteredValue.Name = "lblDateRegisteredValue"
        lblDateRegisteredValue.Size = New Size(23, 15)
        lblDateRegisteredValue.TabIndex = 17
        lblDateRegisteredValue.Text = "—"
        '
        ' btnAdd
        '
        btnAdd.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.ForeColor = Color.White
        btnAdd.Location = New Point(15, 336)
        btnAdd.Name = "btnAdd"
        btnAdd.Kind = ButtonKind.Primary
        btnAdd.Size = New Size(120, 32)
        btnAdd.TabIndex = 18
        btnAdd.Text = "Add Borrower"
        btnAdd.UseVisualStyleBackColor = False
        '
        ' btnUpdate
        '
        btnUpdate.BackColor = Color.FromArgb(CByte(46), CByte(125), CByte(50))
        btnUpdate.FlatAppearance.BorderSize = 0
        btnUpdate.FlatStyle = FlatStyle.Flat
        btnUpdate.ForeColor = Color.White
        btnUpdate.Location = New Point(145, 336)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Kind = ButtonKind.Custom
        btnUpdate.Size = New Size(120, 32)
        btnUpdate.TabIndex = 19
        btnUpdate.Text = "Update"
        btnUpdate.UseVisualStyleBackColor = False
        '
        ' btnDelete
        '
        btnDelete.BackColor = Color.FromArgb(CByte(178), CByte(34), CByte(34))
        btnDelete.FlatAppearance.BorderSize = 0
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.ForeColor = Color.White
        btnDelete.Location = New Point(15, 376)
        btnDelete.Name = "btnDelete"
        btnDelete.Kind = ButtonKind.Custom
        btnDelete.Size = New Size(120, 32)
        btnDelete.TabIndex = 20
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = False
        '
        ' btnClear
        '
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.Location = New Point(145, 376)
        btnClear.Name = "btnClear"
        btnClear.Kind = ButtonKind.Neutral
        btnClear.Size = New Size(120, 32)
        btnClear.TabIndex = 21
        btnClear.Text = "Clear Form"
        btnClear.UseVisualStyleBackColor = True
        '
        ' btnViewHistory
        '
        btnViewHistory.BackColor = Color.FromArgb(CByte(96), CByte(60), CByte(150))
        btnViewHistory.FlatAppearance.BorderSize = 0
        btnViewHistory.FlatStyle = FlatStyle.Flat
        btnViewHistory.ForeColor = Color.White
        btnViewHistory.Location = New Point(15, 416)
        btnViewHistory.Name = "btnViewHistory"
        btnViewHistory.Kind = ButtonKind.Custom
        btnViewHistory.Size = New Size(250, 32)
        btnViewHistory.TabIndex = 22
        btnViewHistory.Text = "View Borrowing History"
        btnViewHistory.UseVisualStyleBackColor = False
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
        ' btnSearchBorrowers
        '
        btnSearchBorrowers.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnSearchBorrowers.FlatAppearance.BorderSize = 0
        btnSearchBorrowers.FlatStyle = FlatStyle.Flat
        btnSearchBorrowers.Font = New Font("Segoe UI", 9F)
        btnSearchBorrowers.ForeColor = Color.White
        btnSearchBorrowers.Location = New Point(688, 76)
        btnSearchBorrowers.Name = "btnSearchBorrowers"
        btnSearchBorrowers.Kind = ButtonKind.Primary
        btnSearchBorrowers.Size = New Size(80, 26)
        btnSearchBorrowers.TabIndex = 4
        btnSearchBorrowers.Text = "Search"
        btnSearchBorrowers.UseVisualStyleBackColor = False
        '
        ' btnRefreshBorrowers
        '
        btnRefreshBorrowers.FlatAppearance.BorderColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefreshBorrowers.FlatStyle = FlatStyle.Flat
        btnRefreshBorrowers.Font = New Font("Segoe UI", 9F)
        btnRefreshBorrowers.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnRefreshBorrowers.Location = New Point(778, 76)
        btnRefreshBorrowers.Name = "btnRefreshBorrowers"
        btnRefreshBorrowers.Kind = ButtonKind.Outline
        btnRefreshBorrowers.Size = New Size(80, 26)
        btnRefreshBorrowers.TabIndex = 5
        btnRefreshBorrowers.Text = "Refresh"
        btnRefreshBorrowers.UseVisualStyleBackColor = True
        '
        ' dgvBorrowers
        '
        dgvBorrowers.AllowUserToAddRows = False
        dgvBorrowers.AllowUserToDeleteRows = False
        dgvBorrowers.AllowUserToOrderColumns = False
        dgvBorrowers.AllowUserToResizeRows = False
        dgvBorrowers.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvBorrowers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(CByte(245), CByte(247), CByte(250))
        dgvBorrowers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvBorrowers.BackgroundColor = Color.White
        dgvBorrowers.BorderStyle = BorderStyle.FixedSingle
        dgvBorrowers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        dgvBorrowers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvBorrowers.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        dgvBorrowers.ColumnHeadersHeight = 32
        dgvBorrowers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvBorrowers.EnableHeadersVisualStyles = False
        dgvBorrowers.GridColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        dgvBorrowers.Location = New Point(320, 114)
        dgvBorrowers.MultiSelect = False
        dgvBorrowers.Name = "dgvBorrowers"
        dgvBorrowers.ReadOnly = True
        dgvBorrowers.RowHeadersVisible = False
        dgvBorrowers.RowTemplate.Height = 26
        dgvBorrowers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvBorrowers.Size = New Size(620, 436)
        dgvBorrowers.TabIndex = 6
        '
        ' lblBorrowersSummary
        '
        lblBorrowersSummary.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblBorrowersSummary.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblBorrowersSummary.ForeColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        lblBorrowersSummary.Location = New Point(320, 558)
        lblBorrowersSummary.Name = "lblBorrowersSummary"
        lblBorrowersSummary.Size = New Size(500, 23)
        lblBorrowersSummary.TabIndex = 7
        lblBorrowersSummary.Text = "Total borrowers: 0"
        lblBorrowersSummary.TextAlign = ContentAlignment.MiddleLeft
        '
        ' frmBorrower
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(960, 600)
        Controls.Add(lblBorrowersSummary)
        Controls.Add(dgvBorrowers)
        Controls.Add(btnRefreshBorrowers)
        Controls.Add(btnSearchBorrowers)
        Controls.Add(txtSearch)
        Controls.Add(lblSearch)
        Controls.Add(grpDetails)
        Controls.Add(pnlHeader)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmBorrower"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fisher Valley College Library - Borrower"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        grpDetails.ResumeLayout(False)
        grpDetails.PerformLayout()
        CType(dgvBorrowers, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents lblIdNumber As Label
    Friend WithEvents txtIdNumber As TextBox
    Friend WithEvents lblFullName As Label
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents lblCourse As Label
    Friend WithEvents cmbCourse As ComboBox
    Friend WithEvents lblYearLevel As Label
    Friend WithEvents cmbYearLevel As ComboBox
    Friend WithEvents lblContact As Label
    Friend WithEvents txtContact As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblAddress As Label
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents lblMembershipStatus As Label
    Friend WithEvents cmbMembershipStatus As ComboBox
    Friend WithEvents lblDateRegisteredCaption As Label
    Friend WithEvents lblDateRegisteredValue As Label
    Friend WithEvents btnAdd As RoundedButton
    Friend WithEvents btnUpdate As RoundedButton
    Friend WithEvents btnDelete As RoundedButton
    Friend WithEvents btnClear As RoundedButton
    Friend WithEvents btnViewHistory As RoundedButton
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearchBorrowers As RoundedButton
    Friend WithEvents btnRefreshBorrowers As RoundedButton
    Friend WithEvents dgvBorrowers As DataGridView
    Friend WithEvents lblBorrowersSummary As Label
End Class
