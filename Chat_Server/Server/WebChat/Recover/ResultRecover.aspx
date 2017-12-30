<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResultRecover.aspx.vb" Inherits="Register_ResultRecover" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="shortcut icon" href="User-Male.ico" /> 
<link rel="bookmark" href="User-Male.ico" />
    <title></title>
</head>
<body>
    <form id="Recoverform" runat="server">
    
    <table align="center" height: 268px;">
        <tr>
            <td height="90" width="9">
                </td>
            <td colspan="2" height="90">
                </td>
            <td height="90">
                </td>
        </tr>
        <tr>
            <td width="9">
                <img style="width: 35px; height: 38px" id="SuccessImage" runat="server" /></td>
            <td>
                <asp:Label ID="RegisterResult" runat="server" Text="Label" Font-Size="Small" 
                    ForeColor="Red"></asp:Label>
            </td>
            <td width="241">
                <asp:Label ID="RegisterLabel" runat="server" Text="Label" Font-Size="Small"></asp:Label></td>
            <td rowspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="9">
                &nbsp;</td>
            <td colspan="2" width="37">
               <div style="text-decoration: underline; color: #990033; font-size: small; width: 240px;" 
                    align="center"> 
                   <asp:Button ID="CloseWeb" Text="关闭" runat="server" Width="102px" style="cursor:hand;"/>
                </div>
            </td>
        </tr>
        <tr>
            <td width="9">
                </td>
            <td colspan="2" height="60">
                &nbsp;</td>
            <td>
                </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
