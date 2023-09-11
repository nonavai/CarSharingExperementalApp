namespace CarSharingAPI.Requests;

public record CreateUserRequest
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RecordNumber { get; set; }
    public string PhoneNumber { get; set; }
}