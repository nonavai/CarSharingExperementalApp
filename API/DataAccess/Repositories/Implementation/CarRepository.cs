using DataAccess.DbContext;
using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class CarRepository : ICarRepository
{
    private CarSharingContext db;

    public CarRepository(CarSharingContext db)
    {
        this.db = db;
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        return await db.Cars.FindAsync(id);
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        return db.Cars.AsEnumerable();
    }

    public async Task<Car> AddAsync(Car entity)
    {
        await db.Cars.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Car> UpdateAsync(Car entity)
    {
        db.Entry(entity).State = EntityState.Modified; // проверить!
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Car?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return null;
        db.Cars.Remove(entity);
        await db.SaveChangesAsync();
        return entity;

    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Cars.AnyAsync(p => p.Id == id);
    }

    public async Task<IQueryable<Car>> GetMany(int[] ids)
    {
        var cars = db.Cars.Where(car => ids.Contains(car.Id));
        return cars;
    }

    public async Task<IQueryable<Car>> SearchCars(int? minYear, int? maxYear, int? minPrice, int? maxPrice, VehicleType vehicleType, FuelType fuelType,
        string mark)
    {
        IQueryable<Car> query = db.Cars;

        // using filters

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
}