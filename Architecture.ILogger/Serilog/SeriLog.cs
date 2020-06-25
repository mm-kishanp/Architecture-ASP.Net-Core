using Serilog;
using Serilog.Events;

namespace Architecture.ILogger.Serilog
{
    public static class Logger
    {
        public static void Logging(string message, LogEventLevel logEventLevel)
        {
            SeriLog seriLog = new SeriLog()
            {
                // change your setting here
            };
            seriLog.WriteLog(message, logEventLevel);
        }
        public static void UserActionLoggin(string message, LogEventLevel logEventLevel)
        {
            SeriLog seriLog = new SeriLog()
            {
                FilePath = "logs\\UserActivity\\"  
            };
            seriLog.WriteLog(message, logEventLevel);
        }
    }
    class SeriLog
    {
        public RollingInterval rollingInterval;
        public string FilePath;
        public string FileName;
        public string outputTemplate;
        public bool SplitFileByLogEventLevel;
        /// <summary>
        /// 
        /// </summary>
        public SeriLog()
        {
            FileName = System.AppDomain.CurrentDomain.FriendlyName;
            SplitFileByLogEventLevel = true;
            rollingInterval = RollingInterval.Minute;
            FilePath = "logs\\" ;
            outputTemplate = "Date : {Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}{NewLine}{Message:lj}{NewLine}======================================================================================================{NewLine}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logEventLevel"></param>
        /// <param name="filePath"></param>
        /// <param name="rollingInterval"></param>
        /// <param name=""></param>
        public void WriteLog(string message, LogEventLevel logEventLevel)
        {
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File(SplitFileByLogEventLevel ? FilePath + FileName + "_" + logEventLevel + "_" + ".txt" : FilePath + FileName + "_" + ".txt", rollingInterval: rollingInterval, outputTemplate: outputTemplate)
                    .CreateLogger();

            switch (logEventLevel)
            {
                case LogEventLevel.Debug:
                    {
                        Log.Debug(message);
                        break;
                    }
                case LogEventLevel.Verbose:
                    {
                        Log.Verbose(message);
                        break;
                    }
                case LogEventLevel.Information:
                    {
                        Log.Information(message);
                        break;
                    }
                case LogEventLevel.Warning:
                    {
                        Log.Warning(message);
                        break;
                    }
                case LogEventLevel.Error:
                    {
                        Log.Error(message);
                        break;
                    }
                case LogEventLevel.Fatal:
                    {
                        Log.Fatal(message);
                        break;
                    }
            }
            Log.CloseAndFlush();
        }
    }
}
