using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class UserRepository : GenericRepository<User> , IUserRepository
{
    private CarSharingContext db;

    public UserRepository(CarSharingContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}