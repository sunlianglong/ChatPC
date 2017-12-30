using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.ClassInfo;

namespace Client
{
    class CueWindow
    {
        public static void Show(string Message)
        {
            CueInfo WinCue = new CueInfo();
            WinCue.CueInfoText.Text = Message;            
            Location.WindowLocation(WinCue);
        }
    }
}
