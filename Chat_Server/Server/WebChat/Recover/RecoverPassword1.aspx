<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RecoverPassword1.aspx.vb" Inherits="RecoverPassword1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="shortcut icon" href="../BackImage/User-Male.ico" /> 
<link rel="bookmark" href="../BackImage/User-Male.ico" />
    <title>CHAT密码管理</title>
    <script type="text/javascript">
        function yearval(year) {
            if (year.value != '') {
                if (Number(year.value) > 2009 || Number(year.value) <= 1890) {
                    window.alert('年份输入有误！');
                    year.value = '';
                    year.focus();
                    return false;
                }
            }
        }
        function OnblurValidate(BoxName) {
            if (BoxName.value != '') {
                if (BoxName.value.length < 6) {
                    window.alert('密码长度为6～15，请正确填写');
                    BoxName.value = '';
                    BoxName.focus();
                    return false;
                }
            }
            var Pwd = window.document.RecoverPwdForm1.NewPwdBox;
            var AgainPwd = window.document.RecoverPwdForm1.ReNewPwdBox;
            if (Pwd.value != '' && AgainPwd.value != '') {
                if (Pwd.value != AgainPwd.value) {
                    window.alert('两次密码输入不一致，请重新输入！');
                    Pwd.value = ''; AgainPwd.value = '';
                    Pwd.focus();
                    return false;
                }
            }
        }
    </script>
</head>
<body>
    <form id="RecoverPwdForm1" runat="server">
    <table style="width:100%; height: 318px;">
    <tr>
      <td>
      <table style="border-width: 2px; border-color: #999999; width:830px; border-bottom-style: solid;" 
              align="center">
        <tr>
         <td width="30px">
                 <img height="60" src="../BackImage/image.png" /></td>
           <td><table style="width:100%; height:23px;">
           <tr><td style="font-family: 微软雅黑; font-size: 14px; 
                   font-weight: bold;  ">
                   CHAT — 密码管理</td></tr>
               </table></td>
          </tr>
      </table></td>      
    </tr>
    <tr>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td>
      <table align="center" style="border: 1px solid #999999; width:700px;">
         <tr>
           <td style="background-color: #CCCCCC; font-size: 14px; font-weight: bolder; height: 23px;">
               找回密码-&gt;通过生日和密码提示问题：</td>
         </tr>   
         <tr>
           <td>&nbsp;</td>
         </tr> 
         <tr>
           <td style="font-size: 14px; font-family: 微软雅黑;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 请回答问题：<asp:Label ID="SecProblem" runat="server" 
                   Text="Label"></asp:Label>
             </td>           
         </tr>  
         <tr>
           <td style="font-size: 14px; height: 5px;">&nbsp;</td>           
         </tr>  
         <tr>
           <td style="font-size: 14px; font-family: 微软雅黑;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 答&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 案：<asp:TextBox 
                   ID="SecProblemAnswer" runat="server" Width="170px" TabIndex="1"></asp:TextBox>
             </td>           
         </tr>  
         <tr>
           <td style="font-size: 14px; height: 5px;">&nbsp;</td>           
         </tr>  
         <tr>
           <td style="font-size: 14px; font-family: 微软雅黑;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 新&nbsp;&nbsp;&nbsp; 密&nbsp;&nbsp; 码：<asp:TextBox 
                   ID="NewPwdBox" runat="server" Width="170px" TabIndex="2" onblur="OnblurValidate(this)"
                   TextMode="Password"></asp:TextBox>
             </td>           
         </tr>  
         <tr>
           <td style="font-size: 14px; height: 5px;">&nbsp;</td>           
         </tr>  
         <tr>
           <td style="font-size: 14px; font-family: 微软雅黑;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 重复新密码：<asp:TextBox ID="ReNewPwdBox" 
                   runat="server" Width="170px" TabIndex="3" TextMode="Password" onblur="OnblurValidate(this)"></asp:TextBox>
             </td>           
         </tr>  
         <tr>
           <td style="font-size: 14px; height: 5px; font-family: 微软雅黑;">&nbsp;</td>
         </tr>
         <tr>
           <td style="font-size: 14px; font-family: 微软雅黑">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
               请输入右边的字符：<asp:TextBox 
                   ID="CheckCodeVal" runat="server" MaxLength="5" Width="70px" TabIndex="4"></asp:TextBox>
                   <img id="CodeImg" onclick="this.src='../Register/CheckCode.aspx'" src="../Register/CheckCode.aspx" />
                                  <input style="cursor:hand;" 
                                         onclick="document.getElementById('CodeImg').src='../Register/CheckCode.aspx?tem='+Math.random();"                                         
                                         type="button" value="刷新验证码" name="Btn_Re" 
                   tabindex="5" /></td>
         </tr>
         <tr>
           <td style="height:60px">           
               <table style="width:100%;">
                   <tr>
                       <td width="140px">&nbsp;</td>
                       <td>
                           <asp:Button ID="NextButton" runat="server" Text="下一步" style="cursor:hand;" 
                               Width="76px" TabIndex="6"/>
                       </td>
                   </tr>
               </table>           
           </td>
         </tr>
         </table></td>
    </tr>
    </table>
    </form>
</body>
</html>
