using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Win_Loading.xaml 的交互逻辑
    /// </summary>
    public partial class Win_Loading : Window
    {
        public Win_Loading()
        {
            InitializeComponent();
            //Validate.ShowLoginNotifyImage(this);            
        }
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
        private void MouseDown_Exit(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        private void MouseDown_Minimize(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();            
            //UserDefinition.WinLogin.Show();
            //UserDefinition.WinLogin.userNumber.Text = this.UserInputNumber.Text.Trim();
        }                
    }
}
