using BusinessLogic.Models;
using BusinessLogic.Models.User;
using DataAccess.Entities;

namespace BusinessLogic.Services;

public interface IUserService : IBaseService<UserDto>
{
    Task<UserDto?> GetByEmailAsync(string email);

    //Task<UserDto> UpdateSome(UserUpdateDto entity);

}