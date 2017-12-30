using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;

namespace Server
{
    class DataLink
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        //SQLEXPRESS连接方式
        //private static string Link = @"Data Source=.\SQLEXPRESS;AttachDbFilename=F:\IM\Server\Data\ChatData.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True;";
        private static string Link = @"Data Source=.\SQLSERVER;Initial Catalog=ChatData;Integrated Security=SSPI";
        /// <summary>
        /// 创建连接数据库实例
        /// </summary>
        public static SqlConnection Connection = new SqlConnection(Link);
        /// <summary>
        /// 用户名验证
        /// </summary>
        private static string userNumVal;
        /// <summary>
        /// 用户信息验证
        /// </summary>
        private static string userInfoVal;
        /// <summary>
        /// 用户昵称
        /// </summary>
        private static String userNickName;
        /// <summary>
        /// 用户名数据验证
        /// </summary>
        private static SqlCommand userNumCmd;
        /// <summary>
        /// 用户信息数据验证
        /// </summary>
        private static SqlCommand userInfoCmd;
        /// <summary>
        /// 好友号码信息验证
        /// </summary>
        private static SqlCommand friendNumCmd;
        /// <summary>
        /// 好友信息数据验证
        /// </summary>
        private static SqlCommand friendInfoCmd;
        /// <summary>
        /// 插入好友信息验证
        /// </summary>
        private static SqlCommand insertFriendInfoCmd;
        /// <summary>
        /// 删除好友信息验证
        /// </summary>
        private static SqlCommand delFriendCmd;
        /// <summary>
        /// 验证用户名消息
        /// </summary>
        private static int ValUserNum;
        /// <summary>
        /// 验证用户信息消息
        /// </summary>
        private static int ValUserInfo;
        /// <summary>
        /// 获取好友昵称
        /// </summary>
        private static String friendNickName;
        /// <summary>
        /// 获取好友性别
        /// </summary>
        private static String friendSex;
        /// <summary>
        /// 定义好友列表序列
        /// </summary>
        private static ArrayList friendNum;
        /// <summary>
        /// 返回好友列表Byte[]
        /// </summary>
        private static byte[] returnList;
        /// <summary>
        /// 查找号码是否存在
        /// 0x0010-用户不存在
        /// 0x0100-服务器超时或连接异常 
        /// </summary>
        /// <param name="friendNumber"></param>
        /// <returns></returns>
        public static String searchFriendVal(string friendNumber)
        {
            try
            {
                userNumVal = string.Format("select count(*) from Users where id={0}", int.Parse(friendNumber));
                userNumCmd = new SqlCommand(userNumVal, DataLink.Connection);
                Connection.Open();
                ValUserNum = Convert.ToInt32(userNumCmd.ExecuteScalar());
                if (ValUserNum == 1)
                {
                    userNickName = string.Format("select NickName,Sex from Users where id={0}", int.Parse(friendNumber));
                    userInfoCmd = new SqlCommand(userNickName,DataLink.Connection);
                    SqlDataReader readUserNickName = userInfoCmd.ExecuteReader();                    
                    if (readUserNickName.Read())
                    {                        
                        return readUserNickName.GetValue(0).ToString();
                    }
                    readUserNickName.Close();
                }
                else
                {
                    return "0x0010";
                }
            }
            catch (SqlException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("数据库连接失败...");
                Program.reColor();
                return "0x0100";
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("连接数据库语句有误...");
                Program.reColor();
                return "0x0100";                
            }
            finally
            {
                Connection.Close(); 
            }
            return "0x0010";
        }
        /// <summary>
        /// 添加好友信息
        /// 0x1000添加好友成功
        /// 0x0101添加好友失败
        /// 0x0100数据库连接失败
        /// </summary>
        /// <param name="friendNumber"></param>
        /// <returns></returns>
        public static String addFriendInfo(String userNumber,String friendNumber)
        {
            try
            {
                String friendNumberVal = string.Format("select count(*) from Friend" + userNumber + " where FriendID={0}", friendNumber);
                friendNumCmd = new SqlCommand(friendNumberVal, DataLink.Connection);
                Connection.Open();
                int friendNumVal = Convert.ToInt32(friendNumCmd.ExecuteScalar());
                Console.Write(friendNumVal.ToString());
                if (friendNumVal.Equals(1))
                {                   
                    return "0x1010";                   
                }
                else
                {
                    String userInfo = String.Format("select NickName,Sex from Users where id={0}", int.Parse(friendNumber));
                    friendInfoCmd = new SqlCommand(friendNumber, DataLink.Connection);
                    SqlDataReader readFriendInfo = userInfoCmd.ExecuteReader();
                    if (readFriendInfo.Read())
                    {
                        friendNickName = readFriendInfo.GetValue(0).ToString();
                        friendSex = readFriendInfo.GetValue(1).ToString();
                    }
                    readFriendInfo.Close();
                    String insertFriendInfo = string.Format("insert into Friend" + userNumber + "(FriendID,FriendNickName,FriendSex) values('{0}','{1}','{2}')",
                                                                                                                 friendNumber, friendNickName, friendSex);
                    insertFriendInfoCmd = new SqlCommand(insertFriendInfo, DataLink.Connection);
                    int result = insertFriendInfoCmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return "0x1000";
                    }
                    else
                    {
                        return "0x0101";
                    }
                }
            }
            catch (SqlException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("数据库连接失败...");
                Program.reColor();
                return "0x0100";
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("连接数据库语句有误...");
                Program.reColor();
                return "0x0100";
            }
            finally
            {
                Connection.Close();
            }            
        }
        /// <summary>
        /// 删除好友
        /// 0x1010删除好友成功
        /// 0x1011删除好友失败
        /// 0x0100数据库连接失败
        /// </summary>
        /// <param name="myNumber"></param>
        /// <param name="friendNumber"></param>
        /// <returns></returns>
        public static String DelFriend(String myNumber, String friendNumber)
        {
            try
            {
                userNumVal = String.Format("delete from Friend" + myNumber + " where FriendID={0}", friendNumber);                
                delFriendCmd = new SqlCommand(userNumVal, DataLink.Connection);
                DataLink.Connection.Open();
                int result = delFriendCmd.ExecuteNonQuery();
                if (result == 1)
                {
                    return "0x1010";
                }
                else
                {
                    return "0x1011";
                }
            }
            catch (SqlException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("数据库连接失败...");
                Program.reColor();
                return "0x0100";
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("连接数据库语句有误...");
                Program.reColor();
                return "0x0100";
            }
            finally
            {
                DataLink.Connection.Close();
            }            
        }
        /// <summary>
        /// 验证用户名和密码
        /// 010-密码错误
        /// 101-号码不存在
        /// 111-登陆成功
        /// </summary>
        /// <returns></returns>
        public static String SqlDataLink(string userNumber,string userPassword)
        {            
            try
            {
                userNumVal = string.Format("select count(*) from Users where id={0}", int.Parse(userNumber));
                userInfoVal = string.Format("select count(*) from Users where ID={0} and UserPassword='{1}'", int.Parse(userNumber), userPassword);
                userNumCmd = new SqlCommand(userNumVal, DataLink.Connection);
                userInfoCmd = new SqlCommand(userInfoVal, DataLink.Connection);
                Connection.Open();
                ValUserNum = Convert.ToInt32(userNumCmd.ExecuteScalar());                
                ValUserInfo = Convert.ToInt32(userInfoCmd.ExecuteScalar());                
            }
            catch (SqlException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("数据库连接失败...");
                Program.reColor();
                return "100";
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("连接数据库语句有误...");
                Program.reColor();
                return "0x0100";
            }
            finally
            { 
                Connection.Close(); 
            }
            if (ValUserNum == 1)
            {
                if (ValUserInfo == 1)
                {
                    return "111";
                }
                else
                {
                    //密码错误
                    return "010";
                }
            }
            else
            {
                //号码不存在
                return "101";
            }            
        }
        public static Byte[] friendList(string userNumber)
        {             
            string userFriendTable = "Friend" + userNumber;            
            try
            {
                userNumVal = string.Format("select FriendID from Friend" + userNumber);
                userNumCmd = new SqlCommand(userNumVal, DataLink.Connection);
                Connection.Open();
                SqlDataReader friendNumber = userNumCmd.ExecuteReader();
                friendNum = new ArrayList();
                while (friendNumber.Read())
                {
                    friendNum.Add(friendNumber[0]);                    
                }
                friendNumber.Close();
                StringCollection onlineList = new StringCollection();
                foreach (object Online in friendNum)
                {
                    onlineList.Add(Online as string);
                }
                IFormatter format = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                format.Serialize(stream, onlineList);                
                returnList = stream.ToArray();
                stream.Close();                
            }
            catch (SqlException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("连接数据库失败...");
                Program.reColor();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("读取好友列表错误");
                Program.reColor();
            }
            finally
            {
                Connection.Close();
            }
            return returnList;            
        }
    }
}
