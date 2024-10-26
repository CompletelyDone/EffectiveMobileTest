using Delivery.API.Contracts;
using Delivery.Core;
using Delivery.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IDataService dataService;
        public OrdersController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] OrderRequest request)
        {
            var (order, error) = Order.Create(
                Guid.NewGuid(),
                request.Weight,
                request.District,
                request.DeliveryTime
                );

            if(!string.IsNullOrEmpty(error) || order == null)
            {
                return BadRequest(error);
            }

            var orderId = await dataService.CreateOrderAsync(order);

            return Ok(orderId);
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetNextDeliveries([FromBody] NextDeliveriesRequest request)
        {
            var orders = await dataService.GetNextOrdersByDistrict(request.District, request.DeliveryTime);

            return Ok(orders);
        }
    }
}
