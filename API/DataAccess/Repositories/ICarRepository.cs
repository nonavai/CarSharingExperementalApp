using DataAccess.Entities;
using Shared.Enums;

namespace DataAccess.Repositories;

public interface ICarRepository : IBaseRepository<Car>
{
    public Task<IQueryable<Car>> GetMany(int[] ids);
    public Task<IQueryable<Car>> SearchCars(int? minYear, int? maxYear, int? minPrice, int? maxPrice, VehicleType vehicleType, FuelType fuelType, string mark, float? radiusKm, float? latitude, float? longitude, bool? isActive );
}