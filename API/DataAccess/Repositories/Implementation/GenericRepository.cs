using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public abstract class GenericRepository<T> : IBaseRepository<T> where T: EntityBase
{
    private CarSharingContext db;

    protected GenericRepository(CarSharingContext db)
    {
        this.db = db;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await db.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return db.Set<T>().AsEnumerable();
    }

    public async Task<T> AddAsync(T entity)
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        return null;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Set<T>().AnyAsync(p => p.Id == id);
    }
}