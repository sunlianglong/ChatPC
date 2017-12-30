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
    class ClientThread
    {
        public TcpListener ThreadLisener;
        public static string[] ClientUser;
        public static string NoticeServer;
        public ClientThread(TcpListener ThreadLisener)
        {
            this.ThreadLisener = ThreadLisener;
        }
        public void ControlConnection()
        {
            try
            {
                int ReceiveData;
                byte[] data = new byte[1024];
                TcpClient ClientInfo = ThreadLisener.AcceptTcpClient();
                ReceiveData = ClientInfo.Client.Receive(data);
                EndPoint ClientUserIp = ClientInfo.Client.RemoteEndPoint;
                string ClientUserInfo = Encoding.ASCII.GetString(data, 0, ReceiveData);
                CheckUserInfo(ClientUserInfo, ClientUserIp, ClientInfo);
                ClientInfo.Close();
            }
            catch { }
        }
        public static void CheckUserInfo(string UserInfo, EndPoint UserIp, TcpClient Client)
        {
            ClientUser = UserInfo.Split(':');            
            if (ClientUser[1].Equals("0"))
            {
                ControlServer.userTable.Add(ClientUser[0], UserIp);
                string ClientUserState="登陆服务器！...";
                NoticeServerUserLoginState(ClientUser[0], ClientUserState);
            }
            else if (ClientUser[1].Equals("1"))
            {
                ControlServer.userTable.Add(ClientUser[0], UserIp);
                string ClientUserState = "以忙碌方式登陆服务器！...";
                NoticeServerUserLoginState(ClientUser[0], ClientUserState);
            }
            else if (ClientUser[1].Equals("2"))
            {
                ControlServer.userTable.Add(ClientUser[0], UserIp);
                string ClientUserState = "以勿扰方式登陆服务器！...";
                NoticeServerUserLoginState(ClientUser[0], ClientUserState);
            }
            else if (ClientUser[1].Equals("3"))
            {
                ControlServer.userTable.Add(ClientUser[0], UserIp);
                string ClientUserState = "以离线方式登陆服务器！...";
                NoticeServerUserLoginState(ClientUser[0], ClientUserState);
            }            
            else if (ClientUser[1].Equals("5"))
            {
                ControlServer.userTable.Add(ClientUser[0], UserIp);
                string ClientUserState = "以隐身方式登陆服务器！...";
                NoticeServerUserLoginState(ClientUser[0], ClientUserState);
            }
            
        }
        public static void NoticeServerUserLoginState(string UserName,string LoginState)
        {
            NoticeServer = UserName + LoginState +"时间："+ System.DateTime.Now.ToString();
            Console.Write("\n{0}",NoticeServer);
            if (ClientUser[1].Equals("0"))
            {
                string NoticeClient = null;
                foreach (DictionaryEntry e in ControlServer.userTable)
                {
                    NoticeClient = NoticeClient + e.Key.ToString() + ":";
                }
                NoticeClientOnline(NoticeClient);
            }
        }
        public static void NoticeClientOnline(string ClientUser)
        {
            Socket Server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 9051);
            if (ClientUser == null)
            { ClientUser = ";"; }
            byte[] HostName = Encoding.ASCII.GetBytes(ClientUser);
            Server.SendTo(HostName, iep);
        }
    }
}
