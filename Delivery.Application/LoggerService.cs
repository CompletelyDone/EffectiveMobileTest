using Delivery.Core;
using System.Text;

namespace Delivery.Application
{
    public class LoggerService : ILoggerService
    {
        private const string DEFAULT_LOGGER_PATH = "log\\logs.txt";
        public LoggerService(string? loggerPath)
        {
            if (loggerPath == null) loggerPath = DEFAULT_LOGGER_PATH;
            if (!File.Exists(loggerPath))
            {
                DirectoryInfo? parent = Directory.GetParent(loggerPath);
                if (parent != null) Directory.CreateDirectory(parent.FullName);
                File.Create(loggerPath).Dispose();
            }
            LoggerPath = loggerPath;
        }
        public string LoggerPath { get; set; }
        public async Task<bool> Log(string message)
        {
            var sb = new StringBuilder();
            sb.Append(DateTime.UtcNow.ToString("yyyy-MM-dd_HH:mm:ss "));
            sb.Append(message);
            using (var sw = new StreamWriter(LoggerPath, true))
            {
                await sw.WriteLineAsync(sb.ToString());
            }
            return true;
        }
    }
}
