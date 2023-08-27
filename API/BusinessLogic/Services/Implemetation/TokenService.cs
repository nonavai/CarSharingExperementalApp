using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Services.Implemetation;

public class TokenService : ITokenService
{
    private readonly IRefreshTokenRepository _tokenRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    

    public TokenService(IRefreshTokenRepository tokenRepository, IMapper mapper, IConfiguration configuration)
    {
        _tokenRepository = tokenRepository;
        _mapper = mapper;
        _configuration = configuration;
    }
    public async Task<RefreshTokenDto> GenerateRefreshToken(UserDto user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Acr, user.RoleId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds);
        
        return new RefreshTokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserId = user.Id,
            UserRoleId = user.RoleId,
            ExpiresAt = token.ValidTo
        };
    }

    public string GenerateAccessToken(RefreshTokenDto refreshTokenDto)
    {
        throw new NotImplementedException();
    }

    /*public async Task<AccessToken> GenerateAccessToken(RefreshTokenDto refreshTokenDto)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, refreshTokenDto.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Acr, refreshTokenDto.UserRoleId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        return new AccessToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = token.ValidTo,
        };
    }*/
    public string UpdateAccessToken(RefreshTokenDto refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = key,
            ValidAudience = _configuration["Jwt:Audience"],
            ValidIssuer = _configuration["Jwt:Issuer"],
            ClockSkew = TimeSpan.Zero
        };
        SecurityToken validatedToken;
        try
        {
            tokenHandler.ValidateToken(refreshToken.Token, validationParameters, out validatedToken);
        }
        catch (SecurityTokenException)
        {
            return null;
        }
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, refreshToken.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Acr, refreshToken.UserRoleId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
        };
        
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}