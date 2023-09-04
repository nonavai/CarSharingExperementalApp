using CarSharingAPI.Helper;
using CustomExceptionsLibrary.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarSharingAPI.Identity;

public class ValidateTokenAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Get the token value from the request
        var token = context.HttpContext.Request.Headers["Authorization"];
        // Get the user id from the token
        var userId = JwtHelper.GetUserIdFromToken(token);
        // Get the user id from the request
        var requestUserId = context.HttpContext.Request.Form["id"];
        // Check if the user ids match
        if (userId != requestUserId)
        {
            // Throw an exception if the user ids do not match
            throw new NotVerifiedException("The user is not authorized to access this resource.");
        }
    }
}