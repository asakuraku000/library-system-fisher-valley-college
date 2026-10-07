' ============================================================
' LIBRARY SYSTEM IN FISHER VALLEY COLLEGE
' Form: frmMemberDashboard (Designer / layout)
'
' Intentionally tiny: the header, sidebar and content host are
' built in code (see BuildShell in frmMemberDashboard.vb), the same
' way Pages\pgLibrary.vb is, so there is nothing to keep in sync.
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMemberDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        SuspendLayout()
        '
        ' frmMemberDashboard
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1280, 720)
        Font = New Font("Segoe UI", 9F)
        MinimumSize = New Size(1000, 600)
        Name = "frmMemberDashboard"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fisher Valley College Library - Member"
        ResumeLayout(False)
    End Sub

End Class
