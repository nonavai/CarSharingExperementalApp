using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class ActivityRepository : GenericRepository<Activity> , IActivityRepository
{
    private CarSharingContext db;
    
    public ActivityRepository(CarSharingContext db) : base(db)
    {
        this.db = db;
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