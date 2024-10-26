using Delivery.Application;
using Delivery.Core.Models;
using Delivery.Data;

namespace Delivery.Test
{
    public class RepositoryTest
    {
        [Fact]
        public async void RepTest()
        {
            var loggerPath = "Data\\log.txt";
            LoggerService loggerService = new LoggerService(loggerPath);
            var orderPath = "Data\\Orders.txt";
            OrderRepository orderRepository = new OrderRepository(orderPath, loggerService);
            Random rnd = new Random();

            string district = "newDistrict";
            double weight = rnd.Next(1, 50);
            DateTime dateTime = DateTime.UtcNow.AddMinutes(rnd.Next(-15, 45));
            Order? order = Order.Create(Guid.NewGuid(), weight, district, dateTime).Order;

            await orderRepository.CreateAsync(order);
        }
    }
}
