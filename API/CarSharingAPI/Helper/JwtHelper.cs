using System.IdentityModel.Tokens.Jwt;

namespace CarSharingAPI.Helper;

public static class JwtHelper
{
    public static async Task<string> GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        if (securityToken == null)
        {
            throw new ArgumentException("Invalid token");
        }
        var claims = securityToken.Claims;
        var userIdClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
        if (userIdClaim == null)
        {
            throw new ArgumentException("Token does not contain a UserId claim");
        }
        return userIdClaim.Value;
    }
}