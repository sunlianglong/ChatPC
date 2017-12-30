Imports System.Data
Imports System.Data.SqlClient
Partial Class RecoverPassword1
    Inherits System.Web.UI.Page
    Dim RecoverUserName As String
    Dim UserProblem(1) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SecProblemAnswer.Focus()
        Application.Lock()
        If (Application("NumberUser") = "") Then
            Response.Redirect("RecoverPassword.aspx")
            Return
        Else
            RecoverUserName = Application("NumberUser").ToString()
            UserProblem = RecoverPwdPro.ValProblem(RecoverUserName, Page)
            SecProblem.Text = UserProblem(0)
        End If
        Application.UnLock()
    End Sub

    Protected Sub NextButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextButton.Click
        If (RecoverInputVal()) Then
            If (ValCodeValidate.ValidateCode(CheckCodeVal, Page)) Then
                If (UserProblem(1) = SecProblemAnswer.Text) Then
                    Try
                        ConnectionData.Link.Open()
                        Dim ModifyVal As Integer = ConnectionData.UserPwdRevision(RecoverUserName, NewPwdBox).ExecuteNonQuery()
                        If (ModifyVal = 1) Then
                            Application("NumberVal") = RecoverUserName
                            Application("RecValidate") = 1
                            Response.Redirect("ResultRecover.aspx")
                        End If
                    Catch ex As Exception
                        CueWindow.Alert(ex.ToString, Page)
                    Finally
                        ConnectionData.Link.Close()
                    End Try
                Else
                    CueWindow.Alert("密码保护问题答案错误！", Page)
                    SecProblemAnswer.Focus()
                End If
            End If
        End If       
    End Sub

    Protected Function RecoverInputVal() As Boolean
        If (SecProblemAnswer.Text.Trim() = "") Then
            CueWindow.Alert("请输入您的密码保护问题答案！", Page)
            SecProblemAnswer.Focus()
            Return False
        ElseIf (NewPwdBox.Text.Trim() = "") Then
            CueWindow.Alert("请输入您的新密码！", Page)
            NewPwdBox.Focus()
            Return False
        ElseIf (ReNewPwdBox.Text.Trim() = "") Then
            CueWindow.Alert("请再次输入您的新密码！", Page)
            ReNewPwdBox.Focus()
            Return False
        ElseIf (NewPwdBox.Text.Length < 6 Or ReNewPwdBox.Text.Length < 6) Then
            CueWindow.Alert("密码长度不能小于6位！", Page)
            NewPwdBox.Focus()
            Return False
        ElseIf (NewPwdBox.Text <> ReNewPwdBox.Text) Then
            CueWindow.Alert("两次密码输入不一致！请重新输入", Page)
            NewPwdBox.Focus()
            Return False
        ElseIf (CheckCodeVal.Text.Trim() = "") Then
            CueWindow.Alert("请输入验证码！", Page)
            CheckCodeVal.Focus()
            Return False
        Else
            Return True
        End If
    End Function
End Class
