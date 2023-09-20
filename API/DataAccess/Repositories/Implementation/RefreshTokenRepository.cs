using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class RefreshTokenRepository : GenericRepository<RefreshToken> , IRefreshTokenRepository
{
    private CarSharingContext db;

    public RefreshTokenRepository(CarSharingContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<RefreshToken?> GetByUserId(int id)
    {
        return db.RefreshTokens.FirstOrDefault(f => f.UserId == id);
    }
    
}