/**********************************************************************************************
* 命名空间: iValonLogService.Common
* 类 名：   LogSingleton
* 创建日期：2017/12/11 12:10:39
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
using iValonLogService.Common;
using log4net;
using LogHelper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace iValonLogService.Common
{
    /// <summary>
    /// 功 能：   中文功能的描述
    /// Function: 英文功能描述
    /// 修改时间、版本：2017/12/11 12:10:39/4.0.30319.42000
    /// 修改人： DESKTOP-834G9H0/admin
    /// </summary>
    public class LogSingleton
    {
        public static log4netLoggingService LogHelper = log4netLoggingService.LogHelper;
        public static AssemblyService AssemblyLogHelper = AssemblyService.LogHelper;
    }
}