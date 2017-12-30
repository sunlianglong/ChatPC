using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Documents;
using System.Media;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Threading;
using WpfApplication1;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Client.ClassInfo;

namespace Client
{
    public partial class Win_Talking : IPopWindowAction
    {
        /// <summary>
        /// 对方号码
        /// </summary>
        private string friendnumber = null;
        /// <summary>
        /// 自己的号码
        /// </summary>
        private string myNumber = null;
        /// <summary>
        /// 发送和接爱信息的网络流
        /// </summary>
        private NetworkStream MyNetStream = null;
        /// <summary>
        /// 接收到消息的提示音 
        /// shakePlay-抖动窗体声音
        /// </summary>                
        SoundPlayer shakePlay = new SoundPlayer(Properties.Resources.shake);
        /// <summary>
        /// 消息来时闪烁任务栏
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="WinkIf"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hWnd,bool WinkIf);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName">当前用昵称</param>
        /// <param name="netStream">发送和接受信息的网络流</param>
        public Win_Talking(string userNumber,string friendNumber,NetworkStream netStream)
        {
            InitializeComponent();                       
            this.Icon = UserImageIcon.Source;
            this.Title = friendNumber;
            this.MyNetStream = netStream;
            this.myNumber = userNumber;
            this.friendnumber = friendNumber;
            this.FriendNickName.Text = friendNumber;
            this.Dictum.Text = UserDefinition.UserDictum;
            this.Key = friendNumber;
        }
        //初始化窗体
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //初始化FontFamily-设置字体样式
            System.Drawing.FontFamily[] MyFamilies = System.Drawing.FontFamily.Families;
            foreach (System.Drawing.FontFamily MyFamily in MyFamilies)
            {
                FontFamilyBox.Items.Add(MyFamily.Name);
            }
            //初始化FontSize-设置字体大小
            for (int i = 8; i <= 72; i += 2)
            {
                FontSizeBox.Items.Add(i);
                switch (i)
                {
                    case 24:
                        i = 32; continue;
                    case 34:
                        i = 46; continue;
                    case 48:
                        i = 70; continue;
                }
            }
        }      
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange msgBoxContent = new TextRange(this.MyMessageBox.Document.ContentStart, this.MyMessageBox.Document.ContentEnd);
            if (!msgBoxContent.IsEmpty)
            {                
                Grid gridMessage;
                String SendMessage = ChatProcess.CheckOut(this.MyMessageBox);
                String msgContent = SendMessage.Substring(0, 5);
                if (msgContent.Equals("#FONT"))
                {
                    MyNetStream.Write(Encoding.Default.GetBytes(this.friendnumber), 0, Encoding.Default.GetBytes(this.friendnumber).Length);
                    MyNetStream.Write(Encoding.Default.GetBytes(SendMessage), 0, Encoding.Default.GetBytes(SendMessage).Length);
                    //this.MyMessageBox得到要发送的消息,this.ViewMessageBox用于传送消息框的宽度
                    gridMessage = ChatProcess.AppendMessage(this.MyMessageBox, this.ViewMessgeBox, this.myNumber);                    
                    BlockUIContainer MessageContent = new BlockUIContainer(gridMessage);
                    this.ViewMessgeBox.Document.Blocks.Add(MessageContent);
                    this.ViewMessgeBox.ScrollToEnd();
                    this.MyMessageBox.Document.Blocks.Clear();
                }
                else
                {
                    String[] picFileName = SendMessage.Split('|');
                    Byte[] imageByte = File.ReadAllBytes(picFileName[1]);
                    String sendMsg = String.Format("{0}:{1}:{2}:{3}:{4}", imageByte.Length.ToString(), this.friendnumber, picFileName[2], picFileName[3], picFileName[4]);
                    
                    MyNetStream.Write(new Byte[] { 0, 8 }, 0, 2);                                                            
                    MyNetStream.Write(Encoding.Default.GetBytes(sendMsg), 0, Encoding.Default.GetBytes(sendMsg).Length);                   
                    //this.MyMessageBox得到要发送的消息,this.ViewMessageBox用于传送消息框的宽度
                    gridMessage = ChatProcess.AppendMessage(this.MyMessageBox, this.ViewMessgeBox, this.myNumber);
                    BlockUIContainer MessageContent = new BlockUIContainer(gridMessage);
                    this.ViewMessgeBox.Document.Blocks.Add(MessageContent);
                    this.ViewMessgeBox.ScrollToEnd();
                    this.MyMessageBox.Document.Blocks.Clear();
                    FileStream file = new FileStream(picFileName[1], FileMode.Open, FileAccess.Read);
                    BinaryReader binaryreader = new BinaryReader(file);
                    Byte[] imgBuffer = new Byte[1024];
                    int bytesRead = 0; int dataSize = 0;
                    do
                    {
                        bytesRead = binaryreader.Read(imgBuffer, 0, 1024);
                        MyNetStream.Write(imgBuffer, 0, bytesRead);
                        dataSize = (int)(bytesRead / 1024);
                    } while (dataSize > 0);
                    file.Close();
                    binaryreader.Close();
                    //MyNetStream.Write(imageByte, 0, imageByte.Length);
                }
            }
        }
        private void Img_SendPic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "请选择要发送的图片";
            OFD.Filter = "图像文件(*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png";
            OFD.Multiselect = false;
            if ((bool)OFD.ShowDialog())
            {                                              
                String FileName = OFD.SafeFileName;                
                if (FileName != null)
                {
                    String[] FileChunk = FileName.Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    String fileExtension = FileChunk[FileChunk.Length - 1].ToLower();
                    switch (fileExtension)
                    {
                        //GIF 动画文件
                        case "gif":
                            ImageExpender img_Ex = new ImageExpender();
                            using (FileStream fs = new FileStream(OFD.FileName, FileMode.Open, FileAccess.Read))
                            {
                                Stream imageStream = fs;
                                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageStream);
                                img_Ex.Image = bitmap;
                                img_Ex.Location = OFD.FileName;
                                img_Ex.ToolTip = OFD.FileName + "|" + fileExtension;                                
                                ChatViewModel.AppendGif(this, img_Ex);
                            }
                            break;
                        //图片文件
                        default:                     
                            Image img = img = new Image();
                            img.Source = new BitmapImage(new Uri(OFD.FileName, UriKind.Absolute));
                            img.ToolTip = OFD.FileName + "|" + fileExtension;                            
                            ChatViewModel.AppendImage(this, img);
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 发送抖动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Img_ShakeWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            MyNetStream.Write(new byte[] { 0, 4 }, 0, 2);            
            MyNetStream.Write(Encoding.Default.GetBytes(this.friendnumber), 0, Encoding.Default.GetBytes(this.friendnumber).Length);
            TalkWinModel.ShakeWindow(this);
            Grid gridMsg = new Grid();
            gridMsg.RowDefinitions.Add(new RowDefinition());
            gridMsg.RowDefinitions.Add(new RowDefinition());
            gridMsg.Margin = new Thickness(3, 3, 3, 3);
            TextBlock serverMessage = new TextBlock();
            serverMessage.Foreground = Brushes.Gray;
            serverMessage.Text = "你向" + friendnumber + "发送了一个抖动窗体";
            serverMessage.Margin = new Thickness(20, 0, 0, 0);
            gridMsg.Children.Add(serverMessage);
            Grid.SetRow(serverMessage, 1);
            gridMsg = ChatProcess.AppendShakeWinHeader(gridMsg, "success");
            BlockUIContainer MessageContent = new BlockUIContainer(gridMsg);                       
            this.ViewMessgeBox.Document.Blocks.Add(MessageContent);
            this.ViewMessgeBox.ScrollToEnd();
        }
        #region IPopWindowAction 成员

        public void DisposeToManager()
        {
            UserDefinition.popWindowManager.Remove(this.Key);
        }

        #endregion

        #region IMetaData 成员

        private String key;
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        #endregion

        private void WindowTalking_Closed(object sender, EventArgs e)
        {
            DisposeToManager();
        }
    }
}
