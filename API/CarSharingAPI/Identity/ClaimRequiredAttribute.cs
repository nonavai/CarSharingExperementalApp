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

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
       
    }
    
}