using Shared.Enums;

namespace CarSharingAPI.Requests.Car;

public record SearchCarRequest
{
    public bool? IsActive { get; set; }
    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public VehicleType? VehicleType { get; set; }
    public FuelType? FuelType { get; set; }
    public string? Mark { get; set; }
}