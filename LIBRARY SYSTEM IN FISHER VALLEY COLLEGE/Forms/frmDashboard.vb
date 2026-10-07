' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmDashboard (Logic)
'
' Admin landing screen. Replaces the flow where admins went
' straight into Book Management with no overview screen at all.
' Shows four summary cards (Total Books / Total Members /
' Currently Borrowed / Overdue Books), a "Book Status" breakdown
' (available vs. borrowed vs. damaged/lost/archived copies), and
' a tabbed "Recently Overdue" / "Recent Transactions" activity
' feed, all pulled live from the DB via Kahleb's shared
' DBConnection module -- same pattern as frmOverdueBooks and
' frmMemberDashboard.
' ============================================================

Imports System.Data

Public Class frmDashboard

    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connMessage As String = ""
        If Not DBConnection.TestConnection(connMessage) Then
            MessageBox.Show(
                "Can't reach the library database yet." & Environment.NewLine & Environment.NewLine &
                connMessage & Environment.NewLine & Environment.NewLine &
                "Make sure MySQL/XAMPP is running and that library_system_db.sql has been imported, then click Refresh.",
                "Database Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        LoadDashboard()
    End Sub

    ''' <summary>
    ''' Refreshes every card and the recently-overdue grid. Each piece is
    ''' wrapped separately so one failing query (e.g. a renamed column)
    ''' doesn't blank out the rest of the dashboard.
    ''' </summary>
    Private Sub LoadDashboard()
        LoadTotalBooks()
        LoadTotalMembers()
        LoadCurrentlyBorrowed()
        LoadOverdueCount()
        LoadBookStatus()
        LoadRecentOverdue()
        LoadRecentTransactions()

        lblLastUpdated.Text = "Last updated: " & DateTime.Now.ToString("MMM d, yyyy h:mm tt")
    End Sub

    ''' <summary>
    ''' Total Books card -- count of catalog entries (titles) in tbl_books.
    ''' Matches what's listed in Book Management, not total physical copies.
    ''' </summary>
    Private Sub LoadTotalBooks()
        Try
            Dim dt As DataTable = DBConnection.GetDataTable("SELECT COUNT(*) AS cnt FROM tbl_books")
            lblTotalBooksValue.Text = If(dt.Rows.Count > 0, dt.Rows(0)("cnt").ToString(), "0")
        Catch ex As Exception
            lblTotalBooksValue.Text = "—"
            ShowLoadError("Total Books", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Total Members card -- registered borrowers only (role = 'member'),
    ''' the seeded admin account isn't counted as a "member".
    ''' </summary>
    Private Sub LoadTotalMembers()
        Try
            Dim dt As DataTable = DBConnection.GetDataTable(
                "SELECT COUNT(*) AS cnt FROM tbl_members WHERE role = 'member'")
            lblTotalMembersValue.Text = If(dt.Rows.Count > 0, dt.Rows(0)("cnt").ToString(), "0")
        Catch ex As Exception
            lblTotalMembersValue.Text = "—"
            ShowLoadError("Total Members", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Currently Borrowed card -- open loans (not yet returned), whether
    ''' or not they're overdue. Mirrors the "Return Books" screen's list.
    ''' </summary>
    Private Sub LoadCurrentlyBorrowed()
        Try
            Dim dt As DataTable = DBConnection.GetDataTable(
                "SELECT COUNT(*) AS cnt FROM tbl_transactions WHERE date_returned IS NULL")
            lblCurrentlyBorrowedValue.Text = If(dt.Rows.Count > 0, dt.Rows(0)("cnt").ToString(), "0")
        Catch ex As Exception
            lblCurrentlyBorrowedValue.Text = "—"
            ShowLoadError("Currently Borrowed", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Overdue Books card -- reuses vw_overdue_books so the count always
    ''' matches the Overdue Books screen exactly.
    ''' </summary>
    Private Sub LoadOverdueCount()
        Try
            Dim dt As DataTable = DBConnection.GetDataTable("SELECT COUNT(*) AS cnt FROM vw_overdue_books")
            lblOverdueValue.Text = If(dt.Rows.Count > 0, dt.Rows(0)("cnt").ToString(), "0")
        Catch ex As Exception
            lblOverdueValue.Text = "—"
            ShowLoadError("Overdue Books", ex)
        End Try
    End Sub

    ''' <summary>
    ''' "Book Status" widget -- breakdown of the catalog by tbl_books.status
    ''' (Available / Damaged / Lost / Archived), with total vs. currently
    ''' available vs. borrowed-out copies per group. This is the "available
    ''' vs. borrowed" view the Total Books card alone doesn't show.
    ''' </summary>
    Private Sub LoadBookStatus()
        Try
            Dim sql As String =
                "SELECT status AS `Status`, COUNT(*) AS `Titles`, " &
                "SUM(copies_total) AS `Total Copies`, SUM(copies_available) AS `Available Copies`, " &
                "(SUM(copies_total) - SUM(copies_available)) AS `Borrowed Copies` " &
                "FROM tbl_books " &
                "GROUP BY status " &
                "ORDER BY FIELD(status, 'Available', 'Damaged', 'Lost', 'Archived')"

            Dim dt As DataTable = DBConnection.GetDataTable(sql)
            dgvBookStatus.DataSource = dt

            If dt.Rows.Count = 0 Then
                dgvBookStatus.Columns.Clear()
            End If

        Catch ex As Exception
            ShowLoadError("Book Status", ex)
        End Try
    End Sub

    ''' <summary>
    ''' "Recently Overdue" mini-list -- top 5 most overdue loans, so an
    ''' admin can spot who to follow up with without leaving the dashboard.
    ''' </summary>
    Private Sub LoadRecentOverdue()
        Try
            Dim sql As String =
                "SELECT accession_number AS `Accession No.`, title AS `Book Title`, " &
                "borrower_name AS `Borrower`, due_date AS `Due Date`, " &
                "days_overdue AS `Days Overdue`, computed_fine AS `Fine (PHP)` " &
                "FROM vw_overdue_books ORDER BY days_overdue DESC LIMIT 5"

            Dim dt As DataTable = DBConnection.GetDataTable(sql)
            dgvRecentOverdue.DataSource = dt

            If dgvRecentOverdue.Columns.Contains("Fine (PHP)") Then
                dgvRecentOverdue.Columns("Fine (PHP)").DefaultCellStyle.Format = "N2"
            End If

            If dt.Rows.Count = 0 Then
                dgvRecentOverdue.Columns.Clear()
            End If

        Catch ex As Exception
            ShowLoadError("Recently Overdue", ex)
        End Try
    End Sub

    ''' <summary>
    ''' "Recent Transactions" mini-list -- a generic recent-activity feed
    ''' mixing the latest borrow events and the latest return events
    ''' (unlike "Recently Overdue", which only ever shows late loans).
    ''' </summary>
    Private Sub LoadRecentTransactions()
        Try
            Dim sql As String =
                "SELECT * FROM (" &
                "  SELECT 'Borrowed' AS `Activity`, b.accession_number AS `Accession No.`, b.title AS `Book Title`, " &
                "  m.full_name AS `Member`, t.date_borrowed AS `Date` " &
                "  FROM tbl_transactions t " &
                "  INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                "  INNER JOIN tbl_members m ON t.member_id = m.member_id " &
                "  UNION ALL " &
                "  SELECT 'Returned' AS `Activity`, b.accession_number AS `Accession No.`, b.title AS `Book Title`, " &
                "  m.full_name AS `Member`, t.date_returned AS `Date` " &
                "  FROM tbl_transactions t " &
                "  INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                "  INNER JOIN tbl_members m ON t.member_id = m.member_id " &
                "  WHERE t.date_returned IS NOT NULL " &
                ") AS activity " &
                "ORDER BY `Date` DESC " &
                "LIMIT 10"

            Dim dt As DataTable = DBConnection.GetDataTable(sql)
            dgvRecentTransactions.DataSource = dt

            If dt.Rows.Count = 0 Then
                dgvRecentTransactions.Columns.Clear()
            End If

        Catch ex As Exception
            ShowLoadError("Recent Transactions", ex)
        End Try
    End Sub

    Private Sub ShowLoadError(section As String, ex As Exception)
        MessageBox.Show(
            $"Couldn't load '{section}'." & Environment.NewLine & ex.Message,
            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub btnRefreshDashboard_Click(sender As Object, e As EventArgs) Handles btnRefreshDashboard.Click
        LoadDashboard()
    End Sub

End Class
