Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class RecoverPwdPro
    Public Shared UserPro(1) As String
    Public Shared Function ValProblem(ByVal RecoverPwdUserName As String, ByVal Page As Page) As Object
        Try
            ConnectionData.Link.Open()
            Dim Problem As SqlDataReader = ConnectionData.RecoverPwdProblem(RecoverPwdUserName).ExecuteReader()
            If (Problem.Read()) Then
                UserPro(0) = Problem.GetValue(0).ToString()
                UserPro(1) = Problem.GetValue(1).ToString()
                Return UserPro
            End If
        Catch ex As Exception
            CueWindow.Alert(ex.ToString, Page)
        Finally
            ConnectionData.Link.Close()
        End Try
    End Function
End Class
