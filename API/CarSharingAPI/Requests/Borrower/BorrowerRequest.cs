using Shared.Enums;

namespace CarSharingAPI.Requests.Borrower;

public record BorrowerRequest
{
    
    public DateTime Birth { get; set; }
    public string Country { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public LicenceCategory Category { get; set; }
    public DateTime LicenceExipiry { get; set; }
    public DateTime LicenceIssue { get; set; }
    public string LicenceId { get; set; }
    public string PlaceOfIssue { get; set; }
}