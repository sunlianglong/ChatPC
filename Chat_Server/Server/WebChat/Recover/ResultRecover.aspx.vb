
Partial Class Register_ResultRecover
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Application.Lock()
        If (Application("NumberVal") = "" Or Application("RecValidate") = 0) Then
            Response.Redirect("Reg.aspx")
            Return
        Else
            Dim UserNumber As String = Application("NumberVal")
            Dim Validate As Integer = CInt(Application("RecValidate"))
            If (Validate = 1) Then
                Me.Title = "[修改成功]"
                SuccessImage.Src = "../Register/image/success.gif"
                RegisterResult.Text = UserNumber
                RegisterLabel.Text = "恭喜您！密码修改成功，点击下边按钮关闭本页面"
            Else
                Response.Redirect("Reg.aspx")
            End If
        End If
        Application("RegName") = ""
        Application("Validate") = 0
        Application.UnLock()
        CloseWeb.Attributes.Add("onclick", "window.close();")
    End Sub
End Class
