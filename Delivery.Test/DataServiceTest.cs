using Delivery.Core.Models;

namespace Delivery.Test
{
    public class DataServiceTest
    {
        [Fact]
        public void CreateOrderTest()
        {
            string orderPath = "Data\\Orders.txt";
            double weight = 1.0;
            string district = "newDistrict";
            DateTime dateTime = DateTime.UtcNow.AddMinutes(15);

            Order? order = Order.Create(Guid.NewGuid(), weight, district, dateTime).Order;


            Assert.True(File.Exists(orderPath));
        }
    }
}
