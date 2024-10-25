using Delivery.Core.Models;

namespace Delivery.Data
{
    public class DataService
    {
        public DataService(string ordersPath) 
        {
            if (!File.Exists(ordersPath))
            {
                var parent = Directory.GetParent(ordersPath);
                Directory.CreateDirectory(parent.FullName);
                File.Create(ordersPath).Dispose();
            }
            OrderPath = ordersPath;
        }
        public string OrderPath { get; set; }

        public async Task<Guid> CreateOrder(Order order)
        {
            string str = $"{order.Id} {order.Weight} {order.District} {order.DeliveryTime.ToString("yyyy-MM-dd_HH:mm:ss")}";

            using (StreamWriter sw = new StreamWriter(OrderPath, true))
            {
                await sw.WriteLineAsync(str);
            }

            return order.Id;
        }


    }
}
