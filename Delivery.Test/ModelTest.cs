using Delivery.Core.Models;

namespace Delivery.Test
{
    public class ModelTest
    {
        [Fact]
        public void CreateValidOrder()
        {
            double weight = 1.0;
            string district = "newDistrict";
            DateTime dateTime = DateTime.UtcNow.AddDays(1);

            var order = Order.Create(Guid.NewGuid(), weight, district, dateTime);

            Assert.Empty(order.Error);
        }
    }
}