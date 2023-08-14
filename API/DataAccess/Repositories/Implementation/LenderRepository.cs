using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class LenderRepository : ILenderRepository
{
    private CarSharingContext db;

    public LenderRepository(CarSharingContext db)
    {
        this.db = db;
    }

    public async Task<Lender?> GetByIdAsync(int id)
    {
        return await db.Lenders.FindAsync(id);
    }

    public async Task<IEnumerable<Lender>> GetAllAsync()
    {
        return db.Lenders.AsEnumerable();
    }

    public async Task<Lender> AddAsync(Lender entity)
    {
        await db.Lenders.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Lender> UpdateAsync(Lender entity)
    {
        db.Entry(entity).State = EntityState.Modified; // проверить!
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Lender?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return null;
        db.Lenders.Remove(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Lenders.AnyAsync(p => p.Id == id);
    }
    
}