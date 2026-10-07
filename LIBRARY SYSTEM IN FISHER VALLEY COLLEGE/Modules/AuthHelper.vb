' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Module: AuthHelper
'
' Purpose: One shared place to hash passwords so login and
' createaccount always hash the same way. SHA-256 is a big
' step up from storing plain text, though for a real production
' system a salted algorithm like bcrypt would be stronger.
' ============================================================

Imports System.Security.Cryptography
Imports System.Text

Module AuthHelper

    Public Function HashPassword(plainText As String) As String
        Using sha As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(plainText))
            Dim sb As New StringBuilder()
            For Each b As Byte In bytes
                sb.Append(b.ToString("x2"))
            Next
            Return sb.ToString()
        End Using
    End Function

    ' --- Logged-in session info ---------------------------------------
    ' Set once by Form1 right after a successful login, then read by
    ' whichever dashboard opens (main = admin, frmMemberDashboard =
    ' member) and by any other form that needs to know who's using the
    ' app. Kept here since AuthHelper is already the shared auth module.
    Public Property CurrentMemberId As Integer = 0
    Public Property CurrentFullName As String = ""
    Public Property CurrentUsername As String = ""
    Public Property CurrentRole As String = ""

    Public ReadOnly Property IsAdmin As Boolean
        Get
            Return String.Equals(CurrentRole, "admin", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    ' Called on logout so a closed session can't leak into the next login.
    Public Sub ClearSession()
        CurrentMemberId = 0
        CurrentFullName = ""
        CurrentUsername = ""
        CurrentRole = ""
    End Sub

End Module
