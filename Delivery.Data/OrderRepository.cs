using Delivery.Core;
using Delivery.Core.Models;
using System.Globalization;
using System.Text;

namespace Delivery.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ILoggerService logger;
        public OrderRepository(string ordersPath, ILoggerService logger)
        {
            if (!File.Exists(ordersPath))
            {
                DirectoryInfo? parent = Directory.GetParent(ordersPath);
                if (parent != null) Directory.CreateDirectory(parent.FullName);
                File.Create(ordersPath).Dispose();
            }
            OrderPath = ordersPath;
            this.logger = logger;
        }
        public string OrderPath { get; set; }

        public async Task<Guid> CreateAsync(Order? order)
        {
            if (order == null) return Guid.Empty;
            var sb = new StringBuilder();
            sb.Append(order.Id + " ");
            sb.Append(order.Weight + " ");
            sb.Append(order.District + " ");
            sb.Append(order.DeliveryTime.ToString("yyyy-MM-dd_HH:mm:ss"));

            using (StreamWriter sw = new StreamWriter(OrderPath, true))
            {
                await sw.WriteLineAsync(sb.ToString());
            }

            await logger.Log("Order created");

            return order.Id;
        }
        public async Task<List<Order>> GetFilteredByDistrictNext30Minutes(string district, DateTime deliveryTime)
        {
            List<Order> orders = [];
            using (StreamReader sr = new StreamReader(OrderPath))
            {
                string? line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    var parts = line.Split(' ');
                    if (parts[2] != district) continue;
                    var datetime = DateTime.ParseExact(parts[3], "yyyy-MM-dd_HH:mm:ss", CultureInfo.InvariantCulture);
                    if (datetime <= deliveryTime.AddMinutes(30) && datetime >= deliveryTime)
                    {
                        var order = Order.Create(Guid.Parse(parts[0]), double.Parse(parts[1]), parts[2], datetime).Order;
                        if (order != null) orders.Add(order);
                    }
                }
            }
            return orders;
        }
    }
}
