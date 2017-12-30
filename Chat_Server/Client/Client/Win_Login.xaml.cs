using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Interop;
using Client.ClassInfo;

namespace Client
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public Window1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 登陆按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void land_Click(object sender, RoutedEventArgs e)
        {           
            this.border1.Visibility = Visibility.Hidden;
            this.border2.Visibility = Visibility.Hidden;
            UserDefinition.inputVal = Validate.LandVal(this);
            if (UserDefinition.inputVal)
            {
                Validate.ChangeState(true, this);
                ConnectServer();
            }
        }       

        private void Automatism_Checked(object sender, RoutedEventArgs e)
        {
            Remember.IsChecked = true;
        }
        /// <summary>
        /// 打开注册页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Register(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://"+ IpConfig.getIp() +"/Register/Reg.aspx");
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("浏览器错误！");
            }
        }
        /// <summary>
        /// 打开密码找回页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Recover(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://" + IpConfig.getIp() + "/Recover/RecoverPassword.aspx");
            }
            catch
            {
                System.Windows.MessageBox.Show("浏览器错误！");
            }
        }
    }   
}
