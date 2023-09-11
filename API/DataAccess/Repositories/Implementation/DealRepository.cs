using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class DealRepository : GenericRepository<Deal> ,IDealRepository
{
    private CarSharingContext db;

    public DealRepository(CarSharingContext db) : base(db)
    {
        this.db = db;
    }
}