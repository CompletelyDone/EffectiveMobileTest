using System.Text;

namespace Delivery.Application
{
    public class LoggerService : ILoggerService
    {
        public LoggerService(string loggerPath)
        {
            if (!File.Exists(loggerPath))
            {
                var parent = Directory.GetParent(loggerPath);
                Directory.CreateDirectory(parent.FullName);
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
