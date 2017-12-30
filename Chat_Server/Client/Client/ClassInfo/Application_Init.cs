using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Client.ClassInfo;

namespace Client
{        
    class Application_Init
    {
        /// <summary>
        /// 初始化托盘区图标
        /// </summary>     
        public static void Initial()
        {
            UserDefinition.notifyIcon.BalloonTipText = "Hello!LAN CHAT";
            UserDefinition.notifyIcon.Text = "Hello!LAN CHAT-当前未登陆!";            
            UserDefinition.notifyIcon.Icon = UserDefinition.Icon_Login;
            UserDefinition.notifyIcon.Visible = true;
            UserDefinition.notifyIcon.MouseDoubleClick += new MouseEventHandler(notifyIcon_MouseDoubleClick);
            UserDefinition.notifyIcon.ShowBalloonTip(1000);
            UserDefinition.menuItems=new MenuItem[3];
            UserDefinition.menuItems[0] = new MenuItem();
            UserDefinition.menuItems[0].Text = "显示主面板";
            UserDefinition.menuItems[0].Click += new EventHandler(nofityShow_Click);
            UserDefinition.menuItems[1] = new MenuItem();
            UserDefinition.menuItems[1].Text = "-";
            UserDefinition.menuItems[2] = new MenuItem();
            UserDefinition.menuItems[2].Text = "退出";            
            UserDefinition.menuItems[2].Click += new EventHandler(notifyExit_Click);
            UserDefinition.menuItems[0].DefaultItem = true;
            UserDefinition.notifyIconMenu = new ContextMenu(UserDefinition.menuItems);
            UserDefinition.notifyIcon.ContextMenu = UserDefinition.notifyIconMenu;
        }
        /// <summary>
        /// 托盘区图标状态
        /// </summary>
        /// <param name="LoginState">登陆状态</param>
        public static void notifyIconUserState(int LoginState)
        {            
            switch(LoginState)
            {
                case 0://在线
                    UserDefinition.notifyIcon.Icon = UserDefinition.Icon_Online;
                    UserDefinition.notifyIcon.Text = "Hello! 您的当前状态为：在线";
                    UserDefinition.notifyIcon.Visible = true;
                    break;
                case 1://忙碌
                    UserDefinition.notifyIcon.Icon = UserDefinition.Icon_Busyness;
                    UserDefinition.notifyIcon.Text = "Hello! 您的当前状态为：忙碌";
                    UserDefinition.notifyIcon.Visible = true;
                    break;
                case 2://请勿打扰
                    UserDefinition.notifyIcon.Icon = UserDefinition.Icon_Not;
                    UserDefinition.notifyIcon.Text = "Hello! 您的当前状态为：请勿打扰";
                    UserDefinition.notifyIcon.Visible = true;
                    break;
                case 3://离线
                    UserDefinition.notifyIcon.Icon = UserDefinition.Icon_Offline;
                    UserDefinition.notifyIcon.Text = "Hello! 您的当前状态为：离线";
                    UserDefinition.notifyIcon.Visible = true;
                    break;
                case 5://隐身
                    UserDefinition.notifyIcon.Icon = UserDefinition.Icon_Hide;
                    UserDefinition.notifyIcon.Text = "Hello! 您的当前状态为：隐身";
                    UserDefinition.notifyIcon.Visible = true;
                    break;
            }
        }
        /// <summary>
        /// 托盘区图标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //如果登陆窗体已关闭
            if (UserDefinition.LoginWindowClose)
            {
                //如果主窗体已在前台
                if (UserDefinition.MainWindowState)
                {
                    UserDefinition.MainWindow.Hide();                    
                    UserDefinition.MainWindowState = false;
                }
                else
                {
                    UserDefinition.MainWindow.Show();
                    UserDefinition.MainWindow.Activate();
                    UserDefinition.MainWindowState = true;                    
                }
            }
            else
            {
                //如果登陆窗体已在前台
                if (UserDefinition.LoginWindowState)
                {
                    UserDefinition.WinLogin.Hide();
                    UserDefinition.LoginWindowState = false;
                }
                else
                {
                    UserDefinition.WinLogin.Show();
                    UserDefinition.WinLogin.Activate();
                    UserDefinition.LoginWindowState = true;
                }
            }
        }
        /// <summary>
        /// 单击托盘区“显示”菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void nofityShow_Click(object sender, EventArgs e)
        {
            //如果登陆窗体已关闭
            if (UserDefinition.LoginWindowClose)
            {
                //如果主窗体已隐藏
                if (!UserDefinition.MainWindowState)
                {
                    UserDefinition.MainWindow.Show();
                    UserDefinition.MainWindow.Activate();
                    UserDefinition.MainWindowState = true;
                }
                else
                {
                    UserDefinition.MainWindow.Activate();
                }
            }
            else
            {
                //如果登陆窗体已隐藏
                if (!UserDefinition.LoginWindowState)
                {
                    UserDefinition.WinLogin.Show();
                    UserDefinition.WinLogin.Activate();
                    UserDefinition.LoginWindowState = true;
                }
                else
                {
                    UserDefinition.WinLogin.Activate();
                }
            }
        }
        /// <summary>
        /// 单击托盘区“退出”菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void notifyExit_Click(object sender, EventArgs e)
        {
            if (UserDefinition.LoginWindowClose)
            {
                Win_Main.myNetStream.Write(new byte[] { 0, 1 }, 0, 2);
                Win_Main.myNetStream.Close();
                Application_Init.setFreeNotifyIcon();
                Environment.Exit(0);
            }
            else
            {
                Application_Init.setFreeNotifyIcon();
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// 释放托盘区图标
        /// </summary>
        public static void setFreeNotifyIcon()
        {
            UserDefinition.notifyIcon.Visible = false;
            UserDefinition.notifyIcon.Dispose();
        }
    }
}
