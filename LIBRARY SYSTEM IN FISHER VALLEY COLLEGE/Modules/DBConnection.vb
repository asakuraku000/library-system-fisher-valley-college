' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Module: DBConnection
' Part: Kahleb — Database Tables / Connections
'
' Purpose: One shared place for the MySQL connection so every
' teammate's form calls the same functions instead of writing
' their own connection code.
'
' Setup:
'   1. Tools > NuGet Package Manager > Manage NuGet Packages for
'      Solution > install "MySql.Data" (official Oracle connector).
'   2. Run Database/library_system_db.sql in phpMyAdmin or
'      MySQL Workbench first.
'   3. Update the constants below if your XAMPP setup differs
'      (e.g. you set a root password).
' ============================================================

Imports System.Data
Imports MySql.Data.MySqlClient

Module DBConnection

    ' --- Update these to match your local MySQL setup ---
    Private Const SERVER As String = "localhost"
    Private Const PORT As String = "3306"
    Private Const DATABASE_NAME As String = "fvc_library_system"
    Private Const USER_ID As String = "root"
    Private Const PASSWORD As String = ""   ' default XAMPP MySQL has no password

    Private ReadOnly ConnString As String =
        $"Server={SERVER};Port={PORT};Database={DATABASE_NAME};Uid={USER_ID};Pwd={PASSWORD};"

    Private _memberColumnsChecked As Boolean = False

    ''' <summary>
    ''' Makes sure tbl_members has the columns the app needs (account_type,
    ''' address, membership_status). Older copies of the database (e.g. the
    ''' fvc_library_system.sql dump) were created without them, which caused
    ''' "Unknown column 'account_type' in 'field list'" when registering.
    ''' Safe to call any number of times - it only adds what is missing.
    ''' </summary>
    Public Sub EnsureMemberColumns()
        If _memberColumnsChecked Then Return

        Dim needed As New Dictionary(Of String, String) From {
            {"account_type", "ENUM('Student', 'Teacher') NULL"},
            {"address", "VARCHAR(255) NULL"},
            {"membership_status", "ENUM('Active', 'Inactive', 'Suspended') NOT NULL DEFAULT 'Active'"}
        }

        Using conn As MySqlConnection = GetConnection()
            conn.Open()

            Dim existing As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            Using cmd As New MySqlCommand(
                "SELECT COLUMN_NAME FROM information_schema.COLUMNS " &
                "WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tbl_members'", conn)
                Using rd As MySqlDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        existing.Add(rd.GetString(0))
                    End While
                End Using
            End Using

            For Each kvp In needed
                If Not existing.Contains(kvp.Key) Then
                    Using alter As New MySqlCommand(
                        "ALTER TABLE tbl_members ADD COLUMN " & kvp.Key & " " & kvp.Value, conn)
                        alter.ExecuteNonQuery()
                    End Using

                    If kvp.Key = "account_type" Then
                        Using upd As New MySqlCommand(
                            "UPDATE tbl_members SET account_type = 'Student' " &
                            "WHERE role = 'member' AND account_type IS NULL", conn)
                            upd.ExecuteNonQuery()
                        End Using
                    End If
                End If
            Next
        End Using

        _memberColumnsChecked = True
    End Sub

    Private _schemaChecked As Boolean = False

    ''' <summary>
    ''' Brings an older copy of the database up to date, automatically and safely
    ''' (it only adds what is missing, so it can run any number of times):
    '''   tbl_books         + price                      (basis of the damaged / lost penalty)
    '''   tbl_transactions  + returned_at, return_condition, penalty_percent,
    '''                       penalty_amount, remarks, processed_by
    '''   tbl_settings      + damaged_penalty_percent / lost_penalty_percent
    '''   tbl_activity_log  new - what users did and when (Activity Log screen)
    '''   tbl_book_history  new - permanent return / penalty history (Book History screen)
    ''' </summary>
    Public Sub EnsureSchema()
        If _schemaChecked Then Return

        Try
            EnsureMemberColumns()
        Catch
            ' handled again when the Create Account screen needs it
        End Try

        Try
            Using conn As MySqlConnection = GetConnection()
                conn.Open()

                AddColumnIfMissing(conn, "tbl_books", "price", "DECIMAL(10,2) NOT NULL DEFAULT 0.00")

                AddColumnIfMissing(conn, "tbl_transactions", "returned_at", "DATETIME NULL")
                AddColumnIfMissing(conn, "tbl_transactions", "return_condition", "ENUM('Good','Damaged','Lost') NULL")
                AddColumnIfMissing(conn, "tbl_transactions", "penalty_percent", "DECIMAL(5,2) NOT NULL DEFAULT 0.00")
                AddColumnIfMissing(conn, "tbl_transactions", "penalty_amount", "DECIMAL(10,2) NOT NULL DEFAULT 0.00")
                AddColumnIfMissing(conn, "tbl_transactions", "remarks", "VARCHAR(255) NULL")
                AddColumnIfMissing(conn, "tbl_transactions", "processed_by", "VARCHAR(100) NULL")

                RunSql(conn,
                    "CREATE TABLE IF NOT EXISTS tbl_settings (" &
                    "setting_key VARCHAR(50) NOT NULL PRIMARY KEY, " &
                    "setting_value VARCHAR(255) NOT NULL, " &
                    "description VARCHAR(255), " &
                    "updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP" &
                    ") ENGINE=InnoDB")

                RunSql(conn,
                    "INSERT IGNORE INTO tbl_settings (setting_key, setting_value, description) VALUES " &
                    "('fine_per_day', '5.00', 'Overdue fine in PHP per day'), " &
                    "('damaged_penalty_percent', '50', 'Penalty for a damaged / torn book, as a percent of the book price'), " &
                    "('lost_penalty_percent', '100', 'Penalty for a lost / not returned book, as a percent of the book price')")

                RunSql(conn,
                    "CREATE TABLE IF NOT EXISTS tbl_activity_log (" &
                    "log_id INT AUTO_INCREMENT PRIMARY KEY, " &
                    "member_id INT NULL, " &
                    "username VARCHAR(50) NULL, " &
                    "full_name VARCHAR(100) NULL, " &
                    "role VARCHAR(20) NULL, " &
                    "action VARCHAR(50) NOT NULL, " &
                    "details VARCHAR(500) NULL, " &
                    "created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, " &
                    "INDEX idx_activity_created (created_at), " &
                    "INDEX idx_activity_member (member_id)" &
                    ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4")

                RunSql(conn,
                    "CREATE TABLE IF NOT EXISTS tbl_book_history (" &
                    "history_id INT AUTO_INCREMENT PRIMARY KEY, " &
                    "transaction_id INT NULL, " &
                    "book_id INT NULL, " &
                    "accession_number VARCHAR(20) NULL, " &
                    "book_title VARCHAR(200) NOT NULL, " &
                    "member_id INT NULL, " &
                    "borrower_name VARCHAR(100) NULL, " &
                    "id_number VARCHAR(20) NULL, " &
                    "date_borrowed DATE NULL, " &
                    "due_date DATE NULL, " &
                    "date_returned DATETIME NULL, " &
                    "return_condition ENUM('Good','Damaged','Lost') NOT NULL DEFAULT 'Good', " &
                    "days_late INT NOT NULL DEFAULT 0, " &
                    "late_fine DECIMAL(10,2) NOT NULL DEFAULT 0.00, " &
                    "book_price DECIMAL(10,2) NOT NULL DEFAULT 0.00, " &
                    "penalty_percent DECIMAL(5,2) NOT NULL DEFAULT 0.00, " &
                    "penalty_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00, " &
                    "total_charge DECIMAL(10,2) NOT NULL DEFAULT 0.00, " &
                    "remarks VARCHAR(255) NULL, " &
                    "processed_by VARCHAR(100) NULL, " &
                    "recorded_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, " &
                    "UNIQUE KEY uq_history_transaction (transaction_id), " &
                    "INDEX idx_history_returned (date_returned)" &
                    ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4")

                ' Books that were already returned before this update get a history row too.
                RunSql(conn, HistorySnapshotSql(False))
            End Using

            _schemaChecked = True
        Catch
            ' If this fails (e.g. MySQL is off), the screens show their own database messages.
        End Try
    End Sub

    ''' <summary>The INSERT ... SELECT that copies a returned loan into tbl_book_history.</summary>
    Private Function HistorySnapshotSql(forOneTransaction As Boolean) As String
        Dim sql As String =
            "INSERT IGNORE INTO tbl_book_history (transaction_id, book_id, accession_number, book_title, " &
            "member_id, borrower_name, id_number, date_borrowed, due_date, date_returned, return_condition, " &
            "days_late, late_fine, book_price, penalty_percent, penalty_amount, total_charge, remarks, processed_by) " &
            "SELECT t.transaction_id, t.book_id, b.accession_number, b.title, " &
            "t.member_id, m.full_name, m.id_number, t.date_borrowed, t.due_date, " &
            "COALESCE(t.returned_at, t.date_returned), COALESCE(t.return_condition, 'Good'), " &
            "GREATEST(DATEDIFF(t.date_returned, t.due_date), 0), t.fine_amount, b.price, " &
            "t.penalty_percent, t.penalty_amount, t.fine_amount + t.penalty_amount, t.remarks, t.processed_by " &
            "FROM tbl_transactions t " &
            "INNER JOIN tbl_books b ON t.book_id = b.book_id " &
            "INNER JOIN tbl_members m ON t.member_id = m.member_id " &
            "WHERE t.date_returned IS NOT NULL " &
            "AND NOT EXISTS (SELECT 1 FROM tbl_book_history h WHERE h.transaction_id = t.transaction_id)"

        If forOneTransaction Then sql &= " AND t.transaction_id = @tid"
        Return sql
    End Function

    ''' <summary>
    ''' Saves a snapshot of a just-returned (or lost) loan into tbl_book_history, so the
    ''' return date, condition and penalty stay on record even if the book or the
    ''' borrower is changed or deleted later.
    ''' </summary>
    Public Sub RecordBookHistory(transactionId As Integer)
        Try
            EnsureSchema()
            ExecuteNonQuery(HistorySnapshotSql(True),
                            New Dictionary(Of String, Object) From {{"@tid", transactionId}})
        Catch
            ' The return itself is already saved; the history row is picked up on the next start-up check.
        End Try
    End Sub

    ''' <summary>Reads a value from tbl_settings, or returns the fallback if it isn't there.</summary>
    Public Function ReadSetting(key As String, fallback As String) As String
        Try
            Dim dt As DataTable = GetDataTable(
                "SELECT setting_value FROM tbl_settings WHERE setting_key = @k",
                New Dictionary(Of String, Object) From {{"@k", key}})
            If dt.Rows.Count > 0 AndAlso Not IsDBNull(dt.Rows(0)(0)) Then
                Return dt.Rows(0)(0).ToString()
            End If
        Catch
        End Try
        Return fallback
    End Function

    Private Sub AddColumnIfMissing(conn As MySqlConnection, tableName As String, columnName As String, definition As String)
        Dim found As Boolean
        Using cmd As New MySqlCommand(
            "SELECT COUNT(*) FROM information_schema.COLUMNS " &
            "WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @t AND COLUMN_NAME = @c", conn)
            cmd.Parameters.AddWithValue("@t", tableName)
            cmd.Parameters.AddWithValue("@c", columnName)
            found = Convert.ToInt32(cmd.ExecuteScalar()) > 0
        End Using

        If Not found Then
            RunSql(conn, "ALTER TABLE " & tableName & " ADD COLUMN " & columnName & " " & definition)
        End If
    End Sub

    Private Sub RunSql(conn As MySqlConnection, sql As String)
        Using cmd As New MySqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ''' <summary>
    ''' Returns a new, unopened MySqlConnection using the shared connection string.
    ''' Callers are responsible for opening/closing (or use the helpers below).
    ''' </summary>
    Public Function GetConnection() As MySqlConnection
        Return New MySqlConnection(ConnString)
    End Function

    ''' <summary>
    ''' Quick connectivity check — wire this to a "Test Connection" button
    ''' while setting up so the group can confirm MySQL is reachable.
    ''' </summary>
    Public Function TestConnection(ByRef message As String) As Boolean
        Try
            Using conn As New MySqlConnection(ConnString)
                conn.Open()
            End Using
            message = "Connected successfully to '" & DATABASE_NAME & "'."
            Return True
        Catch ex As Exception
            message = "Connection failed: " & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Runs a SELECT query and returns the results as a DataTable.
    ''' Use this to populate DataGridViews (e.g. the Overdue Books list).
    ''' </summary>
    Public Function GetDataTable(sql As String, Optional params As Dictionary(Of String, Object) = Nothing) As DataTable
        Dim dt As New DataTable()
        Using conn As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(sql, conn)
                If params IsNot Nothing Then
                    For Each kvp In params
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value)
                    Next
                End If
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ''' <summary>
    ''' Runs an INSERT / UPDATE / DELETE statement. Returns rows affected.
    ''' Use this for actions like "Mark as Returned".
    ''' </summary>
    Public Function ExecuteNonQuery(sql As String, Optional params As Dictionary(Of String, Object) = Nothing) As Integer
        Using conn As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(sql, conn)
                If params IsNot Nothing Then
                    For Each kvp In params
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value)
                    Next
                End If
                conn.Open()
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

End Module
