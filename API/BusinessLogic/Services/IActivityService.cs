using BusinessLogic.Models.Activity;
using DataAccess.Entities;

namespace BusinessLogic.Services;

public interface IActivityService : IBaseService<ActivityDto>
{
    Task<ActivityDto?> GetByCarIdAsync(int id);
    Task<IQueryable<ActivityDto>> GetByRadiusAsync(ActivityDtoGeo dto);
}