' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmReports (Logic)
'
' Implements the 7 report types from the group's spec (Book
' Inventory, Borrowing, Return, Overdue, Member, Most Borrowed,
' Books by Category) behind one shared screen instead of 7
' separate ones: pick a type from cmbReportType, optionally turn
' on the date filter (only meaningful for Borrowing/Return/Most
' Borrowed, since those are the ones with a date to filter on),
' Generate, then Export CSV or Print/Print Preview.
'
' Wired to the sidebar's previously-dead "Reports" button
' (ButtonReports) in main.vb. Built on Kahleb's shared
' Modules/DBConnection.vb, same pattern as the other screens.
' ============================================================

Imports System.Data
Imports System.IO
Imports System.Drawing.Printing
Imports System.Text
Imports System.Linq

Public Class frmReports

    ' Report types that have a meaningful "date" column to filter on.
    ' Book Inventory / Overdue / Member / Books by Category are all
    ' point-in-time snapshots, so the date filter doesn't apply to them.
    Private ReadOnly DateFilterableReports As String() = {
        "Borrowing Report", "Return Report", "Most Borrowed Books"
    }

    Private currentDataTable As DataTable
    Private currentReportTitle As String = ""

    ' Tracks how many rows of currentDataTable have already been printed,
    ' so PrintReportPage can pick up where it left off across pages.
    Private printRowIndex As Integer = 0

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not AuthHelper.IsAdmin Then
            MessageBox.Show("Only admin accounts can view reports.", "Access Denied",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        Dim connMessage As String = ""
        If Not DBConnection.TestConnection(connMessage) Then
            MessageBox.Show(
                "Can't reach the library database yet." & Environment.NewLine & Environment.NewLine &
                connMessage & Environment.NewLine & Environment.NewLine &
                "Make sure MySQL/XAMPP is running and that library_system_db.sql has been imported.",
                "Database Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        cmbReportType.Items.Clear()
        cmbReportType.Items.AddRange(New Object() {
            "Book Inventory Report",
            "Borrowing Report",
            "Return Report",
            "Overdue Report",
            "Member Report",
            "Most Borrowed Books",
            "Books by Category"
        })
        cmbReportType.SelectedIndex = 0
    End Sub

    Private Sub cmbReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReportType.SelectedIndexChanged
        Dim supportsDateFilter As Boolean =
            Array.IndexOf(DateFilterableReports, cmbReportType.SelectedItem?.ToString()) >= 0

        chkUseDateFilter.Enabled = supportsDateFilter
        If Not supportsDateFilter Then
            chkUseDateFilter.Checked = False
        End If
    End Sub

    Private Sub chkUseDateFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseDateFilter.CheckedChanged
        dtpFrom.Enabled = chkUseDateFilter.Checked
        dtpTo.Enabled = chkUseDateFilter.Checked
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        GenerateReport()
    End Sub

    ''' <summary>
    ''' Builds and runs the SQL for whichever report type is selected,
    ''' then loads the result into dgvReport.
    ''' </summary>
    Private Sub GenerateReport()
        Dim reportType As String = cmbReportType.SelectedItem?.ToString()
        If String.IsNullOrEmpty(reportType) Then Return

        Try
            Dim dt As DataTable
            Dim useDates As Boolean = chkUseDateFilter.Enabled AndAlso chkUseDateFilter.Checked
            Dim dateParams As New Dictionary(Of String, Object) From {
                {"@from", dtpFrom.Value.Date},
                {"@to", dtpTo.Value.Date.AddDays(1).AddSeconds(-1)}
            }

            Select Case reportType

                Case "Book Inventory Report"
                    dt = DBConnection.GetDataTable(
                        "SELECT accession_number AS `Accession No.`, isbn AS `ISBN`, title AS `Title`, author AS `Author`, " &
                        "publisher AS `Publisher`, year_published AS `Year Published`, category AS `Category`, " &
                        "copies_total AS `Total Copies`, copies_available AS `Available`, status AS `Status` " &
                        "FROM tbl_books ORDER BY title")

                Case "Borrowing Report"
                    Dim sql As String =
                        "SELECT t.transaction_id AS `ID`, b.accession_number AS `Accession No.`, b.title AS `Title`, " &
                        "m.full_name AS `Borrower`, m.id_number AS `ID Number`, t.date_borrowed AS `Date Borrowed`, " &
                        "t.due_date AS `Due Date`, t.status AS `Status` " &
                        "FROM tbl_transactions t " &
                        "INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                        "INNER JOIN tbl_members m ON t.member_id = m.member_id"
                    If useDates Then sql &= " WHERE t.date_borrowed BETWEEN @from AND @to"
                    sql &= " ORDER BY t.date_borrowed DESC"
                    dt = DBConnection.GetDataTable(sql, If(useDates, dateParams, Nothing))

                Case "Return Report"
                    Dim sql As String =
                        "SELECT t.transaction_id AS `ID`, b.accession_number AS `Accession No.`, b.title AS `Title`, " &
                        "m.full_name AS `Borrower`, t.date_borrowed AS `Date Borrowed`, t.date_returned AS `Date Returned`, " &
                        "t.fine_amount AS `Fine (PHP)`, IF(t.fine_paid = 1, 'Yes', 'No') AS `Fine Paid` " &
                        "FROM tbl_transactions t " &
                        "INNER JOIN tbl_books b ON t.book_id = b.book_id " &
                        "INNER JOIN tbl_members m ON t.member_id = m.member_id " &
                        "WHERE t.date_returned IS NOT NULL"
                    If useDates Then sql &= " AND t.date_returned BETWEEN @from AND @to"
                    sql &= " ORDER BY t.date_returned DESC"
                    dt = DBConnection.GetDataTable(sql, If(useDates, dateParams, Nothing))

                Case "Overdue Report"
                    dt = DBConnection.GetDataTable(
                        "SELECT accession_number AS `Accession No.`, title AS `Title`, borrower_name AS `Borrower`, " &
                        "id_number AS `ID Number`, due_date AS `Due Date`, days_overdue AS `Days Overdue`, " &
                        "computed_fine AS `Fine (PHP)` FROM vw_overdue_books ORDER BY days_overdue DESC")

                Case "Member Report"
                    dt = DBConnection.GetDataTable(
                        "SELECT id_number AS `ID Number`, full_name AS `Full Name`, course AS `Course`, " &
                        "year_level AS `Year Level`, contact_number AS `Contact No.`, email AS `Email`, " &
                        "address AS `Address`, membership_status AS `Membership Status`, " &
                        "date_registered AS `Date Registered` FROM tbl_members WHERE role = 'member' ORDER BY full_name")

                Case "Most Borrowed Books"
                    Dim sql As String =
                        "SELECT b.accession_number AS `Accession No.`, b.title AS `Title`, b.author AS `Author`, " &
                        "COUNT(t.transaction_id) AS `Times Borrowed` " &
                        "FROM tbl_transactions t INNER JOIN tbl_books b ON t.book_id = b.book_id"
                    If useDates Then sql &= " WHERE t.date_borrowed BETWEEN @from AND @to"
                    sql &= " GROUP BY b.book_id, b.accession_number, b.title, b.author ORDER BY `Times Borrowed` DESC"
                    dt = DBConnection.GetDataTable(sql, If(useDates, dateParams, Nothing))

                Case "Books by Category"
                    dt = DBConnection.GetDataTable(
                        "SELECT COALESCE(category, '(Uncategorized)') AS `Category`, COUNT(*) AS `Titles`, " &
                        "SUM(copies_total) AS `Total Copies`, SUM(copies_available) AS `Available Copies` " &
                        "FROM tbl_books GROUP BY category ORDER BY `Category`")

                Case Else
                    Return
            End Select

            currentDataTable = dt
            currentReportTitle = reportType
            dgvReport.DataSource = dt

            If dgvReport.Columns.Contains("Fine (PHP)") Then
                dgvReport.Columns("Fine (PHP)").DefaultCellStyle.Format = "N2"
            End If

            UpdateSummary(dt)

        Catch ex As Exception
            MessageBox.Show("Unable to generate report." & Environment.NewLine & ex.Message,
                             "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateSummary(dt As DataTable)
        Dim text As String = $"{dt.Rows.Count} record(s) — generated {DateTime.Now:MMM d, yyyy h:mm tt}"

        If dt.Columns.Contains("Fine (PHP)") Then
            Dim totalFines As Decimal = 0D
            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("Fine (PHP)")) Then totalFines += Convert.ToDecimal(row("Fine (PHP)"))
            Next
            text &= $" | Total fines: PHP {totalFines:N2}"
        End If

        lblReportSummary.Text = text
    End Sub

    ''' <summary>
    ''' Exports whatever is currently shown in dgvReport to a CSV file the
    ''' user picks a save location for.
    ''' </summary>
    Private Sub btnExportCsv_Click(sender As Object, e As EventArgs) Handles btnExportCsv.Click
        If currentDataTable Is Nothing OrElse currentDataTable.Rows.Count = 0 Then
            MessageBox.Show("Generate a report first.", "Nothing to Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Filter = "CSV files (*.csv)|*.csv"
            sfd.FileName = currentReportTitle.Replace(" ", "_") & "_" & DateTime.Now.ToString("yyyyMMdd_HHmm") & ".csv"

            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    WriteCsv(sfd.FileName, currentDataTable)
                    MessageBox.Show("Report exported to:" & Environment.NewLine & sfd.FileName,
                                     "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Failed to export report." & Environment.NewLine & ex.Message,
                                     "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Plain CSV writer: quotes any field containing a comma, quote, or
    ''' newline, doubling embedded quotes per the usual CSV convention.
    ''' </summary>
    Private Sub WriteCsv(path As String, dt As DataTable)
        Using writer As New StreamWriter(path, False, Encoding.UTF8)
            writer.WriteLine(String.Join(",", dt.Columns.Cast(Of DataColumn)().Select(Function(c) CsvField(c.ColumnName))))

            For Each row As DataRow In dt.Rows
                Dim fields = dt.Columns.Cast(Of DataColumn)().Select(
                    Function(c) CsvField(If(IsDBNull(row(c)), "", row(c).ToString())))
                writer.WriteLine(String.Join(",", fields))
            Next
        End Using
    End Sub

    Private Function CsvField(value As String) As String
        If value.Contains(",") OrElse value.Contains("""") OrElse value.Contains(vbCr) OrElse value.Contains(vbLf) Then
            Return """" & value.Replace("""", """""") & """"
        End If
        Return value
    End Function

    ''' <summary>
    ''' Renders the current report as a simple paginated table and opens
    ''' it in a print preview window (from which the user can also print).
    ''' </summary>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If currentDataTable Is Nothing OrElse currentDataTable.Rows.Count = 0 Then
            MessageBox.Show("Generate a report first.", "Nothing to Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim pd As New PrintDocument()
        pd.DocumentName = currentReportTitle
        AddHandler pd.BeginPrint, Sub(s As Object, ev As PrintEventArgs) printRowIndex = 0
        AddHandler pd.PrintPage, AddressOf PrintReportPage

        Using preview As New PrintPreviewDialog()
            preview.Document = pd
            preview.Width = 900
            preview.Height = 700
            preview.StartPosition = FormStartPosition.CenterParent
            preview.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Draws one printed page of the current report: a title/subtitle on
    ''' the first page, a column header row on every page, then as many
    ''' data rows as fit before spilling onto the next page.
    ''' </summary>
    Private Sub PrintReportPage(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim titleFont As New Font("Segoe UI", 14, FontStyle.Bold)
        Dim subtitleFont As New Font("Segoe UI", 9, FontStyle.Italic)
        Dim headerFont As New Font("Segoe UI", 8, FontStyle.Bold)
        Dim cellFont As New Font("Segoe UI", 8)
        Dim sf As New StringFormat() With {.Trimming = StringTrimming.EllipsisCharacter, .FormatFlags = StringFormatFlags.NoWrap}

        Dim leftMargin As Single = e.MarginBounds.Left
        Dim rightMargin As Single = e.MarginBounds.Right
        Dim y As Single = e.MarginBounds.Top

        If printRowIndex = 0 Then
            g.DrawString("Fisher Valley College Library", titleFont, Brushes.Black, leftMargin, y)
            y += 26
            g.DrawString(currentReportTitle & " — generated " & DateTime.Now.ToString("MMM d, yyyy h:mm tt"),
                         subtitleFont, Brushes.DimGray, leftMargin, y)
            y += 24
        End If

        Dim colCount As Integer = currentDataTable.Columns.Count
        Dim colWidth As Single = (rightMargin - leftMargin) / colCount

        ' Column header row (redrawn on every page).
        Dim x As Single = leftMargin
        For Each col As DataColumn In currentDataTable.Columns
            g.DrawString(col.ColumnName, headerFont, Brushes.Black, New RectangleF(x, y, colWidth - 2, 18), sf)
            x += colWidth
        Next
        y += 18
        g.DrawLine(Pens.Black, leftMargin, y, rightMargin, y)
        y += 4

        Const rowHeight As Single = 16
        While printRowIndex < currentDataTable.Rows.Count
            If y + rowHeight > e.MarginBounds.Bottom Then
                e.HasMorePages = True
                Return
            End If

            Dim row As DataRow = currentDataTable.Rows(printRowIndex)
            x = leftMargin
            For c As Integer = 0 To colCount - 1
                Dim text As String = If(IsDBNull(row(c)), "", row(c).ToString())
                g.DrawString(text, cellFont, Brushes.Black, New RectangleF(x, y, colWidth - 4, rowHeight), sf)
                x += colWidth
            Next

            y += rowHeight
            printRowIndex += 1
        End While

        e.HasMorePages = False
    End Sub

End Class
