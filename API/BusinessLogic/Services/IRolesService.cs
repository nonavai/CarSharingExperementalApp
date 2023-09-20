using BusinessLogic.Models.Roles;
using DataAccess.Entities;

namespace BusinessLogic.Services;

public interface IRolesService : IBaseService<RolesDto>
{
    Task<RolesDto> GetByUserIdAsync(int id);
}