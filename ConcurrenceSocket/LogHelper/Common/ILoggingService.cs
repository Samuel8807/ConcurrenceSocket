/**********************************************************************************************
* 命名空间: iValonLogService.Common
* 类 名：   ILoggingService
* 创建日期：2017/12/11 9:53:11
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace iValonLogService.Common
{
    /// <summary>
    /// 功 能：   中文功能的描述
    /// Function: 英文功能描述
    /// 修改时间、版本：2017/12/11 9:53:11/4.0.30319.42000
    /// 修改人： DESKTOP-834G9H0/admin
    /// </summary>
    public interface ILoggingService
    {
        bool IsDebugEnabled
        {
            get;
        }

        bool IsInfoEnabled
        {
            get;
        }

        bool IsWarnEnabled
        {
            get;
        }

        bool IsErrorEnabled
        {
            get;
        }

        bool IsFatalEnabled
        {
            get;
        }

        void Debug(object message);

        void DebugFormatted(string format, params object[] args);

        void Info(object message);

        void InfoFormatted(string format, params object[] args);

        void Warn(object message);

        void Warn(object message, Exception exception);

        void WarnFormatted(string format, params object[] args);

        void Error(object message);

        void Error(object message, Exception exception);

        void ErrorFormatted(string format, params object[] args);

        void Fatal(object message);

        void Fatal(object message, Exception exception);

        void FatalFormatted(string format, params object[] args);
    }
}