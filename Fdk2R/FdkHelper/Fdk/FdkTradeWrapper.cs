using SoftFX.Extended;
using SoftFX.Extended.Events;
using System;
using System.IO;
using System.Threading;

namespace FdkMinimal
{
    public class FdkTradeWrapper
    {
        public void Connect(string address, string username, string password, string logPath)
        {
            EnsureDirectoriesCreated(logPath);

            // Create builder
            SetupBuilder(address, username, password, logPath);
            Trade = new DataTrade
            {
                SynchOperationTimeout = 300000
            };
            var connectionString = Builder.ToString();
            Trade.Initialize(connectionString);
            Trade.Logon += OnLogon;
            Trade.Start();
            var timeoutInMilliseconds = Trade.SynchOperationTimeout;
            if (!_syncEvent.WaitOne(timeoutInMilliseconds))
            {
                throw new TimeoutException("Timeout of logon waiting has been reached");
            }
        }
 
        internal void SetupBuilder(string address, string username, string password, string logPath)
        {
            // Create builder
            ConnectionStringBuilder builder = null;

            if(!FdkHelper.UseLrp)
            {
                var fixBuilder = new FixConnectionStringBuilder();
                fixBuilder.TargetCompId = "EXECUTOR";
                fixBuilder.ProtocolVersion = FixProtocolVersion.TheLatestVersion.ToString();
                fixBuilder.SecureConnection = true;
                fixBuilder.Port = 5004; ////ExcludeMessagesFromLogs = "W",
                fixBuilder.DecodeLogFixMessages = true;
                fixBuilder.Address = address;
                fixBuilder.Username = username;
                fixBuilder.Password = password;
                fixBuilder.FixLogDirectory = logPath;
                fixBuilder.FixEventsFileName = string.Format("{0}.trade.events.log", username);
                fixBuilder.FixMessagesFileName = string.Format("{0}.trade.messages.log", username);
                builder = fixBuilder;
            }
            else
            {
                var fixBuilder = new LrpConnectionStringBuilder();
                fixBuilder.SecureConnection = true;
                fixBuilder.Port = 5004; ////ExcludeMessagesFromLogs = "W",
                fixBuilder.Address = address;
                fixBuilder.Username = username;
                fixBuilder.Password = password;
                fixBuilder.MessagesLogFileName = string.Format("{0}.trade.events.log", username);
                builder = fixBuilder;
            }


            Builder = builder;
        }

        public DataTrade Trade { get; set; }

        internal ConnectionStringBuilder Builder { get; private set; }
        readonly AutoResetEvent _syncEvent = new AutoResetEvent(false);

        private void OnLogon(object sender, LogonEventArgs e)
        {
            _syncEvent.Set();
        }

        static void EnsureDirectoriesCreated(string logPath)
        {
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
        }
    }
}