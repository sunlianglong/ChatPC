using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Server
{
    class Transmit
    {
        /// <summary>
        /// 序列化在线列表
        /// </summary>
        /// <returns>序列化后的在线用户数组</returns>
        public static byte[] alignmentOnlineList()
        {
            StringCollection onlineList = new StringCollection();
            foreach (object Online in ControlServer.userTable.Keys)
            {
                onlineList.Add(Online as string);
            }
            IFormatter format = new BinaryFormatter();
            MemoryStream stream=new MemoryStream();
            format.Serialize(stream, onlineList);
            byte[] returnList = stream.ToArray();
            stream.Close();
            return returnList;
        }
        /// <summary>
        /// 提取命令
        /// </summary>
        /// <param name="clientMessage">只提取前两个这节</param>
        /// <returns>拼接成的字符串命令</returns>
        public static string DecodingBytes(byte[] clientMessage)
        {
            return string.Concat(clientMessage[0].ToString(), clientMessage[1].ToString());
        }
        public static void transmitThread(object userNumber)
        {
            Socket clientSocket = ControlServer.userTable[userNumber] as Socket;
            while (true)
            {
                try
                {
                    byte[] receivecmd = new byte[32];
                    int receiveCount = clientSocket.Receive(receivecmd, receivecmd.Length, 0);
                    string cmd = DecodingBytes(receivecmd);
                    switch (cmd)
                    {
                        case "01"://用户离线
                            {
                                ControlServer.userTable.Remove(userNumber);
                                string offlinemsg = string.Format("[系统消息]用户{0}在{1}  已下线...  当前在线人数:{2}\r\n", userNumber, System.DateTime.Now, ControlServer.userTable.Count);
                                Console.WriteLine(offlinemsg);
                                //foreach (DictionaryEntry userInfo in ControlServer.userTable)
                                //{
                                //    string clientName = userInfo.Key as string;
                                //    Socket clientSkt = userInfo.Value as Socket;
                                //    clientSkt.Send(Encoding.Default.GetBytes(offlinemsg));
                                //}
                                Thread.CurrentThread.Abort();
                                break;
                            }
                        //case "02"://请求在线列表
                        //    {
                        //        byte[] onlinemsg = alignmentOnlineList();
                        //        //发送响应信号
                        //        clientSocket.Send(new byte[] { 1, 1 });
                        //        clientSocket.Send(onlinemsg);
                        //        break;
                        //    }
                        //case "03"://对所有人发送抖动窗口
                        //    {
                        //        string shakeWinMsg = string.Format("[系统提示]用户{0}向您发送了一个抖动窗口", userNumber);
                        //        foreach (DictionaryEntry userInfo in ControlServer.userTable)
                        //        {
                        //            string clientName = userInfo.Key as string;
                        //            Socket clientSkt = userInfo.Value as Socket;
                        //            if (!clientName.Equals(userNumber))
                        //            {
                        //                clientSkt.Send(new byte[] { 1, 2 });
                        //                clientSkt.Send(Encoding.Default.GetBytes(shakeWinMsg));
                        //            }
                        //        }
                        //        break;
                        //    }
                        case "04"://对指定用户发送抖动窗口
                            {
                                string Receiver = null;
                                byte[] ReceiverBuff = new byte[32];
                                int len = clientSocket.Receive(ReceiverBuff, ReceiverBuff.Length, 0);
                                Receiver = Encoding.Default.GetString(ReceiverBuff,0,len).TrimEnd('\0');
                                string shakeWinCmd = string.Format("11/{0}:{1}", userNumber, Receiver);
                                string shakeWinMsg = string.Format("{1}:{2}/用户{0}向您发送了一个抖动窗口", userNumber, userNumber, Receiver);
                                if (ControlServer.userTable.ContainsKey(Receiver))
                                {
                                    Socket receiverSocket = ControlServer.userTable[Receiver] as Socket;
                                    receiverSocket.Send(Encoding.Default.GetBytes(shakeWinCmd), Encoding.Default.GetBytes(shakeWinCmd).Length, 0);
                                    receiverSocket.Send(Encoding.Default.GetBytes(shakeWinMsg), Encoding.Default.GetBytes(shakeWinMsg).Length, 0);
                                }
                                else
                                {
                                    string failMessage = string.Format("sendshakefail:{0}/error", Receiver);
                                    clientSocket.Send(Encoding.Default.GetBytes(failMessage), Encoding.Default.GetBytes(failMessage).Length, 0);
                                }
                                break;
                            }
                        case "05"://查找好友
                            {
                                byte[] msgBuff = new byte[32];
                                int len = clientSocket.Receive(msgBuff, msgBuff.Length, 0);                                
                                string friendNumber = Encoding.Default.GetString(msgBuff,0,len).TrimEnd('\0');                                
                                string valSearchFriend = DataLink.searchFriendVal(friendNumber);//返回查找结果
                                string sendSearchMsg = String.Format("12/{0}", valSearchFriend);
                                clientSocket.Send(Encoding.Default.GetBytes(sendSearchMsg), Encoding.Default.GetBytes(sendSearchMsg).Length, 0);                               
                                break;
                            }
                        case "06"://添加好友
                            {
                                Byte[] msgBuff = new Byte[32];
                                int len = clientSocket.Receive(msgBuff, msgBuff.Length, 0);
                                String friendNumber = Encoding.Default.GetString(msgBuff,0,len).TrimEnd('\0');
                                if (friendNumber.Equals(userNumber.ToString()))
                                {
                                    String sendAddMsg = String.Format("13/{0}:{1}", "0x1001",friendNumber);
                                    clientSocket.Send(Encoding.Default.GetBytes(sendAddMsg), Encoding.Default.GetBytes(sendAddMsg).Length, 0);
                                }
                                else
                                {
                                    String valInsertInfo = DataLink.addFriendInfo(userNumber.ToString(), friendNumber);
                                    String sendAddMsg = String.Format("13/{0}:{1}", valInsertInfo, friendNumber);
                                    clientSocket.Send(Encoding.Default.GetBytes(sendAddMsg), Encoding.Default.GetBytes(sendAddMsg).Length, 0);
                                }
                                break;
                            }
                        case "07"://删除好友
                            {
                                Byte[] msgBuff = new Byte[32];
                                int len = clientSocket.Receive(msgBuff, msgBuff.Length, 0);
                                String friendNumber = Encoding.Default.GetString(msgBuff,0,len).TrimEnd('\0');
                                String valDelInfo = DataLink.DelFriend(userNumber.ToString(), friendNumber);
                                String sendDelInfo = String.Format("14/{0}", valDelInfo);
                                clientSocket.Send(Encoding.Default.GetBytes(sendDelInfo), Encoding.Default.GetBytes(sendDelInfo).Length, 0);
                                break;
                            }
                        case "08"://发送图片
                            {
                                Byte[] msgBuff = new Byte[64];
                                int len = clientSocket.Receive(msgBuff, msgBuff.Length, 0);
                                String Receiver = Encoding.Default.GetString(msgBuff, 0, len).TrimEnd('\0');
                                //int imageLen = clientSocket.Receive(imageBuff, imageBuff.Length, 0);
                                String[] splitMsg = Receiver.Split(':');
                                int imgSize = Convert.ToInt32(splitMsg[0]);
                                int imgSplit = (int)(imgSize / 1024), bytesRead = 0;
                                Byte[] imageBuff = new Byte[1024];
                                MemoryStream imageStream = new MemoryStream();                                
                                do
                                {
                                    bytesRead = clientSocket.Receive(imageBuff, 1024, 0);                                    
                                    imageStream.Write(imageBuff, 0, bytesRead);                                    
                                    imgSplit--;
                                } while (imgSplit >= 0);
                                Byte[] imageByte = imageStream.ToArray();                                                               
                                imageStream.Close();
                                String sendMessage = String.Format("15/{0}:{1}:{2}:{3}:{4}", imageByte.Length.ToString(), userNumber.ToString(), splitMsg[2], splitMsg[3], splitMsg[4]);
                                if (ControlServer.userTable.ContainsKey(splitMsg[1]))
                                {
                                    Socket receiverSocket = ControlServer.userTable[splitMsg[1]] as Socket;
                                    receiverSocket.Send(Encoding.Default.GetBytes(sendMessage), Encoding.Default.GetBytes(sendMessage).Length, 0);                                   
                                    receiverSocket.Send(imageByte);
                                }
                                else
                                {
                                    string failMessage = string.Format("sendmessagefail:{0}/error", Receiver);
                                    clientSocket.Send(Encoding.Default.GetBytes(failMessage));
                                }
                                break;
                            }
                        default://将信息转发给指定用户
                            {
                                string Receiver = Encoding.Default.GetString(receivecmd,0,receiveCount).TrimEnd('\0');
                                byte[] msgBuff = new byte[64*1024];
                                int len = clientSocket.Receive(msgBuff, msgBuff.Length, 0);
                                string Message = Encoding.Default.GetString(msgBuff,0,len).TrimEnd('\0');
                                string sendMsg = string.Format(userNumber.ToString() + ":" + Receiver + "/" + Message);                                
                                //查找用户表是否存在指定的用户
                                if (ControlServer.userTable.ContainsKey(Receiver))
                                {
                                    Socket receiverSocket = ControlServer.userTable[Receiver] as Socket;
                                    receiverSocket.Send(Encoding.Default.GetBytes(sendMsg), Encoding.Default.GetBytes(sendMsg).Length, 0);
                                }
                                else
                                {
                                    string failMessage = string.Format("sendmessagefail:{0}/error", Receiver);
                                    clientSocket.Send(Encoding.Default.GetBytes(failMessage), Encoding.Default.GetBytes(failMessage).Length, 0);
                                }
                                break;
                            }
                    }
                }
                catch (SocketException)
                {
                    //将发生异常的用户从用户列表中移除
                    ControlServer.userTable.Remove(userNumber);
                    string exceptionMessage = string.Format("[系统消息]用户{0}的客户端在{1}意外终止...当前在线人数{2}\r\n", userNumber, System.DateTime.Now, ControlServer.userTable.Count);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exceptionMessage);
                    Program.reColor();
                    Thread.CurrentThread.Abort();
                }
            }            
        }
    }
}
