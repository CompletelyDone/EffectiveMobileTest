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
            if (request.DeliveryTime <= DateTime.UtcNow) return BadRequest("Неверное время доставки");

            var (order, error) = Order.Create(
                Guid.NewGuid(),
                request.Weight,
                request.District,
                request.DeliveryTime
                );

            if (!string.IsNullOrEmpty(error) || order == null)
            {
                return BadRequest(error);
            }

            var orderId = await dataService.CreateOrderAsync(order);

            return Ok(orderId);
        }
        /*
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetNextDeliveries([FromQuery] NextDeliveriesRequest request)
        {
            var orders = await dataService.GetNextOrdersByDistrict(request.District, request.DeliveryTime);

            return Ok(orders);
        }
        */
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetDeliveriesByDate([FromQuery] DeliveriesByDateAndDistrictRequest request)
        {
            var orders = await dataService.GetOrdersByDateInRangeAndByDistrict(request.District,request.DateTimeFrom,request.DateTimeTo);

            return Ok(orders);
        }
    }
}
