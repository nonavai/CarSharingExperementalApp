namespace DataAccess.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int UserRoleId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}