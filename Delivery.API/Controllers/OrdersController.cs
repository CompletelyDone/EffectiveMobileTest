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
        private readonly ILoggerService loggerService;
        public OrdersController(IDataService dataService, ILoggerService loggerService)
        {
            this.dataService = dataService;
            this.loggerService = loggerService;
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] OrderRequest request)
        {
            if (request.DeliveryTime <= DateTime.UtcNow)
            {
                await loggerService.LogAsync("BadRequest. Wrong delivery time.");
                return BadRequest("Неверное время доставки");
            }
            var (order, error) = Order.Create(
                Guid.NewGuid(),
                request.Weight,
                request.District,
                request.DeliveryTime
                );
            if (!string.IsNullOrEmpty(error) || order == null)
            {
                await loggerService.LogAsync("BadRequest. Order creating error.");
                return BadRequest(error);
            }
            var orderId = await dataService.CreateOrderAsync(order);
            if(orderId.Equals(Guid.Empty))
            {
                await loggerService.LogAsync("BadRequest. Order creating error.");
                return BadRequest("Ошибка создания заказа.");
            }
            await loggerService.LogAsync("OrderController. CreateOrder. Success.");
            return Ok(orderId);
        }
        [HttpGet]
        [Route("NextDeliveries")]
        public async Task<ActionResult<List<Order>>> GetNextDeliveries([FromQuery] NextDeliveriesRequest request)
        {
            if (string.IsNullOrEmpty(request.District))
            {
                await loggerService.LogAsync("BadRequest. GetNextDeliveries. District null or empty.");
                return BadRequest("Заполнить поле район.");
            }
            var orders = await dataService.GetNextOrdersByDistrictAsync(request.District, request.DeliveryTime);
            await loggerService.LogAsync("OrderController. GetNextDeliveries. Success.");
            return Ok(orders);
        }
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetDeliveriesByDate([FromQuery] DeliveriesByDateAndDistrictRequest request)
        {
            if (string.IsNullOrEmpty(request.District))
            {
                await loggerService.LogAsync("BadRequest. GetNextDeliveries. District null or empty.");
                return BadRequest("Заполнить поле район.");
            }
            if (request.DateTimeFrom > request.DateTimeTo)
            {
                await loggerService.LogAsync("OrderController. GetDeliveriesByDate. DeliveryTime error.");
                return BadRequest("Неверно выбраны сроки доставки.");
            }
            var orders = await dataService.GetOrdersByDateInRangeAndByDistrictAsync(request.District, request.DateTimeFrom, request.DateTimeTo);
            await loggerService.LogAsync("OrderController. GetDeliveriesByDate. Success.");
            return Ok(orders);
        }
    }
}
