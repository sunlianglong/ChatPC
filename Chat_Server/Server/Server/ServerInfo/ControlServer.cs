using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.IO;

namespace Server
{
    class ControlServer
    {
        /// <summary>
        /// 服务器端监听器
        /// </summary>
        public static TcpListener ServerListener = null;
        /// <summary>
        /// 保存所有客户端的登陆号码及网络流的哈希表
        /// </summary>
        public static Hashtable userTable = new Hashtable();
        /// <summary>
        /// 关闭监听并释放资源
        /// </summary>
        public void Close()
        {
            if (ServerListener != null)
            {
                ServerListener.Stop();
            }
            //关闭客户端连接并清理资源
            if (userTable.Count != 0)
            {
                foreach (Socket Session in userTable.Values)
                {
                    Session.Shutdown(SocketShutdown.Both);
                }
                userTable.Clear();
                userTable = null;
            }
        }
        /// <summary>
        /// 启动服务器
        /// </summary>
        public void StartServer()
        {
            ServerListener = new TcpListener(IpConfig.getIp(), IpConfig.Port);
            ServerListener.Start();            
            Console.Write("监听服务启动成功...");
            Program.PrintSuccess();
            Console.Write("服务器启动成功，等待用户连接中...");
            Program.PrintSuccess();
            Console.WriteLine();
            while (true)
            {                
                Socket newClient = ServerListener.AcceptSocket();//创建新用户Socket 
                byte[] packetBuff = new byte[64];
                int len = newClient.Receive(packetBuff, packetBuff.Length, 0);
                Console.WriteLine("新用户请求连接...");
                string userLoginInfo = Encoding.Default.GetString(packetBuff,0,len).TrimEnd('\0');
                string[] userinfo = userLoginInfo.Split('|');
                string userNumber = userinfo[0];//用户号码
                string userPassword = userinfo[1];//用户密码
                //连接数据库验证用户号码，密码
                string valUserLoginInfo = DataLink.SqlDataLink(userNumber, userPassword);
                Console.WriteLine("查找用户数据，正在进行验证...");
                if (!valUserLoginInfo.Equals("111"))
                {
                    if (valUserLoginInfo.Equals("010"))
                    {
                        newClient.Send(Encoding.Default.GetBytes("ErrorPassword"), Encoding.Default.GetBytes("ErrorPassword").Length, 0);
                        Console.WriteLine("验证失败，密码错误...\r\n");
                        continue;
                    }
                    else
                    {
                        newClient.Send(Encoding.Default.GetBytes("ErrorNonentity"), Encoding.Default.GetBytes("ErrorNonentity").Length, 0);
                        Console.WriteLine("验证失败，没有在服务器中找到此用户的信息...\r\n");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("用户号码和密码已经通过验证...");
                    newClient.Send(Encoding.Default.GetBytes("Successful"), Encoding.Default.GetBytes("Successful").Length, 0);
                    //验证是否为唯一用户
                    //if (userTable.Count != 0 && userTable.ContainsKey(userNumber))
                    //{
                    //    newClient.Send(Encoding.Default.GetBytes("cmd::Failed"));
                    //    Console.WriteLine("登陆服务器失败，该用户已经登陆...\r\n");
                    //    continue;
                    //}
                    //else
                    //{
                        //newClient.Send(Encoding.Default.GetBytes("cmd::Successful"));
                        //返回用户好友列表
                        newClient.Send(DataLink.friendList(userNumber),DataLink.friendList(userNumber).Length, 0);
                    //}
                }                
                //将新的连接加入用户表
                userTable.Add(userNumber, newClient);
                string loginInfo = string.Format("[系统消息]用户{0}登陆服务器...时间:{1}  当前在线人数:{2}\r\n", userNumber, System.DateTime.Now, userTable.Count);
                Console.WriteLine(loginInfo);
                Thread newClientThread = new Thread(new ParameterizedThreadStart(Transmit.transmitThread));
                newClientThread.Start(userNumber);
                //通知客户端谁在线
                //foreach (DictionaryEntry userInfo in userTable)
                //{
                //    string clientNumber = userInfo.Key as string;
                //    Socket clientSocket = userInfo.Value as Socket;
                //    if (!clientNumber.Equals(userNumber))
                //    {
                //        clientSocket.Send(Encoding.Default.GetBytes(loginInfo));
                //    }
                //}
            }          
        }
    }
}
