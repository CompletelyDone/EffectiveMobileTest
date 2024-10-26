namespace Delivery.Core
{
    public interface ILoggerService
    {
        string LoggerPath { get; set; }

        Task<bool> Log(string message);
    }
}