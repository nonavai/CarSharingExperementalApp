using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class FeedBackRepository : GenericRepository<FeedBack> , IFeedBackRepository
{
    private CarSharingContext db;
    
    public FeedBackRepository(CarSharingContext db) : base(db)
    {
        this.db = db;
    }
    
    public async Task<IQueryable<FeedBack>> GetByCarId(int id)
    {
        return db.FeedBacks.Where(f => f.CarId == id);
    }

    public async Task<IQueryable<FeedBack>> GetByUserId(int id)
    {
        return db.FeedBacks.Where(f => f.UserId == id);
    }
    
}