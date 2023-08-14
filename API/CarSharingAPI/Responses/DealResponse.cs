using DataAccess.Enums;

namespace CarSharingAPI.Responses;

public record DealResponse
{
    public int Id { get; set; }
    public int LenderId { get; set; }
    public int CarId { get; set; }
    public int BorrowerId { get; set; }
    public DateTime BookingStart { get; set; }
    public DateTime BookingEnd { get; set; }
    public DealState State { get; set; }
    public float TotalPrice { get; set; }
}