using Delivery.Application;
using Delivery.Core.Models;
using Delivery.Data;

namespace Delivery.Test
{
    public class DataServiceTest
    {
        [Fact]
        public void CreateOrderTest()
        {
            string orderPath = "Data\\Orders.txt";
            DataService dataService = new DataService();
            double weight = 1.0;
            string district = "newDistrict";
            DateTime dateTime = DateTime.UtcNow.AddMinutes(15);

            Order? order = Order.Create(Guid.NewGuid(), weight, district, dateTime).Order;


            Assert.True(File.Exists(orderPath));
        }
    }
}
