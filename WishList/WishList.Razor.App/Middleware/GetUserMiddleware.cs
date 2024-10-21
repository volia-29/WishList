using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WishList.Services.Interfaces;

namespace WishList.Razor.App.Middleware
{
    public class GetUserMiddleware
    {
        private readonly RequestDelegate _next;

        public GetUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            var userId = GetUserId(context); // or however you identify the user
            if (int.TryParse(userId, out var id))
            {
                var user = await userService.GetByIdAsync(id);
                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }

            await _next(context);
        }

        private string? GetUserId(HttpContext context)
        {
            try
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(context.Session.GetString("Token"));
                var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                return claimValue;
            }
            catch (Exception)
            {
                //TODO: Logger.Error
                return null;
            }
        }
    }
}
