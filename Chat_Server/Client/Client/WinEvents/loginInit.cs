using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    class loginInit
    {
        public static void Init(Window1 winLogin)
        {
            //初始化登陆窗体打开的位置
            Location.WindowLeft = Location.WindowWidth - winLogin.Width - 60;
            Location.WindowTop = (Location.WindowHeight - winLogin.Height) / 2;
            Location.WindowLocation(winLogin, Location.WindowLeft, Location.WindowTop);   
        }
    }
}
