using Delivery.Core.Models;

namespace Delivery.Core
{
    public interface IDataService
    {
        Task<Guid> CreateOrderAsync(Order order);
        Task<List<Order>> GetNextOrdersByDistrict(string district, DateTime deliveryTime);
        Task<List<Order>> GetOrdersByDateInRangeAndByDistrict(string district, DateTime dateFrom, DateTime dateTo);
    }
}