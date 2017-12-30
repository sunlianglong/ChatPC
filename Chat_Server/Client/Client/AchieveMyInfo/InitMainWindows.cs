using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;
using System.Net;
using System.Net.Sockets;
using Client.ClassInfo;

namespace Client
{
    class InitMainWindow
    {
        /// <summary>
        /// 好友列表
        /// </summary>
        /// <param name="MainWin">主窗体句柄</param>
        public static void Init(Win_Main MainWin)
        {
            //用户基本信息
            MainWin.State.SelectedIndex = UserDefinition.UserLoginState;
            MainWin.UserNickName.Text = UserDefinition.UserNickName;
            MainWin.DictumBox.Text = UserDefinition.UserDictum;
            //创建分组实例
            FriendListInit.ViewItemList = new TreeViewItem();
            //添加好友列表内容
            if (FriendListInit.friend != null)
            {
                foreach (string UserNickName in FriendListInit.friend)
                {
                    addList(UserNickName);
                }
            }
            //int friendCount = FriendListInit.ViewItemList.Items.Count;
            string header = string.Format("我的好友");
            FriendListInit.ViewItemList.Header = header;
            FriendListInit.ViewItemList.Style = (Style)System.Windows.Application.Current.FindResource("TreeViewItemStyle_Normal");
            MainWin.FriendList.Items.Add(FriendListInit.ViewItemList);
            //FriendListInit.ViewItemList = new TreeViewItem();
            //FriendListInit.ViewItemList.Header = "黑名单";
            //MainWin.FriendList.Items.Add(FriendListInit.ViewItemList);
        }
        public static void addList(string userNickName)
        {
            //创建实例
            FriendListInit.NickName = new TextBlock();//用户昵称
            FriendListInit.NickName.Foreground = Brushes.Black;
            FriendListInit.UserHeader = new StackPanel();
            FriendListInit.UserHeader.Orientation = Orientation.Horizontal;
            FriendListInit.UserHeader.Height = 50; FriendListInit.UserHeader.Width = 170;
            FriendListInit.UserIcon = new Image();//用户头像
            FriendListInit.UserIcon.Height = 40; FriendListInit.UserIcon.Width = 40;
            FriendListInit.image = new BitmapImage(new Uri(@"pack://application:,,,/BackImage/" + "header.ico"));
            FriendListInit.UserIcon.Source = FriendListInit.image;
            FriendListInit.AddTree = new TreeViewItem();
            //添加用户信息
            FriendListInit.NickName.Text = userNickName;
            FriendListInit.UserHeader.Children.Add(FriendListInit.UserIcon);
            FriendListInit.UserHeader.Children.Add(FriendListInit.NickName);            
            FriendListInit.AddTree.Header = FriendListInit.UserHeader;
            FriendListInit.AddTree.Margin = new Thickness(-20, 1, 0, 1);
            FriendListInit.AddTree.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(Win_Main.AddTree_MouseDoubleClick);
            FriendListInit.AddTree.MouseRightButtonUp += new System.Windows.Input.MouseButtonEventHandler(AddTree_MouseRightButtonUp);            
            FriendListInit.AddTree.ContextMenu = rightButtonMenu();
            FriendListInit.ViewItemList.Items.Add(FriendListInit.AddTree);
        }

        static void AddTree_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null)
            {
                item.Focus();                
            }
        }

        static ContextMenu rightButtonMenu()
        {            
            MenuItem sendmsgitem = new MenuItem();
            sendmsgitem.Header = "发送消息";
            sendmsgitem.Click += new RoutedEventHandler(Win_Main.sendmsgitem_Click);            
            MenuItem removeitem = new MenuItem();          
            removeitem.Header = "删除好友";
            removeitem.Click += new RoutedEventHandler(Win_Main.removeitem_Click);
            ContextMenu contextmenu = new ContextMenu();
            contextmenu.Items.Add(sendmsgitem);
            contextmenu.Items.Add(new Separator());
            contextmenu.Items.Add(removeitem);
            return contextmenu;
        }
    }
}
