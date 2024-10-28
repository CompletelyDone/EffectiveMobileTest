using Delivery.Core.Models;

namespace Delivery.Core
{
    public interface IOrderRepository
    {
        string OrderPath { get; set; }
        Task<Guid> CreateAsync(Order? order);
        Task<List<Order>> GetFilteredByDistrictNext30Minutes(string district, DateTime deliveryTime);
        Task<List<Order>> GetFilteredByDateInRangeAndByDistrict(string district, DateTime dateFrom, DateTime dateTo);
    }
}