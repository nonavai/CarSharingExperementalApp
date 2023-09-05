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
    private readonly IRolesService _rolesService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    

    public TokenService( IMapper mapper, IConfiguration configuration, IRolesService rolesService, IRefreshTokenRepository refreshTokenRepository)
    {
        _mapper = mapper;
        _configuration = configuration;
        _rolesService = rolesService;
        _refreshTokenRepository = refreshTokenRepository;
    }
    public async Task<RefreshTokenDto> GenerateRefreshToken(UserDto user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds);
        
        var refreshTokenDto = new RefreshTokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserId = user.Id,
            UserRoleId = user.RoleId,
            ExpiresAt = token.ValidTo
        };
        var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
        var newRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        refreshTokenDto.Id = newRefreshToken.Id;
        return refreshTokenDto;
    }
    public async Task<string> GenerateAccessToken(RefreshTokenDto refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = key,
            ValidAudience = _configuration["JwtSettings:Audience"],
            ValidIssuer = _configuration["JwtSettings:Issuer"],
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
            new Claim(JwtRegisteredClaimNames.Jti, refreshToken.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
        };
        await AddClaimRoles(refreshToken.UserRoleId, claims);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(10),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<RefreshTokenDto?> GetByUserId(int id)
    {
        var refreshToken = await _refreshTokenRepository.GetByUserId(id);
        if (refreshToken == null)
        {
            return null;
        }
        var refreshTokenDto = _mapper.Map<RefreshTokenDto>(refreshToken);
        return refreshTokenDto;
    }


    private async Task AddClaimRoles(int roleId, List<Claim> claims)
    {
        var roles = await _rolesService.GetByIdAsync(roleId);
        if (roles.Admin) claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        if (roles.BorrowerId.HasValue) claims.Add(new Claim(ClaimTypes.Role, "Borrower"));
        if (roles.LenderId.HasValue) claims.Add(new Claim(ClaimTypes.Role, "Lender"));
    }
    
}