namespace BusinessLogic.Models.Lender;

public class LenderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public bool IsVerified { get; set; }
    public int Deals { get; set; }
    public float Rating { get; set; }
}