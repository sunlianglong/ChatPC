using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Client.ClassInfo;

namespace Client
{
    class Validate
    {
        //登陆验证
        public static bool LandVal(Window1 Winlogin)
        {
            if (Winlogin.userNumber.Text == "" && Winlogin.pwdBox.Password == "")
            {
                Winlogin.border1.Visibility = Visibility.Visible;
                Winlogin.UserCue.Text = " 请输入帐号后再登陆！";                
                return false;
            }
            else if (Winlogin.userNumber.Text == "" && Winlogin.pwdBox.Password != "")
            {
                Winlogin.border1.Visibility = Visibility.Visible;
                Winlogin.UserCue.Text = " 请输入帐号后再登陆！";                
                return false;
            }
            else if (Winlogin.userNumber.Text.Trim().Length < 5)
            {
                Winlogin.border1.Visibility = Visibility.Visible;
                Winlogin.UserCue.Text = " 帐号长度不小于5位！";
                return false;
            }            
            else if (!int.TryParse(Winlogin.userNumber.Text.Trim(), out UserDefinition.Number))
            {
                Winlogin.border1.Visibility = Visibility.Visible;
                Winlogin.UserCue.Text = "请输入正确的登陆号码！";
                return false;
            }
            else if (Winlogin.userNumber.Text != "" && Winlogin.pwdBox.Password == "")
            {
                Winlogin.border2.Visibility = Visibility.Visible;
                Winlogin.PwdCue.Text = " 请输入密码后再登陆！";
                Winlogin.pwdBox.Focus();
                return false;
            }
            else if (Winlogin.pwdBox.Password.Length < 6)
            {
                Winlogin.border2.Visibility = Visibility.Visible;
                Winlogin.PwdCue.Text = "密码长度不小于6位！";
                Winlogin.pwdBox.Focus();
                return false;
            }
            else
            {
                return true;
            }           
        }
        //登陆状态
        public static void ChangeState(bool state, Window1 Winlogin)
        {
            if (state)
            {
                //Winlogin.Hide();
                //Winlogin.pwdBox.Password = null;
                //UserDefinition.WinLoading.UserInputNumber.Text = Winlogin.username.Text.Trim();                
                //UserDefinition.WindowLocation(UserDefinition.WinLoading, UserDefinition.WindowLeft, UserDefinition.WindowTop);
                LoginClear(Winlogin);
                LoadingShow(Winlogin);
                ShowLoginNotifyImage(Winlogin);
                Winlogin.UserInputNumber.Text = Winlogin.userNumber.Text.Trim();                
            }
            else
            {
                //if (UserDefinition.WinLoading.IsActive)
                //{
                //    UserDefinition.WinLoading.Hide();
                //}
                LoginShow(Winlogin);
                LoadingClear(Winlogin);                
            }
        }       

        //显示GIF图片
        public static void ShowLoginNotifyImage(Window1 Winlogin)
        {
            Stream imageStream = Application.GetResourceStream(new Uri(@"BackImage/loading.gif", UriKind.Relative)).Stream;
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageStream);
            Winlogin.LoadingImg.Image = bitmap;           
        }
        public static void ShowSearchImage(Win_Main winMain)
        {
            Stream imageStream = Application.GetResourceStream(new Uri(@"SysImage/wait.gif", UriKind.Relative)).Stream;
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageStream);
            winMain.searchImg.Image = bitmap;
        }
        //用户状态
        public static void OnlineStateUser(Win_Main WinMain)
        {
            switch (WinMain.State.SelectedIndex)
            {
                case 0:
                    WinMain.OnlineState.Text = "[在线]";
                    Application_Init.notifyIconUserState(0);
                    break;
                case 1:
                    WinMain.OnlineState.Text = "[忙碌]";
                    Application_Init.notifyIconUserState(1);
                    break;
                case 2:
                    WinMain.OnlineState.Text = "[请勿打扰]";
                    Application_Init.notifyIconUserState(2);
                    break;
                case 3:
                    WinMain.OnlineState.Text = "[离线]";
                    Application_Init.notifyIconUserState(3);
                    break;
                case 5:
                    WinMain.OnlineState.Text = "[隐身]";
                    Application_Init.notifyIconUserState(5);
                    break;
            }
        }

        #region//控件隐藏与显示
        public static void LoginClear(Window1 Winlogin)
        {
            Winlogin.textBlock1.Visibility = Visibility.Collapsed;
            Winlogin.user.Visibility = Visibility.Collapsed;
            Winlogin.textBlock2.Visibility = Visibility.Collapsed;
            Winlogin.pwdBox.Visibility = Visibility.Collapsed;
            Winlogin.textBlock3.Visibility = Visibility.Collapsed;
            Winlogin.State.Visibility = Visibility.Collapsed;
            Winlogin.Remember.Visibility = Visibility.Collapsed;
            Winlogin.Automatism.Visibility = Visibility.Collapsed;
            Winlogin.land.Visibility = Visibility.Collapsed;
            Winlogin.textBlock4.Visibility = Visibility.Collapsed;
            Winlogin.textBlock5.Visibility = Visibility.Collapsed;
            Winlogin.textBlock6.Visibility = Visibility.Collapsed;
        }

        public static void LoginShow(Window1 Winlogin)
        {
            Winlogin.textBlock1.Visibility = Visibility.Visible;
            Winlogin.user.Visibility = Visibility.Visible;
            Winlogin.textBlock2.Visibility = Visibility.Visible;
            Winlogin.pwdBox.Visibility = Visibility.Visible;
            Winlogin.textBlock3.Visibility = Visibility.Visible;
            Winlogin.State.Visibility = Visibility.Visible;
            Winlogin.Remember.Visibility = Visibility.Visible;
            Winlogin.Automatism.Visibility = Visibility.Visible;
            Winlogin.land.Visibility = Visibility.Visible;
            Winlogin.textBlock4.Visibility = Visibility.Visible;
            Winlogin.textBlock5.Visibility = Visibility.Visible;
            Winlogin.textBlock6.Visibility = Visibility.Visible;
        }

        public static void LoadingShow(Window1 Winlogin)
        {
            Winlogin.LoadingImg.Visibility = Visibility.Visible;
            Winlogin.UserInputNumber.Visibility = Visibility.Visible;
            Winlogin.LoadingLabel.Visibility = Visibility.Visible;
            Winlogin.Cancel.Visibility = Visibility.Visible;
        }

        public static void LoadingClear(Window1 Winlogin)
        {
            Winlogin.LoadingImg.Visibility = Visibility.Collapsed;
            Winlogin.UserInputNumber.Visibility = Visibility.Collapsed;
            Winlogin.LoadingLabel.Visibility = Visibility.Collapsed;
            Winlogin.Cancel.Visibility = Visibility.Collapsed;
        }
        #endregion

    }
}
