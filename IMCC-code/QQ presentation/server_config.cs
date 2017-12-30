using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQ_presentation
{
    
    public class server_config : INotifyPropertyChanged
    {
        private string ip_config_class;
        public string Ip_config_class
        {
            get { return ip_config_class; }
            set
            {
                ip_config_class = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Ip_config_class"));
            }
        }
        private string port_config_class;
        public string Port_config_class
        {
            get { return port_config_class; }
            set
            {
                port_config_class = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Port_config_class"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public server_config()
        {
            Ip_config_class = "";
            Port_config_class = "";
        }
    }
    public class GlobalUse
    {
        public static server_config _server_config { get; set; }
        static GlobalUse()
        {
            _server_config = new server_config();
        }
    }
}
