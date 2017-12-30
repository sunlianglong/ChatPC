Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class Detection
    Public Shared Function UserNameVal(ByVal UserNameInput As TextBox, ByVal BirthDay As String, ByVal yearBox As TextBox, ByVal Page As Page) As Boolean
        Try
            ConnectionData.Link.Open()
            Dim UserName As String = ConnectionData.UserNameDetection(UserNameInput).ExecuteScalar()
            Dim RecoverPwdVal As SqlDataReader = ConnectionData.RecoverPwdDetection(UserNameInput, BirthDay).ExecuteReader()
            If (UserName = UserNameInput.Text) Then
                If (RecoverPwdVal.Read) Then
                    If (RecoverPwdVal.GetValue(0).ToString() = UserNameInput.Text.Trim() And RecoverPwdVal.GetValue(1).ToString() = BirthDay) Then
                        Return True
                    End If
                Else
                    CueWindow.Alert("很遗憾，您的生日信息输入有误！", Page)
                    yearBox.Focus()
                    Return False
                End If
            Else
                CueWindow.Alert("对不起，您输入的CHAT号码不存在！", Page)
            End If
        Catch ex As Exception
            CueWindow.Alert(ex.ToString(), Page)
        Finally
            ConnectionData.Link.Close()
        End Try
    End Function
End Class
