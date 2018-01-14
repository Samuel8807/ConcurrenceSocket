using iValonLogService.Common;
using SuperSocket.ClientEngine;
using System;
using System.Net;
using System.Text;

namespace EasyClient
{
    public class SSClient
    {
        private AsyncTcpSession client;
        DnsEndPoint p;
        private string name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ip">服务器IP</param>
        /// <param name="port">服务器端口</param>
        public SSClient(string name, string ip, int port)
        {
            this.name = name;
            p = new DnsEndPoint(ip, port);
            client = new AsyncTcpSession();
            // 连接断开事件
            client.Closed += client_Closed;
            // 收到服务器数据事件
            client.DataReceived += client_DataReceived;
            // 连接到服务器事件
            client.Connected += client_Connected;
            // 发生错误的处理
            client.Error += client_Error;
        }
        void client_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
            LogSingleton.LogHelper.Error(e.Exception.Message);
            if (!client.IsConnected)
            {
                client.Connect(p);
            }
        }

        void client_Connected(object sender, EventArgs e)
        {
            LogSingleton.LogHelper.Fatal($"Connected-连接成功：{name}");
            Console.WriteLine("连接成功");
        }

        void client_DataReceived(object sender, DataEventArgs e)
        {
            string msg = Encoding.Default.GetString(e.Data);
            string str = $"接收消息({this.name})：{msg.Trim('\0')}";
            LogSingleton.LogHelper.Info("Received-" + str);
            Console.WriteLine(str);
        }

        void client_Closed(object sender, EventArgs e)
        {
            LogSingleton.LogHelper.Warn($"Closed-连接断开：{name}");
            Console.WriteLine("连接断开");
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        public void Connect()
        {
            client.Connect(p);
        }

        /// <summary>
        /// 向服务器发命令行协议的数据
        /// </summary>
        /// <param name="key">命令名称</param>
        /// <param name="data">数据</param>
        public void SendCommand(string key, string data)
        {
            if (client.IsConnected)
            {
                string str = string.Format("{0} {1}\r\n", key, data);
                LogSingleton.LogHelper.Info("Send-" + str);
                byte[] arr = Encoding.Default.GetBytes(str);
                client.Send(arr, 0, arr.Length);
                // Console.WriteLine($"发送消息：{str}");
            }
            else
            {
                //  throw new InvalidOperationException("未建立连接");
                LogSingleton.LogHelper.Info("Send-未建立连接");
                Console.WriteLine("未建立连接");
            }
        }
    }
}
