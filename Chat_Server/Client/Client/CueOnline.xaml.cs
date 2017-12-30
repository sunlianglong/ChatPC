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
using System.Threading;
using System.Windows.Threading;
using Client.ClassInfo;

namespace Client
{
    /// <summary>
    /// CueOnline.xaml 的交互逻辑
    /// </summary>
    public partial class CueOnline : Window
    {
        private delegate void rollWindow();
        public CueOnline()
        {
            InitializeComponent();
            this.Left = Location.WindowWidth - 255;
            this.Top = Location.WindowHeight;
            UserDefinition.timer = new DispatcherTimer();
            UserDefinition.timer.Interval = TimeSpan.FromSeconds(0.5);
            UserDefinition.timer.Tick += new EventHandler(CueOnlineWindow);
            UserDefinition.timer.Start();
        }
        public void CueOnlineWindow(object sender, EventArgs e)
        {
            for (int start = 1; start <= 17; start++)
            {
                this.Top -= start;
                Thread.Sleep(30);
            }
            UserDefinition.timer.Stop();
            Thread.Sleep(2000);
            for (int start = 0; start <= 17; start++)
            {
                this.Top += start;
                Thread.Sleep(30);
            }
            this.Close();

        }
        private void WinClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            for (int start = 0; start <= 17; start++)
            {
                this.Top += start;
                Thread.Sleep(30);
            }
            this.Close();
        }        
    }
}
