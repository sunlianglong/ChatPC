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
using System.Windows.Shapes;

namespace QQ_presentation
{
    /// <summary>
    /// signup.xaml 的交互逻辑
    /// </summary>
    public partial class signup : Window
    {
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        public signup()
        {
            InitializeComponent();
            string ip = IPHelper.GetAddressIP();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //App.Current.Shutdown();                                //主窗口关闭
              this.Close();  //这行代码也是关闭窗口
        }
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;   //主窗口最小化
        }
        private void getip_Click(object sender, RoutedEventArgs e)
        {
            string ip = IPHelper.GetAddressIP();
            ip_show.Text = ip;
        }

        private void pwdjudge()
        {
            if (ip_show.Text == "")
            {
                MessageBox.Show("用户名或IP地址不能为空");

            }
            else if (usrpwd.Password != usrpwd1.Password)
            {
                MessageBox.Show("前后密码不一致");
            }
            else
            {
                registerPhp(ip_show.Text, usrpwd.Password);
                MessageBox.Show("注册成功");
                this.Close();

            }
        }

        private void signout_Click(object sender, RoutedEventArgs e)
        {
            pwdjudge();
        }

        private void registerPhp(string ip_text, string pwd_text)
        {
            // 待请求的地址
            string url = "http://123.206.17.117/chatting/register.php";

            // 创建 WebRequest 对象，WebRequest 是抽象类，定义了请求的规定,
            // 可以用于各种请求，例如：Http, Ftp 等等。
            // HttpWebRequest 是 WebRequest 的派生类，专门用于 Http
            System.Net.HttpWebRequest request
                = System.Net.HttpWebRequest.Create(url) as System.Net.HttpWebRequest;

            // 请求的方式通过 Method 属性设置 ，默认为 GET
            // 可以将 Method 属性设置为任何 HTTP 1.1 协议谓词：GET、HEAD、POST、PUT、DELETE、TRACE 或 OPTIONS。
            request.Method = "POST";

            // 还可以在请求中附带 Cookie
            // 但是，必须首先创建 Cookie 容器
            request.CookieContainer = new System.Net.CookieContainer();

            System.Net.Cookie requestCookie
                = new System.Net.Cookie("Request", "RequestValue", "/", "localhost");
            request.CookieContainer.Add(requestCookie);



            //Console.WriteLine("请输入请求参数:");

            // 输入 POST 的数据.
            //string inputData = Console.ReadLine();

            // 拼接成请求参数串，并进行编码，成为字节
            //string postData = "firstone=" + inputData;
            string ip = ip_text;
            string password = pwd_text;
            string postData = "name=" + ip + "&" + "password=" + password;
            //string postDataName = "name=" + ip;
            //string postDataPwd = "password" + password;
            byte[] byte1 = Encoding.UTF8.GetBytes(postData);
            //byte[] byte1 = encoding.GetBytes(postDataName);
            //byte[] byte2 = encoding.GetBytes(postDataPwd);
            // 设置请求的参数形式
            request.ContentType = "application/x-www-form-urlencoded";

            // 设置请求参数的长度.
            request.ContentLength = byte1.Length;

            // 取得发向服务器的流
            System.IO.Stream newStream = request.GetRequestStream();

            // 使用 POST 方法请求的时候，实际的参数通过请求的 Body 部分以流的形式传送
            newStream.Write(byte1, 0, byte1.Length);

            // 完成后，关闭请求流.
            newStream.Close();

            // GetResponse 方法才真的发送请求，等待服务器返回
            System.Net.HttpWebResponse response
                = (System.Net.HttpWebResponse)request.GetResponse();

            // 首先得到回应的头部，可以知道返回内容的长度或者类型
            Console.WriteLine("Content length is {0}", response.ContentLength);
            Console.WriteLine("Content type is {0}", response.ContentType);

            // 回应的 Cookie 在 Cookie 容器中
            foreach (System.Net.Cookie cookie in response.Cookies)
            {
                Console.WriteLine("Name: {0}, Value: {1}", cookie.Name, cookie.Value);
            }
            Console.WriteLine();

            // 然后可以得到以流的形式表示的回应内容
            System.IO.Stream receiveStream
                = response.GetResponseStream();

            // 还可以将字节流包装为高级的字符流，以便于读取文本内容
            // 需要注意编码
            System.IO.StreamReader readStream
                = new System.IO.StreamReader(receiveStream, Encoding.UTF8);

            Console.WriteLine("Response stream received.");
            Console.WriteLine(readStream.ReadToEnd());

            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();

        }
    }
}
