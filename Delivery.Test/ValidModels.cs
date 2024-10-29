namespace Delivery.Test
{
    public static class ValidModels
    {
        public static Guid Guid { get; } = Guid.NewGuid();
        public static double Weight { get; } = 5;
        public static string District { get; } = "Ленинский";
        public static DateTime DeliveryDateTime = DateTime.UtcNow.AddMinutes(15);
        public static string OrderPath { get; } = "data\\orders.txt";
    }
}
