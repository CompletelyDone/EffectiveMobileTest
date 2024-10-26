namespace Delivery.API.Contracts
{
    public record NextDeliveriesRequest(
        string District,
        DateTime DeliveryTime
        );
}
