using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class BorrowerRepository : IBorrowerRepository
{
    private CarSharingContext db;
    
    public async Task<Borrower?> GetByIdAsync(int id)
    {
        return await db.Borrowers.FindAsync(id);
    }

    public async Task<IEnumerable<Borrower>> GetAllAsync()
    {
        return db.Borrowers.AsEnumerable();
    }

    public async Task<Borrower> AddAsync(Borrower entity)
    {
        await db.Borrowers.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Borrower> UpdateAsync(Borrower entity)
    {
        db.Entry(entity).State = EntityState.Modified; // проверить!
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<Borrower?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            db.Borrowers.Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        return null;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Borrowers.AnyAsync(p => p.Id == id);
    }
    
}