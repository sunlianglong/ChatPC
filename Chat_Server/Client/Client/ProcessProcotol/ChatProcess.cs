using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WpfApplication1;
using System.IO;
using System.Windows.Media.Imaging;

namespace Client
{
    class ChatProcess
    {
        public static String CheckOut(RichTextBox TalkMessage)
        {
            String rtbContent = null; 
            //得到流文档
            FlowDocument document = TalkMessage.Document;                       
            //遍历文档顶级节点
            foreach (Block blk in document.Blocks)
            {
                //当前对象是元素
                if(blk is Paragraph)
                {
                    Paragraph para = blk as Paragraph;
                    //判断文档类型
                    foreach (Inline line in para.Inlines)
                    {
                        //字符类型
                        if (line is Run)
                        {                            
                            Run run = (line as Run);
                            rtbContent = ProtocolFactory.GetTextProtocol(run, TalkMessage);                                                      
                        }
                        //图片类型
                        else if (line is InlineUIContainer)
                        {
                            InlineUIContainer IUC = line as InlineUIContainer;
                            //GIF图片
                            if (IUC.Child is ImageExpender)
                            {
                                ImageExpender imageGif = IUC.Child as ImageExpender;
                                rtbContent = ProtocolFactory.GetPictureProtocol(imageGif);
                            }
                            //普通图片
                            else if (IUC.Child is Image)
                            {
                                Image Img = IUC.Child as Image;
                                rtbContent = ProtocolFactory.GetPictureProtocol(Img);
                            }
                        }
                    }
                }                
            }
            return rtbContent;
        }
        //定义信息头
        public static Grid AppendMyHeader(Grid Total_Grid, string HeaderColor, string NickName)
        {
            BrushConverter color = new BrushConverter();
            Brush brush = (Brush)color.ConvertFromString(HeaderColor);
            TextBlock nickName = new TextBlock();
            nickName.Text = NickName;
            nickName.Margin = new Thickness(3, 3, 0, 3);
            nickName.Foreground = brush;
            TextBlock SendTime = new TextBlock();
            SendTime.Text = DateTime.Now.ToLongTimeString();
            SendTime.Foreground = brush;
            SendTime.Margin = new Thickness(3, 3, 0, 3);
            StackPanel Header = new StackPanel();
            Header.Orientation = Orientation.Horizontal;
            Header.Children.Add(nickName);
            Header.Children.Add(SendTime);
            Total_Grid.Children.Add(Header);
            //将信息头添加到表格第一行
            Grid.SetRow(Header, 0);
            return Total_Grid;
        }
        public static Grid AppendShakeWinHeader(Grid grid, string msgMode)
        {
            BitmapImage bitImage = null;
            if (msgMode.Equals("success"))
            {
                bitImage = new BitmapImage(new Uri(@"pack://application:,,,/SysImage/Right.png"));
            }
            else
            {
                bitImage = new BitmapImage(new Uri(@"pack://application:,,,/SysImage/cue.gif"));
            }
            Image Img = new Image();
            Img.Height = 16;
            Img.Stretch = Stretch.Fill;                        
            Img.Source = bitImage;
            TextBlock SendTime = new TextBlock();
            SendTime.Text = DateTime.Now.ToLongTimeString();
            SendTime.Foreground = Brushes.Gray;
            SendTime.Margin = new Thickness(3, 3, 0, 3);
            StackPanel Header = new StackPanel();
            Header.Orientation = Orientation.Horizontal;
            Header.Children.Add(Img);
            Header.Children.Add(SendTime);
            grid.Children.Add(Header);
            //将信息头添加到表格第一行
            Grid.SetRow(Header, 0);
            return grid;
        }
        //定义信息内容
        public static Grid AppendMessage(RichTextBox TalkMessage,RichTextBox ViewMessageBox, string myNumber)
        {
            //2 * 1的表格
            Grid Total_Grid = new Grid();
            Total_Grid.RowDefinitions.Add(new RowDefinition());
            Total_Grid.RowDefinitions.Add(new RowDefinition());            
            Total_Grid = AppendMyHeader(Total_Grid, "#FF0082BA", myNumber);
            WrapPanel UserTalkMessage = new WrapPanel();
            UserTalkMessage.Margin = new Thickness(20, 0, 3, 0);
            UserTalkMessage.Orientation = Orientation.Horizontal;
            //得到流文档
            FlowDocument document = TalkMessage.Document;

            //遍历文档顶级节点
            foreach (Block blk in document.Blocks)
            {
                //当前对象是元素
                if (blk is Paragraph)
                {
                    Paragraph para = blk as Paragraph;
                    //判断文档类型
                    foreach (Inline line in para.Inlines)
                    {
                        //字符类型
                        if (line is Run)
                        {                            
                            Run run = (line as Run);
                            TextBlock TextMessage = new TextBlock();                            
                            TextMessage.Text = run.Text;
                            TextMessage.Foreground = run.Foreground;
                            TextMessage.FontFamily = run.FontFamily;
                            TextMessage.FontSize = run.FontSize;
                            TextMessage.FontStyle = run.FontStyle;
                            TextMessage.FontWeight = run.FontWeight;
                            TextMessage.Width = ViewMessageBox.ActualWidth - 40;
                            TextMessage.TextWrapping = TextWrapping.Wrap;                            
                            UserTalkMessage.Children.Add(TextMessage);
                        }
                        //图片类型
                        else if (line is InlineUIContainer)
                        {
                            InlineUIContainer IUC = line as InlineUIContainer;
                            //GIF类型
                            if (IUC.Child is ImageExpender)
                            {
                                ImageExpender image_Gif = new ImageExpender();
                                image_Gif.Visibility = Visibility.Visible;
                                image_Gif.Stretch = Stretch.None;
                                String imgToolTip = (IUC.Child as ImageExpender).ToolTip.ToString();
                                String imgDirectory = imgToolTip.Substring(0, imgToolTip.IndexOf("|"));                                
                                using (FileStream fs = new FileStream(imgDirectory, FileMode.Open))
                                {
                                    Stream imageStream = fs;
                                    System.Drawing.Bitmap bitMap = new System.Drawing.Bitmap(imageStream);
                                    image_Gif.Image = bitMap;
                                    UserTalkMessage.Children.Add(image_Gif);
                                    continue;
                                }
                            }
                            //普通图片类型
                            else if (IUC.Child is Image)
                            {
                                Image Img = new Image();
                                Img.Stretch = (IUC.Child as Image).Stretch;
                                Img.Source = (IUC.Child as Image).Source;
                                UserTalkMessage.Children.Add(Img);
                                continue;
                            }                            
                        }
                    }
                }
            }
            Total_Grid.Children.Add(UserTalkMessage);
            //将信息内容添加到表格第二行
            Grid.SetRow(UserTalkMessage, 1);
            return Total_Grid;
        }
    }
}
