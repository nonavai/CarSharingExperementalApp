using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;
using DataAccess.Entities;

namespace BusinessLogic.Services;

public interface ITokenService
{
    Task<RefreshTokenDto> GenerateRefreshToken(UserDto userId);
    Task<string> GenerateAccessToken(RefreshTokenDto refreshTokenDto);
    Task<RefreshTokenDto> GetByUserIdAsync(int id);
    Task<int> GetUserIdFromToken(string token);
}