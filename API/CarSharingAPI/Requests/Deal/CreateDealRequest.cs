using Shared.Enums;

namespace CarSharingAPI.Requests.Deal;

public record CreateDealRequest
{
    public int LenderId { get; set; }
    public int CarId { get; set; }
    public int BorrowerId { get; set; }
}