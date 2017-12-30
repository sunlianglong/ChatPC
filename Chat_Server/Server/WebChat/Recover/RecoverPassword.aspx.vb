
Partial Class RecoverPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNameBox.Focus()
    End Sub

    Protected Sub NextButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextButton.Click
        Dim BirthDay As String = year.Text.Trim() & "-" & Month.SelectedItem.Text & "-" & Day.SelectedItem.Text
        If (InputVal()) Then
            If (Detection.UserNameVal(UserNameBox, BirthDay, year, Page)) Then
                Application("NumberUser") = UserNameBox.Text.Trim()
                Response.Redirect("RecoverPassword1.aspx")
            End If
        End If
    End Sub

    Protected Function InputVal() As Boolean
        If (UserNameBox.Text.Trim() = "") Then
            CueWindow.Alert("请输入您的CHAT号码！", Page)
            UserNameBox.Focus()
            Return False
        ElseIf (year.Text.Trim() = "" Or Month.SelectedIndex = 0 Or Day.SelectedIndex = 0) Then
            CueWindow.Alert("请完善您的生日信息！", Page)
            year.Focus()
            Return False
        Else
            Return True
        End If
    End Function
End Class
