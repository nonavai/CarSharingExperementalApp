namespace CarSharingAPI.Responses;

public record LenderResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public bool IsVerified { get; set; }
    public int Deals { get; set; }
    public float Rating { get; set; }
}