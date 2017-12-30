using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// SettingApp.xaml 的交互逻辑
    /// </summary>
    public partial class SettingApp : Window
    {
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();


        }
        public SettingApp()
        {
            InitializeComponent();
            this.DataContext = GlobalUse._server_config;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void finnish_Click(object sender, RoutedEventArgs e)
        {
            
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["IP_CONFIG"].Value = ip_config.Text;
            cfa.AppSettings.Settings["PORT_CONFIG"].Value = port_config.Text;
            cfa.Save();
            ConfigurationManager.RefreshSection("appSettings");
            GlobalUse._server_config.Ip_config_class = ConfigurationManager.AppSettings["IP_CONFIG"];
            GlobalUse._server_config.Port_config_class = ConfigurationManager.AppSettings["PORT_CONFIG"];
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ip_config.Text = ConfigurationManager.AppSettings["IP_CONFIG"];
            port_config.Text = ConfigurationManager.AppSettings["PORT_CONFIG"];
        }
    }
}
