using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Sockets;
using Client.ClassInfo;

namespace Client
{
    /// <summary>
    /// Win_Main.xaml 的交互逻辑
    /// </summary>
    public partial class Win_Main : Window
    {        
        /// <summary>
        /// 拖动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {                
                try
                {
                    this.DragMove();                    
                }
                catch (Exception)
                { }
            }
        }
        /// <summary>
        /// 记录当前窗体位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Location.WindowLeft = this.Left;
            Location.WindowTop = this.Top;
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Exit(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                UserDefinition.MainWindowState = false;
                this.Hide();
            }
        } 
        /// <summary>
        /// 最小化窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Minimize(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                UserDefinition.MainWindowState = false;
                this.Hide();                
            }
        }
        /// <summary>
        /// 更新当前登陆状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void State_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Validate.OnlineStateUser(this);
        }

        private void searchAddFriendButton_Click(object sender, RoutedEventArgs e)
        {           
            if(!friendNumber.Text.Trim().Equals(null))
            {
                if (friendNumber.Text.Trim().Length < 5)
                { return; }
                if (int.TryParse(friendNumber.Text.Trim(), out UserDefinition.Number))
                {
                    Validate.ShowSearchImage(this);
                    myNetStream.Write(new byte[] { 0, 5 }, 0, 2);
                    myNetStream.Write(Encoding.Default.GetBytes(friendNumber.Text.Trim()), 0, Encoding.Default.GetBytes(friendNumber.Text.Trim()).Length);                    
                }
            }
        }

        private void AddFriendButton_Click(object sender, RoutedEventArgs e)
        {
            if (friendListBox.SelectedIndex.Equals(-1))
            {
                CueWindow.Show("请选择要您添加的好友！");
            }
            else
            {                
                ListBoxItem listboxitem = (ListBoxItem)friendListBox.SelectedItem;
                string friendNum = ((TextBlock)((StackPanel)listboxitem.Content).Children[1]).Text;
                myNetStream.Write(new byte[] { 0, 6 }, 0, 2);
                myNetStream.Write(Encoding.Default.GetBytes(friendNum), 0, Encoding.Default.GetBytes(friendNum).Length);
            }
        }
    }
}
