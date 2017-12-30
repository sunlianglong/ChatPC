using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {            
            Program program = new Program();            
            program.Init();
        }
        private void Init()
        {
            ControlServer serverListener = new ControlServer();
            try
            {
                reColor();
                PrintHeader();
                Console.WriteLine("\n\n服务器初始化中...");
                Console.Write("正在获取配置信息...");
                PrintSuccess();
                Console.Write("服务器IP地址：{0}...", IpConfig.getIp().ToString());
                PrintSuccess();
                Console.Write("正在打开端口：{0}...", IpConfig.Port);
                PrintSuccess();
                Console.Write("初始化服务器成功...");
                PrintSuccess();
                Console.Write("正在开启服务....");
                PrintSuccess();
                serverListener.StartServer();              
            }
            catch 
            {
                PrintFail();
                serverListener.Close();
            }

        }
        private void PrintHeader()
        {
            Console.WriteLine("****************************************************************************");
            Console.WriteLine("*   The Server of LANCHAT Talking Service                                   ");
            Console.WriteLine("*   Developer by: zhao                                      ");
            Console.WriteLine("****************************************************************************");
            Console.WriteLine();
        }
        public static void reColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void PrintSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success");
            reColor();
        }        
        private void PrintFail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n服务器出现异常...");
            Console.ReadLine();
        }
    }
}
