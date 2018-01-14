using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Concurrence
{
    /// <summary>
    /// 会话管理
    /// </summary>
    public class SessionManager
    {
        private static object obj = new object();
        private static List<AppSession> SessionList = new List<AppSession>();

        public static void Add(AppSession s)
        {
            lock (obj)
            {
                SessionList.Add(s);
            }
        }

        public static void Remove(AppSession s)
        {
            lock (obj)
            {
                SessionList.Remove(s);
            }
        }


        public static List<AppSession> Select()
        {
            lock (obj)
            {
               return SessionList;
            }
        }
    }
}