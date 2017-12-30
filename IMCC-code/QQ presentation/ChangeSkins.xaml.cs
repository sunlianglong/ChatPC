using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

namespace QQ_presentation
{
    /// <summary>
    /// ChangeSkins.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeSkins : Window
    {
        string file = null;
        BitmapImage bitmapimg = new BitmapImage();
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        public ChangeSkins()
        {
            InitializeComponent();
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
            string spath = "D:\\Program Files (x86)\\IMCC\\skins";
            if (Directory.Exists(spath))
            {

            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(spath);
                directoryInfo.Create();
            }

            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            int rand = GlobeClass.GetRandomSeed();
            string rand_name = rand + ".png";
            cfa.AppSettings.Settings["BK_NUM"].Value = rand_name;
            string file2 = spath+"\\" + cfa.AppSettings.Settings["BK_NUM"].Value;
            cfa.Save();
            ConfigurationManager.RefreshSection("appSettings");
            FileInfo f1 = new FileInfo(file);
            f1.CopyTo(file2, true);
            MessageBox.Show("更换完成！");
            
        }
    }
}
