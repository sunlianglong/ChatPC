using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Threading;
using System.Net.Sockets;


namespace Client.ClassInfo
{
    class UserDefinition
    {        
        /// <summary>
        /// 定义登陆号码为整数型
        /// </summary>
        public static int Number;
        /// <summary>
        /// 登陆窗体输入验证
        /// </summary>
        public static bool inputVal { get; set; }
        /// <summary>
        /// 创建登陆窗体实例
        /// </summary>
        public static Window1 WinLogin;
        /// <summary>
        /// 创建主窗体实例
        /// </summary>
        public static Win_Main MainWindow;
        /// <summary>
        /// 创建聊天窗体实例
        /// </summary>
        public static Win_Talking WinTalk;
        public static DispatcherTimer timer;
        /// <summary>
        /// 创建提示用户好友上线窗体实例
        /// </summary>
        public static CueOnline WinCueOnline;
        /// <summary>
        /// 登陆窗体的隐藏状态
        /// </summary>
        public static bool LoginWindowState { get; set; }
        /// <summary>
        /// 登陆窗体的关闭状态
        /// </summary>
        public static bool LoginWindowClose { get; set; }
        /// <summary>
        /// 主窗体的隐藏状态
        /// </summary>
        public static bool MainWindowState { get; set; }
        /// <summary>
        /// 用户登陆号码
        /// </summary>
        public static string UserNumber { get; set; }
        /// <summary>
        /// 用户登陆状态
        /// </summary>
        public static int UserLoginState { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public static string UserNickName { get; set; }
        /// <summary>
        /// 用户个性签名
        /// </summary>
        public static string UserDictum { get; set; }
        /// <summary>
        /// 删除好友时返回信息
        /// </summary>
        public static String DelFriendInfo { get; set; }
        /// <summary>
        /// 发送抖动窗体失败时提示
        /// </summary>
        public static string shakeCueMsg = "抖动窗体发送失败，您的好友已经离线或隐身";
        /// <summary>
        /// 发送消息失败时提示
        /// </summary>
        public static string sendMsgCue = "对不起，此软件暂不支持留言功能，您的好友已经离线";
        /// <summary>
        /// 创建聊天窗口管理器
        /// </summary>
        public static PopWindowManager popWindowManager = new PopWindowManager();
        /// <summary>
        /// image图片Byte[]数组
        /// </summary>
        public static Byte[] imgByte { get; set; }
        /// <summary>
        /// 创建托盘区图标菜单项
        /// </summary>
        public static System.Windows.Forms.MenuItem[] menuItems;
        /// <summary>
        /// 创建托盘区动态ICO'timer'实例
        /// </summary>
        public static System.Timers.Timer timer_Notify;
        /// <summary>
        /// 获取应用程序目录
        /// </summary>
        public static String AppDirectory = Directory.GetCurrentDirectory();
        /// <summary>
        /// 创建托盘区图标实例
        /// </summary>
        public static System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        /// <summary>
        /// 创建托盘区图标右键菜单实例
        /// </summary>
        public static System.Windows.Forms.ContextMenu notifyIconMenu = new System.Windows.Forms.ContextMenu();
        /// <summary>
        /// 未登陆时的托盘区图标
        /// </summary>
        public static System.Drawing.Icon Icon_Login = new System.Drawing.Icon(UserDefinition.AppDirectory + "\\Icon\\Login.ico");
        /// <summary>
        /// 在线时的托盘区图标
        /// </summary>
        public static System.Drawing.Icon Icon_Online = new System.Drawing.Icon(UserDefinition.AppDirectory + "\\Icon\\Online.ico");
        /// <summary>
        /// 隐身时的托盘区图标
        /// </summary>
        public static System.Drawing.Icon Icon_Hide = new System.Drawing.Icon(UserDefinition.AppDirectory + "\\Icon\\Hide.ico");
        /// <summary>
        /// 忙碌时的托盘区图标
        /// </summary>
        public static System.Drawing.Icon Icon_Busyness = new System.Drawing.Icon(UserDefinition.AppDirectory + "\\Icon\\Busyness.ico");
        /// <summary>
        /// 请勿打扰时的托盘区图标
        /// </summary>
        public static System.Drawing.Icon Icon_Not = new System.Drawing.Icon(UserDefinition.AppDirectory + "\\Icon\\No.ico");
        /// <summary>
        /// 离线时的托盘区图标
        /// </summary>
        public static System.Drawing.Icon Icon_Offline = new System.Drawing.Icon(UserDefinition.AppDirectory + "\\Icon\\Off.ico");
    }
}
