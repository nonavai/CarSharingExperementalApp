using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarSharingAPI.Identity;

public class ValidateTokenAttribute : ActionFilterAttribute
{
    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        // Get the token value from the request
        var token = context.HttpContext.Request.Headers["Authorization"];
        // Get the user id from the token
        var tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();
        var userId = await tokenService.GetUserIdFromToken(token);
        // Get the user id from the request
        using var reader = new StreamReader(context.HttpContext.Request.Body);
        reader.BaseStream.Seek(0, SeekOrigin.Begin); 
        var body = await reader.ReadToEndAsync();
        
        // Check if the user ids match
        /*if (userId != requestUserId)
        {
            // Throw an exception if the user ids do not match
            throw new NotVerifiedException("The user is not authorized to access this resource.");
        }*/
        var a = 2;
    }
}