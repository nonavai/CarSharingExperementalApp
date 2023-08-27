using BusinessLogic.Models;
using BusinessLogic.Models.User;
using DataAccess.Entities;

namespace BusinessLogic.Services;

public interface IUserService : IBaseService<UserDto>
{
    Task<UserDto?> GetByEmailAsync(string email);
    /*Task RemoveRefreshTokenAsync(int id);
    Task<string?> GetRefreshTokenAsync(int id);
    Task<string> SaveRefreshToken(int id, string refreshToken);*/

    //Task<UserDto> UpdateSome(UserUpdateDto entity);

}