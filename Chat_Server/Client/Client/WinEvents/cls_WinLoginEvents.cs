using System;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Threading;
using Client.ClassInfo;

namespace Client
{ 
    public partial class Window1 : Window
    {
        /// <summary>
        /// 登陆窗体默认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Win_Login_Loaded(object sender, RoutedEventArgs e)
        {            
            this.userNumber.Focus();
            loginInit.Init(this);
            UserDefinition.WinLogin = this;
            UserDefinition.LoginWindowState = true;
            Application_Init.Initial();
        }
        /// <summary>
        /// 拖动窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.border1.Visibility = Visibility.Hidden;
                this.border2.Visibility = Visibility.Hidden;
                try
                {
                    this.DragMove();
                }
                catch (Exception)
                { }
            }
        } 
        /// <summary>
        /// 记录当前窗体的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUp_Window(object sender, MouseEventArgs e)
        {
            Location.WindowLeft = this.Left;
            Location.WindowTop = this.Top;
        }
        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Close(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Application_Init.setFreeNotifyIcon();
                Environment.Exit(Environment.ExitCode);
            }
        }
        /// <summary>
        /// 最小化按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Minimize(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                UserDefinition.LoginWindowState = false;
                Hide();
            }
        }
        /// <summary>
        /// 隐藏提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void username_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.border1.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 隐藏提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.border2.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 隐藏提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void username_MouseEnter(object sender, MouseEventArgs e)
        {
            this.border1.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 隐藏提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pwdBox_MouseEnter(object sender, MouseEventArgs e)
        {
            this.border2.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {            
            Validate.ChangeState(false, this);
        }
        /// <summary>
        /// 窗体关闭时发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Win_Login_Closed(object sender, EventArgs e)
        {
            UserDefinition.LoginWindowClose = true;
        }
        private string userNo;
        private string userInfo;
        private void ConnectServer()
        {
            userNo = userNumber.Text.Trim();
            string userPassword = pwdBox.Password;
            userInfo = userNo + "|" + userPassword;
            try
            {
                TcpClient clientLogin = new TcpClient();
                clientLogin.BeginConnect(
                    IpConfig.getIp(),
                    IpConfig.ServerPort,
                    new AsyncCallback(ConnectCallback),
                    clientLogin);
            }
            catch (SocketException)
            {
                //无法连接服务器

                Dispatcher.BeginInvoke((Action)(() => {
                    Validate.ChangeState(false, this);
                    CueWindow.Show("登陆服务器失败，请稍候重试！");
                }));
            }
        }
        private void ConnectCallback(IAsyncResult iar)
        {
            try
            {
                TcpClient client = iar.AsyncState as TcpClient;
                if (client != null)
                {
                    client.EndConnect(iar);
                    NetworkStream netStream = client.GetStream();
                    //向服务器发送用户信息
                    netStream.Write(Encoding.Default.GetBytes(userInfo), 0, Encoding.Default.GetBytes(userInfo).Length);
                    byte[] returnMsg = new byte[32];
                    int len = netStream.Read(returnMsg, 0, returnMsg.Length);
                    string userInfoValidate = Encoding.Default.GetString(returnMsg, 0, len).TrimEnd('\0');//得到服务器返回结果
                    if (!userInfoValidate.Equals("Successful"))
                    {
                        if (userInfoValidate.Equals("ErrorPassword"))
                        {
                            //密码错误

                            Dispatcher.BeginInvoke((Action)(() => {
                                Validate.ChangeState(false, this);
                                CueWindow.Show("对不起，您的密码输入错误");
                                this.pwdBox.Focus(); 
                            }));
                        }
                        else
                        {
                            //该用户不存在
                            Dispatcher.BeginInvoke((Action)(() => {
                                Validate.ChangeState(false, this);
                                CueWindow.Show("该用户不存在，请注册！");
                            }));
                        }
                    }
                    else
                    {
                        //returnMsg = new byte[32];
                        //netStream.Read(returnMsg, 0, returnMsg.Length);
                        //string userOnlyValidate = Encoding.Default.GetString(returnMsg).TrimEnd('\0');                    
                        //if (userOnlyValidate.Equals("cmd::Successful"))
                        //{
                        Dispatcher.BeginInvoke((Action)(() => {
                            loginClient(userNo, netStream, client);
                        }));
                        //}
                        //else
                        //{
                        //    return "Fail";
                        //}
                    }
                }
            }
            catch (SocketException)
            {
                //无法连接服务器

                Dispatcher.BeginInvoke((Action)(() => {
                    Validate.ChangeState(false, this);
                    CueWindow.Show("登陆服务器失败，请稍候重试！");
                }));
            }
        }
        private void loginClient(
            string userNumber,
            NetworkStream netStream,
            TcpClient client)
        {
            byte[] returnMsg = new byte[64 * 1024];
            int bytCount = netStream.Read(returnMsg, 0, returnMsg.Length);
            IFormatter format = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            stream.Write(returnMsg, 0, bytCount);
            stream.Position = 0;
            StringCollection onlineList = (StringCollection)format.Deserialize(stream);
            FriendListInit.friend = onlineList;

            Application_Init.notifyIconUserState(UserDefinition.WinLogin.State.SelectedIndex);
            UserDefinition.UserLoginState = UserDefinition.WinLogin.State.SelectedIndex;
            UserDefinition.UserNickName = userNumber;
            UserDefinition.UserDictum = "路上没有失败，只有暂时的不成功！";
            UserDefinition.MainWindow = new Win_Main(userNumber, netStream);
            this.Close();
            Location.WindowLocation(UserDefinition.MainWindow);
        }
    }
}
