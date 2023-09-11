using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class LenderRepository : GenericRepository<Lender> , ILenderRepository
{
    private CarSharingContext db;


    public LenderRepository(CarSharingContext db) : base(db)
    {
        this.db = db;
    }
}