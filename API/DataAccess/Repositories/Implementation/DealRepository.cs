using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class DealRepository : IDealRepository
{
    private CarSharingContext db;
    public async Task<Deal?> GetByIdAsync(int id)
    {
        return await db.Deals.FindAsync(id);
    }

    public async Task<IEnumerable<Deal>> GetAllAsync()
    {
        return db.Deals.AsEnumerable();
    }

    public async Task<Deal> AddAsync(Deal entity)
    {
        await db.Deals.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Deal> UpdateAsync(Deal entity)
    {
        db.Entry(entity).State = EntityState.Modified; // проверить!
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Deal?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return null;
        db.Deals.Remove(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Deals.AnyAsync(p => p.Id == id);
    }
}