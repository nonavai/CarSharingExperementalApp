using BusinessLogic.Models.FeedBack;

namespace BusinessLogic.Services;

public interface IFeedBackService : IBaseService<FeedBackDto>
{
    Task<IQueryable<FeedBackDto>> GetByCarId(int id);
    Task<IQueryable<FeedBackDto>> GetByUserId(int id);
}