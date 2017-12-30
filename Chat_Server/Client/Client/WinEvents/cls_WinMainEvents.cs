using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Media;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
using Microsoft.Win32;
using WpfApplication1;
using Client.ClassInfo;
using System.Windows.Interop;

namespace Client
{
    /// <summary>
    /// Win_Main.xaml 的交互逻辑
    /// </summary>
    public partial class Win_Main : Window
    {        
        /// <summary>
        /// 数据缓冲区大小
        /// </summary>
        private int maxPacket = 1024 * 1024;
        /// <summary>
        /// 接受消息线程
        /// </summary>
        private Thread receiveThread = null;
        /// <summary>
        /// 用户号码
        /// </summary>
        public static string myNumber;
        /// <summary>
        /// 发送和接收消息的网络流
        /// </summary>
        public static NetworkStream myNetStream;
        /// <summary>
        /// 接收到消息的提示音
        /// msgPlay-消息声音       
        /// </summary>
        private SoundPlayer msgPlay = new SoundPlayer(Properties.Resources.msg);
        /// <summary>
        /// 添加listboxitem
        /// </summary>
        private ListBoxItem items;
        /// <summary>
        /// 添加查找的用户文本内容
        /// </summary>
        private TextBlock friendText;
        /// <summary>
        /// 服务器关闭
        /// </summary>
        private delegate void cueServerDisconnection();
        /// <summary>
        /// 委托事件
        /// 向RichTextBox中填加内容
        /// </summary>
        /// <param name="addMessage">要添加信息的内容</param>
        /// <returns></returns>
        private delegate void viewReceiveMesssage(string addMessage);
        /// <summary>
        /// 委托事件
        /// 发送抖动窗体
        /// </summary>
        private delegate void shakeWindow(string shakeMyWin);
        /// <summary>
        /// 委托事件
        /// 查找好友
        /// </summary>
        private delegate void searchFriendEvent(string searchFriendMsgResult);
        /// <summary>
        /// 委托事件
        /// 添加好友
        /// </summary>
        /// <param name="addMsgResult"></param>
        private delegate void addFriendEvent(String addFriendMsgResult);
        /// <summary>
        /// 委托事件
        /// 删除好友
        /// </summary>
        /// <param name="delFriendMsgResult"></param>
        private delegate void delFriendEvent(String delFriendMsgResult);
        /// <summary>
        /// 委托事件
        /// 接收图片
        /// </summary>
        /// <param name="receiveMsg">接收到的基本信息</param>
        /// <param name="imgInfo">图片Byte数组</param>
        private delegate void receiveImageEvent(Byte[] imgInfo, String msg);
        /// <summary>
        /// 创建字符串转换为Brush的实例
        /// </summary>
        private BrushConverter color = new BrushConverter();
        /// <summary>
        /// 创建字符串转换为FontWeight的实例
        /// </summary>
        private FontWeightConverter fontWeight = new FontWeightConverter();
        /// <summary>
        /// 创建字符串转换为FontStyle的实例
        /// </summary>
        private FontStyleConverter fontStyle = new FontStyleConverter();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userNumber">用户号码</param>
        /// <param name="netStream">发送和接收信息的网络流</param>
        public Win_Main(string userNumber, NetworkStream netStream)
        {
            InitializeComponent();
            InitMainWindow.Init(this);
            UserDefinition.MainWindowState = true;
            myNumber = userNumber;
            myNetStream = netStream;
            //启动接收消息线程
            receiveThread = new Thread(new ThreadStart(ReciveMessage));
            receiveThread.Start();
        }
        /// <summary>
        /// 接受消息
        /// </summary>
        private void ReciveMessage()
        {
            try
            {
                while (true)
                {
                    byte[] packet = new byte[maxPacket];
                    int len = myNetStream.Read(packet, 0, packet.Length);                    
                    string receiveMsg = Encoding.Default.GetString(packet, 0, len).TrimEnd('\0');                    
                    string[] splitCmd = receiveMsg.Split('/');
                    string Cmd = splitCmd[0];                    
                    switch (Cmd)
                    {
                        case "11"://服务器要求做抖动窗体
                            {                                
                                string executeContent = splitCmd[1];
                                string[] splitExecuteContent = executeContent.Split(':');
                                string executeWinNumber = splitExecuteContent[0];
                                Thread shakeWinThread = new Thread(new ParameterizedThreadStart(shakeWinStart));
                                shakeWinThread.Start(executeWinNumber);
                                Cmd = null; executeContent = null;
                                break;
                            }
                        case "12"://服务器返回查找的好友信息
                            {
                                string executeContent = splitCmd[1];
                                Thread searchFriendThread = new Thread(new ParameterizedThreadStart(searchFiendExecute));
                                searchFriendThread.Start(executeContent);
                                Cmd = null; executeContent = null;
                                break;
                            }
                        case "13"://服务器返回添加好友信息
                            {
                                string executeContent = splitCmd[1];
                                Thread addFriendThread = new Thread(new ParameterizedThreadStart(addFriendExecute));
                                addFriendThread.Start(executeContent);
                                Cmd = null; executeContent = null;
                                break;
                            }
                        case "14"://服务器返回删除好友信息
                            {
                                string executeContent = splitCmd[1];
                                Thread delFriendThread = new Thread(new ParameterizedThreadStart(delFriendExecute));
                                delFriendThread.Start(executeContent);
                                Cmd = null; executeContent = null;
                                break;
                            }
                        case "15"://接收图片
                            {
                                String executeContent = splitCmd[1];
                                int imgSize = Convert.ToInt32(executeContent.Substring(0, executeContent.IndexOf(':')));
                                int imgSplit = (int)(imgSize / 1024), bytesRead = 0;
                                Byte[] imgBuffer = new Byte[1024];
                                MemoryStream imageStream = new MemoryStream();
                                do
                                {
                                    bytesRead = myNetStream.Read(imgBuffer, 0, 1024);
                                    imageStream.Write(imgBuffer, 0, bytesRead);
                                    imgSplit--;
                                } while (imgSplit >= 0);                                
                                Byte[] imageByte = imageStream.ToArray();                                
                                imageStream.Close();
                                receiveImageExecute(imageByte, executeContent);
                                Cmd = null; executeContent = null;
                                break;
                            }
                        default://接收用户消息或系统消息
                            {
                                Cmd = null;
                                //接收用户或系统消息线程
                                Thread receiveMsgThread = new Thread(new ParameterizedThreadStart(receiveMessage));
                                receiveMsgThread.Start(receiveMsg);
                                break;
                            }
                    }
                }
            }
            catch (IOException)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new cueServerDisconnection(serverDisconnection));
            }
        }
        /// <summary>
        /// 服务器关闭时
        /// </summary>
        private void serverDisconnection()
        {
            CueWindow.Show("服务器已关闭，IM即将关闭！");
            Environment.Exit(0);
        }

        public static void AddTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                TreeViewItem item = (TreeViewItem)UserDefinition.MainWindow.FriendList.SelectedItem;
                string friendNumber = ((TextBlock)((StackPanel)item.Header).Children[1]).Text;
                if (!UserDefinition.popWindowManager.Equals(friendNumber))
                {
                    UserDefinition.WinTalk = new Win_Talking(myNumber, friendNumber, myNetStream);
                    UserDefinition.popWindowManager.Add(UserDefinition.WinTalk);
                    UserDefinition.WinTalk.Activate();
                    UserDefinition.WinTalk.Show();                    
                }
                else
                {
                    //得到当前窗体的句柄
                    WindowInteropHelper Helper = new WindowInteropHelper(UserDefinition.WinTalk);
                    IntPtr ptr = Helper.Handle;
                    Win_Talking.FlashWindow(ptr, true);//闪烁任务栏  
                }
            }            
        }

        public static void sendmsgitem_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)UserDefinition.MainWindow.FriendList.SelectedItem;
            string friendNumber = ((TextBlock)((StackPanel)item.Header).Children[1]).Text;
            if (!UserDefinition.popWindowManager.Equals(friendNumber))
            {
                UserDefinition.WinTalk = new Win_Talking(myNumber, friendNumber, myNetStream);
                UserDefinition.popWindowManager.Add(UserDefinition.WinTalk);
                UserDefinition.WinTalk.Activate();
                UserDefinition.WinTalk.Show();
            }
            else
            {
                //得到当前窗体的句柄
                WindowInteropHelper Helper = new WindowInteropHelper(UserDefinition.WinTalk);
                IntPtr ptr = Helper.Handle;
                Win_Talking.FlashWindow(ptr, true);//闪烁任务栏  
            }
        }

        public static void removeitem_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)UserDefinition.MainWindow.FriendList.SelectedItem;
            string friendNumber = ((TextBlock)((StackPanel)item.Header).Children[1]).Text;
            CueDelWindow.Show(friendNumber);
            if (UserDefinition.DelFriendInfo == "Certain")
            {                
                if (UserDefinition.DelFriendInfo == "Certain")
                {
                    myNetStream.Write(new Byte[] { 0, 7 }, 0, 2);
                    myNetStream.Write(Encoding.Default.GetBytes(friendNumber), 0, Encoding.Default.GetBytes(friendNumber).Length);                    
                }
            }
        }
        /// <summary>
        /// 产生抖动窗口
        /// </summary>
        private void ShakeWindow(string myNumWinShake)
        {
            Win_Talking talkWin = UserDefinition.popWindowManager.GetMemberByKey(myNumWinShake) as Win_Talking;
            if (talkWin != null)
            {
                TalkWinModel.ShakeWindow(talkWin);
            }
            else
            {
                UserDefinition.WinTalk = new Win_Talking(myNumber, myNumWinShake, myNetStream);
                UserDefinition.popWindowManager.Add(UserDefinition.WinTalk);
                UserDefinition.WinTalk.Show();
                UserDefinition.WinTalk.Activate();
                TalkWinModel.ShakeWindow(UserDefinition.WinTalk);
            }
        }
        /// <summary>
        /// 执行发送抖动窗口线程
        /// </summary>
        private void shakeWinStart(object shakeWinNumber)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new shakeWindow(ShakeWindow), shakeWinNumber);
        }
        /// <summary>
        /// 执行接收用户或系统消息线程
        /// </summary>
        private void receiveMessage(object receiveMsg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new viewReceiveMesssage(appendtext), receiveMsg);
        }
        /// <summary>
        /// 执行查找好友线程
        /// </summary>
        /// <param name="resultSearch"></param>
        private void searchFiendExecute(object resultSearch)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new searchFriendEvent(searchFriend), resultSearch);
        }
        /// <summary>
        /// 执行添加好友线程
        /// </summary>
        /// <param name="resultAddFriend"></param>
        private void addFriendExecute(object resultAddFriend)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new addFriendEvent(addFriend), resultAddFriend);
        }
        /// <summary>
        /// 执行删除好友线程
        /// </summary>
        /// <param name="resultDelFriend"></param>
        private void delFriendExecute(object resultDelFriend)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new delFriendEvent(delFriend), resultDelFriend);
        }
        private void receiveImageExecute(object imageInfo, String msgContent)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new receiveImageEvent(addImageInfo), imageInfo, msgContent);
        }
        /// <summary>
        /// 委托向RichTextBox中添加消息
        /// </summary>
        /// <param name="Message">要添加的消息</param>
        /// <returns>返回委托事件</returns>
        private void appendtext(string Message)
        {            
            String[] disposeMsg = Message.Split('/');
            String[] executeContent = disposeMsg[0].Split(':');            
            if (executeContent[0].Equals("sendshakefail"))
            {
                string cueMessage = UserDefinition.shakeCueMsg;
                this.appendcuetext(executeContent[1], cueMessage);
            }
            else if (executeContent[0].Equals("sendmessagefail"))
            {
                string cueMessage = UserDefinition.sendMsgCue;
                this.appendcuetext(executeContent[1], cueMessage);
            }
            else
            {                
                //查找该窗口是否存在
                Win_Talking TalkWin = UserDefinition.popWindowManager.GetMemberByKey(executeContent[0]) as Win_Talking;
                if (TalkWin != null)//该窗口存在
                {
                    //2 * 1的表格
                    Grid Total_Grid = new Grid();
                    Total_Grid.RowDefinitions.Add(new RowDefinition());
                    Total_Grid.RowDefinitions.Add(new RowDefinition());
                    WrapPanel UserTalkMessage = new WrapPanel();
                    UserTalkMessage.Margin = new Thickness(20 , 0, 3, 0);
                    UserTalkMessage.Orientation = Orientation.Horizontal;
                    BlockUIContainer MessageContent;
                    string[] explainMessage = disposeMsg[1].Split('|');
                    //检查信息头
                    if (explainMessage[0] == "#FONT")//字符信息
                    {
                        //将字符串转换为Brush
                        Brush brush = (Brush)color.ConvertFromString(explainMessage[1]);
                        //将字符串转换为FontWeight
                        FontWeight weight = (FontWeight)fontWeight.ConvertFromString(explainMessage[3]);
                        //将字符串转换为FontStyle
                        FontStyle style = (FontStyle)fontStyle.ConvertFromString(explainMessage[4]);
                        TextBlock TextMessage = new TextBlock();
                        TextMessage.Foreground = brush;
                        TextMessage.FontFamily = new FontFamily(explainMessage[2]);
                        TextMessage.FontWeight = weight;
                        TextMessage.FontStyle = style;
                        TextMessage.FontSize = double.Parse(explainMessage[5]);
                        TextMessage.Text = explainMessage[6].Replace("(*Split*)","|").Replace("(*Slash*)","/");
                        TextMessage.Width = TalkWin.ViewMessgeBox.ActualWidth - 40;
                        TextMessage.TextWrapping = TextWrapping.Wrap;
                        TextMessage.Margin = new Thickness(0, 0, 0, -15);
                        UserTalkMessage.Children.Add(TextMessage);
                        Total_Grid = ChatProcess.AppendMyHeader(Total_Grid, "#FFB21C1C", executeContent[0]);
                        Total_Grid.Children.Add(UserTalkMessage);
                        Grid.SetRow(UserTalkMessage, 1);
                        MessageContent = new BlockUIContainer(Total_Grid);
                        TalkWin.ViewMessgeBox.Document.Blocks.Add(MessageContent);
                        msgPlay.Play();
                    }
                    else if (explainMessage[0] == "#PIC")//图片信息
                    {
                        ImageExpender image_Gif = new ImageExpender();
                        Stream imageStream = null;
                        System.Drawing.Bitmap bitMap = null;
                        image_Gif.Stretch = Stretch.None;
                        image_Gif.Visibility = Visibility.Visible;
                        imageStream = Application.GetResourceStream(new Uri(@"/SysImage/ImageLoading.gif", UriKind.Relative)).Stream;
                        bitMap = new System.Drawing.Bitmap(imageStream);
                        image_Gif.Image = bitMap;
                        UserTalkMessage.Children.Add(image_Gif);
                        Total_Grid = ChatProcess.AppendMyHeader(Total_Grid, "#FFB21C1C", executeContent[0]);
                        Total_Grid.Children.Add(UserTalkMessage);
                        Grid.SetRow(UserTalkMessage, 1);
                        MessageContent = new BlockUIContainer(Total_Grid);
                        TalkWin.ViewMessgeBox.Document.Blocks.Add(MessageContent);
                        msgPlay.Play();
                    }
                    else//提示信息
                    {                        
                        Grid gridMsg = new Grid();
                        gridMsg.RowDefinitions.Add(new RowDefinition());
                        gridMsg.RowDefinitions.Add(new RowDefinition());
                        gridMsg.Margin = new Thickness(3, 3, 3, 3);
                        TextBlock serverMessage = new TextBlock();
                        serverMessage.Foreground = Brushes.Gray;
                        serverMessage.Text = explainMessage[0];
                        serverMessage.Margin = new Thickness(20, 0, 0, 0);
                        gridMsg.Children.Add(serverMessage);
                        Grid.SetRow(serverMessage, 1);
                        gridMsg = ChatProcess.AppendShakeWinHeader(gridMsg, "success");
                        MessageContent = new BlockUIContainer(gridMsg);
                        TalkWin.ViewMessgeBox.Document.Blocks.Add(MessageContent);
                    }                    
                    TalkWin.ViewMessgeBox.ScrollToEnd();
                }
                //如果该窗口不存在，则打入托盘区消息提示状态
                else
                {
                    UserDefinition.WinCueOnline = new CueOnline();
                    UserDefinition.WinCueOnline.Show();
                    UserDefinition.WinTalk = new Win_Talking(myNumber, executeContent[0], myNetStream);
                    UserDefinition.popWindowManager.Add(UserDefinition.WinTalk);
                    UserDefinition.WinTalk.Show();
                    UserDefinition.WinTalk.WindowState = WindowState.Minimized;

                    //2 * 1的表格
                    Grid Total_Grid = new Grid();
                    Total_Grid.RowDefinitions.Add(new RowDefinition());
                    Total_Grid.RowDefinitions.Add(new RowDefinition());
                    WrapPanel UserTalkMessage = new WrapPanel();
                    UserTalkMessage.Margin = new Thickness(20, 0, 3, 0);
                    UserTalkMessage.Orientation = Orientation.Horizontal;
                    BlockUIContainer MessageContent;
                    string[] explainMessage = disposeMsg[1].Split('|');

                    //将字符串转换为Brush
                    Brush brush = (Brush)color.ConvertFromString(explainMessage[1]);
                    //将字符串转换为FontWeight
                    FontWeight weight = (FontWeight)fontWeight.ConvertFromString(explainMessage[3]);
                    //将字符串转换为FontStyle
                    FontStyle style = (FontStyle)fontStyle.ConvertFromString(explainMessage[4]);
                    TextBlock TextMessage = new TextBlock();
                    TextMessage.Foreground = brush;
                    TextMessage.FontFamily = new FontFamily(explainMessage[2]);
                    TextMessage.FontWeight = weight;
                    TextMessage.FontStyle = style;
                    TextMessage.FontSize = double.Parse(explainMessage[5]);
                    TextMessage.Text = explainMessage[6].Replace("(*Split*)", "|").Replace("(*Slash*)", "/");
                    TextMessage.Width = UserDefinition.WinTalk.ViewMessgeBox.ActualWidth - 40;
                    TextMessage.TextWrapping = TextWrapping.Wrap;
                    TextMessage.Margin = new Thickness(0, 0, 0, -15);
                    UserTalkMessage.Children.Add(TextMessage);
                    Total_Grid = ChatProcess.AppendMyHeader(Total_Grid, "#FFB21C1C", executeContent[0]);
                    Total_Grid.Children.Add(UserTalkMessage);
                    Grid.SetRow(UserTalkMessage, 1);
                    MessageContent = new BlockUIContainer(Total_Grid);
                    UserDefinition.WinTalk.ViewMessgeBox.Document.Blocks.Add(MessageContent);
                    UserDefinition.WinTalk.ViewMessgeBox.ScrollToEnd();
                    msgPlay.Play();
                }
            }
        }
        /// <summary>
        /// 接收图片
        /// </summary>
        /// <param name="addMsg">添加好友信息</param>
        /// <param name="imageByte">图片Byte信息</param>
        private void addImageInfo(Byte[] imageByte, String info)
        {            
            String[] splitInfo = info.Split(':');
            Win_Talking TalkWin = UserDefinition.popWindowManager.GetMemberByKey(splitInfo[1]) as Win_Talking;
            if (TalkWin != null)
            {
                //2 * 1的表格
                Grid Total_Grid = new Grid();
                Total_Grid.RowDefinitions.Add(new RowDefinition());
                Total_Grid.RowDefinitions.Add(new RowDefinition());
                WrapPanel UserTalkMessage = new WrapPanel();
                UserTalkMessage.Margin = new Thickness(20, 0, 3, 0);
                UserTalkMessage.Orientation = Orientation.Horizontal;
                BlockUIContainer msgContent;
                if (splitInfo[2].Equals("gif"))
                {
                    ImageExpender img_Ex = new ImageExpender();
                    img_Ex.Height = Convert.ToDouble(splitInfo[3]); img_Ex.Width = Convert.ToDouble(splitInfo[4]);
                    MemoryStream gifms = new MemoryStream(imageByte);
                    Stream imageStream = gifms;
                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageStream);
                    img_Ex.Image = bitmap;
                    UserTalkMessage.Children.Add(img_Ex);
                }
                else
                {
                    Image img = new Image();
                    img.Height = Convert.ToDouble(splitInfo[3]); img.Width = Convert.ToDouble(splitInfo[4]);
                    BitmapImage bitImage = new BitmapImage();
                    bitImage.BeginInit();
                    MemoryStream ms = new MemoryStream(imageByte);
                    bitImage.StreamSource = ms;
                    bitImage.EndInit();
                    img.Source = bitImage;
                    UserTalkMessage.Children.Add(img);
                }
                Total_Grid = ChatProcess.AppendMyHeader(Total_Grid, "#FFB21C1C", splitInfo[1]);
                Total_Grid.Children.Add(UserTalkMessage);
                Grid.SetRow(UserTalkMessage, 1);
                msgContent = new BlockUIContainer(Total_Grid);
                TalkWin.ViewMessgeBox.Document.Blocks.Add(msgContent);
                TalkWin.ViewMessgeBox.ScrollToEnd();
                msgPlay.Play();
            }            
        }
        /// <summary>
        /// 检测聊天窗体是否存在
        /// </summary>
        /// <param name="Key">聊天窗体唯一标示</param>
        /// <returns>返回false , 表示不存在</returns>
        public static bool CheckChatWindowExists(String Key)
        {
            if (UserDefinition.popWindowManager.GetMemberByKey(Key) == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 向指定的窗体添加提示信息
        /// </summary>
        /// <param name="thisWindow">要指定的窗体接口内容</param>
        /// <param name="Message">提示信息内容</param>
        private void appendcuetext(string thisWindow,string Message)
        {
            Grid gridMsg = new Grid();
            gridMsg.RowDefinitions.Add(new RowDefinition());
            gridMsg.RowDefinitions.Add(new RowDefinition());
            gridMsg.Margin = new Thickness(3, 3, 3, 3);
            TextBlock serverMessage = new TextBlock();
            serverMessage.Foreground = Brushes.Gray;
            serverMessage.Text = Message;
            serverMessage.Margin = new Thickness(20, 0, 0, 0);
            gridMsg.Children.Add(serverMessage);
            Grid.SetRow(serverMessage, 1);
            gridMsg = ChatProcess.AppendShakeWinHeader(gridMsg, "fail");
            BlockUIContainer MessageContent = new BlockUIContainer(gridMsg);
            Win_Talking TalkWin = UserDefinition.popWindowManager.GetMemberByKey(thisWindow) as Win_Talking;
            TalkWin.ViewMessgeBox.Document.Blocks.Add(MessageContent);
            TalkWin.ViewMessgeBox.ScrollToEnd();
        }
        private void Notify_NewMessage_Icon()
        {
            UserDefinition.timer_Notify = new System.Timers.Timer(100);
            UserDefinition.timer_Notify.Elapsed += new System.Timers.ElapsedEventHandler(timer_Notify_Elapsed);
            UserDefinition.timer_Notify.Start();
        }
        static void timer_Notify_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Drawing.Icon notify_newMsg_Icon = new System.Drawing.Icon(@System.Windows.Forms.Application.StartupPath + "\\Icon\\Login.ico", new System.Drawing.Size(16, 16));
            UserDefinition.notifyIcon.Icon = notify_newMsg_Icon;
        }
        /// <summary>
        /// 查找好友
        /// </summary>
        /// <param name="searchResult">返回查找的信息</param>
        private void searchFriend(string searchResult)
        {
            if(searchResult.Equals("0x0010"))
            {
                searchImg.Visibility = Visibility.Collapsed;
                CueWindow.Show("查找的好友信息不存在！");
            }            
            else if (searchResult.Equals("0x0100"))
            {
                searchImg.Visibility = Visibility.Collapsed;
                CueWindow.Show("连接服务器超时！");
            }
            else
            {
                StackPanel friendPanel = new StackPanel();
                friendPanel.Margin = new Thickness(3, 5, 5, 5);
                friendPanel.Orientation = Orientation.Horizontal;
                searchImg.Visibility = Visibility.Collapsed;
                BitmapImage userHeader = new BitmapImage(new Uri("pack://application:,,,/BackImage/header.ico"));
                Image img = new Image();
                img.Height = 18; img.Width = 18; img.Stretch = Stretch.Fill;
                img.Source = userHeader;
                friendPanel.Children.Add(img);
                friendText = new TextBlock();
                friendText.Text = friendNumber.Text.Trim();//用户号码
                friendText.Margin = new Thickness(5, 0, 40, 0);
                friendPanel.Children.Add(friendText);
                friendText = new TextBlock();
                friendText.Text = searchResult;//用户昵称                
                friendPanel.Children.Add(friendText);
                items = new ListBoxItem();
                items.Content = friendPanel;
                friendListBox.Items.Add(items);
                friendNumber.Text = null;
                searchAddFriendButton.IsEnabled = false;
            }
        }
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="addResult"></param>
        private void addFriend(String addResult)
        {
            String[] resultInfo = addResult.Split(':');
            switch (resultInfo[0])
            {
                case "0x1000":
                    {
                        InitMainWindow.addList(resultInfo[1]);
                        CueWindow.Show("添加好友成功！");
                        friendListBox.Items.Clear();
                        searchAddFriendButton.IsEnabled = true;
                        break;
                    }
                case "0x1001":
                    {
                        CueWindow.Show("对不起，您不能添加自己为好友！");
                        friendListBox.Items.Clear();
                        searchAddFriendButton.IsEnabled = true;
                        break;
                    }
                case "0x1010":
                    {
                        CueWindow.Show("该用户已经存在于您的好友列表中！");
                        friendListBox.Items.Clear();
                        searchAddFriendButton.IsEnabled = true;
                        break;
                    }
                default:
                    {
                        CueWindow.Show("0x0100服务器超时！");
                        friendListBox.Items.Clear();
                        searchAddFriendButton.IsEnabled = true;
                        break;
                    }
            }            
        }
        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="delResult"></param>
        private void delFriend(String delResult)
        {
            switch (delResult)
            {
                case "0x1010":
                    {
                        TreeViewItem item = (TreeViewItem)UserDefinition.MainWindow.FriendList.SelectedItem;
                        FriendListInit.ViewItemList.Items.Remove(item);
                        CueWindow.Show("删除好友成功！");
                        break;
                    }
                case "0x1011":
                    {
                        CueWindow.Show("删除好友失败！");
                        break;
                    }
                default:
                    {
                        CueWindow.Show("0x0100服务器超时！");
                        break;
                    }
            }
        }
    }
}