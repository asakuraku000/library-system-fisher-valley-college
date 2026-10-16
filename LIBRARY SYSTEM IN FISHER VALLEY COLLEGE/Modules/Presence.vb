' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Module: Presence
'
' Purpose: "Active now / Active 1 min ago" for every user, like
' Messenger, so the admin can see who is online and when each
' user was last active. Also records the last login time.
'
'   StartSession()  - called right after a successful login: saves
'                     the login time and starts a 30-second heartbeat.
'   Touch()         - heartbeat; refreshes tbl_members.last_active.
'   EndSession()    - called on logout: stops the heartbeat and marks
'                     the user offline (last_active = logout time).
'
' Columns used (added automatically by DBConnection.EnsureSchema):
'   tbl_members.last_login, tbl_members.last_active, tbl_members.is_online
' ============================================================

Imports System.Data

Public Module Presence

    ' A user who is flagged online AND was seen within this many seconds shows "Active now".
    Private Const ONLINE_WINDOW_SECONDS As Long = 90

    Private ReadOnly heartbeat As New System.Windows.Forms.Timer() With {.Interval = 30000}
    Private heartbeatHooked As Boolean = False

    ''' <summary>Extra SELECT columns that AddStatusColumns turns into "Last Active" and "Last Login".</summary>
    Public Const SqlStatusColumns As String =
        "is_online AS `_online`, TIMESTAMPDIFF(SECOND, last_active, NOW()) AS `_secs`, last_login AS `_login`"

    Public Sub StartSession()
        Try
            DBConnection.EnsureSchema()
            If AuthHelper.CurrentMemberId > 0 Then
                DBConnection.ExecuteNonQuery(
                    "UPDATE tbl_members SET last_login = NOW(), last_active = NOW(), is_online = 1 WHERE member_id = @id",
                    New Dictionary(Of String, Object) From {{"@id", AuthHelper.CurrentMemberId}})
            End If
        Catch
            ' Presence must never get in the user's way.
        End Try

        If Not heartbeatHooked Then
            AddHandler heartbeat.Tick, AddressOf OnHeartbeat
            heartbeatHooked = True
        End If
        heartbeat.Start()
    End Sub

    Public Sub Touch()
        Try
            If AuthHelper.CurrentMemberId <= 0 Then Return
            DBConnection.ExecuteNonQuery(
                "UPDATE tbl_members SET last_active = NOW(), is_online = 1 WHERE member_id = @id",
                New Dictionary(Of String, Object) From {{"@id", AuthHelper.CurrentMemberId}})
        Catch
        End Try
    End Sub

    Public Sub EndSession()
        heartbeat.Stop()
        Try
            If AuthHelper.CurrentMemberId <= 0 Then Return
            DBConnection.ExecuteNonQuery(
                "UPDATE tbl_members SET last_active = NOW(), is_online = 0 WHERE member_id = @id",
                New Dictionary(Of String, Object) From {{"@id", AuthHelper.CurrentMemberId}})
        Catch
        End Try
    End Sub

    Private Sub OnHeartbeat(sender As Object, e As EventArgs)
        Touch()
    End Sub

    ''' <summary>Turns "online flag + seconds since last activity" into text like "Active 1 min ago".</summary>
    Public Function FormatStatus(isOnline As Object, secondsAgo As Object) As String
        If secondsAgo Is Nothing OrElse IsDBNull(secondsAgo) Then Return "Never logged in"

        Dim s As Long = Convert.ToInt64(secondsAgo)
        If s < 0 Then s = 0

        Dim online As Boolean = (isOnline IsNot Nothing AndAlso Not IsDBNull(isOnline) AndAlso Convert.ToInt32(isOnline) = 1)
        If online AndAlso s <= ONLINE_WINDOW_SECONDS Then Return "Active now"

        Dim mins As Long = s \ 60
        If mins < 1 Then Return "Active 1 min ago"
        If mins < 60 Then Return "Active " & mins & " min ago"

        Dim hrs As Long = mins \ 60
        If hrs < 24 Then Return "Active " & hrs & If(hrs = 1, " hr ago", " hrs ago")

        Dim days As Long = hrs \ 24
        Return "Active " & days & If(days = 1, " day ago", " days ago")
    End Function

    ''' <summary>
    ''' Converts the helper columns (_online, _secs, _login from SqlStatusColumns)
    ''' into two readable columns, "Last Active" and "Last Login", and removes the helpers.
    ''' </summary>
    Public Sub AddStatusColumns(dt As DataTable)
        If dt Is Nothing Then Return
        If Not dt.Columns.Contains("_online") OrElse Not dt.Columns.Contains("_secs") Then Return

        Dim colActive As New DataColumn("Last Active", GetType(String))
        Dim colLogin As New DataColumn("Last Login", GetType(String))
        dt.Columns.Add(colActive)
        dt.Columns.Add(colLogin)

        For Each r As DataRow In dt.Rows
            r("Last Active") = FormatStatus(r("_online"), r("_secs"))

            If dt.Columns.Contains("_login") AndAlso Not IsDBNull(r("_login")) Then
                r("Last Login") = Convert.ToDateTime(r("_login")).ToString("MMM d, yyyy h:mm tt")
            Else
                r("Last Login") = "-"
            End If
        Next

        dt.Columns.Remove("_online")
        dt.Columns.Remove("_secs")
        If dt.Columns.Contains("_login") Then dt.Columns.Remove("_login")
    End Sub

End Module
