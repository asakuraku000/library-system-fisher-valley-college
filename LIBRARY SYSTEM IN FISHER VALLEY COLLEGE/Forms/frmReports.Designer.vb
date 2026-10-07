' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmReports (Designer / layout)
'
' The 7 report types from the group's spec, all generated through
' one shared screen: pick a type, optionally filter by date, hit
' Generate, then Export CSV or Print. Wired to the sidebar's
' previously-dead "Reports" button (ButtonReports) in main.vb.
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReports
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlHeader = New Panel()
        lblSubtitle = New Label()
        lblTitle = New Label()

        lblReportType = New Label()
        cmbReportType = New ComboBox()
        lblDateFrom = New Label()
        dtpFrom = New DateTimePicker()
        lblDateTo = New Label()
        dtpTo = New DateTimePicker()
        chkUseDateFilter = New CheckBox()
        btnGenerate = New RoundedButton()
        btnExportCsv = New RoundedButton()
        btnPrint = New RoundedButton()

        lblReportSummary = New Label()
        dgvReport = New DataGridView()

        pnlHeader.SuspendLayout()
        CType(dgvReport, ComponentModel.ISupportInitialize).BeginInit()
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
        lblTitle.Size = New Size(100, 28)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Reports"
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
        lblSubtitle.Text = "Fisher Valley College Library — Report Generator"
        '
        ' lblReportType
        '
        lblReportType.AutoSize = True
        lblReportType.Font = New Font("Segoe UI", 9F)
        lblReportType.Location = New Point(20, 84)
        lblReportType.Name = "lblReportType"
        lblReportType.Size = New Size(80, 15)
        lblReportType.TabIndex = 1
        lblReportType.Text = "Report Type:"
        '
        ' cmbReportType
        '
        cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbReportType.Font = New Font("Segoe UI", 9F)
        cmbReportType.Location = New Point(126, 80)
        cmbReportType.Name = "cmbReportType"
        cmbReportType.Size = New Size(260, 23)
        cmbReportType.TabIndex = 2
        '
        ' lblDateFrom
        '
        lblDateFrom.AutoSize = True
        lblDateFrom.Font = New Font("Segoe UI", 9F)
        lblDateFrom.Location = New Point(410, 84)
        lblDateFrom.Name = "lblDateFrom"
        lblDateFrom.Size = New Size(38, 15)
        lblDateFrom.TabIndex = 3
        lblDateFrom.Text = "From:"
        '
        ' dtpFrom
        '
        dtpFrom.Enabled = False
        dtpFrom.Font = New Font("Segoe UI", 9F)
        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Location = New Point(452, 80)
        dtpFrom.Name = "dtpFrom"
        dtpFrom.Size = New Size(120, 23)
        dtpFrom.TabIndex = 4
        dtpFrom.Value = DateTime.Today.AddMonths(-1)
        '
        ' lblDateTo
        '
        lblDateTo.AutoSize = True
        lblDateTo.Font = New Font("Segoe UI", 9F)
        lblDateTo.Location = New Point(582, 84)
        lblDateTo.Name = "lblDateTo"
        lblDateTo.Size = New Size(26, 15)
        lblDateTo.TabIndex = 5
        lblDateTo.Text = "To:"
        '
        ' dtpTo
        '
        dtpTo.Enabled = False
        dtpTo.Font = New Font("Segoe UI", 9F)
        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Location = New Point(614, 80)
        dtpTo.Name = "dtpTo"
        dtpTo.Size = New Size(120, 23)
        dtpTo.TabIndex = 6
        dtpTo.Value = DateTime.Today
        '
        ' chkUseDateFilter
        '
        chkUseDateFilter.AutoSize = True
        chkUseDateFilter.Font = New Font("Segoe UI", 9F)
        chkUseDateFilter.Location = New Point(750, 83)
        chkUseDateFilter.Name = "chkUseDateFilter"
        chkUseDateFilter.Size = New Size(112, 19)
        chkUseDateFilter.TabIndex = 7
        chkUseDateFilter.Text = "Apply date filter"
        '
        ' btnGenerate
        '
        btnGenerate.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnGenerate.FlatAppearance.BorderSize = 0
        btnGenerate.FlatStyle = FlatStyle.Flat
        btnGenerate.Font = New Font("Segoe UI", 9F)
        btnGenerate.ForeColor = Color.White
        btnGenerate.Location = New Point(900, 78)
        btnGenerate.Name = "btnGenerate"
        btnGenerate.Kind = ButtonKind.Primary
        btnGenerate.Size = New Size(90, 28)
        btnGenerate.TabIndex = 8
        btnGenerate.Text = "Generate"
        btnGenerate.UseVisualStyleBackColor = False
        '
        ' btnExportCsv
        '
        btnExportCsv.FlatAppearance.BorderColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnExportCsv.FlatStyle = FlatStyle.Flat
        btnExportCsv.Font = New Font("Segoe UI", 9F)
        btnExportCsv.Location = New Point(998, 78)
        btnExportCsv.Name = "btnExportCsv"
        btnExportCsv.Kind = ButtonKind.Outline
        btnExportCsv.Size = New Size(90, 28)
        btnExportCsv.TabIndex = 9
        btnExportCsv.Text = "Export CSV"
        btnExportCsv.UseVisualStyleBackColor = True
        '
        ' btnPrint
        '
        btnPrint.FlatAppearance.BorderColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        btnPrint.FlatStyle = FlatStyle.Flat
        btnPrint.Font = New Font("Segoe UI", 9F)
        btnPrint.Location = New Point(1070, 78)
        btnPrint.Name = "btnPrint"
        btnPrint.Kind = ButtonKind.Outline
        btnPrint.Size = New Size(90, 28)
        btnPrint.TabIndex = 10
        btnPrint.Text = "Print"
        btnPrint.UseVisualStyleBackColor = True
        '
        ' lblReportSummary
        '
        lblReportSummary.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblReportSummary.AutoSize = True
        lblReportSummary.Font = New Font("Segoe UI", 9F, FontStyle.Italic)
        lblReportSummary.ForeColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        lblReportSummary.Location = New Point(20, 124)
        lblReportSummary.Name = "lblReportSummary"
        lblReportSummary.Size = New Size(320, 15)
        lblReportSummary.TabIndex = 11
        lblReportSummary.Text = "Pick a report type and click Generate."
        '
        ' dgvReport
        '
        dgvReport.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvReport.AllowUserToAddRows = False
        dgvReport.AllowUserToDeleteRows = False
        dgvReport.ReadOnly = True
        dgvReport.RowHeadersVisible = False
        dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvReport.Font = New Font("Segoe UI", 9F)
        dgvReport.Location = New Point(20, 150)
        dgvReport.Name = "dgvReport"
        dgvReport.Size = New Size(1140, 470)
        dgvReport.TabIndex = 12
        '
        ' frmReports
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1180, 660)
        Controls.Add(dgvReport)
        Controls.Add(lblReportSummary)
        Controls.Add(btnPrint)
        Controls.Add(btnExportCsv)
        Controls.Add(btnGenerate)
        Controls.Add(chkUseDateFilter)
        Controls.Add(dtpTo)
        Controls.Add(lblDateTo)
        Controls.Add(dtpFrom)
        Controls.Add(lblDateFrom)
        Controls.Add(cmbReportType)
        Controls.Add(lblReportType)
        Controls.Add(pnlHeader)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmReports"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fisher Valley College Library - Reports"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        CType(dgvReport, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label

    Friend WithEvents lblReportType As Label
    Friend WithEvents cmbReportType As ComboBox
    Friend WithEvents lblDateFrom As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents lblDateTo As Label
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents chkUseDateFilter As CheckBox
    Friend WithEvents btnGenerate As RoundedButton
    Friend WithEvents btnExportCsv As RoundedButton
    Friend WithEvents btnPrint As RoundedButton

    Friend WithEvents lblReportSummary As Label
    Friend WithEvents dgvReport As DataGridView
End Class
