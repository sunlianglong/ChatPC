using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QQ_presentation
{
    /// <summary>
    /// EditFriend.xaml 的交互逻辑
    /// </summary>
    public partial class EditFriend : Window
    {
        string ip_address;
        string ip_friend;
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();

        }

        public EditFriend(string usr_ip, string friend_ip)
        {
            InitializeComponent();
            ip_address = usr_ip;
            ip_friend=friend_ip;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ip_me = ip_address;
            string ip_fd = ip_friend;
            string post_group = add_group.Text;
            string post_beizhu = add_beizhu.Text;
            if(Judge.GroupJudge_group(ip_me,post_group)==0)
            {
                ADD.FriendAdd(ip_me, ip_fd, post_group, post_beizhu);
                ADD.FriendAdd(ip_fd, ip_me, "我的好友", "");
                ADD.GroupdAdd(ip_me, post_group);
                if(Judge.GroupJudge_group(ip_fd, "我的好友") == 0)
                {
                    ADD.GroupdAdd(ip_fd, "我的好友");
                }
            }
            else
            {
                if(Judge.user_friend_judge(ip_me,ip_fd)==0)
                {
                    if(Judge.user_friend_judge(ip_fd,ip_me)==0)
                    {
                        ADD.FriendAdd(ip_me, ip_fd, post_group, post_beizhu);
                        ADD.FriendAdd(ip_fd, ip_me, "我的好友", "");
                    }
                    else
                    {
                        ADD.FriendAdd(ip_me, ip_fd, post_group, post_beizhu);
                    }
                }
                else
                {
                    Update.user_friend_update(ip_me, ip_fd, post_group, post_beizhu);
                }
            }
            this.Close();
        }
        
    }
}
