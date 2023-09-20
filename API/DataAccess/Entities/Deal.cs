using Shared.Enums;

namespace DataAccess.Entities;

public class Deal : EntityBase
{
    public int LenderId { get; set; }
    public int CarId { get; set; }
    public int BorrowerId { get; set; }
    public DateTime BookingStart { get; set; }
    public DateTime BookingEnd { get; set; }
    public DealState State { get; set; }
    public float TotalPrice { get; set; }
    public int? Raiting { get; set; }

    public Lender Lender { get; set; }
    public Borrower Borrower { get; set; }
    public Car Car { get; set; }
}