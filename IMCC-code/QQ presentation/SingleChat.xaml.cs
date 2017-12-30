using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QQ_presentation
{
    /// <summary>
    /// SingleChat.xaml 的交互逻辑
    /// </summary>
    public partial class SingleChat : Window
    {
        Socket clientSocket;
        Thread thread;
        string userName;
        string ip_con ;
        string port;
        string friend ;
        public SingleChat(string i,string i_friend,string i_ip,string i_port)
        {
            InitializeComponent();
            userName = i;
            friend = i_friend;
            ip_con = i_ip;
            port = i_port;
        }
        public void IP_connected(string ip_address, string port_num)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //这里的ip地址，端口号都是服务端绑定的相关数据。
            IPAddress ip = IPAddress.Parse(ip_address);
            var endpoint = new IPEndPoint(ip, Convert.ToInt32(port_num));
            try
            {
                clientSocket.Connect(endpoint); //链接有端口号与IP地址确定服务端.
                                                //登录时给服务器发送登录消息
                string str = userName + "|" + " ";
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                clientSocket.Send(buffer);
            }
            catch
            {
                MessageBox.Show("与服务器连接失败");
            }
            thread = new Thread(ReceMsg);
            thread.IsBackground = true; //设置后台线程
            thread.Start();
        }
        void ReceMsg()
        {

            while (true)
            {
                try
                {
                    var buffer = new byte[1024 * 1024 * 2];
                    int dateLength = clientSocket.Receive(buffer); //接收服务端发送过来的数据
                    //把接收到的字节数组转成字符串显示在文本框中。
                    string ReceiveMsg = Encoding.UTF8.GetString(buffer, 0, dateLength);
                    string[] msgTxt = ReceiveMsg.Split('|');
                    string newstr =msgTxt[2];
                    ShowSmsg(newstr);
                }
                catch
                {
                }
            }
        }

        private void ShowSmsg(string mess)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        (ThreadStart)delegate ()
        {
            string FriendText = mess;
            if (FriendText != "")
            {
                ChatControl chatcontro2 = new ChatControl
                {
                    Message = FriendText,
                    ChatSourceType = ChatSourceEnum.Sender
                };
                MyStackPanel.Children.Add(chatcontro2);
                FriendText = "";
                MyScrollViewer.ScrollToEnd();
            }
        });


        }
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //App.Current.Shutdown();                                //主窗口关闭
            this.Close();  //这行代码也是关闭窗口
            clientSocket.Close();
        }
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;   //主窗口最小化
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Bitmap b3 = PicHelp.DownloadPic(friend, "pic");
            if (b3 != null)
            {
                header.ImageSource = PicHelp.ChangeBitmapToImageSource(b3);
            }
            IP_connected(ip_con, port);
            UserInformation f1 = UserInformationGet.GetUserInfor(friend);
            nickname.Content = f1.netname;
            ip_address_show.Content = f1.ip_address;
            singure.Content = f1.geqian_data;

        }



        private void RightPicButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图像文件文件(*.jpg,*.png)|*.jpg;*.png";
            openFileDialog.Title = "选取图像";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string location in openFileDialog.FileNames)
                {
                    ImageSource source = new BitmapImage(new Uri(location));
                    if (source != null)
                    {
                        ChatControl chatcontrol = new ChatControl
                        {
                            Message = source,
                            ChatSourceType = ChatSourceEnum.Sender
                        };
                        MyStackPanel.Children.Add(chatcontrol);
                        MyScrollViewer.ScrollToEnd();
                    }
                }
            }
        }
        private void LeftPicButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图像文件文件(*.jpg,*.png)|*.jpg;*.png";
            openFileDialog.Title = "选取图像";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string location in openFileDialog.FileNames)
                {
                    ImageSource source = new BitmapImage(new Uri(location));
                    if (source != null)
                    {
                        ChatControl chatcontrol = new ChatControl
                        {
                            Message = source,
                            ChatSourceType = ChatSourceEnum.Receiver
                        };
                        MyStackPanel.Children.Add(chatcontrol);
                        MyScrollViewer.ScrollToEnd();
                    }
                }
            }
        }
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LeftTextBox.Text != "")
                {
                    ChatControl chatcontrol = new ChatControl
                    {
                        Message = LeftTextBox.Text,
                        ChatSourceType = ChatSourceEnum.Receiver
                    };
                    MyStackPanel.Children.Add(chatcontrol);
                    MyScrollViewer.ScrollToEnd();
                    //界面显示消息
                    string newstr = friend + ":" + LeftTextBox.Text;
                    //发往服务器的消息     格式为 （发送者|接收者|信息）
                    string str = userName + "|" + friend + "|" + LeftTextBox.Text;
                    //将消息转化为字节数据传输
                    byte[] buffer = Encoding.UTF8.GetBytes(str);
                    clientSocket.Send(buffer);
                    LeftTextBox.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("与服务器连接失败");
            }
        }

        private void nickname_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MyProfile p1 = new MyProfile(userName, friend);
            p1.Show();
        }
    }
}
