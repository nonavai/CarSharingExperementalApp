using Shared.Enums;

namespace CarSharingAPI.Responses;

public record CarResponse
{
    public int Id { get; set; }
    public int LenderId { get; set; }
    public int Year { get; set; }
    public string RegistrationNumber { get; set; }
    public string Mark { get; set; }
    public string Model { get; set; }
    public float Price { get; set; }
    public FuelType FuelType { get; set; }
    public VehicleType VehicleType  { get; set; }
    public string Color  { get; set; }
}