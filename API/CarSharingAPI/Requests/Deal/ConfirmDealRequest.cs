using Shared.Enums;

namespace CarSharingAPI.Requests.Deal;

public record ConfirmDealRequest
{
    public DateTime BookingStart { get; set; }
    public DateTime BookingEnd { get; set; }
    public DealState State { get; set; }
    public float TotalPrice { get; set; }
}