using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// AddNew.xaml 的交互逻辑
    /// </summary>
    public partial class AddNew : Window
    {
        string ip_address;
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        public AddNew(string user_ip)
        {
            InitializeComponent();
            ip_address = user_ip;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;   //主窗口最小化
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            SearchListView.RaiseEvent(eventArg);
        }
        private void search_friend_btn_Click(object sender, RoutedEventArgs e)
        {
            string search_ip = search_friend_txt.Text;
            //搜索好友

            ObservableCollection<UserInformation> friends = UserInfoGetList.GetList<UserInformation>(search_ip);
            SearchListView.ItemsSource = friends;
            SearchListView.ItemTemplate = this.FindResource("SearchList") as DataTemplate;
            SearchListView.SelectionMode = SelectionMode.Single;
            SearchListView.Width = 300;
            SearchListView.MouseDoubleClick += click_add_new_click;



        }
        private void click_add_new_click(object sender, RoutedEventArgs e)
        {
            ListView v = (ListView)sender;
            UserInformation f = v.SelectedItem as UserInformation;
            MyProfile m2 = new MyProfile(ip_address,f.ip_address);
            m2.Show();
        }
        
    }
}
