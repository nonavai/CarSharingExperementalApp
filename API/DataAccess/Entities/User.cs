using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class User : EntityBase
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string RecordNumber { get; set; }
    public string? Description { get; set; }

    public Borrower Borrower { get; set; }
    public Lender Lender { get; set; }
    public Roles Role { get; set; }
    public IEnumerable<FeedBack> FeedBacks { get; set; }
}