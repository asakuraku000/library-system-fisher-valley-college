<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        PanelReportContent = New Panel()
        PanelReportViewer = New Panel()
        SplitContainer1 = New SplitContainer()
        PanelReportOptions = New Panel()
        LabelStudentInfoTitle = New Label()
        LabelIDNumber = New Label()
        TextBoxID = New TextBox()
        LabelName = New Label()
        TextBoxName = New TextBox()
        LabelCourse = New Label()
        ComboBoxCourse = New ComboBox()
        LabelBookInfoTitle = New Label()
        LabelBookID = New Label()
        TextBoxBookID = New TextBox()
        LabelBookTitle = New Label()
        TextBoxBookTitle = New TextBox()
        LabelAuthor = New Label()
        TextBoxAuthor = New TextBox()
        ButtonBorrow = New Button()
        ButtonReturn = New Button()
        PictureBoxLogo = New PictureBox()
        LabelTitle = New Label()
        PanelHeader = New Panel()
        ButtonBooks = New Button()
        ButtonBorrower = New Button()
        PanelLeft = New Panel()
        ButtonReports = New Button()
        ButtonManageUsers = New Button()
        ButtonLogOut = New Button()
        ButtonOverdues = New Button()
        ButtonReturnBooks = New Button()
        ButtonBorrowBooks = New Button()
        ButtonDashboard = New Button()
        PanelContentHost = New Panel()
        PanelReportViewer.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        PanelReportOptions.SuspendLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).BeginInit()
        PanelHeader.SuspendLayout()
        PanelLeft.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelReportContent
        ' 
        PanelReportContent.BackColor = Color.White
        PanelReportContent.BorderStyle = BorderStyle.FixedSingle
        PanelReportContent.Location = New Point(20, 12)
        PanelReportContent.Name = "PanelReportContent"
        PanelReportContent.Size = New Size(796, 512)
        PanelReportContent.TabIndex = 0
        ' 
        ' PanelReportViewer
        ' 
        PanelReportViewer.BackColor = Color.LightGray
        PanelReportViewer.Controls.Add(PanelReportContent)
        PanelReportViewer.Dock = DockStyle.Fill
        PanelReportViewer.Location = New Point(0, 0)
        PanelReportViewer.Name = "PanelReportViewer"
        PanelReportViewer.Size = New Size(922, 676)
        PanelReportViewer.TabIndex = 0
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.FixedPanel = FixedPanel.Panel1
        SplitContainer1.Location = New Point(160, 64)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(PanelReportOptions)
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(PanelReportViewer)
        SplitContainer1.Size = New Size(1346, 676)
        SplitContainer1.SplitterDistance = 420
        SplitContainer1.TabIndex = 3
        ' 
        ' PanelReportOptions
        ' 
        PanelReportOptions.BackColor = Color.WhiteSmoke
        PanelReportOptions.Controls.Add(LabelStudentInfoTitle)
        PanelReportOptions.Controls.Add(LabelIDNumber)
        PanelReportOptions.Controls.Add(TextBoxID)
        PanelReportOptions.Controls.Add(LabelName)
        PanelReportOptions.Controls.Add(TextBoxName)
        PanelReportOptions.Controls.Add(LabelCourse)
        PanelReportOptions.Controls.Add(ComboBoxCourse)
        PanelReportOptions.Controls.Add(LabelBookInfoTitle)
        PanelReportOptions.Controls.Add(LabelBookID)
        PanelReportOptions.Controls.Add(TextBoxBookID)
        PanelReportOptions.Controls.Add(LabelBookTitle)
        PanelReportOptions.Controls.Add(TextBoxBookTitle)
        PanelReportOptions.Controls.Add(LabelAuthor)
        PanelReportOptions.Controls.Add(TextBoxAuthor)
        PanelReportOptions.Controls.Add(ButtonBorrow)
        PanelReportOptions.Controls.Add(ButtonReturn)
        PanelReportOptions.Dock = DockStyle.Fill
        PanelReportOptions.Location = New Point(0, 0)
        PanelReportOptions.Name = "PanelReportOptions"
        PanelReportOptions.Size = New Size(420, 676)
        PanelReportOptions.TabIndex = 0
        ' 
        ' LabelStudentInfoTitle
        ' 
        LabelStudentInfoTitle.AutoSize = True
        LabelStudentInfoTitle.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        LabelStudentInfoTitle.Location = New Point(12, 12)
        LabelStudentInfoTitle.Name = "LabelStudentInfoTitle"
        LabelStudentInfoTitle.Size = New Size(143, 19)
        LabelStudentInfoTitle.TabIndex = 0
        LabelStudentInfoTitle.Text = "Student Information"
        ' 
        ' LabelIDNumber
        ' 
        LabelIDNumber.AutoSize = True
        LabelIDNumber.Location = New Point(12, 44)
        LabelIDNumber.Name = "LabelIDNumber"
        LabelIDNumber.Size = New Size(68, 15)
        LabelIDNumber.TabIndex = 1
        LabelIDNumber.Text = "ID Number:"
        ' 
        ' TextBoxID
        ' 
        TextBoxID.BackColor = Color.FromArgb(CByte(255), CByte(250), CByte(230))
        TextBoxID.Location = New Point(110, 40)
        TextBoxID.Name = "TextBoxID"
        TextBoxID.Size = New Size(120, 23)
        TextBoxID.TabIndex = 2
        ' 
        ' LabelName
        ' 
        LabelName.AutoSize = True
        LabelName.Location = New Point(12, 78)
        LabelName.Name = "LabelName"
        LabelName.Size = New Size(42, 15)
        LabelName.TabIndex = 3
        LabelName.Text = "Name:"
        ' 
        ' TextBoxName
        ' 
        TextBoxName.BackColor = Color.FromArgb(CByte(255), CByte(250), CByte(230))
        TextBoxName.Location = New Point(110, 74)
        TextBoxName.Name = "TextBoxName"
        TextBoxName.Size = New Size(120, 23)
        TextBoxName.TabIndex = 4
        ' 
        ' LabelCourse
        ' 
        LabelCourse.AutoSize = True
        LabelCourse.Location = New Point(12, 112)
        LabelCourse.Name = "LabelCourse"
        LabelCourse.Size = New Size(47, 15)
        LabelCourse.TabIndex = 5
        LabelCourse.Text = "Course:"
        ' 
        ' ComboBoxCourse
        ' 
        ComboBoxCourse.Items.AddRange(New Object() {"BSCS", "BSIT", "BSED"})
        ComboBoxCourse.Location = New Point(110, 108)
        ComboBoxCourse.Name = "ComboBoxCourse"
        ComboBoxCourse.Size = New Size(120, 23)
        ComboBoxCourse.TabIndex = 6
        ' 
        ' LabelBookInfoTitle
        ' 
        LabelBookInfoTitle.AutoSize = True
        LabelBookInfoTitle.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        LabelBookInfoTitle.Location = New Point(12, 150)
        LabelBookInfoTitle.Name = "LabelBookInfoTitle"
        LabelBookInfoTitle.Size = New Size(127, 19)
        LabelBookInfoTitle.TabIndex = 7
        LabelBookInfoTitle.Text = "Book Information"
        ' 
        ' LabelBookID
        ' 
        LabelBookID.AutoSize = True
        LabelBookID.Location = New Point(12, 182)
        LabelBookID.Name = "LabelBookID"
        LabelBookID.Size = New Size(51, 15)
        LabelBookID.TabIndex = 8
        LabelBookID.Text = "Book ID:"
        ' 
        ' TextBoxBookID
        ' 
        TextBoxBookID.BackColor = Color.FromArgb(CByte(255), CByte(250), CByte(230))
        TextBoxBookID.Location = New Point(110, 178)
        TextBoxBookID.Name = "TextBoxBookID"
        TextBoxBookID.Size = New Size(120, 23)
        TextBoxBookID.TabIndex = 9
        ' 
        ' LabelBookTitle
        ' 
        LabelBookTitle.AutoSize = True
        LabelBookTitle.Location = New Point(12, 214)
        LabelBookTitle.Name = "LabelBookTitle"
        LabelBookTitle.Size = New Size(32, 15)
        LabelBookTitle.TabIndex = 10
        LabelBookTitle.Text = "Title:"
        ' 
        ' TextBoxBookTitle
        ' 
        TextBoxBookTitle.BackColor = Color.FromArgb(CByte(255), CByte(250), CByte(230))
        TextBoxBookTitle.Location = New Point(110, 210)
        TextBoxBookTitle.Name = "TextBoxBookTitle"
        TextBoxBookTitle.Size = New Size(120, 23)
        TextBoxBookTitle.TabIndex = 11
        ' 
        ' LabelAuthor
        ' 
        LabelAuthor.AutoSize = True
        LabelAuthor.Location = New Point(12, 246)
        LabelAuthor.Name = "LabelAuthor"
        LabelAuthor.Size = New Size(47, 15)
        LabelAuthor.TabIndex = 12
        LabelAuthor.Text = "Author:"
        ' 
        ' TextBoxAuthor
        ' 
        TextBoxAuthor.BackColor = Color.FromArgb(CByte(255), CByte(250), CByte(230))
        TextBoxAuthor.Location = New Point(110, 242)
        TextBoxAuthor.Name = "TextBoxAuthor"
        TextBoxAuthor.Size = New Size(120, 23)
        TextBoxAuthor.TabIndex = 13
        ' 
        ' ButtonBorrow
        ' 
        ButtonBorrow.Location = New Point(28, 285)
        ButtonBorrow.Name = "ButtonBorrow"
        ButtonBorrow.Size = New Size(75, 28)
        ButtonBorrow.TabIndex = 14
        ButtonBorrow.Text = "Borrow"
        ' 
        ' ButtonReturn
        ' 
        ButtonReturn.Location = New Point(120, 285)
        ButtonReturn.Name = "ButtonReturn"
        ButtonReturn.Size = New Size(75, 28)
        ButtonReturn.TabIndex = 15
        ButtonReturn.Text = "Return"
        ' 
        ' PictureBoxLogo
        ' 
        PictureBoxLogo.BackColor = Color.WhiteSmoke
        PictureBoxLogo.Image = My.Resources.Resources._A0DF6743_89CC_4595_A3E3_7962AA7C6ABC_
        PictureBoxLogo.Location = New Point(0, 0)
        PictureBoxLogo.Name = "PictureBoxLogo"
        PictureBoxLogo.Size = New Size(80, 64)
        PictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBoxLogo.TabIndex = 2
        PictureBoxLogo.TabStop = False
        ' 
        ' LabelTitle
        ' 
        LabelTitle.AutoSize = True
        LabelTitle.Font = New Font("Segoe UI", 20F)
        LabelTitle.ForeColor = Color.White
        LabelTitle.Location = New Point(84, 12)
        LabelTitle.Name = "LabelTitle"
        LabelTitle.Size = New Size(188, 37)
        LabelTitle.TabIndex = 3
        LabelTitle.Text = "Library System"
        ' 
        ' PanelHeader
        ' 
        PanelHeader.BackColor = Color.FromArgb(CByte(10), CByte(120), CByte(200))
        PanelHeader.Controls.Add(PictureBoxLogo)
        PanelHeader.Controls.Add(LabelTitle)
        PanelHeader.Dock = DockStyle.Top
        PanelHeader.Location = New Point(160, 0)
        PanelHeader.Name = "PanelHeader"
        PanelHeader.Size = New Size(1346, 64)
        PanelHeader.TabIndex = 4
        ' 
        ' ButtonBooks
        ' 
        ButtonBooks.Dock = DockStyle.Top
        ButtonBooks.FlatAppearance.BorderSize = 0
        ButtonBooks.FlatStyle = FlatStyle.Flat
        ButtonBooks.ForeColor = Color.White
        ButtonBooks.Location = New Point(0, 112)
        ButtonBooks.Name = "ButtonBooks"
        ButtonBooks.Padding = New Padding(12, 0, 0, 0)
        ButtonBooks.Size = New Size(160, 48)
        ButtonBooks.TabIndex = 7
        ButtonBooks.Text = "Books"
        ButtonBooks.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ButtonDashboard
        ' 
        ButtonDashboard.Dock = DockStyle.Top
        ButtonDashboard.FlatAppearance.BorderSize = 0
        ButtonDashboard.FlatStyle = FlatStyle.Flat
        ButtonDashboard.ForeColor = Color.White
        ButtonDashboard.Location = New Point(0, 64)
        ButtonDashboard.Name = "ButtonDashboard"
        ButtonDashboard.Padding = New Padding(12, 0, 0, 0)
        ButtonDashboard.Size = New Size(160, 48)
        ButtonDashboard.TabIndex = 8
        ButtonDashboard.Text = "Dashboard"
        ButtonDashboard.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ButtonBorrower
        ' 
        ButtonBorrower.Dock = DockStyle.Top
        ButtonBorrower.FlatAppearance.BorderSize = 0
        ButtonBorrower.FlatStyle = FlatStyle.Flat
        ButtonBorrower.ForeColor = Color.White
        ButtonBorrower.Location = New Point(0, 160)
        ButtonBorrower.Name = "ButtonBorrower"
        ButtonBorrower.Padding = New Padding(12, 0, 0, 0)
        ButtonBorrower.Size = New Size(160, 48)
        ButtonBorrower.TabIndex = 6
        ButtonBorrower.Text = "Borrower"
        ButtonBorrower.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' PanelLeft
        ' 
        PanelLeft.BackColor = Color.FromArgb(CByte(17), CByte(78), CByte(128))
        PanelLeft.Controls.Add(ButtonReports)
        PanelLeft.Controls.Add(ButtonManageUsers)
        PanelLeft.Controls.Add(ButtonLogOut)
        PanelLeft.Controls.Add(ButtonOverdues)
        PanelLeft.Controls.Add(ButtonReturnBooks)
        PanelLeft.Controls.Add(ButtonBorrowBooks)
        PanelLeft.Controls.Add(ButtonBorrower)
        PanelLeft.Controls.Add(ButtonBooks)
        PanelLeft.Controls.Add(ButtonDashboard)
        PanelLeft.Dock = DockStyle.Left
        PanelLeft.Location = New Point(0, 0)
        PanelLeft.Name = "PanelLeft"
        PanelLeft.Size = New Size(160, 740)
        PanelLeft.TabIndex = 5
        ' 
        ' ButtonReports
        ' 
        ButtonReports.Dock = DockStyle.Top
        ButtonReports.FlatAppearance.BorderSize = 0
        ButtonReports.FlatStyle = FlatStyle.Flat
        ButtonReports.ForeColor = Color.White
        ButtonReports.Location = New Point(0, 448)
        ButtonReports.Name = "ButtonReports"
        ButtonReports.Padding = New Padding(12, 0, 0, 0)
        ButtonReports.Size = New Size(160, 48)
        ButtonReports.TabIndex = 0
        ButtonReports.Text = "Reports"
        ButtonReports.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ButtonManageUsers
        ' 
        ButtonManageUsers.Dock = DockStyle.Top
        ButtonManageUsers.FlatAppearance.BorderSize = 0
        ButtonManageUsers.FlatStyle = FlatStyle.Flat
        ButtonManageUsers.ForeColor = Color.White
        ButtonManageUsers.Location = New Point(0, 400)
        ButtonManageUsers.Name = "ButtonManageUsers"
        ButtonManageUsers.Padding = New Padding(12, 0, 0, 0)
        ButtonManageUsers.Size = New Size(160, 48)
        ButtonManageUsers.TabIndex = 1
        ButtonManageUsers.Text = "Manage Users"
        ButtonManageUsers.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ButtonLogOut
        ' 
        ButtonLogOut.Dock = DockStyle.Bottom
        ButtonLogOut.FlatAppearance.BorderSize = 0
        ButtonLogOut.FlatStyle = FlatStyle.Flat
        ButtonLogOut.ForeColor = Color.White
        ButtonLogOut.Location = New Point(0, 692)
        ButtonLogOut.Name = "ButtonLogOut"
        ButtonLogOut.Padding = New Padding(12, 0, 0, 0)
        ButtonLogOut.Size = New Size(160, 48)
        ButtonLogOut.TabIndex = 1
        ButtonLogOut.Text = "Log Out"
        ButtonLogOut.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ButtonOverdues
        ' 
        ButtonOverdues.Dock = DockStyle.Top
        ButtonOverdues.FlatAppearance.BorderSize = 0
        ButtonOverdues.FlatStyle = FlatStyle.Flat
        ButtonOverdues.ForeColor = Color.White
        ButtonOverdues.Location = New Point(0, 304)
        ButtonOverdues.Name = "ButtonOverdues"
        ButtonOverdues.Padding = New Padding(12, 0, 0, 0)
        ButtonOverdues.Size = New Size(160, 48)
        ButtonOverdues.TabIndex = 3
        ButtonOverdues.Text = "Overdues Books"
        ButtonOverdues.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ButtonReturnBooks
        ' 
        ButtonReturnBooks.Dock = DockStyle.Top
        ButtonReturnBooks.FlatAppearance.BorderSize = 0
        ButtonReturnBooks.FlatStyle = FlatStyle.Flat
        ButtonReturnBooks.ForeColor = Color.White
        ButtonReturnBooks.Location = New Point(0, 256)
        ButtonReturnBooks.Name = "ButtonReturnBooks"
        ButtonReturnBooks.Padding = New Padding(12, 0, 0, 0)
        ButtonReturnBooks.Size = New Size(160, 48)
        ButtonReturnBooks.TabIndex = 4
        ButtonReturnBooks.Text = "Return Books"
        ButtonReturnBooks.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ButtonBorrowBooks
        ' 
        ButtonBorrowBooks.Dock = DockStyle.Top
        ButtonBorrowBooks.FlatAppearance.BorderSize = 0
        ButtonBorrowBooks.FlatStyle = FlatStyle.Flat
        ButtonBorrowBooks.ForeColor = Color.White
        ButtonBorrowBooks.Location = New Point(0, 208)
        ButtonBorrowBooks.Name = "ButtonBorrowBooks"
        ButtonBorrowBooks.Padding = New Padding(12, 0, 0, 0)
        ButtonBorrowBooks.Size = New Size(160, 48)
        ButtonBorrowBooks.TabIndex = 5
        ButtonBorrowBooks.Text = "Borrow Books"
        ButtonBorrowBooks.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' PanelContentHost
        ' Hosts Books / Overdue Books / Manage Users "inline" instead of as
        ' separate popup windows -- the matching sidebar button embeds the
        ' screen here and hides SplitContainer1 (the default view) while
        ' it's open. Starts hidden; only one of the two is visible at a time.
        ' 
        PanelContentHost.BackColor = Color.WhiteSmoke
        PanelContentHost.Dock = DockStyle.Fill
        PanelContentHost.Location = New Point(160, 64)
        PanelContentHost.Name = "PanelContentHost"
        PanelContentHost.Size = New Size(1346, 676)
        PanelContentHost.TabIndex = 9
        PanelContentHost.Visible = False
        ' 
        ' main
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1506, 740)
        Controls.Add(PanelContentHost)
        Controls.Add(SplitContainer1)
        Controls.Add(PanelHeader)
        Controls.Add(PanelLeft)
        Name = "main"
        Text = "main"
        PanelReportViewer.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        PanelReportOptions.ResumeLayout(False)
        PanelReportOptions.PerformLayout()
        CType(PictureBoxLogo, ComponentModel.ISupportInitialize).EndInit()
        PanelHeader.ResumeLayout(False)
        PanelHeader.PerformLayout()
        PanelLeft.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Private WithEvents PanelReportContent As Panel
    Private WithEvents PanelReportViewer As Panel
    Private WithEvents SplitContainer1 As SplitContainer
    Private WithEvents PanelReportOptions As Panel
    Private WithEvents LabelStudentInfoTitle As Label
    Private WithEvents LabelIDNumber As Label
    Private WithEvents TextBoxID As TextBox
    Private WithEvents LabelName As Label
    Private WithEvents TextBoxName As TextBox
    Private WithEvents LabelCourse As Label
    Private WithEvents ComboBoxCourse As ComboBox
    Private WithEvents LabelBookInfoTitle As Label
    Private WithEvents LabelBookID As Label
    Private WithEvents TextBoxBookID As TextBox
    Private WithEvents LabelBookTitle As Label
    Private WithEvents TextBoxBookTitle As TextBox
    Private WithEvents LabelAuthor As Label
    Private WithEvents TextBoxAuthor As TextBox
    Private WithEvents ButtonBorrow As Button
    Private WithEvents ButtonReturn As Button
    Private WithEvents PictureBoxLogo As PictureBox
    Private WithEvents LabelTitle As Label
    Private WithEvents PanelHeader As Panel
    Private WithEvents ButtonBooks As Button
    Private WithEvents ButtonBorrower As Button
    Private WithEvents PanelLeft As Panel
    Private WithEvents ButtonReports As Button
    Private WithEvents ButtonManageUsers As Button
    Private WithEvents ButtonLogOut As Button
    Private WithEvents ButtonOverdues As Button
    Private WithEvents ButtonReturnBooks As Button
    Private WithEvents ButtonBorrowBooks As Button
    Private WithEvents ButtonDashboard As Button
    Private WithEvents PanelContentHost As Panel
End Class
