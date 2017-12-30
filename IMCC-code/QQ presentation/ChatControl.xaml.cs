using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QQ_presentation
{
    /// <summary>
    /// ChatControl.xaml 的交互逻辑
    /// </summary>
    public partial class ChatControl : UserControl
    {
        public ChatControl()
        {
            InitializeComponent();
            //if (ChatSourceType == ChatSourceEnum.Receiver)
            //{
            //	BackBorder.Background = FindResource("OtherInfoBackgroud") as SolidColorBrush;
            //	BackGrid.RenderTransform = new ScaleTransform(1, -1);
            //	MyTextBlock.Margin = new Thickness(32, 22, 25, 22);
            //	MyImage.Margin = new Thickness(32, 22, 25, 22);
            //	MyRectangle.Margin = new Thickness(10, 0, 0, 0);
            //}

            //if (Message.GetType() == typeof(string))
            //{
            //	//文本消息
            //	MyTextBlock.Text = Message as string;
            //	MyTextBlock.Visibility = Visibility.Visible;
            //}
            //else if (Message.GetType() == typeof(ImageSource))
            //{
            //	//图片消息
            //	MyImage.Source = Message as ImageSource;
            //	MyImage.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //	//语音消息
            //}
        }
        /// <summary>
		/// 消息
		/// </summary>
		object message = null;
        /// <summary>
        /// 消息
        /// </summary>
        public object Message
        {
            get
            {
                return message;
            }
            set
            {
                if (value.GetType() == typeof(string))
                {
                    //文本消息
                    MyTextBlock.Text = value as string;
                    MyTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    //图片消息
                    MyImage.Source = value as ImageSource;
                    MyImage.Visibility = Visibility.Visible;
                }

                message = value;
            }
        }

        ChatSourceEnum chatSourceType = ChatSourceEnum.Sender;
        public ChatSourceEnum ChatSourceType
        {
            get
            {
                return chatSourceType;
            }
            set
            {
                if (value == ChatSourceEnum.Receiver)
                {
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    MyTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    BackBorder.Background = FindResource("OtherInfoBackgroud") as SolidColorBrush;
                    BackGrid.RenderTransform = new ScaleTransform(-1, 1);
                    MyTextBlock.Margin = new Thickness(32, 22, 25, 22);
                    MyImage.Margin = new Thickness(32, 22, 25, 22);
                    MyRectangle.Margin = new Thickness(10, 0, 0, 0);
                }
                else
                {
                    this.HorizontalAlignment = HorizontalAlignment.Right;
                    MyTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                    BackBorder.Background = FindResource("MyInfoBackgroud") as SolidColorBrush;
                    BackGrid.RenderTransform = new ScaleTransform(1, 1);
                    MyTextBlock.Margin = new Thickness(25, 22, 32, 22);
                    MyImage.Margin = new Thickness(25, 22, 32, 22);
                    MyRectangle.Margin = new Thickness(0, 0, 10, 0);
                }
                chatSourceType = value;
            }
        }
    }

    public enum ChatSourceEnum
    {
        /// <summary>
        /// 发送
        /// </summary>
        Sender = 0,
        /// <summary>
        /// 接收
        /// </summary>
        Receiver
    }
}
