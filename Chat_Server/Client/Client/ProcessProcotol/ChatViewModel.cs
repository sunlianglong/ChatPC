using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Threading;
using WpfApplication1;

namespace Client
{
    class ChatViewModel
    {       

        /// <summary>
        ///     向指定聊天窗口的输入框中打入用户选择的JPG图像
        /// </summary>
        /// <param name="win_TalkWindow" type="Hi_Baidu.Win_TalkWindow">
        ///     <para>
        ///         聊天窗口句柄
        ///     </para>
        /// </param>
        /// <param name="img" type="System.Windows.Controls.Image">
        ///     <para>
        ///         用户选中的图像
        ///     </para>
        /// </param>
        public static void AppendImage(Win_Talking win_TalkWindow, Image img)
        {
            img.Stretch = Stretch.None;            ;
            InlineUIContainer uiContainer = new InlineUIContainer(img, win_TalkWindow.MyMessageBox.Selection.End);
            //win_TalkWindow.MyMessageBox.Document.Blocks.Add(new Paragraph(uiContainer));
        }

        /// <summary>
        ///      向指定聊天窗口的输入框中打入用户选择的GIF图像
        /// </summary>
        /// <param name="win_TalkWindow" type="Hi_Baidu.Win_TalkWindow">
        ///     <para>
        ///         聊天窗口句柄
        ///     </para>
        /// </param>
        /// <param name="img_Ex" type="WpfApplication1.ImageExpender">
        ///     <para>
        ///         用户选中的GIF动画对象
        ///     </para>
        /// </param>
        public static void AppendGif(Win_Talking win_TalkWindow, ImageExpender img_Ex)
        {
            img_Ex.Stretch = Stretch.None;
            InlineUIContainer uiContainer = new InlineUIContainer(img_Ex, win_TalkWindow.MyMessageBox.Selection.End);
            //win_TalkWindow.MyMessageBox.Document.Blocks.Add(new Paragraph(uiContainer));
        }       
    }
}
