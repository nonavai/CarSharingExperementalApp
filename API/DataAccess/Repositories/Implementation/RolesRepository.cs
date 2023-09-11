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
}