using DataAccess.Entities;

namespace DataAccess.Repositories;

public interface IRolesRepository : IBaseRepository<Roles>
{
    Task<Roles?> GetByUserIdAsync(int id);
}