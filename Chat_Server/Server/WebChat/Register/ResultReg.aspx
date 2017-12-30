<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResultReg.aspx.vb" Inherits="Register_ResultReg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="shortcut icon" href="User-Male.ico" /> 
<link rel="bookmark" href="User-Male.ico" />
    <title></title>
</head>
<body>
    <form id="ResultRegForm" runat="server">
    
    <table align="center">
        <tr>
            <td width="9px"; height="90px";>
                </td>
            <td colspan="2" height="90px">
                </td>
            <td height="90px">
                </td>
        </tr>
        <tr>
            <td width="9px";>
                <img style="width: 35px; height: 38px" id="regimage" runat="server" /></td>
            <td width="241px";>
               <div align="center"> <asp:Label ID="RegisterLabel" runat="server" Text="Label"></asp:Label></div>
            </td>
            <td>
                <div style="font-size: large; font-weight: bold; color: #FF0000; font-family: 黑体;"><asp:Label ID="RegisterResult" runat="server" Text="Label" Font-Size="Large"></asp:Label></div></td>
            <td rowspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="9px";>
                &nbsp;</td>
            <td class="style2" colspan="2">
               <div style="text-decoration: underline; color: #990033; font-size: small; width: 240px;" 
                    align="center"> 
                   <asp:Button ID="CloseWeb" Text="关闭退出" runat="server" Width="102px" />
                </div>
            </td>
        </tr>
        <tr>
            <td width="9px"; height="60px";>
                </td>
            <td colspan="2" height="60px";>
                </td>
            <td height="60px";>
                </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
