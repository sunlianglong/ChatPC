
Partial Class Register_ResultReg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Application.Lock()
        If (Application("UserNumber") = 0 Or Application("Validate") = 0) Then
            Response.Redirect("Reg.aspx")
            Return
        Else
            Dim UserNumber As Integer = CInt(Application("UserNumber"))
            Dim Validate As Integer = CInt(Application("Validate"))
            If (Validate = 1) Then
                Me.Title = "[注册成功]"
                regimage.Src = "image/success.gif"
                RegisterLabel.Text = "恭喜您！注册成功，您的号码为："
                RegisterResult.Text = UserNumber.ToString()
            Else
                Response.Redirect("Reg.aspx")
            End If
        End If
        Application("UserNumber") = 0
        Application("Validate") = 0
        Application.UnLock()
        CloseWeb.Attributes.Add("onclick", "window.close();")
    End Sub
End Class
