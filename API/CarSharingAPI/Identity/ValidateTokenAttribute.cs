using System.Text.RegularExpressions;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Exceptions;

namespace CarSharingAPI.Identity;

public class ValidateTokenAttribute : ActionFilterAttribute
{
    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        // Get the token value from the request
        
        //var reader = await new StreamReader(context.HttpContext.Request.Body).ReadToEndAsync();
        
        var token = context.HttpContext.Request.Cookies["Authorization"];
        // Get the user id from the token
        var tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();
        var userId = await tokenService.GetUserIdFromToken(token);
        // Get the user id from the request
        /*using var reader = new StreamReader(context.HttpContext.Request.Body);*/

        
        var requestQueryString = context.HttpContext.Request.QueryString;
        var stringRequestUserId = Regex.Match(requestQueryString.Value, @"id=(\d+)").Groups[1].Value;
        var requestUserId = Convert.ToInt32(stringRequestUserId);
        

        // Check if the user ids match
        if (userId != requestUserId)
        {
            // Throw an exception if the user ids do not match
            throw new NotVerifiedException("The user is not authorized to access this resource.");
        }
    }
}