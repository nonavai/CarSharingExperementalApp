using Shared.Enums;

namespace CarSharingAPI.Requests;

public record CreateCarRequest
{
    public int LenderId { get; set; } // lender id should be taken from the token
    public int Year { get; set; }
    public string RegistrationNumber { get; set; }
    public string Mark { get; set; }
    public string Model { get; set; }
    public float Price { get; set; }
    public FuelType FuelType { get; set; }
    public VehicleType VehicleType  { get; set; }
    public string VehicleBody { get; set; }
    public string Color  { get; set; }
}