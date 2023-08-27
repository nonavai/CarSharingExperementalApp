using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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

        var rolesSercice = context.HttpContext.RequestServices.GetService<IRolesService>();
        //var roles = await rolesSercice.GetByIdAsync();
    }
}