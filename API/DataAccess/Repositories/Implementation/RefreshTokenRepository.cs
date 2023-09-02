using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private CarSharingContext db;

    public RefreshTokenRepository(CarSharingContext db)
    {
        this.db = db;
    }

    public async Task<RefreshToken?> GetByIdAsync(int id)
    {
        return await db.RefreshTokens.FindAsync(id);
    }

    public async Task<IEnumerable<RefreshToken>> GetAllAsync()
    {
        return db.RefreshTokens.AsEnumerable();
    }

    public async Task<RefreshToken> AddAsync(RefreshToken entity)
    {
        await db.RefreshTokens.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<RefreshToken> UpdateAsync(RefreshToken entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<RefreshToken?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            db.RefreshTokens.Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        return null;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Activity.AnyAsync(p => p.Id == id);
    }

    public async Task<RefreshToken?> GetByUserId(int id)
    {
        return db.RefreshTokens.FirstOrDefault(f => f.UserId == id);
    }
    
}