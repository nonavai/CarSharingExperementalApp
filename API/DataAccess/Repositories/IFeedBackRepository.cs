using DataAccess.Entities;

namespace DataAccess.Repositories.Implementation;

public interface IFeedBackRepository : IBaseRepository<FeedBack>
{
    Task<IQueryable<FeedBack>> GetByCarId(int id);
    Task<IQueryable<FeedBack>> GetByUserId(int id);
}