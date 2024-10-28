namespace Delivery.API.Contracts
{
    public record DeliveriesByDateAndDistrictRequest(
        string District,
        DateTime DateTimeFrom,
        DateTime DateTimeTo
        );
}
