using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class RolesRepository : IRolesRepository
{
    private CarSharingContext db;

    public RolesRepository(CarSharingContext db)
    {
        this.db = db;
    }

    public async Task<Roles?> GetByIdAsync(int id)
    {
        return await db.Roles.FindAsync(id);
    }

    public async Task<IEnumerable<Roles>> GetAllAsync()
    {
        return db.Roles.AsEnumerable();
    }

    public async Task<Roles> AddAsync(Roles entity)
    {
        await db.Roles.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Roles> UpdateAsync(Roles entity)
    {
        db.Entry(entity).State = EntityState.Modified; // проверить!
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Roles?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return null;
        db.Roles.Remove(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Lenders.AnyAsync(p => p.Id == id);
    }
}