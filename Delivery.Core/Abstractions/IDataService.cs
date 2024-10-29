using Delivery.Core.Models;

namespace Delivery.Core
{
    public interface IDataService
    {
        Task<Guid> CreateOrderAsync(Order order);
        Task<List<Order>> GetNextOrdersByDistrictAsync(string district, DateTime deliveryTime);
        Task<List<Order>> GetOrdersByDateInRangeAndByDistrictAsync(string district, DateTime dateFrom, DateTime dateTo);
    }
}