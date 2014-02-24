using System;
using log4net.Appender;
using log4net.Core;
using Microsoft.AspNet.SignalR.Client.Http;
using SignalRConnector;

namespace Log4Net.Appender.SignalR
{
    public class SignalRAppender : AppenderSkeleton
    {
        public IPublishToSignalR PublishToSignalR { get; set; }

        public string Uri { get; set; }

        public bool CallViaPersistentConnection { get; set; }

        public string HubName { get; set; }

        public string GroupName { get; set; }

        public string MethodToCall { get; set; }

        public override void ActivateOptions()
        {
            base.ActivateOptions();
            InitializeTarget();
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            string asyncBody = RenderLoggingEvent(loggingEvent);
            PublishToSignalR.WriteToQueue(GetFromLogLevel(loggingEvent.Level), asyncBody);
        }

        protected void InitializeTarget()
        {
            //base.InitializeTarget();

            SetPublisher();

            PublishToSignalR.Connect(new DefaultHttpClient());
        }

        private void SetPublisher()
        {
            if (PublishToSignalR != null)
            {
                return;
            }

            if (string.IsNullOrEmpty(Uri))
            {
                throw new ArgumentNullException(Uri);
            }

            //Try to set appropriate publisher if not set
            if (CallViaPersistentConnection)
            {
                PublishToSignalR = new PersistentConnectionPublisher(new ConnectionProxy((Uri)));
            }
            else
            {
                CallViaHub();
            }
        }

        private void CallViaHub()
        {
            if (string.IsNullOrEmpty(GroupName) || string.IsNullOrEmpty(MethodToCall) ||
                string.IsNullOrEmpty(HubName))
            {
                throw new ArgumentException(
                    "GroupName,MethodToCall & HubName are mandatory when CallViaPersistentConnection is set to false");
            }
            var hubconnection = new HubConnectionProxy(Uri);

            PublishToSignalR = new PublishToHub(hubconnection, GroupName, MethodToCall, HubName);
        }

        private LogLevel GetFromLogLevel(Level logLevel)
        {
            if (logLevel == Level.Info)
            {
                return LogLevel.Info;
            }

            if (logLevel == Level.Warn)
            {
                return LogLevel.Warn;
            }

            if (logLevel == Level.Error)
            {
                return LogLevel.Error;
            }

            if (logLevel == Level.Debug)
            {
                return LogLevel.Debug;
            }

            throw new ArgumentOutOfRangeException("logLevel");
        }
    }
}