using Shared.Enums;

namespace BusinessLogic.Models.Car;

public class CarFilterDto
{
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public VehicleType VehicleType { get; set; }
    public FuelType FuelType { get; set; }
    public string Mark { get; set; }
}