using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace QQ_presentation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string ip_address;
        //1string ip_address = "10.0.2.15";
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }

        public MainWindow(string getip)
        {
            InitializeComponent();
            ip_address = getip;
            GlobalUse._server_config.Ip_config_class = ConfigurationManager.AppSettings["IP_CONFIG"];
            GlobalUse._server_config.Port_config_class = ConfigurationManager.AppSettings["PORT_CONFIG"];
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();                                //主窗口关闭
            //  this.Close();  这行代码也是关闭窗口
        }
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;   //主窗口最小化
        }

        string tempTxt2 = string.Empty;    //定义一个全局变量，用来存储获取焦点之前 TextBox 的值
        private void txtbox2_GotFocus(object sender, RoutedEventArgs e)   //获取焦点执行的事件
        {
            TextBox txtbox2 = sender as TextBox;
            tempTxt2 = txtbox2.Text;
            txtbox2.Text = string.Empty;
            txtbox2.Background = System.Windows.Media.Brushes.White;          //获取焦点后，将文本框的背景色改成白色
            txtbox2.BorderBrush = System.Windows.Media.Brushes.Transparent;
            pic_search.Visibility = Visibility.Hidden;    //获取焦点后，隐藏搜索图标
            pic_offline3.Visibility = Visibility.Visible; //获取焦点后，显示关闭图标
        }

        private void txtbox2_LostFocus(object sender, RoutedEventArgs e)   //丢失焦点之后，该处理的事件
        {
            if (txtbox2.Text == string.Empty)
            {
                txtbox2.Text = tempTxt2;
            }
            pic_search.Visibility = Visibility.Visible;      //失去焦点后，重现隐藏图标
            pic_offline3.Visibility = Visibility.Hidden;     //失去焦点后，隐藏关闭图标
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string spath = "D:\\Program Files (x86)\\IMCC\\skins";
            

            if (Directory.Exists(spath))
            {
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string skin_name = cfa.AppSettings.Settings["BK_NUM"].Value;
                string file2 = spath + "\\" + cfa.AppSettings.Settings["BK_NUM"].Value;
                cfa.Save();
                if(File.Exists(file2))
                {
                    BitmapImage bg1 = new BitmapImage(new Uri(file2));
                    background.Source = bg1;
                }
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(spath);
                directoryInfo.Create();
            }


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
                    if(hd1!=null)
                    {
                        header.ImageSource = hd1;
                    }
                    
                }
                else
                {
                    Bitmap b3 = PicHelp.DownloadPic(ip_address, "pic");
                    if(b3!=null)
                    {
                        Configuration cfa2 = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        int rand = GlobeClass.GetRandomSeed();
                        string rand_name = rand + ".png";
                        cfa2.AppSettings.Settings["HD_NUM"].Value = rand_name;
                        string file3 = spath1 + "\\" + cfa2.AppSettings.Settings["HD_NUM"].Value;
                        cfa2.Save();
                        ConfigurationManager.RefreshSection("appSettings");

                        b3.Save(file3);
                        BitmapImage hd2 = new BitmapImage(new Uri(file3));
                        if (hd2 != null)
                        {
                            header.ImageSource = hd2;
                        }
                    }
                    
                }
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(spath1);
                directoryInfo.Create();
            }
            

            UserInformation user = UserInformationGet.GetUserInfor(ip_address);
            GlobalUse._server_config.Ip_config_class = ConfigurationManager.AppSettings["IP_CONFIG"];
            GlobalUse._server_config.Port_config_class = ConfigurationManager.AppSettings["PORT_CONFIG"];
            //string ip_con = "172.18.114.137";
            //string port = "51111";
            //IPHelper.IP_connected(ip_con,port);
            lab_name.Content = user.netname;
            lab_qian.Content = user.geqian_data;
            if (Judge.FriendJudge(ip_address) == 1 && Judge.GroupJudge(ip_address) == 1)
            {
                Test.GetGroupList(ip_address).ToList().ForEach(s =>
                {
                    Expander t = new Expander();
                    t.Header = s;
                    t.Background = System.Windows.Media.Brushes.Transparent;
                    t.HeaderTemplate = this.FindResource("ExpanderHeaderTemplate") as DataTemplate;
                    ListView v = new ListView();
                    
                    v.ItemsSource = s.Children;
                    v.Width = 280;
                    v.Background = System.Windows.Media.Brushes.Transparent;
                    v.BorderThickness = new Thickness(0);
                    v.MouseDoubleClick += V_MouseDoubleClick;
                    v.ItemTemplate = this.FindResource("FriendList") as DataTemplate;
                    v.SelectionMode = SelectionMode.Single;
                    t.Content = v;
                    FriendListControl.Children.Add(t);
                    object obj = this.FindResource("TextBlockStateStyle");

                });
            }
        }


        private void V_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ip_server = GlobalUse._server_config.Ip_config_class;
            //port_server = GlobalUse._server_config.Port_config_class;
            //throw new NotImplementedException();
            ListView v = (ListView)sender;
            FriendInfo f = v.SelectedItem as FriendInfo;
            SingleChat s1 = new SingleChat(ip_address, f.friend_ip, GlobalUse._server_config.Ip_config_class, GlobalUse._server_config.Port_config_class);
            s1.Show();
        }

        private void Profile(object sender, MouseButtonEventArgs e)
        {
            MyProfile m1 = new MyProfile(ip_address, "null");
            m1.Show();
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            scrollViewer.RaiseEvent(eventArg);
        }

        private void message_Click(object sender, RoutedEventArgs e)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string skin_name = cfa.AppSettings.Settings["BK_NUM"].Value;
            MessageBox.Show(skin_name);
            //SingleChat c1 = new SingleChat();
            //c1.Show();

        }

        private void add_new_Click(object sender, RoutedEventArgs e)
        {
            AddNew a1 = new AddNew(ip_address);
            a1.Show();
        }

        private void FriendShow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m1 = new MainWindow(ip_address);
            m1.Show();
            this.Close();
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            SettingApp s1 = new SettingApp();
            s1.Show();
        }

        private void skins_change_Click(object sender, RoutedEventArgs e)
        {
            ChangeSkins sk1 = new ChangeSkins();
            sk1.Show();
        }
    }
}
