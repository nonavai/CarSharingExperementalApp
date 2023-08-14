using DataAccess.Entities;

namespace DataAccess.Repositories;

public interface IActivityRepository : IBaseRepository<Activity>
{
    Task<Activity?> GetByCarIdAsync(int id);
    Task<IQueryable<Activity>> GetByRadiusAsync(float radiusKm, float latitude, float longitude);
}