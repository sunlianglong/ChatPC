Imports Microsoft.VisualBasic

Public Class CueWindow
    Public Shared Sub Alert(ByVal MessageCue As String, ByVal WebPage As Page)
        Dim StrScript As String
        StrScript = ("<script language=javascript>")
        StrScript += ("alert('" + MessageCue + "');")
        StrScript += ("</script>")
        WebPage.ClientScript.RegisterStartupScript(WebPage.GetType(), "MsgBox", StrScript)
    End Sub
End Class
