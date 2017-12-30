using Microsoft.Win32;
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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.IO;
using System.Configuration;
using System.Drawing;

namespace QQ_presentation
{
    /// <summary>
    /// ChangeImg.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeImg : Window
    {
        string table = "pic";
        string fileContents = null;
        byte[] buffer = null;
        string file = null;
        BitmapImage bitmapimg = new BitmapImage();
        string ip_address;
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        public ChangeImg(string ip_get)
        {
            InitializeComponent();
            ip_address = ip_get;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;   //主窗口最小化
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "图片(*.jpg;*.png;*.gif;*.bmp;*.jpeg)|*.jpg;*.png;*.gif;*.bmp;*.jpeg";
            if ((bool)openfiledialog.ShowDialog())
            {

                bitmapimg = new BitmapImage(new Uri(openfiledialog.FileName));
                file = openfiledialog.FileName;
                img_show.Source = bitmapimg;
            }
        }

        private void upload_Click(object sender, RoutedEventArgs e)
        {
            PicHelp.UploadPic(ip_address, file, table);
        }

        private void reload_Click(object sender, RoutedEventArgs e)
        {
            PicHelp.ReloadPic(ip_address, file, table);

        }

    }
}
