using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BusinessLogic.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace CarSharingAPI.Identity;

public class ClaimRequiredAttribute : Attribute, IAuthorizationFilter
{
    private readonly Claim _claim;

    public ClaimRequiredAttribute(Claim claim)
    {
        _claim = claim;
    }

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.User.FindFirst(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        if (userId == null)
        {
            context.Result = new UnauthorizedResult();
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        //var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        //var requestId = context.HttpContext.
        //var rolesSercice = context.HttpContext.RequestServices.GetService<IRolesService>();
        //var simplePrinciple = Jwtse.GetPrincipal(token);
        //var auth = await context.HttpContext.Request.Headers.Authorization.
        //var roles = await rolesSercice.GetByIdAsync();
    }
    
}