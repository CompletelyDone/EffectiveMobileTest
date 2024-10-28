namespace Delivery.Core.Models
{
    public class Order
    {
        const double MIN_WEIGHT = 0.01;
        const double MAX_WEIGHT = 100.0;
        const int MIN_DISTRICT_LEN = 3;
        const int MAX_DISTRICT_LEN = 100;
        private Order(Guid id, double weight, string district, DateTime deliveryTime)
        {
            Id = id;
            Weight = weight;
            District = district;
            DeliveryTime = deliveryTime;
        }

        public Guid Id { get; }
        public double Weight { get; }
        public string District { get; }
        public DateTime DeliveryTime { get; }
        public static (Order? Order, string Error) Create(Guid id, double weight, string district, DateTime deliveryTime)
        {
            var error = string.Empty;
            Order order;

            if (weight < MIN_WEIGHT) return(null, error = $"Weight can not be less than {MIN_WEIGHT}");
            if (weight > MAX_WEIGHT) return(null, error = $"Weight can not be more than {MAX_WEIGHT}");
            if (district.Length < MIN_DISTRICT_LEN) return(null, error = $"District length can not be less than {MIN_DISTRICT_LEN}");
            if (district.Length > MAX_DISTRICT_LEN) return(null, error = $"District length can not be more than {MAX_DISTRICT_LEN}");

            order = new Order(id, weight, district, deliveryTime);

            return (order, error);
        }
    }
}
