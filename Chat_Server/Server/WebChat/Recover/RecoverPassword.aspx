<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RecoverPassword.aspx.vb" Inherits="RecoverPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="shortcut icon" href="User-Male.ico" /> 
<link rel="bookmark" href="User-Male.ico" />
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
    </script>
</head>
<body>
    <form id="RecoverPwdForm" runat="server">
    <table style="width:100%">
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
      <table align="center" style="border: 1px solid #999999; width:700px">
         <tr>
           <td style="background-color: #CCCCCC; font-size: 14px; font-weight: bolder; height: 23px;">
               找回密码-&gt;通过生日和密码提示问题：</td>
         </tr>   
         <tr>
           <td>&nbsp;</td>
         </tr> 
         <tr>
           <td style="font-family: 微软雅黑; font-size: 14px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CHAT号码：<asp:TextBox 
                   ID="UserNameBox" runat="server" Width="150px" TabIndex="1"></asp:TextBox>
             </td>
         </tr> 
         <tr>
           <td style="font-size: 14px">&nbsp;</td>           
         </tr>  
         <tr>
           <td style="font-size: 14px; font-family: 微软雅黑;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 出生日期：<asp:TextBox 
                   ID="year" runat="server" Width="50px" 
                                        MaxLength="4" style="ime-mode:disabled" 
                                        
                   onKeyup="this.value=this.value.replace(/[^0-9]/g,'');" onBlur="yearval(this)" 
                                        TabIndex="2"/>年<asp:DropDownList ID="Month" 
                   runat="server" TabIndex="3">
                                         <asp:ListItem Selected="True">-</asp:ListItem>
                                         <asp:ListItem>01</asp:ListItem>
                                         <asp:ListItem>02</asp:ListItem>
                                         <asp:ListItem>03</asp:ListItem>
                                         <asp:ListItem>04</asp:ListItem>
                                         <asp:ListItem>05</asp:ListItem>
                                         <asp:ListItem>06</asp:ListItem>
                                         <asp:ListItem>07</asp:ListItem>
                                         <asp:ListItem>08</asp:ListItem>
                                         <asp:ListItem>09</asp:ListItem>
                                         <asp:ListItem>10</asp:ListItem>
                                         <asp:ListItem>11</asp:ListItem>
                                         <asp:ListItem>12</asp:ListItem>                    
                                    </asp:DropDownList>月<asp:DropDownList ID="Day" 
                   runat="server" TabIndex="4">
                                         <asp:ListItem Selected="True">-</asp:ListItem>
                                         <asp:ListItem>01</asp:ListItem>
                                         <asp:ListItem>02</asp:ListItem>
                                         <asp:ListItem>03</asp:ListItem>
                                         <asp:ListItem>04</asp:ListItem>
                                         <asp:ListItem>05</asp:ListItem>
                                         <asp:ListItem>06</asp:ListItem>
                                         <asp:ListItem>07</asp:ListItem>
                                         <asp:ListItem>08</asp:ListItem>
                                         <asp:ListItem>09</asp:ListItem>
                                         <asp:ListItem>10</asp:ListItem>
                                         <asp:ListItem>11</asp:ListItem>
                                         <asp:ListItem>12</asp:ListItem>
                                         <asp:ListItem>13</asp:ListItem>
                                         <asp:ListItem>14</asp:ListItem>
                                         <asp:ListItem>15</asp:ListItem>                     
                                         <asp:ListItem>16</asp:ListItem>
                                         <asp:ListItem>17</asp:ListItem>
                                         <asp:ListItem>18</asp:ListItem>
                                         <asp:ListItem>19</asp:ListItem>
                                         <asp:ListItem>20</asp:ListItem>                     
                                         <asp:ListItem>21</asp:ListItem>
                                         <asp:ListItem>22</asp:ListItem>
                                         <asp:ListItem>23</asp:ListItem>
                                         <asp:ListItem>24</asp:ListItem>
                                         <asp:ListItem>25</asp:ListItem>
                                         <asp:ListItem>26</asp:ListItem>
                                         <asp:ListItem>27</asp:ListItem>
                                         <asp:ListItem>28</asp:ListItem>                     
                                         <asp:ListItem>29</asp:ListItem>
                                         <asp:ListItem>30</asp:ListItem> 
                                         <asp:ListItem>31</asp:ListItem>                    
                                   </asp:DropDownList>日</td>
         </tr>
         <tr>
           <td></td>
         </tr>
         <tr>
           <td style="height:60px">           
               <table style="width:100%;">
                   <tr>
                       <td width="126px">&nbsp;</td>
                       <td>
                           <asp:Button ID="NextButton" runat="server" Text="下一步" style="cursor:hand;" 
                               Width="76px" TabIndex="5"/>
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
