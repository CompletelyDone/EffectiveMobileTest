using Delivery.Core.Models;
using System.Text;

namespace Delivery.Application
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
            var sb = new StringBuilder();
            sb.Append(order.Id + " ");
            sb.Append(order.Weight + " ");
            sb.Append(order.District + " ");
            sb.Append(order.DeliveryTime.ToString("yyyy-MM-dd_HH:mm:ss"));

            using (StreamWriter sw = new StreamWriter(OrderPath, true))
            {
                await sw.WriteLineAsync(sb.ToString());
            }

            return order.Id;
        }
    }
}
