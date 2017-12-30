using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Windows;

namespace QQ_presentation
{
    public class FriendInfo : INotifyPropertyChanged
    {
        public string friend_ip { get; set; }
        public string group { get; set; }
        public string state { get; set; }
        public string beizhu { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    public class GroupInfo : INotifyPropertyChanged
    {
        public string goup_name { get; set; }
        public string friends_number { get; set; }
        public ObservableCollection<FriendInfo> Children { get; set; }
        public GroupInfo()
        {
            this.Children = new ObservableCollection<FriendInfo>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Test
    {
        public static ObservableCollection<GroupInfo> GetGroupList(string ip_address)
        {
            string ip = ip_address;
            ObservableCollection<GroupInfo> GroupList = new ObservableCollection<GroupInfo>();
            ObservableCollection<GroupInfo> groups = GroupInfoGet.GetList<GroupInfo>(ip);
            ObservableCollection<FriendInfo> friends = FriendInfoGet.GetList<FriendInfo>(ip);
            foreach (GroupInfo g in groups)
            {
                GroupList.Add(g);
                foreach (FriendInfo f in friends)
                {
                    if (f.group == g.goup_name)
                    {
                        g.Children.Add(f);
                    }
                }
            }
            return GroupList;
        }
    }


    public class GroupInfoGet
    {
        public static ObservableCollection<GroupInfo> GetList<GroupInfo>(string ip)
        {
            //unicode转中文
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
            string url = "http://123.206.17.117/chatting/read_user_group_info.php";

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
            string search = ip;
            string postData = "search=" + search;
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
            //Console.WriteLine(sqldata);
            sqldata = unicode_js_1(sqldata);


            //var groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            //List<GroupInfo> groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            string[] s = Regex.Split(sqldata, "},");
            int lens = s.Length;


            ObservableCollection<GroupInfo> groups = new ObservableCollection<GroupInfo>();
            for (int i = 0; i < lens; i++)
            {
                if (i != lens - 1)
                    s[i] += "}";

                GroupInfo friend_i = JsonConvert.DeserializeObject<GroupInfo>(s[i]);
                groups.Add(friend_i);

            }
            return groups;

            //string str1 = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //int flag = Convert.ToInt32(str1);// 1 表示成功    0 表示失败
            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();
        }
    }

    public class FriendInfoGet
    {
        public static ObservableCollection<FriendInfo> GetList<FriendInfo>(string ip)
        {
            //unicode转中文
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
            string url = "http://123.206.17.117/chatting/read_user_all_friend.php";

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
            string search = ip;
            string postData = "search=" + search;
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
            //Console.WriteLine(sqldata);
            sqldata = unicode_js_1(sqldata);


            //var groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            //List<GroupInfo> groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            string[] s = Regex.Split(sqldata, "},");
            int lens = s.Length;


            ObservableCollection<FriendInfo> friends = new ObservableCollection<FriendInfo>();
            for (int i = 0; i < lens; i++)
            {
                if (i != lens - 1)
                    s[i] += "}";

                FriendInfo friend_i = JsonConvert.DeserializeObject<FriendInfo>(s[i]);
                friends.Add(friend_i);

            }
            return friends;

            //string str1 = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //int flag = Convert.ToInt32(str1);// 1 表示成功    0 表示失败
            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();
        }
    }
    class UserInformation:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string Ip_address;
        public string ip_address {
            get
            {
                return Ip_address;
            }
            set
            {
                this.Ip_address = value;
                if (PropertyChanged != null)
                {   //如果Name属性发生了改变，则触发这个事件  
                    PropertyChanged(this, new PropertyChangedEventArgs("ip_address"));
                }
            }
        }

        private string Netname;
        public string netname {
            get
            {
                return Netname;
            }
            set
            {
                this.Netname = value;
                if (PropertyChanged != null)
                {   //如果Name属性发生了改变，则触发这个事件  
                    PropertyChanged(this, new PropertyChangedEventArgs("netname"));
                }
            }
        }
        public string geqian_data { get; set; }
        public string home_data { get; set; }
        public string school_data { get; set; }
        public string class_data { get; set; }
        public string touxiang_num { get; set; }
        public string sex { get; set; }
        public string phone_num { get; set; }
        public string email { get; set; }
        public string birthday { get; set; }
    }
    public class UserInfoGetList
    {
        public static ObservableCollection<UserInformation> GetList<UserInformation>(string ip)
        {
            //unicode转中文
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
            string url = "http://123.206.17.117/chatting/read_user_for_search.php";

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
            string search = ip;
            string postData = "search=" + search;
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
            //Console.WriteLine(sqldata);
            sqldata = unicode_js_1(sqldata);


            //var groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            //List<GroupInfo> groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            string[] s = Regex.Split(sqldata, "},");
            int lens = s.Length;


            ObservableCollection<UserInformation> friends = new ObservableCollection<UserInformation>();
            for (int i = 0; i < lens; i++)
            {
                if (i != lens - 1)
                    s[i] += "}";

                UserInformation friend_i = JsonConvert.DeserializeObject<UserInformation>(s[i]);
                friends.Add(friend_i);

            }
            return friends;

            //string str1 = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //int flag = Convert.ToInt32(str1);// 1 表示成功    0 表示失败
            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();
        }
    }

    class UserInformationGet
    {
        public static UserInformation GetUserInfor(string ip_address)
        {
            //unicode转中文
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
            string url = "http://123.206.17.117/chatting/read_user_for_search.php";

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
            string search = ip_address;
            string postData = "search=" + search;
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
            sqldata = sqldata.Replace("\"", "\'");
            sqldata = sqldata.Replace("[", "");
            sqldata = sqldata.Replace("]", "");

            UserInformation rb = JsonConvert.DeserializeObject<UserInformation>(sqldata);
            return rb;
            //string str1 = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //int flag = Convert.ToInt32(str1);// 1 表示成功    0 表示失败
            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();

        }
    }
    public class ADD
    {
        public static void GroupdAdd(string ip_address, string group)
        {
            int judge;
            string judgestr = "null  ";
            //url
            string url = "http://123.206.17.117/chatting/write_into_group.php";

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
            string ip_me = ip_address;
            string post_group = group;
            string postData = "my_ipaddress=" + ip_me + "&" + "goup_name=" + post_group;

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

            //Console.WriteLine("\n");
            sqldata = sqldata.Replace("\"", "\'");
            sqldata = sqldata.Replace("[", "");
            sqldata = sqldata.Replace("]", "");
            sqldata = unicode_js_1(sqldata);

        }
        public static void FriendAdd(string ip_address, string ip_friend, string group, string beizhu)
        {
            //int judge;
            //url
            string url = "http://123.206.17.117/chatting/write_into_AddFriends_PC.php";

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
            string ip_me = ip_address;
            string ip_fd = ip_friend;
            string post_group = group;
            string post_beizhu = beizhu;
            string postData = "ip=" + ip_me + "&" + "friend_ip=" + ip_fd + "&" + "group=" + post_group + "&" + "beizhu=" + post_beizhu;
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

            //string str1 = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //int flag = Convert.ToInt32(str1);// 1 表示成功    0 表示失败
            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();
        }
        public static string unicode_js_1(string str)
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
        public static string unicode_0(string str)
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
    }



    public class IPHelper
    {
        public static Socket clientSocket;
        public static Thread thread;
        public static string userName = "张三";
        public static string mess = "";
        public static string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }

        public static void IP_connected(string ip_address,string port_num)
        {
            
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //这里的ip地址，端口号都是服务端绑定的相关数据。
            IPAddress ip = IPAddress.Parse(ip_address);
            var endpoint = new IPEndPoint(ip, Convert.ToInt32(port_num));
            try
            {
                clientSocket.Connect(endpoint); //链接有端口号与IP地址确定服务端.
                                                //登录时给服务器发送登录消息
                string str = userName + "|" + " ";
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                clientSocket.Send(buffer);
            }
            catch
            {
                MessageBox.Show("与服务器连接失败");
            }
            thread = new Thread(ReceMsg);
            thread.IsBackground = true; //设置后台线程
            thread.Start();
        }
        public static void ReceMsg()
        {
            while (true)
            {

                try
                {
                    var buffer = new byte[1024 * 1024 * 2];
                    int dateLength = clientSocket.Receive(buffer); //接收服务端发送过来的数据
                    //把接收到的字节数组转成字符串显示在文本框中。
                    string ReceiveMsg = Encoding.UTF8.GetString(buffer, 0, dateLength);
                    string[] msgTxt = ReceiveMsg.Split('|');
                    string newstr = "      " + msgTxt[0] + "：我" + "\r\n" + "      " + msgTxt[2] + "           ____[" + DateTime.Now + "]" + "\r\n" + "\r\n";
                    mess = newstr;
                }
                catch
                {

                }
            }
        }
        



    }

}
