using Delivery.Core.Models;

namespace Delivery.Test
{
    public class CoreTest
    {
        [Fact]
        public void CreateValidOrder()
        {
            var (order, error) = Order.Create(
                ValidModels.Guid,
                ValidModels.Weight,
                ValidModels.District,
                ValidModels.DeliveryDateTime);

            Assert.Multiple(() =>
            {
                Assert.Empty(error);
                Assert.NotNull(order);
                Assert.Equal(ValidModels.Guid, order.Id);
                Assert.Equal(ValidModels.Weight, order.Weight);
                Assert.Equal(ValidModels.District, order.District);
                Assert.Equal(ValidModels.DeliveryDateTime, order.DeliveryTime);
            });
        }
        [Fact]
        public void CreateOrderWithWeightEqualsZero()
        {
            var invalidWeight = 0;

            var (order, error) = Order.Create(
                ValidModels.Guid,
                invalidWeight,
                ValidModels.District,
                ValidModels.DeliveryDateTime);

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(error);
                Assert.Null(order);
            });
        }
        [Fact]
        public void CreateOrderWithEmptyDistrict()
        {
            var emptyDistrict = string.Empty;

            var (order, error) = Order.Create(
                ValidModels.Guid,
                ValidModels.Weight,
                emptyDistrict,
                ValidModels.DeliveryDateTime);

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(error);
                Assert.Null(order);
            });
        }
    }
} 
