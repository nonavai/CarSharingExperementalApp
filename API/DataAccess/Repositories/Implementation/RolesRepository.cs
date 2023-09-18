using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class RolesRepository : GenericRepository<Roles> , IRolesRepository
{
    private CarSharingContext db;


    public RolesRepository(CarSharingContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<Roles?> GetByUserIdAsync(int id)
    {
        return db.Roles.FirstOrDefault(f => f.UserId == id);
    }
}