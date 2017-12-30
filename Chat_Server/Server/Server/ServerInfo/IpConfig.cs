using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class IpConfig
    {
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public static IPAddress getIp()
        {
            IPHostEntry Local = Dns.GetHostByName(Dns.GetHostName());
            IPAddress LocalIP = Local.AddressList[0];
            return LocalIP;
        }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public static int Port = 9050;
        /// <summary>
        /// 接收数据缓冲区：1024K
        /// </summary>
        public const int maxPacket = 10240 * 1024;
        
    }
}
