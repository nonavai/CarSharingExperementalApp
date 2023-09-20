namespace BusinessLogic.Models.User;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string RecordNumber { get; set; }
    public string Description { get; set; }
    //public string? Token { get; set; }
}