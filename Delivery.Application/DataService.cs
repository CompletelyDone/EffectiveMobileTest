using Delivery.Core;
using Delivery.Core.Models;

namespace Delivery.Application
{
    public class DataService : IDataService
    {
        private readonly IOrderRepository orderRepository;
        public DataService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<Guid> CreateOrderAsync(Order order)
        {
            return await orderRepository.CreateAsync(order);
        }
        public async Task<List<Order>> GetNextOrdersByDistrictAsync(string district, DateTime deliveryTime)
        {
            return await orderRepository.GetFilteredByDistrictNext30Minutes(district, deliveryTime);
        }
        public async Task<List<Order>> GetOrdersByDateInRangeAndByDistrictAsync(string district, DateTime dateFrom, DateTime dateTo)
        {
            return await orderRepository.GetFilteredByDateInRangeAndByDistrict(district, dateFrom, dateTo);
        }
    }
}
