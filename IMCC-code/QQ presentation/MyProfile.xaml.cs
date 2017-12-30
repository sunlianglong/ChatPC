using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
    /// MyProfile.xaml 的交互逻辑
    /// </summary>
    public partial class MyProfile : Window
    {
        string ip_address;
        string ip_friend;
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }

        public MyProfile(string user_ip,string friend_ip)
        {
            InitializeComponent();
            ip_address = user_ip;
            ip_friend = friend_ip;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;   //主窗口最小化
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ip_friend == "null")
            {
                //个人界面
                string spath1 = "D:\\Program Files (x86)\\IMCC\\User\\" + ip_address + "\\header";


                if (Directory.Exists(spath1))
                {
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    string skin_name = cfa.AppSettings.Settings["HD_NUM"].Value;
                    string file2 = spath1 + "\\" + cfa.AppSettings.Settings["HD_NUM"].Value;
                    cfa.Save();
                    if (File.Exists(file2))
                    {
                        BitmapImage hd1 = new BitmapImage(new Uri(file2));
                        if (hd1 != null)
                        {
                            header.ImageSource = hd1;
                        }

                    }
                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(spath1);
                    directoryInfo.Create();
                }
            }
            else
            {
                //朋友界面
                Bitmap b3 = PicHelp.DownloadPic(ip_friend, "pic");
                if (b3 != null)
                {
                    header.ImageSource = PicHelp.ChangeBitmapToImageSource(b3);
                }
            }
            string init_ip;
            if(ip_friend=="null")
            {
                init_ip = ip_address;
            }
            else
            {
                init_ip = ip_friend;
            }
            
            UserInformation user = UserInformationGet.GetUserInfor(init_ip);
            nickname.Content = user.netname;
            singure.Content = user.geqian_data;
            ip_show.Content = user.ip_address;
            email_show.Content = user.email;
            if(user.sex=="boy"||user.sex=="男")
            {
                sex_show.Content = "男";
            }
            else
            {
                sex_show.Content = "女";
            }
            birthday_show.Content = user.birthday;
            home_show.Content = user.home_data;
            school_show.Content = user.school_data;
            class_show.Content = user.class_data;
            phone_show.Content = user.phone_num;
        }

        private void edit_info_Click(object sender, RoutedEventArgs e)
        {
            if(ip_friend=="null")
            {
                EditInfo e1 = new EditInfo(ip_address);
                e1.Show();
            }
            else
            {
                EditFriend e2 = new EditFriend(ip_address, ip_friend);
                e2.Show();
            }
            
        }
    }
}
