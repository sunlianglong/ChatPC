using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// EditInfo.xaml 的交互逻辑
    /// </summary>
    public partial class EditInfo : Window
    {
        string ip_address;
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        public EditInfo(string get_ip)
        {
            InitializeComponent();
            ip_address = get_ip;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;   //主窗口最小化
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserInformation user = UserInformationGet.GetUserInfor(ip_address);
            //nickname.Text = user.netname;
            //singure.Text = user.geqian_data;
            nick_show.Text = user.netname;
            singure_show.Text = user.geqian_data;
            ip_show.Text = user.ip_address;
            email_show.Text = user.email;
            if (user.sex == "boy" || user.sex == "男")
            {
                sex_show.Text = "男";
            }
            else
            {
                sex_show.Text = "女";
            }
            birthday_show.Text = user.birthday;
            home_show.Text = user.home_data;
            school_show.Text = user.school_data;
            class_show.Text = user.class_data;
            phone_show.Text = user.phone_num;
        }

        private void save_info_Click(object sender, RoutedEventArgs e)
        {
            string unicode_js_1(string str)
            {
                string outStr = "";
                Regex reg = new Regex(@"(?i)\\u([0-9a-f]{4})");
                outStr = reg.Replace(str, delegate (Match m1)
                {
                    return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();
                });
                return outStr;
            }
            //中英文转unicode
            string unicode_0(string str)
            {
                string outStr = "";
                if (!string.IsNullOrEmpty(str))
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        String ss = ((int)str[i]).ToString("x");
                        if (ss.Length != 4)
                        {
                            for (int jj = 0; jj <= 4 - ss.Length; jj++)
                            {
                                ss = "0" + ss;
                            }

                        }
                        outStr += "\\u" + ss;
                    }
                }
                return outStr;
            }


            //url
            string url = "http://123.206.17.117/chatting/UpPersonalDataPC.php";

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
            string post_ip = ip_address;
            string post_netname = nick_show.Text;
            string post_geqian_data = singure_show.Text;
            string post_home = home_show.Text;
            string post_school = school_show.Text;
            string post_class = class_show.Text;
            string post_touxiang = "222222";
            string post_sex = "";
            if (sex_show.Text == "男")
            {
                post_sex = "boy";
            }
            else
            {
                post_sex = "gril";
            }
            string post_phone = phone_show.Text;
            string post_email = email_show.Text;
            string post_birthday = birthday_show.Text;
            string postData = "ip_address=" + post_ip + "&" + "netname=" + post_netname + "&" + "geqian_data=" + post_geqian_data
                + "&" + "home_data=" + post_home + "&" + "school_data=" + post_school + "&" + "class_data=" + post_class + "&" + "touxiang_num=" + post_touxiang +
                "&" + "sex_data=" + post_sex + "&" + "phone_data=" + post_phone + "&" + "email_data=" + post_email + "&" + "birthday_data=" + post_birthday;

            byte[] byte1 = Encoding.UTF8.GetBytes(postData);
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


            // 然后可以得到以流的形式表示的回应内容
            System.IO.Stream receiveStream
                = response.GetResponseStream();

            // 还可以将字节流包装为高级的字符流，以便于读取文本内容
            // 需要注意编码
            System.IO.StreamReader readStream
                = new System.IO.StreamReader(receiveStream, Encoding.UTF8);

            string sqldata = readStream.ReadToEnd();
            //Console.WriteLine(sqldata);
            //Console.WriteLine("\n");
            sqldata = sqldata.Replace("\"", "\'");
            sqldata = sqldata.Replace("[", "");
            sqldata = sqldata.Replace("]", "");
            sqldata = unicode_js_1(sqldata);


            //var groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            //List<GroupInfo> groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            string[] s = Regex.Split(sqldata, "},");
            int lens = s.Length;



            //string str1 = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //int flag = Convert.ToInt32(str1);// 1 表示成功    0 表示失败
            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();
            this.Close();
        }

        private void sex_show_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedContent = ((ComboBoxItem)sex_show.SelectedItem).Content.ToString();
            if (selectedContent == "男")
            {
                sex_show.Text = "男";
            }
            if (selectedContent == "否")
            {
                sex_show.Text = "女";
            }
        }

        private void changeImg_Click(object sender, RoutedEventArgs e)
        {
            ChangeImg c1 = new ChangeImg(ip_address);
            c1.Show();
        }
    }
}
