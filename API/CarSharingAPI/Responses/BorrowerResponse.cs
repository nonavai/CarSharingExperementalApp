using DataAccess.Enums;

namespace CarSharingAPI.Responses;

public record BorrowerResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Birth { get; set; }
    public string Country { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public LicenceCategory Category { get; set; }
    public DateTime LicenceExpiry { get; set; }

}