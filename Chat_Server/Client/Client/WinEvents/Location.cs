using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class Location
    {
        /// <summary>
        /// 监控窗体与显示器的左边距
        /// </summary>
        public static Double WindowLeft { get; set; }
        /// <summary>
        /// 监控窗体与显示器顶部边距
        /// </summary>
        public static Double WindowTop { get; set; }
        /// <summary>
        /// 显示器工作区的宽度
        /// </summary>
        public static Double WindowWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width;
        /// <summary>
        /// 显示器工作区的高度
        /// </summary>
        public static Double WindowHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
        /// <summary>
        /// 定义登陆窗体在显示器中的位置
        /// </summary>
        /// <param name="Winlogin">得到登陆窗体实例名称</param>
        /// <param name="left">得到窗体与当前显示器的左边距</param>
        /// <param name="top">得到窗体与当前显示器的顶部边距</param>
        public static void WindowLocation(Window1 Winlogin, Double left, Double top)
        {
            Winlogin.Left = left;
            Winlogin.Top = top;
            Winlogin.Activate();            
        }
        /// <summary>
        /// 定义主窗体在显示器中的位置
        /// </summary>
        /// <param name="WinMain">得到主窗体的实例名称</param>
        public static void WindowLocation(Win_Main WinMain)
        {
            WinMain.Left = Location.WindowLeft;
            WinMain.Top = Location.WindowTop;
            WinMain.Show();
            WinMain.Activate();
        }
        /// <summary>
        /// 定义提示窗体在显示器中的位置
        /// </summary>
        /// <param name="WinWarn">得到提示窗体的实例名称</param>
        public static void WindowLocation(CueInfo WinWarn)
        {
            WinWarn.Left = Location.WindowLeft - 15;
            WinWarn.Top = Location.WindowTop + 150;
            WinWarn.ShowDialog();
            WinWarn.Activate();
        }
        /// <summary>
        /// 定义删除时提示窗体在显示器中的位置
        /// </summary>
        /// <param name="WinWarn">窗体的实例名称</param>
        public static void WindowLocation(CueDelInfo WinWarn)
        {
            WinWarn.Left = Location.WindowLeft - 15;
            WinWarn.Top = Location.WindowTop + 150;
            WinWarn.ShowDialog();
            WinWarn.Activate();
        }
    }
}
