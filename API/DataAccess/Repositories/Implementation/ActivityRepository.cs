using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class ActivityRepository : IActivityRepository
{
    private CarSharingContext db;

    public ActivityRepository(CarSharingContext db)
    {
        this.db = db;
    }

    public async Task<Activity?> GetByIdAsync(int id)
    {
        return await db.Activity.FindAsync(id);
    }

    public async Task<IEnumerable<Activity>> GetAllAsync()
    {
        return db.Activity.AsEnumerable();
    }

    public async Task<Activity> AddAsync(Activity entity)
    {
        await db.Activity.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Activity> UpdateAsync(Activity entity)
    {
        db.Entry(entity).State = EntityState.Modified; // проверить!
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Activity?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            db.Activity.Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        return null;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Activity.AnyAsync(p => p.Id == id);
    }

    public async Task<Activity?> GetByCarIdAsync(int id)
    {
        return await db.Activity.FirstOrDefaultAsync(a => a.CarId == id);
    }

    public async Task<IQueryable<Activity>> GetByRadiusAsync(float radiusKm, float latitude, float longitude)
    {
        var earthRadiusKm = 6371; 
        var maxDistance = radiusKm / earthRadiusKm;
        return db.Activity.Where(loc =>
            Math.Acos(
                Math.Sin(latitude * (Math.PI / 180)) * Math.Sin(loc.Latitude * (Math.PI / 180)) +
                Math.Cos(latitude * (Math.PI / 180)) * Math.Cos(loc.Latitude * (Math.PI / 180)) *
                Math.Cos((longitude - loc.Longitude) * (Math.PI / 180))
            ) <= maxDistance).AsQueryable();
    }
}