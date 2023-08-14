namespace BusinessLogic.Models.Activity;

public class ActivityDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public bool IsActive { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}