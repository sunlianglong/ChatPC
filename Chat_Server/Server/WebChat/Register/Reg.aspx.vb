Imports System.Data
Imports System.Data.SqlClient
Partial Class Reg
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNameBox.Focus()
    End Sub

    Protected Sub RegButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RegButton.Click
        If (InputVal()) Then
            If (ValCodeValidate.ValidateCode(CodeValBox, Page)) Then
                Try
                    Dim Number As Integer
                    Dim InsertVal As Integer
                    Dim UserInputName As String = UserNameBox.Text.Trim()
                    Dim UserInputPwd As String = Password2.Text.Trim()
                    Dim BirthDay As String = year.Text.Trim() & "-" & Month.SelectedItem.Text & "-" & Day.SelectedItem.Text
                    Dim Sex As String = SexVal()
                    Dim ProtectProblem As String = SecProblem.SelectedItem.Text
                    Dim ProtectAnswer As String = ProblemAnswerBox.Text
                    Dim Adress As String = Country.SelectedItem.Text & Province.SelectedItem.Text & "省" & City.SelectedItem.Text & "市"
                    Dim InsertSql As String
                    InsertSql = "Insert Into Users(NickName,UserPassword,BirthDate,Sex,SecretQuestion,SecretAnswer,Address,FaceId) Values('" & UserInputName & "','" & UserInputPwd & "','" & BirthDay & "','" & Sex & "','" & ProtectProblem & "','" & ProtectAnswer & "','" & Adress & "',0)"
                    Dim InsertCommand As New SqlCommand(InsertSql, ConnectionData.Link)
                    ConnectionData.Link.Open()
                    InsertVal = InsertCommand.ExecuteNonQuery()
                    If (InsertVal = 1) Then
                        InsertSql = "SELECT @@Identity FROM Users"
                        InsertCommand.CommandText = InsertSql
                        Number = Convert.ToInt32(InsertCommand.ExecuteScalar)
                        Me.createTable(Number.ToString())
                        Application("UserNumber") = Number
                        Application("Validate") = 1
                        Response.Redirect("ResultReg.aspx")
                    End If
                Catch ex As Exception
                    CueWindow.Alert(ex.ToString(), Page)
                Finally
                    ConnectionData.Link.Close()
                End Try
            End If
        End If
    End Sub
    Protected Function InputVal() As Boolean
        If (UserNameBox.Text = "") Then
            CueWindow.Alert("请输入您的昵称！", Page)
            UserNameBox.Focus()
            Return False
        ElseIf (Password1.Text = "") Then
            CueWindow.Alert("请输入您的密码！", Page)
            Password1.Focus()
            Return False
        ElseIf (Password2.Text = "") Then
            CueWindow.Alert("请输入您的确认密码！", Page)
            Password2.Focus()
            Return False
        ElseIf (Password1.Text <> Password2.Text) Then
            CueWindow.Alert("两次密码输入不一致，请重新输入！", Page)
            Password1.Focus()
        ElseIf (year.Text = "" Or Month.SelectedIndex = 0 Or Day.SelectedIndex = 0) Then
            CueWindow.Alert("请完善您的生日信息！", Page)
            Return False
        ElseIf (male.Checked = False And female.Checked = False) Then
            CueWindow.Alert("请选择您的性别！", Page)
            Return False
        ElseIf (SecProblem.SelectedIndex = 0) Then
            CueWindow.Alert("请选择您的密码保护问题！", Page)
            SecProblem.Focus()
            Return False
        ElseIf (ProblemAnswerBox.Text = "") Then
            CueWindow.Alert("请输入您的密码保护问题答案！", Page)
            ProblemAnswerBox.Focus()
            Return False
        ElseIf (Country.SelectedIndex = 0 Or Province.SelectedIndex = 0 Or City.SelectedIndex = 0) Then
            CueWindow.Alert("请完善您的所在地信息！", Page)
            Return False
        ElseIf (CodeValBox.Text = "") Then
            CueWindow.Alert("请输入您的验证码！", Page)
            CodeValBox.Focus()
            Return False
        Else
            Return True
        End If
    End Function
    Protected Function SexVal() As String
        Dim Sex As String
        If (male.Checked = True And female.Checked = False) Then
            Sex = male.Text.Trim()
        ElseIf (male.Checked = False And female.Checked = True) Then
            Sex = female.Text.Trim()
        End If
        Return Sex
    End Function
    Protected Sub createTable(ByVal userNumber As String)
        Dim createTable As String
        createTable = "Create Table Friend" & userNumber & "(FriendID varchar(50) NOT NULL,FriendNickName varchar(50) NOT NULL,FriendSex varchar(2) NOT NULL)"
        Dim createCommand As New SqlCommand(createTable, ConnectionData.Link)
        'createCommand.CommandType = CommandType.Text
        createCommand.ExecuteNonQuery()
    End Sub
End Class
