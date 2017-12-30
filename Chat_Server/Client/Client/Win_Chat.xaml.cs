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
    /// Win_Chat.xaml 的交互逻辑
    /// </summary>
    public partial class Win_Chat : Window
    {       
        public Win_Chat()
        {
            InitializeComponent();
            this.Icon = UserImageIcon.Source;
            this.Title = UserNickName.Text;
            System.Drawing.FontFamily[] MyFamilies = System.Drawing.FontFamily.Families;
            foreach (System.Drawing.FontFamily MyFamily in MyFamilies)
            {
                this.FontFamilyBox.Items.Add(MyFamily.Name);
            }           
            for (int i = 8; i <= 72; i+=2)
            {
                this.FontSizeBox.Items.Add(i);
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
                this.Close();
                
            }
        }
        private void MouseDown_Minimize(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void FontFamilyBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FontFamilyBox.SelectedItem != null)
            {
                this.MyMessageBox.FontFamily = new FontFamily(this.FontFamilyBox.SelectedItem.ToString());
            }
        }

        private void FontSizeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FontSizeBox.SelectedItem != null)
            {
                this.MyMessageBox.FontSize = (int)this.FontSizeBox.SelectedItem;                
            }
        }

        #region//字体状态栏显示与隐藏
        private void Img_Font_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (FontColumn.Visibility == Visibility.Hidden)
            {
                FontColumn.Visibility = Visibility.Visible;
                FontFamilyBox.Visibility = Visibility.Visible;
                FontSizeBox.Visibility = Visibility.Visible;
                FontBoldButton.Visibility = Visibility.Visible;
                FontItalicButton.Visibility = Visibility.Visible;
                FontUnderlineButton.Visibility = Visibility.Visible;
            }
            else
            {
                FontColumn.Visibility = Visibility.Hidden;
                FontFamilyBox.Visibility = Visibility.Hidden;
                FontSizeBox.Visibility = Visibility.Hidden;
                FontBoldButton.Visibility = Visibility.Hidden;
                FontItalicButton.Visibility = Visibility.Hidden;
                FontUnderlineButton.Visibility = Visibility.Hidden;
            }
        }
        #endregion

    }
}
