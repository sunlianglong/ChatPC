using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Client.ClassInfo;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    class ProtocolFactory
    {        
        public static String GetTextProtocol(Run inline, RichTextBox TalkMessage)
        {            
            TextRange msgBoxContent = new TextRange(TalkMessage.Document.ContentStart, TalkMessage.Document.ContentEnd);
            String TextProtocol = "#FONT|";
            TextProtocol += inline.Foreground.ToString() + "|";
            TextProtocol += inline.FontFamily.ToString() + "|";
            TextProtocol += inline.FontWeight.ToString() + "|";
            TextProtocol += inline.FontStyle.ToString() + "|";
            TextProtocol += inline.FontSize.ToString() + "|";
            TextProtocol += msgBoxContent.Text.Replace("|", "(*Split*)").Replace("/","(*Slash*)") + "|";            
            return TextProtocol;
        }
        public static String GetPictureProtocol(Image img)
        {
            string PicProtocol = "#PIC|";
            PicProtocol += img.ToolTip.ToString() + "|";
            PicProtocol += img.ActualHeight.ToString() + "|";
            PicProtocol += img.ActualWidth.ToString() + "|";
            return PicProtocol;
        }
    }
}
