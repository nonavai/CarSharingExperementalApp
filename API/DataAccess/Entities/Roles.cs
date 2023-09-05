
namespace DataAccess.Entities;

public class Roles
{
    public int Id { get; set; }
    public bool Admin { get; set; }
    
    public int? BorrowerId { get; set; }
    
    public int? LenderId { get; set; }
    
    public Lender Lender { get; set; }
    public User User { get; set; }
    public Borrower Borrower { get; set; }
}