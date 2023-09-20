using DataAccess.DbContext;
using DataAccess.Entities;
using Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class CarRepository : GenericRepository<Car> , ICarRepository
{
    private CarSharingContext db;

    public CarRepository(CarSharingContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<IQueryable<Car>> GetMany(int[] ids)
    {
        var cars = db.Cars.Where(car => ids.Contains(car.Id));
        return cars;
    }

    public async Task<IQueryable<Car>> SearchCars(int? minYear, int? maxYear, int? minPrice, int? maxPrice, VehicleType vehicleType, FuelType fuelType,
        string mark, float? radiusKm, float? latitude, float? longitude, bool? isActive)
    {
        IQueryable<Car> query;

        if (radiusKm.HasValue && latitude.HasValue && longitude.HasValue)
        {
            query = await GetByRadiusAsync(radiusKm.Value, latitude.Value, longitude.Value);
        }
        else
        {
            query = db.Cars;
        }

        if (isActive.HasValue)
            query = query.Where(c => c.IsActive == isActive.Value);

        if (minYear.HasValue)
            query = query.Where(c => c.Year >= minYear);

        if (maxYear.HasValue)
            query = query.Where(c => c.Year <= maxYear);

        if (minPrice.HasValue)
            query = query.Where(c => c.Price >= minPrice);

        if (maxPrice.HasValue)
            query = query.Where(c => c.Price <= maxPrice);

        if (vehicleType != VehicleType.None)
            query = query.Where(c => c.VehicleType == vehicleType);

        if (fuelType != FuelType.None)
            query = query.Where(c => c.FuelType == fuelType);

        if (!string.IsNullOrEmpty(mark))
            query = query.Where(c => c.Mark.Contains(mark));
        
        
        return  await Task.FromResult(query);
    }
    private async Task<IQueryable<Car>> GetByRadiusAsync(float radiusKm, float latitude, float longitude)
    {
        var earthRadiusKm = 6371; 
        var maxDistance = radiusKm / earthRadiusKm;
        return db.Cars.Where(loc =>
            Math.Acos(
                Math.Sin(latitude * (Math.PI / 180)) * Math.Sin(loc.Latitude * (Math.PI / 180)) +
                Math.Cos(latitude * (Math.PI / 180)) * Math.Cos(loc.Latitude * (Math.PI / 180)) *
                Math.Cos((longitude - loc.Longitude) * (Math.PI / 180))
            ) <= maxDistance).AsQueryable();
    }
    
}