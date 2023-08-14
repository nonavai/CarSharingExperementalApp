using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class FeedBackRepository : IFeedBackRepository
{
    private CarSharingContext db;
    public async Task<FeedBack?> GetByIdAsync(int id)
    {
        return await db.FeedBacks.FindAsync(id);
    }

    public async Task<IEnumerable<FeedBack>> GetAllAsync()
    {
        return db.FeedBacks.AsEnumerable();
    }

    public async Task<FeedBack> AddAsync(FeedBack entity)
    {
        await db.FeedBacks.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<FeedBack> UpdateAsync(FeedBack entity)
    {
        db.Entry(entity).State = EntityState.Modified; // проверить!
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<FeedBack?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            db.FeedBacks.Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        return null;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Borrowers.AnyAsync(p => p.Id == id);
    }

    public async Task<IQueryable<FeedBack>> GetByCarId(int id)
    {
        return db.FeedBacks.Where(f => f.CarId == id);
    }

    public async Task<IQueryable<FeedBack>> GetByUserId(int id)
    {
        return db.FeedBacks.Where(f => f.UserId == id);
    }
}