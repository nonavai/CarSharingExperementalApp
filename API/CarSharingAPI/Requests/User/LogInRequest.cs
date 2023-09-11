namespace CarSharingAPI.Requests;

public record LogInRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}