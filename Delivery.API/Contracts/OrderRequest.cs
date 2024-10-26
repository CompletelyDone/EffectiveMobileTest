namespace Delivery.API.Contracts
{
    public record OrderRequest(
        double Weight,
        string District,
        DateTime DeliveryTime
        );
}
