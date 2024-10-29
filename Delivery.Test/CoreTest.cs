using Delivery.Core.Models;

namespace Delivery.Test
{
    public class CoreTest
    {
        private Guid validId = Guid.NewGuid();
        private double validWeight = 1;
        private string validDistrict = "Ленинский";
        private DateTime validDate = DateTime.UtcNow.AddMinutes(15);
        [Fact]
        public void CreateValidOrder()
        {
            var (order, error) = Order.Create(
                validId,
                validWeight,
                validDistrict,
                validDate);

            Assert.Multiple(() =>
            {
                Assert.Empty(error);
                Assert.NotNull(order);
                Assert.Equal(validId, order.Id);
                Assert.Equal(validWeight, order.Weight);
                Assert.Equal(validDistrict, order.District);
                Assert.Equal(validDate, order.DeliveryTime);
            });
        }
        [Fact]
        public void CreateOrderWithWeightEqualsZero()
        {
            var invalidWeight = 0;

            var (order, error) = Order.Create(
                validId,
                invalidWeight,
                validDistrict,
                validDate);

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
                validId,
                validWeight,
                emptyDistrict,
                validDate);

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(error);
                Assert.Null(order);
            });
        }
    }
} 
