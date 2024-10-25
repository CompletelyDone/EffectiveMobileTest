using Delivery.Core.Models;
using Delivery.Data;

namespace Delivery.Test
{
    public class DataServiceTest
    {
        [Fact]
        public async void CreateOrderTest()
        {
            string orderPath = "Data\\Orders.txt";
            DataService dataService = new DataService(orderPath);
            double weight = 1.0;
            string district = "newDistrict";
            DateTime dateTime = DateTime.UtcNow.AddDays(1);
            Order order = Order.Create(Guid.NewGuid(), weight, district, dateTime).Order;

            await dataService.CreateOrder(order);

            Assert.True(File.Exists(orderPath));
        }
    }
}
