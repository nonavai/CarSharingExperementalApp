using DataAccess.Entities;
using Shared.Enums;

namespace DataAccess.Repositories;

public interface ICarRepository : IBaseRepository<Car>
{
    public Task<IQueryable<Car>> GetMany(int[] ids);
    public Task<IQueryable<Car>> SearchCars(int? MinYear, int? MaxYear, int? MinPrice, int? MaxPrice, VehicleType VehicleType, FuelType FuelType, string Mark);
}