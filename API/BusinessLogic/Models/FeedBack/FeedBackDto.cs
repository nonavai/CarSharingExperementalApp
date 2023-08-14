namespace BusinessLogic.Models.FeedBack;

public class FeedBackDto
{
    public int Id { get; set; }
    public int LenderId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
}