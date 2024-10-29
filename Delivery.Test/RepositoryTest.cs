using Delivery.Application;
using Delivery.Core;
using Delivery.Core.Models;
using Delivery.Data;

namespace Delivery.Test
{
    public class RepositoryTest
    {
        private ILoggerService loggerService = new LoggerService(null);
        [Fact]
        public void OrderRepositoryCtor()
        {
            OrderRepository orderRepository = new OrderRepository(ValidModels.OrderPath, loggerService);

            Assert.True(File.Exists(ValidModels.OrderPath));
        }
        [Fact]
        public async void OrderRepositoryCreateAsync()
        {
            OrderRepository orderRepository = new OrderRepository(ValidModels.OrderPath, loggerService);
            var (order, error) = Order.Create(
                ValidModels.Guid, 
                ValidModels.Weight,
                ValidModels.District,
                ValidModels.DeliveryDateTime);

            await orderRepository.CreateAsync(order);

            Assert.Multiple(() =>
            {
                Assert.NotNull(order);
                Assert.True(File.Exists(ValidModels.OrderPath));
                Assert.Contains($"{order.Id} {order.Weight} {order.District.Replace(" ", "")} {order.DeliveryTime.ToString("yyyy-MM-dd_HH:mm:ss")}",File.ReadAllText(ValidModels.OrderPath));
            });
        }
    }
}
