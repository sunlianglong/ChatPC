using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Net.Sockets;
using Client.ClassInfo;

namespace Client
{
    class IpConfig
    {
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        /// <returns>服务器IP地址</returns>
        public static IPAddress getIp()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(UserDefinition.AppDirectory + "\\Config\\IpAddress.xml");            
            string IP = xml.GetElementsByTagName("IPConfig")[0].InnerXml;
            return IPAddress.Parse(IP);
        }
        /// <summary>
        /// 聊天端口号
        /// </summary>
        public static int ServerPort = 9050;
    }
}
