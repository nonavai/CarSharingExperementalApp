using DataAccess.Enums;
using Microsoft.AspNetCore.Components;

namespace CarSharingAPI.Requests;

public record SearchCarRequest
{
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public VehicleType? VehicleType { get; set; }
    public FuelType? FuelType { get; set; }
    public string? Mark { get; set; }
}