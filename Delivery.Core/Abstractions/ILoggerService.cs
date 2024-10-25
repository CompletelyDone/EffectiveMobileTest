namespace Delivery.Application
{
    public interface ILoggerService
    {
        string LoggerPath { get; set; }

        Task<bool> Log(string message);
    }
}