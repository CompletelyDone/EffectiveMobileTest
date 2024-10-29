using Delivery.Application;
using Delivery.Core;
using Delivery.Data;

namespace Delivery.Test
{
    public class RepositoryTest
    {
        private ILoggerService loggerService = new LoggerService(null);
        private string validOrderPath = "data\\orders.txt";
        [Fact]
        public void OrderRepositoryCtorTest()
        {
            OrderRepository orderRepository = new OrderRepository(validOrderPath, loggerService);

            Assert.True(File.Exists(validOrderPath));
        }
        [Fact]
        public async void OrderRepositoryCtorTest2()
        {
            OrderRepository orderRepository = new OrderRepository(validOrderPath, loggerService);

            orderRepository.CreateAsync();
        }
    }
}
