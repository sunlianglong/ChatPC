using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QQ_presentation
{
    class Judge
    {
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
        
        public static int FriendJudge(string ip_address)
        {
            int judge;
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
            //Console.WriteLine(sqldata);
            //Console.WriteLine("\n");
            sqldata = sqldata.Replace("\"", "\'");
            sqldata = sqldata.Replace("[", "");
            sqldata = sqldata.Replace("]", "");
            //Console.WriteLine(sqldata);
            sqldata = unicode_js_1(sqldata);
            string judge_str = "null  ";
            if (sqldata == judge_str)
            {
                judge = 0;
            }
            else
            {
                judge = 1;
            }
            return judge;

            //string str1 = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //int flag = Convert.ToInt32(str1);// 1 表示成功    0 表示失败
            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();
        }
        public static int GroupJudge(string ip_address)
        {
            int judge;
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
            //Console.WriteLine(sqldata);
            //Console.WriteLine("\n");
            sqldata = sqldata.Replace("\"", "\'");
            sqldata = sqldata.Replace("[", "");
            sqldata = sqldata.Replace("]", "");
            //Console.WriteLine(sqldata);
            sqldata = unicode_js_1(sqldata);
            string judge_str = "null  ";
            if (sqldata == judge_str)
            {
                judge = 0;
            }
            else
            {
                judge = 1;
            }
            return judge;
            //var groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            //List<GroupInfo> groups = JsonConvert.DeserializeObject<List<GroupInfo>>(sqldata);
            string[] s = Regex.Split(sqldata, "},");
            int lens = s.Length;

            //string str1 = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //int flag = Convert.ToInt32(str1);// 1 表示成功    0 表示失败
            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();

        }
        public static int GroupJudge_group(string ip_address,string goup_name)
        {
            int judge;
            string judgestr = "null  ";
            //url
            string url = "http://123.206.17.117/chatting/read_user_group_info_juge.php";

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
            string search= ip_address;
            string post_group = goup_name;
            string postData = "search=" + search + "&" + "goup_name=" + post_group;

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
            //Console.WriteLine(sqldata);
            if (sqldata == judgestr)
            {
                judge = 0;

            }
            else
            {
                judge = 1;
            }
            return judge;
            response.Close();
            readStream.Close();
        }
        public static int user_friend_judge(string ip_address, string friend_ip)
        {
            int judge;
            //url
            string url = "http://123.206.17.117/chatting/read_user_friend_info_judge.php";

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
            string friend = friend_ip;
            string postData = "search=" + search + "&" + "friend=" + friend;
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
            string judge_str = "null  ";
            if (sqldata == judge_str)
            {
                judge = 0;
            }
            else
            {
                judge = 1;
            }
            //Console.WriteLine(judge);
            return judge;

            //string str1 = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
            //int flag = Convert.ToInt32(str1);// 1 表示成功    0 表示失败
            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();
        }
    }
}
