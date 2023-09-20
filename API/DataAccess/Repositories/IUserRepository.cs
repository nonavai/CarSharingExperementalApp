using DataAccess.Entities;

namespace DataAccess.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    //Task<string?> GetRefreshTokenAsync(int id);
}