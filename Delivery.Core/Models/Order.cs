namespace Delivery.Core.Models
{
    public class Order
    {
        public const double MIN_WEIGHT = 0.01;
        public const double MAX_WEIGHT = 100.0;
        private Order(Guid id, double weight, District district, DateTime deliveryTime)
        {
            Id = id;
            Weight = weight;
            District = district;
            DeliveryTime = deliveryTime;
        }

        public Guid Id { get; }
        public double Weight { get; }
        public District District { get; }
        public DateTime DeliveryTime { get; }
        public static (Order Order, string Error) Create(Guid id, double weight, District district, DateTime deliveryTime)
        {
            var error = string.Empty;

            if (weight < MIN_WEIGHT) error = "Weight can not be less than 0.01";
            if (weight > MAX_WEIGHT) error = "Weight can not be more than 100.0";

            var order = new Order(id, weight, district, deliveryTime);

            return (order, error);
        }
    }
}
