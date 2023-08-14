using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Enums;

namespace BusinessLogic.Models.Borrower;

public class BorrowerDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Birth { get; set; }
    public string Country { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public LicenceCategory Category { get; set; }
    public DateTime LicenceExpiry { get; set; }
    public DateTime LicenceIssue { get; set; }
    public string LicenceId { get; set; }
    public string PlaceOfIssue { get; set; }

    
    //some info that needed from driver 
}