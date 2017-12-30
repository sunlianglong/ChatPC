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
using Client.ClassInfo;

namespace Client
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        private Expander ex = new Expander();
        private ListView list = new ListView();
        private ListViewItem listitem;
        public Window2()
        {
            InitializeComponent();
            for (int i = 1; i <= 6; i++)
            {
                add();
                list.Items.Add(listitem);
            }
            ex.Style = (Style)System.Windows.Application.Current.FindResource("ExpanderStyleNormal");
            ex.Header = "我的好友";            
            ex.Content = list;
            treeView1.Items.Add(ex);
        }
        private void add()
        {            
           BrushConverter bru = new BrushConverter();
           Brush color = (Brush)bru.ConvertFromString("TransParent");
           list.Background = color;
           list.BorderBrush = color;
           listitem = new ListViewItem();
           StackPanel panel = new StackPanel();
           panel.Orientation = Orientation.Horizontal;
           Image image = new Image();
           image.Source = new BitmapImage(new Uri(UserDefinition.AppDirectory + "\\Icon\\Online.ico"));
           image.Height = 40;
           TextBlock text = new TextBlock();
           text.Text = "旋律～～";
           panel.Children.Add(image); panel.Children.Add(text);
           listitem.Content = panel;
        }
    }
}
