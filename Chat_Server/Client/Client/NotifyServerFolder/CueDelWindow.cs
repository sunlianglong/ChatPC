using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class CueDelWindow
    {
        public static void Show(String friendNumber)
        {
            CueDelInfo DelCueWin = new CueDelInfo();
            DelCueWin.CueInfoText.Text = "确定删除好友" + friendNumber + "？";
            Location.WindowLocation(DelCueWin);
        }
    }
}
