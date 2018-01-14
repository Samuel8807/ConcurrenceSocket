using iValonLogService.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Concurrence
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        AppServer appServer = new AppServer();

        public MainWindow()
        {
            InitializeComponent();
            btnStart.Command = new RelayCommand(param => btnStartExecute(param));
            btnStop.Command = new RelayCommand(param => btnStopExecute(param));
            btnSend.Command = new RelayCommand(param => btnSendExecute(param));

            appServer.NewSessionConnected += appServer_NewSessionConnected;
            appServer.SessionClosed += appServer_SessionClosed;
            appServer.NewRequestReceived += appServer_NewRequestReceived;

        }

        /// <summary>
        /// 设置监听ip、端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Setup the appServer  
            //Setup with listening port 
            ServerConfig config = new ServerConfig();
            config.ClearIdleSession = false;
            config.ClearIdleSessionInterval = 120;
            config.DisableSessionSnapshot = false;
            config.Disabled = false;
            config.IdleSessionTimeOut = 300;
            config.Ip = "127.0.0.1";
            config.KeepAliveInterval = 60;
            config.KeepAliveTime = 600;
            config.ListenBacklog = 100;
            config.LogAllSocketException = false;
            config.LogBasicSessionActivity = true;
            config.LogCommand = false;
            config.MaxConnectionNumber = 20100;
            config.MaxRequestLength = 1024;
            config.Mode = SocketMode.Tcp;
            config.Port = 2012;
            config.ReceiveBufferSize = 4096;
            config.Security = "None";
            config.SendBufferSize = 2048;
            config.SendTimeOut = 5000;
            config.SendingQueueSize = 5;
            config.SessionSnapshotInterval = 5;
            config.StartupOrder = 0;
            config.SyncSend = false;

            if (!appServer.Setup(config))
                tbMsg.Text = "Failed to setup!\r\n";

            if (!appServer.Start())
            {
                tbMsg.Text += "Failed to start!\r\n";
                return;
            }
            else
                tbMsg.Text += "The server started successfully!\r\n";
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="param"></param>
        private void btnStartExecute(object param)
        {
            //Try to start the appServer  
            if (!appServer.Start())
            {
                tbMsg.Text += "Failed to start!\r\n";
                return;
            }

            tbMsg.Text += "The server started successfully!\r\n";

        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="param"></param>
        private void btnStopExecute(object param)
        {
            //Stop the appServer  
            appServer.Stop();

            tbMsg.Text += "The server was stopped!\r\n";
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="param"></param>
        private void btnSendExecute(object param)
        {
            try
            {
                var list = appServer.GetAllSessions().ToList();
                int count = int.Parse(textBox.Text);
                if (count > list.Count())
                {
                    count = list.Count();
                }
                for (int i = 0; i < count; i++)
                {
                    int c = i;
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(100);
                        string str = $"{new Random().Next(1, 999)} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\r\n";
                        Console.WriteLine($"{list[c].SessionID}-{str}");
                        // list[c].Send(Encoding.Default.GetBytes(str), 0, str.Length);
                    });
                }
            }
            catch (Exception ex)
            {
                appServer.Logger.Error(ex);
            }
        }

        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="session"></param>
        void appServer_NewSessionConnected(AppSession session)
        {
            string str = $"Session:{session.SessionID},endPoint:{session.RemoteEndPoint} connect\r\n";
            //LogSingleton.LogHelper.Info("NewSession-" + str);
            appServer.Logger.Info("NewSession-" + str);
            Console.WriteLine(str);
            //  SessionManager.Add(session);
            this.Dispatcher.Invoke(delegate { labClientCount.Content = $"当前用户数：{appServer.SessionCount}"; });
        }

        /// <summary>
        /// 客户端接收的数据
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        void appServer_NewRequestReceived(AppSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            Thread.Sleep(300);
            var key = requestInfo.Key;
            var body = requestInfo.Body;

            string str = $"接收消息：Session:{session.SessionID}, key:{key}, body:{body}";
            // LogSingleton.LogHelper.Info("Received-" + str);
            appServer.Logger.Info("Received-" + str);
            Console.WriteLine(str);

            string str1 = $"{new Random().Next(1, 999)} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\r\n";
            // LogSingleton.LogHelper.Info("Send-" + str1);
            appServer.Logger.Info("Send-" + str1);
            Console.WriteLine($"{session.SessionID}-{str1}");
            session.Send(Encoding.Default.GetBytes(str1), 0, str1.Length);
            //switch (key)
            //{
            //    case "1":
            //        Console.WriteLine("hello word");
            //        break;
            //    case "2":
            //        Console.WriteLine("LOL");
            //        break;
            //    case "3":
            //        Console.WriteLine(body);
            //        session.Send(body + "--已处理");
            //        break;
            //    default: break;
            //}
        }

        /// <summary>
        /// 客户端关闭
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        void appServer_SessionClosed(AppSession session, CloseReason value)
        {
            string str = $"Session:{session.SessionID},endPoint:{session.RemoteEndPoint} disconnected   Reason:{value.ToString()}\r\n";
            //  LogSingleton.LogHelper.Info("SessionClosed-" + str);
            appServer.Logger.Info("SessionClosed-" + str);
            Console.WriteLine(str);
            // SessionManager.Remove(session);
            this.Dispatcher.Invoke(delegate { labClientCount.Content = $"当前用户数：{appServer.SessionCount}"; });
        }

    }
}
