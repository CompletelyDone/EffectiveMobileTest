using Delivery.Core;
using Delivery.Core.Models;
using System.Globalization;
using System.Text;

namespace Delivery.Data
{
    public class OrderRepository : IOrderRepository
    {
        private const string DEFAULT_ORDER_PATH = "data\\orders.txt";
        private readonly ILoggerService logger;
        public OrderRepository(string? ordersPath, ILoggerService logger)
        {
            if (ordersPath == null) ordersPath = DEFAULT_ORDER_PATH;
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
            if (order == null)
            {
                await logger.Log("Order is empty");
                return Guid.Empty;
            }

            var sb = new StringBuilder();
            sb.Append(order.Id + " ");
            sb.Append(order.Weight + " ");
            var validDistrict = new StringBuilder(order.District);
            validDistrict.Replace(" ", "");
            validDistrict.Append(' ');
            sb.Append(validDistrict);
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
                    if (parts.Length != 4) continue;
                    var searchDistrict = new StringBuilder(district);
                    searchDistrict.Replace(" ", "");
                    if (parts[2].ToLower() != searchDistrict.ToString().ToLower()) continue;
                    DateTime datetime;
                    DateTime.TryParseExact(parts[3], "yyyy-MM-dd_HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime);
                    if (datetime == DateTime.MinValue) continue;
                    if (datetime <= deliveryTime.AddMinutes(30) && datetime >= deliveryTime)
                    {
                        var (order, error) = Order.Create(Guid.Parse(parts[0]), double.Parse(parts[1]), parts[2], datetime);
                        if (!string.IsNullOrEmpty(error) || order == null)
                        {
                            await logger.Log(error);
                            continue;
                        }
                        orders.Add(order);
                    }
                }
            }

            await logger.Log("Get next deliveries");

            return orders;
        }
        public async Task<List<Order>> GetFilteredByDateInRangeAndByDistrict(string district, DateTime dateFrom, DateTime dateTo)
        {
            List<Order> orders = [];

            using (StreamReader sr = new StreamReader(OrderPath))
            {
                string? line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    var parts = line.Split(' ');
                    if (parts.Length != 4) continue;
                    var searchDistrict = new StringBuilder(district);
                    searchDistrict.Replace(" ", "");
                    if (parts[2].ToLower() != searchDistrict.ToString().ToLower()) continue;
                    DateTime datetime;
                    DateTime.TryParseExact(parts[3], "yyyy-MM-dd_HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime);
                    if (datetime == DateTime.MinValue) continue;
                    if (datetime >= dateFrom && datetime <= dateTo)
                    {
                        var (order, error) = Order.Create(Guid.Parse(parts[0]), double.Parse(parts[1]), parts[2], datetime);
                        if (!string.IsNullOrEmpty(error) || order == null)
                        {
                            await logger.Log(error);
                            continue;
                        }
                        orders.Add(order);
                    }
                }
            }

            return orders;
        }
    }
}
