using System.Reflection;
using log4net;
using log4net.Config;

namespace MonefyWeb.Transversal.Utils
{
    public class Logger : ILogger
    {
        private readonly log4net.ILog _logger;

        public Logger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            this._logger = log4net.LogManager.GetLogger("file");
        }

        public void Debug(string message)
        {
            this._logger?.Debug(message);
        }

        public void Info(string message)
        {
            this._logger?.Info(message);
        }

        public void Error(string message, Exception? ex = null)
        {
            this._logger?.Error(message, ex?.InnerException);
        }
    }
}
