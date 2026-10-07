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
