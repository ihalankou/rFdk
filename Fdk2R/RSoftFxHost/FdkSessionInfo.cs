﻿using System;
using SoftFX.Extended;

namespace RHost
{
    public static class FdkSessionInfo
    {
        private static DataFeed Feed
        {
            get { return FdkHelper.Wrapper.ConnectLogic.Feed; }
        }
        public static string GetSessionInfo()
        {
            SessionInfo sessionInfo = Feed.Server.GetSessionInfo();
            var result = FdkVars.RegisterVariable(sessionInfo, "SessionInfo");
            return result;
        }

        public static string PlatformCompany(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.PlatformCompany;
        }
        public static string PlatformName(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.PlatformName;
        }
        public static string TradingSessionId(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.TradingSessionId;
        }
        public static DateTime CloseTime(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.CloseTime;
        }
        public static DateTime EndTime(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.EndTime;
        }
        public static DateTime OpenTime(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.OpenTime;
        }
        public static DateTime StartTime(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.StartTime;
        }
        public static bool IsClosed(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.IsClosed;
        }
        public static int ServerTimeZoneOffset(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.ServerTimeZoneOffset;
        }
        public static string Status(string varName)
        {
            var session = FdkVars.GetValue<SessionInfo>(varName);
            return session.Status.ToString();
        }
        
    }
}
