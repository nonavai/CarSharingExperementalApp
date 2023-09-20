
namespace DataAccess.Entities;

public class Roles : EntityBase
{
    public bool Admin { get; set; }
    public int UserId { get; set; }
    
    public int? BorrowerId { get; set; }
    
    public int? LenderId { get; set; }
    
    public Lender Lender { get; set; }
    public User User { get; set; }
    public Borrower Borrower { get; set; }
}