' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Module: AuditHelper
'
' Purpose: Centralized audit logging for tracking user actions
' ============================================================

Imports System.Data

Module AuditHelper

    ''' <summary>
    ''' Logs an action to the audit trail
    ''' </summary>
    ''' <param name="userId">ID of the user performing the action</param>
    ''' <param name="username">Username of the user performing the action</param>
    ''' <param name="action">Action being performed (LOGIN, LOGOUT, CREATE_BOOK, etc.)</param>
    ''' <param name="tableAffected">Table affected by the action (optional)</param>
    ''' <param name="recordId">ID of the record affected (optional)</param>
    ''' <param name="details">Additional details about the action (optional)</param>
    ''' <param name="ipAddress">IP address of the user (optional)</param>
    Public Sub LogAction(userId As Integer, username As String, action As String, 
                        Optional tableAffected As String = "", 
                        Optional recordId As Integer = 0,
                        Optional details As String = "",
                        Optional ipAddress As String = "")
        Try
            Dim sql As String = "
                INSERT INTO tbl_audit_log (user_id, username, action, table_affected, record_id, details, ip_address)
                VALUES (@uid, @uname, @action, @table, @rid, @details, @ip)"
                
            Dim params As New Dictionary(Of String, Object) From {
                {"@uid", userId},
                {"@uname", username},
                {"@action", action},
                {"@table", If(String.IsNullOrEmpty(tableAffected), Nothing, tableAffected)},
                {"@rid", If(recordId = 0, Nothing, recordId)},
                {"@details", If(String.IsNullOrEmpty(details), Nothing, details)},
                {"@ip", If(String.IsNullOrEmpty(ipAddress), Nothing, ipAddress)}
            }
            
            DBConnection.ExecuteNonQuery(sql, params)
        Catch ex As Exception
            ' Don't let audit logging failures break the main functionality
            ' In a production system, you might want to log this to a file or event log
            Debug.WriteLine($"Audit logging failed: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Gets the current IP address of the user
    ''' </summary>
    Public Function GetIPAddress() As String
        Try
            Dim strHostName As String = System.Net.Dns.GetHostName()
            Dim ipHostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)
            Dim ipAddress As System.Net.IPAddress() = ipHostInfo.AddressList
            
            ' Return the first IPv4 address
            For Each ip As System.Net.IPAddress In ipAddress
                If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
            
            ' If no IPv4 found, return the first address
            If ipAddress.Length > 0 Then
                Return ipAddress(0).ToString()
            End If
        Catch ex As Exception
            ' Return localhost if we can't determine the IP
            Return "127.0.0.1"
        End Try
        
        Return "127.0.0.1"
    End Function

End Module