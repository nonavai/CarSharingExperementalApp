using Shared.Enums;

namespace CarSharingAPI.Responses.Deal;

public record CreateDealResponse
{
    public int Id { get; set; }
    public int LenderId { get; set; }
    public int CarId { get; set; }
    public int BorrowerId { get; set; }
}