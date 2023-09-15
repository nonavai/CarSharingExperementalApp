namespace CarSharingAPI.Requests.Activity;

public record ActivityGeoRequest
{
    public float RadiusKm { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}