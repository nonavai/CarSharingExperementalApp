using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;
using DataAccess.Entities;

namespace BusinessLogic.Services;

public interface ITokenService
{
    Task<RefreshTokenDto> GenerateRefreshToken(UserDto userId);
    string GenerateAccessToken(RefreshTokenDto refreshTokenDto);
}