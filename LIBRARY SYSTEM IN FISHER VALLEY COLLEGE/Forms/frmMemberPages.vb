' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Member pages (sidebar > everything except Dashboard)
'
' Every page now lives in its own file and inherits the shared
' shell below (header strip + blank body). The empty ones are
' already linked to their sidebar button in frmMemberDashboard. When you're ready to build one,
' put its controls into pnlBody (Protected, so the page classes
' below can use it) -- or move that page into its own file with
' its own Designer, whichever you prefer.
'
'   Sidebar button      Page class
'   ----------------    ---------------------
'   Browse Books        frmMemberBrowseBooks  (DONE -> its own file, frmMemberBrowseBooks.vb)
'   My Borrowed Books   frmMemberBorrowed  (DONE -> its own file, frmMemberBorrowed.vb)
'   Borrowing History   frmMemberHistory  (DONE -> its own file, frmMemberHistory.vb)
'   My Profile          frmMemberProfile  (DONE -> its own file, frmMemberProfile.vb)
' ============================================================

''' <summary>
''' Shared shell of an empty member page: dark-blue header (title +
''' subtitle, same strip as the Dashboard page) and a blank body.
''' </summary>
Public MustInherit Class frmMemberPageBase
    Inherits Form

    ''' <summary>Blank area under the header -- put the page's controls here.</summary>
    Protected ReadOnly pnlBody As New Panel()

    Private ReadOnly pnlHeader As New Panel()
    Private ReadOnly lblTitle As New Label()
    Private ReadOnly lblSubtitle As New Label()

    Protected Sub New(pageTitle As String, pageSubtitle As String)
        MyBase.New()
        SuspendLayout()

        Text = pageTitle
        Font = New Font("Segoe UI", 9.0F)
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(1000, 700)

        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 64
        pnlHeader.BackColor = Color.FromArgb(17, 78, 128)

        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(18, 10)
        lblTitle.Text = pageTitle

        lblSubtitle.AutoSize = True
        lblSubtitle.Font = New Font("Segoe UI", 9.0F)
        lblSubtitle.ForeColor = Color.WhiteSmoke
        lblSubtitle.Location = New Point(20, 38)
        lblSubtitle.Text = pageSubtitle

        pnlHeader.Controls.Add(lblSubtitle)
        pnlHeader.Controls.Add(lblTitle)

        pnlBody.Dock = DockStyle.Fill
        pnlBody.BackColor = Color.WhiteSmoke
        pnlBody.Padding = New Padding(20)

        ' Fill first, header last (the last control added docks first).
        Controls.Add(pnlBody)
        Controls.Add(pnlHeader)

        ResumeLayout(True)
    End Sub

End Class
