' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Module: ActivityLog
'
' Purpose: One shared place that records what users do (log in,
' log out, borrow / return, add / edit / delete ...) into
' tbl_activity_log, so the admin can review it in the Activity Log
' screen. Writing a log entry NEVER interrupts the user - if the
' database is busy or the write fails, it is silently skipped.
'
' Usage:  ActivityLog.Write("Delete User", "Deleted account ""juan""")
' ============================================================

Public Module ActivityLog

    ''' <summary>Logs an action for whoever is currently logged in.</summary>
    Public Sub Write(action As String, Optional details As String = "")
        WriteAs(AuthHelper.CurrentMemberId, AuthHelper.CurrentUsername,
                AuthHelper.CurrentFullName, AuthHelper.CurrentRole, action, details)
    End Sub

    ''' <summary>Logs an action for a specific user (e.g. a failed login or a new sign-up).</summary>
    Public Sub WriteAs(memberId As Integer, username As String, fullName As String,
                       role As String, action As String, details As String)
        Try
            DBConnection.EnsureSchema()

            If details IsNot Nothing AndAlso details.Length > 480 Then
                details = details.Substring(0, 480)
            End If

            DBConnection.ExecuteNonQuery(
                "INSERT INTO tbl_activity_log (member_id, username, full_name, role, action, details) " &
                "VALUES (@mid, @user, @name, @role, @action, @details)",
                New Dictionary(Of String, Object) From {
                    {"@mid", If(memberId > 0, CObj(memberId), CObj(DBNull.Value))},
                    {"@user", If(String.IsNullOrEmpty(username), CObj(DBNull.Value), CObj(username))},
                    {"@name", If(String.IsNullOrEmpty(fullName), CObj(DBNull.Value), CObj(fullName))},
                    {"@role", If(String.IsNullOrEmpty(role), CObj(DBNull.Value), CObj(role))},
                    {"@action", action},
                    {"@details", If(String.IsNullOrEmpty(details), CObj(DBNull.Value), CObj(details))}
                })
        Catch
            ' Logging must never get in the user's way.
        End Try
    End Sub

End Module
