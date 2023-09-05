using System.IdentityModel.Tokens.Jwt;
using CustomExceptionsLibrary.Exceptions;

namespace CarSharingAPI.Helper;

public static class JwtHelper
{
    public static async Task<int> GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validToken = token.Split()[^1];
        var securityToken = tokenHandler.ReadJwtToken(validToken);
        if (securityToken == null)
        {
            throw new NotVerifiedException("Invalid token");
        }
        var claims = securityToken.Claims;
        var userIdClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
        if (userIdClaim == null)
        {
            throw new NotVerifiedException("Token does not contain a UserId claim");
        }

        var userId = Convert.ToInt32(userIdClaim.Value);
        return userId;
    }
}