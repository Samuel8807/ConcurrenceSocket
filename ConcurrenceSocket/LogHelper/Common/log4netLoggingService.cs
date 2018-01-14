/**********************************************************************************************
* 命名空间: iValonLogService.Common
* 类 名：   LoggingService
* 创建日期：2017/12/11 9:55:00
*
* Ver   负责人  机器名    变更内容
* ────────────────────────────────────────────────
* V0.01 刘灏  DESKTOP-834G9H0  初版
*
* Copyright (c)   NBA@Funmore.Inc.2017 Corporation. All rights reserved.
*┌───────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　  │
*│　版权所有：安吉汽车物流有限公司智能物流事业部　　　　                │
*└───────────────────────────────────┘
*/
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace iValonLogService.Common
{
    /// <summary>
    /// 功 能：   中文功能的描述
    /// Function: 英文功能描述
    /// 修改时间、版本：2017/12/11 9:55:00/4.0.30319.42000
    /// 修改人： DESKTOP-834G9H0/admin
    /// </summary>
    public class log4netLoggingService : ILoggingService
    {
        private readonly ILog log;

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly log4netLoggingService log = new log4netLoggingService();
        }

        public static log4netLoggingService LogHelper
        {
            get
            {

                return Nested.log ;
            }
        }

        private log4netLoggingService()
        {
            XmlConfigurator.Configure(new FileInfo("Configuration/iValonLog.config"));
            log = LogManager.GetLogger(typeof(log4netLoggingService));

        }

        public void Debug(object message)
        {
            Task.Factory.StartNew(()=> { log.Debug(message); });
        }

        public void DebugFormatted(string format, params object[] args)
        {
            log.DebugFormat(CultureInfo.InvariantCulture, format, args);
        }

        public void Info(object message)
        {
            log.Info(message);
        }

        public void InfoFormatted(string format, params object[] args)
        {
            log.InfoFormat(CultureInfo.InvariantCulture, format, args);
        }

        public void Warn(object message)
        {
            Task.Factory.StartNew(() => { log.Warn(message); });
        }

        public void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public void WarnFormatted(string format, params object[] args)
        {
            log.WarnFormat(CultureInfo.InvariantCulture, format, args);
        }

        public void Error(object message)
        {
             log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        public void ErrorFormatted(string format, params object[] args)
        {
            log.ErrorFormat(CultureInfo.InvariantCulture, format, args);
        }

        public void Fatal(object message)
        {
            Task.Factory.StartNew(() => { log.Fatal(message); });
        }

        public void Fatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }

        public void FatalFormatted(string format, params object[] args)
        {
            log.FatalFormat(CultureInfo.InvariantCulture, format, args);
        }

        public bool IsDebugEnabled
        {
            get { return log.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return log.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return log.IsFatalEnabled; }
        }

    }
}