namespace Delivery.Core
{
    public interface ILoggerService
    {
        string LoggerPath { get; set; }
        Task<bool> LogAsync(string message);
    }
}