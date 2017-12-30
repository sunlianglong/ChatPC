using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Client
{
    class FriendListInit
    {
        /// <summary>
        /// 添加分组头部名称
        /// </summary>
        public static TreeViewItem ViewItemList;
        /// <summary>
        /// 添加孩子节点
        /// </summary>
        public static TreeViewItem AddTree;
        /// <summary>
        /// 孩子节点面板
        /// </summary>
        public static StackPanel UserHeader;
        /// <summary>
        /// 用户头像
        /// </summary>
        public static Image UserIcon;
        /// <summary>
        /// 将图片转换为Bit型
        /// </summary>
        public static BitmapImage image;
        /// <summary>
        /// 用户昵称
        /// </summary>
        public static TextBlock NickName;        
        /// <summary>
        /// 用户
        /// </summary>
        public static StringCollection friend;
    }
}
