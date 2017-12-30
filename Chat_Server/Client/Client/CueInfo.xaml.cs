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

namespace Client
{
    /// <summary>
    /// CueInfo.xaml 的交互逻辑
    /// </summary>
    public partial class CueInfo : Window
    {
        public CueInfo()
        {
            InitializeComponent();            
        }
        /// <summary>
        /// 拖动当前窗体
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
        /// 隐藏当前窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Exit(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.Visibility = Visibility.Collapsed;                
            }
        }
        /// <summary>
        /// 隐藏当前窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Certain_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed; 
        }

    }
}
