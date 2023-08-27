using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    private CarSharingContext db;

    public UserRepository(CarSharingContext db)
    {
        this.db = db;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await db.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return db.Users.AsEnumerable();
    }

    public async Task<User> AddAsync(User entity)
    {
        await db.Users.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<User> UpdateAsync(User entity)
    {
        db.Entry(entity).State = EntityState.Modified; // проверить!
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<User?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return null;
        db.Users.Remove(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Users.AnyAsync(p => p.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    
    /*public async Task<string?> GetRefreshTokenAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        return user.Token;
    }*/
    
    
}