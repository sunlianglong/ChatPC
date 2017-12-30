using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Media;
using System.Threading;
using System.Windows.Threading;

namespace Client
{
    class TalkWinModel
    {
        //shakePlay-抖动窗体声音
        private static SoundPlayer shakePlay = new SoundPlayer(Properties.Resources.shake);
        /// <summary>
        /// 执行抖动窗体
        /// </summary>
        /// <param name="talkWindow"></param>
        public static void ShakeWindow(Win_Talking talkWindow)
        {
            if (talkWindow.WindowState == WindowState.Minimized)
            {
                talkWindow.WindowState = WindowState.Normal;
            }
            int shake = 0;
            double OriginalLeft = talkWindow.Left;
            double OriginalTop = talkWindow.Top;
            double newLeftOne = OriginalLeft - 2; double newLeftTwo = OriginalLeft + 2;
            double newTopOne = OriginalTop - 2; double newTopTwo = OriginalTop + 2;
            shakePlay.Play();
            while (shake < 5)
            {
                talkWindow.Left = newLeftOne; talkWindow.Top = newTopOne;
                Thread.Sleep(50);
                talkWindow.Left = newLeftTwo; talkWindow.Top = newTopTwo;
                Thread.Sleep(50);
                shake++;
            }
            talkWindow.Left = OriginalLeft;
            talkWindow.Top = OriginalTop;
        }
    }
}
