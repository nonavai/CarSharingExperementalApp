namespace CarSharingAPI.Requests;

public record ActivityRequest
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    
}