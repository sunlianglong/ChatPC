Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class ConnectionData
    Public Shared Link As New SqlConnection("Server=192.168.1.6;Database=ChatData;Uid=sa;Pwd=happyday")
    Public Shared CommandSql As Object    
    '用户修改查询
    Public Shared Function UserNameDetection(ByVal NormalUser As TextBox) As Object
        Dim NormalUserLinkMethod As String = "select ID from Users where ID='" & NormalUser.Text & "'"
        Dim CommandUser As New SqlCommand(NormalUserLinkMethod, Link)
        Return CommandUser
    End Function    
    Public Shared Function RecoverPwdDetection(ByVal UserName As TextBox, ByVal BirthDate As String) As Object
        Dim LinkMethod As String = "select ID,BirthDate from Users where ID='" & UserName.Text & "' And  BirthDate='" & BirthDate & "'"
        Dim Command As New SqlCommand(LinkMethod, Link)
        Return Command
    End Function
    Public Shared Function RecoverPwdProblem(ByVal RecoverUser As String) As Object
        Dim LinkMethod As String = "select SecretQuestion,SecretAnswer from Users where ID='" & RecoverUser & "'"
        Dim Command As New SqlCommand(LinkMethod, Link)
        Return Command
    End Function
    Public Shared Function UserPwdRevision(ByVal RecoverUser As String, ByVal NewPassword As TextBox) As Object
        Dim LinkMethod As String = "update Users set userpassword='" & NewPassword.Text & "' where ID='" & RecoverUser & "'"
        Dim Command As New SqlCommand(LinkMethod, Link)
        Return Command
    End Function
End Class
