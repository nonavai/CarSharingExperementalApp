using DataAccess.DbContext;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public class BorrowerRepository : GenericRepository<Borrower>, IBorrowerRepository
{
    private CarSharingContext db;
    public BorrowerRepository(CarSharingContext db) : base(db)
    {
        this.db = db;
    }
}