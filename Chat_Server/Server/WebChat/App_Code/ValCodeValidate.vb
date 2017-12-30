Imports Microsoft.VisualBasic

Public Class ValCodeValidate
    Public Shared Function ValidateCode(ByVal CodeValBox As TextBox, ByVal Page As Page) As Boolean
        Dim strCode As Object
        strCode = HttpContext.Current.Session("CheckCode")
        Dim userInputValue As String = CodeValBox.Text
        Dim ValCodeResult As Integer
        ValCodeResult = String.Compare(strCode, userInputValue, True)
        If (ValCodeResult = 0) Then
            Return True
        Else
            CueWindow.Alert("验证码输入有误！", Page)
            CodeValBox.Text = ""
            CodeValBox.Focus()
            Return False
        End If
    End Function
End Class
