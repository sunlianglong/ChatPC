<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reg.aspx.vb" Inherits="Reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="shortcut icon" href="User-Male.ico" /> 
<link rel="bookmark" href="User-Male.ico" />
    <title>CHAT--用户注册</title>
    <script language="javascript" type="text/javascript">
        function OnblurValidate(BoxName) {
            if (BoxName.value != '') {
                if (BoxName.value.length < 6) {
                    window.alert('密码长度为6～15，请正确填写');
                    BoxName.value = '';
                    BoxName.focus();
                    return false;
                }
            }
            var Pwd = window.document.RegPage.Password1;
            var AgainPwd = window.document.RegPage.Password2;
            if (Pwd.value != '' && AgainPwd.value != '') {
                if (Pwd.value != AgainPwd.value) {
                    window.alert('两次密码输入不一致，请重新输入！');
                    Pwd.value = '';AgainPwd.value = '';
                    Pwd.focus();                  
                    return false;
                }
            }
        }
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
        function fEvent(sType, oInput) {
            switch (sType) {
                case "focus":
                    oInput.isfocus = true;
                case "mouseover":                    
                    oInput.style.borderColor = '#3399FF';
                    break;
                case "blur":
                    oInput.isfocus = false;
                case "mouseout":
                    if (!oInput.isfocus) {
                        oInput.style.borderColor = 'Gray';
                    }
                    break;
            }
        }
  </script> 
    </head>
<body>
    <form id="RegPage" runat="server">        
    <table style="width:900px; height:1000px;" align="center">
        <tr>
           <td>               
             <table style="width:830px;" align="center">
                 <tr>
                 <td width="30px">
                 <img height="60" src="../BackImage/image.png" /></td>
                 <td><table style="width:100%; height:23px;">
                      <tr><td style="font-family: 微软雅黑; font-size: 14px; 
                              font-weight: bold;  ">
                              CHAT — 用户注册
                    </td></tr>
                 </table></td>
                </tr>
             </table></td>
        </tr>
        <tr>
          <td>
             <table style="width:830px; height:860px; background-image:url('image/reg-backimg.png') " align="center">
                <tr>
                  <td width="545px" colspan="2">&nbsp;</td>
                  </tr>
                <tr>
                  <td width="621px"></td>
                  <td style="font-size:small; color: #990033;">*注：本页所有内容必填</td>
                  </tr>
                  <tr>
                   <td colspan="2">&nbsp;</td>                      
                  </tr>
                  <tr>
                   <td colspan="2">
                      <table style="border-width: 1px; border-color: #C0C0C0; width:700px; border-bottom-style: solid;" align="center">
                             <tr>
                               <td width="110" style="font-weight:bolder;">
                                   用户信息设置</td>                               
                             </tr>
                      </table></td>                      
                   </tr>
                   <tr><td colspan="2" height="14">&nbsp;</td></tr>  
                   <tr>
                   <td colspan="2">
                     <table style="width:700px;" align="center">
                           <tr>
                             <td width="126px">&nbsp;</td>
                             <td style="font-size:small; font-weight:bolder;" colspan="2">昵称：</td>
                             </tr>
                           <tr>
                             <td width="126px">&nbsp;</td>
                             <td colspan="2"><asp:TextBox ID="UserNameBox" runat="server" TabIndex="1" Width="162px" MaxLength="14"/>
                               </td>
                            </tr>
                            <tr>
                              <td width="126px">&nbsp;</td>
                              <td style="font-size: small; color: #808080" colspan="2">不超过14个字节，只能输入数字和字母</td>
                            </tr>                            
                            <tr>
                              <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                              <td width="126px">&nbsp;</td>
                              <td style="font-size:small; font-weight:bolder; " class="style1">密码：</td>                              
                              <td style="font-size:small; font-weight:bolder;">确认密码：</td>                              
                            </tr>  
                            <tr>
                              <td width="126px">&nbsp;</td>
                              <td width="101px"><asp:TextBox ID="Password1" runat="server" TextMode="Password" MaxLength="15" onblur="OnblurValidate(this)"
                                       TabIndex="3" Width="160px"/></td>                              
                              <td><asp:TextBox ID="Password2" runat="server" TextMode="Password" MaxLength="15" onblur="OnblurValidate(this)"
                                       TabIndex="4" Width="160px"/></td>                              
                            </tr>    
                            <tr>
                              <td width="126px">&nbsp;</td>
                              <td colspan="2" style="color: #808080; font-size:small;">密码长度6～15位，区分大小写</td>                              
                            </tr>    
                            <tr>
                              <td colspan="3">&nbsp;</td>
                            </tr>                                               
                        </table>
                        </td></tr>
                        <tr>
                          <td colspan="2">
                            <table style="border-width: 1px; border-color: #C0C0C0; width:700px; border-bottom-style: solid;" align="center">
                             <tr>
                               <td width="108" style="font-weight:bolder;">
                                安全信息设置</td>
                               <td style="font-size: small; color: #808080;">
                             （以下信息对保护您的帐号安全极为重要，请您慎重填写并牢记）</td>
                             </tr>
                           </table>
                          </td>
                        </tr>
                        <tr><td colspan="2" height="6">&nbsp;</td></tr> 
                        <tr>
                          <td colspan="2">
                            <table style="width:700px;" align="center">
                              <tr>
                                <td width="126px">&nbsp;</td>
                                <td style="font-size:small; font-weight:bolder;" colspan="2">出生日期：</td>
                              </tr>
                              <tr>
                                <td width="126px">&nbsp;</td>
                                <td style="font-size:small;" colspan="2">
                                    <asp:TextBox ID="year" runat="server" Width="50px" 
                                        MaxLength="4" style="ime-mode:disabled" 
                                        onKeyup="this.value=this.value.replace(/[^0-9]/g,'');" onBlur="yearval(this)" 
                                        TabIndex="5"/>年
                                    <asp:DropDownList ID="Month" runat="server" TabIndex="6">
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
                                    </asp:DropDownList>月
                                    <asp:DropDownList ID="Day" runat="server" TabIndex="7">
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
                                <td colspan="3"></td>
                              </tr>    
                              <tr>
                                <td width="126px"></td>
                                <td style="font-size:small; font-weight:bolder;" colspan="2">性别：</td>
                              </tr>  
                              <tr>
                                <td width="126px">&nbsp;</td>
                                <td colspan="2">
                                  <asp:RadioButton ID="male" runat="server" Text="男" Font-Size="Small" 
                                       GroupName="sex" TabIndex="8"/>
                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:RadioButton ID="female" runat="server" Text="女" Font-Size="Small" 
                                       GroupName="sex" TabIndex="9" /></td>  
                              </tr>  
                              <tr>
                                <td colspan="3">&nbsp;</td>
                              </tr>                                                                           
                              <tr>
                                <td width="126px">&nbsp;</td>
                                <td style="font-size:small; font-weight:bolder; width:162px;">密码保护问题：</td>
                                <td style="font-size:small; font-weight:bolder;">密码保护问题答案：</td>
                              </tr>                                                                           
                              <tr>
                                <td width="126px">&nbsp;</td>
                                <td style="font-size:small; font-weight:bolder; width:162px;">
                                    <asp:DropDownList ID="SecProblem" runat="server" Width="160px" TabIndex="10">
                                       <asp:ListItem style="color:#666" Text="请选择您的保护问题" Selected="True" />    
                                       <asp:ListItem Text="您母亲的姓名是?"/> 
                                       <asp:ListItem Text="您父亲的姓名是?"/>
                                       <asp:ListItem Text="您配偶的姓名是?"/>
                                       <asp:ListItem Text="您母亲的生日是?"/>
                                       <asp:ListItem Text="您父亲的生日是?"/>
                                       <asp:ListItem Text="您配偶的生日是?"/>
                                       <asp:ListItem Text="您的出生地是?"/>
                                       <asp:ListItem Text="您的小学校名是?"/>                                       
                                       <asp:ListItem Text="您的中学校名是?"/>                                       
                                       <asp:ListItem Text="您的大学校名是?"/>
                                       <asp:ListItem Text="您最喜欢的休闲运动是什么？"/>
                                       <asp:ListItem Text="您最喜欢的运动员是谁？"/>
                                       <asp:ListItem Text="您最喜欢的歌曲？"/>
                                       <asp:ListItem Text="您最喜欢的食物？"/>
                                       <asp:ListItem Text="您最爱的人的名字？"/>
                                       <asp:ListItem Text="您最爱的电影？"/>
                                       <asp:ListItem Text="您的初恋日期？"/>
                                    </asp:DropDownList>
                                    </td>
                                <td><asp:TextBox ID="ProblemAnswerBox" runat="server" Width="170px" TabIndex="11"/></td>
                              </tr>                                                             
                              <tr>
                                <td colspan="3">&nbsp;</td>
                              </tr>                                                             
                              <tr>
                                <td width="126px">&nbsp;</td>
                                <td style="font-size:small; font-weight:bolder; " colspan="2">
                                    所在地：</td>
                              </tr>                                                             
                              <tr>
                                <td width="126px">&nbsp;</td>
                                <td colspan="2">
                                  <asp:DropDownList ID="Country" runat="server" TabIndex="12">
                                    <asp:ListItem Selected="True">-</asp:ListItem>
                                    <asp:ListItem>中国</asp:ListItem>
                                  </asp:DropDownList>
                                  <asp:DropDownList ID="Province" runat="server" TabIndex="13">
                                    <asp:ListItem Selected="True">-</asp:ListItem>
                                    <asp:ListItem>河南</asp:ListItem>
                                  </asp:DropDownList>
                                  <asp:DropDownList ID="City" runat="server" TabIndex="14">
                                    <asp:ListItem Selected="True">-</asp:ListItem>
                                    <asp:ListItem>许昌</asp:ListItem>
                                    <asp:ListItem>长葛</asp:ListItem>
                                    <asp:ListItem>禹州</asp:ListItem>
                                    <asp:ListItem>郑州</asp:ListItem>
                                    <asp:ListItem>新郑</asp:ListItem>
                                    <asp:ListItem>新密</asp:ListItem>
                                    <asp:ListItem>洛阳</asp:ListItem>
                                    <asp:ListItem>伊川</asp:ListItem>
                                    <asp:ListItem>栾川</asp:ListItem>
                                    <asp:ListItem>宜阳</asp:ListItem>
                                    <asp:ListItem>洛宁</asp:ListItem>
                                    <asp:ListItem>开封</asp:ListItem>
                                    <asp:ListItem>尉氏</asp:ListItem>
                                    <asp:ListItem>新乡</asp:ListItem>
                                    <asp:ListItem>南阳</asp:ListItem>                     
                                    <asp:ListItem>濮阳</asp:ListItem>
                                    <asp:ListItem>安阳</asp:ListItem>
                                    <asp:ListItem>三门峡</asp:ListItem>
                                    <asp:ListItem>平顶山</asp:ListItem>
                                    <asp:ListItem>汝州</asp:ListItem>                     
                                    <asp:ListItem>周口</asp:ListItem>
                                    <asp:ListItem>漯河</asp:ListItem>
                                    <asp:ListItem>驻马店</asp:ListItem>
                                    <asp:ListItem>西平</asp:ListItem>
                                  </asp:DropDownList>
                                  </td>
                              </tr>  
                              <tr>
                                <td colspan="3">&nbsp;</td>
                              </tr>  
                              <tr>
                                <td colspan="3">
                                 <table style="border-width: 1px; border-color: #C0C0C0; width:100%; border-bottom-style: solid;">
                                    <tr>
                                     <td width="110" style="font-weight:bolder;">
                                       注册验证</td>                               
                                    </tr>
                                 </table></td>
                              </tr> 
                              <tr>
                                <td colspan="3">&nbsp;</td>
                              </tr> 
                              <tr>
                                <td width="126px"></td>
                                <td colspan="2" style="font-size:small; font-weight:bolder;">请输入右边的字符：</td>
                              </tr>  
                              <tr>
                                <td width="126px"></td>
                                <td colspan="2">
                                  <asp:TextBox ID="CodeValBox" runat="server" MaxLength="5" Width="160px" 
                                        TabIndex="15" />
                                  <img id="CodeImg" onclick="this.src='CheckCode.aspx'" src="CheckCode.aspx" />
                                  <input style="border: 1px solid #999999; cursor:hand; background-color: #FFFFFF; height:24px;" 
                                         onclick="document.getElementById('CodeImg').src='CheckCode.aspx?tem='+Math.random();"
                                         onFocus="fEvent('focus',this)" onBlur="fEvent('blur',this)"
                                         onMouseOver="fEvent('mouseover',this)" onMouseOut="fEvent('mouseout',this)" 
                                         type="button" value="刷新验证码" name="Btn_Re" tabindex="16" />
                                </td>
                              </tr>  
                              <tr>
                                <td colspan="3">&nbsp;</td>
                              </tr>
                              <tr>
                                <td colspan="3">
                                <table style="width:100%; height:35px;">
                                   <tr>
                                     <td width="126px"></td>
                                     <td>
                                        <asp:Button ID="RegButton" runat="server" Text="创建帐号" style="cursor:hand; font-family:微软雅黑;"
                                         ToolTip="创建帐号" Height="30px" Width="110px" TabIndex="17" BorderColor="#999999"
                                         onFocus="fEvent('focus',this)" onBlur="fEvent('blur',this)"
                                         onMouseOver="fEvent('mouseover',this)" onMouseOut="fEvent('mouseout',this)" 
                                         BorderStyle="Solid" BorderWidth="1px" BackColor="White" /></td>
                                   </tr> 
                                   <tr>
                                     <td colspan="2" height="30">&nbsp;</td>
                                   </tr>                                  
                                </table></td>
                              </tr> 
                              </table>
                          </td>
                        </tr>                        
                        </table>                       
                      </td>                      
                   </tr>                 
              </table>               
       </form>
</body>
</html>
