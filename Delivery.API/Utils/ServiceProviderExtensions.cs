using Delivery.Application;
using Delivery.Core;
using Delivery.Data;

namespace Delivery.API.Utils
{
    public static class ServiceProviderExtensions
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration config)
        {
            var orderPath = config["OrderPath"];
            var loggerPath = config["LoggerPath"];

            services.AddScoped<ILoggerService>(provider =>
            {
                return new LoggerService(loggerPath);
            });

            services.AddScoped<IOrderRepository>(provider =>
            {
                var logger = provider.GetRequiredService<ILoggerService>();
                return new OrderRepository(orderPath,logger);
            });

            services.AddScoped<IDataService>(provider =>
            {
                var orderRepository = provider.GetRequiredService<IOrderRepository>();
                return new DataService(orderRepository);
            });
        }
    }
}
