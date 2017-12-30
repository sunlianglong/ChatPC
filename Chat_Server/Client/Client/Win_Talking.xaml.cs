using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Media;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    /// <summary>
    /// Win_Talking.xaml 的交互逻辑
    /// </summary>
    public partial class Win_Talking : Window
    {
        /// <summary>
        /// 当前聊天窗体与显示器的左边距
        /// </summary>
        private Double TalkWindowLeft;
        /// <summary>
        /// 当前聊天窗体与显示器的顶部边距
        /// </summary>
        private Double TalkWindowTop;
        /// <summary>
        /// 聊天窗体当前的宽度
        /// </summary>
        private Double TalkWindowWidth;
        /// <summary>
        /// 聊天窗体当前的高度
        /// </summary>
        private Double TalkWindowHeight;
        /// <summary>
        /// 当前聊天窗体的状态
        /// </summary>
        private Boolean TalkWindowMax = false;
        /// <summary>
        /// 本地聊天窗体是否添加了下划线样式
        /// </summary>
        private Boolean messageboxUnderline = false;       
        /// <summary>
        /// 创建OuterGlowBitmapEffect实例
        /// </summary>
        private OuterGlowBitmapEffect glow;
        /// <summary>
        /// 是否选中了黑体样式按钮
        /// </summary>
        private Boolean pressBlodFontStyle = false;
        /// <summary>
        /// 是否选中了斜体样式按钮
        /// </summary>
        private Boolean pressItalicFontStyle = false;
        /// <summary>
        /// 是否选中了斜体样式按钮
        /// </summary>
        private Boolean pressUnderlineFontStyle = false;
        /// <summary>
        /// 移动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void MouseDown_Window(object sender, MouseButtonEventArgs e)
        {
            if (!this.TalkWindowMax)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    try
                    {
                        this.DragMove();
                        if (this.WindowState == WindowState.Maximized)
                        {
                            this.Maximize.Visibility = Visibility.Hidden;
                            this.RevertWindow.Visibility = Visibility.Visible;
                        }
                    }
                    catch (Exception)
                    { }
                }
            }
        }        
        /// <summary>
        /// 窗体关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Exit(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.Close();                
            }
        }
        /// <summary>
        /// 最大化窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Maximize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.TalkWindowMax)
            {
                this.TalkWindowLeft = this.Left; this.TalkWindowTop = this.Top;
                this.TalkWindowWidth = this.ActualWidth; this.TalkWindowHeight = this.ActualHeight;                
                this.Left = 0; this.Top = 0;                
                this.Width = Location.WindowWidth; this.Height = Location.WindowHeight;
                this.Maximize.Visibility = Visibility.Hidden;
                this.RevertWindow.Visibility = Visibility.Visible;
                this.TalkWindowMax = true;
            }
        }
        /// <summary>
        /// 还原最大化窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RevertWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.TalkWindowMax)
            {                
                this.Left = this.TalkWindowLeft; this.Top = this.TalkWindowTop;
                this.Width = this.TalkWindowWidth; this.Height = this.TalkWindowHeight;                
                this.RevertWindow.Visibility = Visibility.Hidden;
                this.Maximize.Visibility = Visibility.Visible;
                this.TalkWindowMax = false;
            }
        }
        /// <summary>
        /// 窗体最小化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown_Minimize(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.WindowState = WindowState.Minimized;
            }
        }
        /// <summary>
        /// 关闭聊天窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 显示和隐藏字体标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Img_Font_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (FontColumn.Visibility == Visibility.Hidden)
            {
                FontColumn.Visibility = Visibility.Visible;
            }
            else
            {
                FontColumn.Visibility = Visibility.Hidden;
            }
        }

        #region//字体样式 
        /// <summary>
        /// 设置字体样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void FontFamilyBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FontFamilyBox.SelectedItem != null)
            {
                this.MyMessageBox.FontFamily = new FontFamily(this.FontFamilyBox.SelectedItem.ToString());
            }
        }
        /// <summary>
        /// 设置字体大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontSizeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FontSizeBox.SelectedItem != null)
            {
                this.MyMessageBox.FontSize = (int)this.FontSizeBox.SelectedItem;
            }
        }
        /// <summary>
        /// 鼠标进入粗体样式按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlodBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            glow = new OuterGlowBitmapEffect();
            glow.GlowSize = 10;
            glow.GlowColor = Colors.White;
            this.BlodBorder.BitmapEffect = glow;
        }
        /// <summary>
        /// 鼠标离开粗体样式按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlodBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!pressBlodFontStyle)
            {
                glow = new OuterGlowBitmapEffect();
                glow.GlowSize = 0;
                this.BlodBorder.BitmapEffect = glow;
            }
        }
        /// <summary>
        /// 设置字体是否为粗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Img_FontBlod_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(this.MyMessageBox.FontWeight == FontWeights.Bold))
            {
                this.MyMessageBox.FontWeight = FontWeights.Bold;
                glow = new OuterGlowBitmapEffect();
                glow.GlowSize = 10;
                glow.GlowColor = Colors.White;
                this.BlodBorder.BitmapEffect = glow;
                this.pressBlodFontStyle = true;
            }
            else
            {
                this.MyMessageBox.FontWeight = FontWeights.Normal;
                glow = new OuterGlowBitmapEffect();                                
                glow.GlowSize = 0;              
                this.BlodBorder.BitmapEffect = glow;
                this.pressBlodFontStyle = false;
            }
        }
        /// <summary>
        /// 鼠标进入斜体按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItalicBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            glow = new OuterGlowBitmapEffect();
            glow.GlowSize = 10;
            glow.GlowColor = Colors.White;
            this.ItalicBorder.BitmapEffect = glow;
        }
        /// <summary>
        /// 鼠标离开斜体按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItalicBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!pressItalicFontStyle)
            {
                glow = new OuterGlowBitmapEffect();
                glow.GlowSize = 0;
                this.ItalicBorder.BitmapEffect = glow;
            }
        }
        /// <summary>
        /// 设置字体是否为斜体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Img_FontItalic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(this.MyMessageBox.FontStyle == FontStyles.Italic))
            {
                this.MyMessageBox.FontStyle = FontStyles.Italic;
                glow = new OuterGlowBitmapEffect();
                glow.GlowSize = 10;
                glow.GlowColor = Colors.White;
                this.ItalicBorder.BitmapEffect = glow;
                this.pressItalicFontStyle = true;
            }
            else
            {
                this.MyMessageBox.FontStyle = FontStyles.Normal;
                glow = new OuterGlowBitmapEffect();
                glow.GlowSize = 0;
                this.ItalicBorder.BitmapEffect = glow;
                this.pressItalicFontStyle = false;
            }
        }
        /// <summary>
        /// 鼠标进入下划线按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnderlineBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            glow = new OuterGlowBitmapEffect();
            glow.GlowSize = 10;
            glow.GlowColor = Colors.White;
            this.UnderlineBorder.BitmapEffect = glow;
        }
        /// <summary>
        /// 鼠标离开下划线按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnderlineBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!pressUnderlineFontStyle)
            {
                glow = new OuterGlowBitmapEffect();
                glow.GlowSize = 0;                
                this.UnderlineBorder.BitmapEffect = glow;
            }
        }
        /// <summary>
        /// 设置字体是否有下划线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Img_FontUnderline_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!messageboxUnderline)
            {
                this.MyMessageBox.SelectAll();
                this.MyMessageBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);                
                this.messageboxUnderline = true;
                glow = new OuterGlowBitmapEffect();
                glow.GlowSize = 10;
                glow.GlowColor = Colors.White;
                this.UnderlineBorder.BitmapEffect = glow;
                this.pressUnderlineFontStyle = true;
            }
            else
            {
                this.MyMessageBox.SelectAll();
                this.MyMessageBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                this.messageboxUnderline = false;
                glow = new OuterGlowBitmapEffect();
                glow.GlowSize = 0;
                this.UnderlineBorder.BitmapEffect = glow;
                this.pressUnderlineFontStyle = false;
            }
        }
        #endregion

        /// <summary>
        /// 消息来时闪烁任务栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewMessgeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                //得到当前窗体的句柄
                WindowInteropHelper Helper = new WindowInteropHelper(this);
                IntPtr ptr = Helper.Handle;
                FlashWindow(ptr, true);//闪烁任务栏                
            }
        }
    }
}
