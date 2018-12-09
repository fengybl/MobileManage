using System;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace YJL.Common
{
    public class Log4NetHelper
    {
        private static ILoggerRepository _repository;
        private static ILog _logInfo;

        private static ILog _logError;
        public static void InitLogHelper()
        {
            _repository = LogManager.CreateRepository("LogHelperRepository");
            XmlConfigurator.Configure(_repository, new FileInfo("log4net.config"));
            _logInfo = LogManager.GetLogger(_repository.Name, "loginfo");
            _logError = LogManager.GetLogger(_repository.Name,"logerror");
        }

        public static void WriteLog(string info)
        {
            if (_logInfo.IsInfoEnabled)
                _logInfo.Info(info);
        }
        public static void WriteLog(string info, Exception ex)
        {
            if (_logError.IsErrorEnabled)
                _logError.Error(info, ex);
        }
    }
}
