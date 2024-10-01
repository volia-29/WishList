using System.Security.Claims;
using WishList.Services.Interfaces;

namespace WishList.App.Middleware
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
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier); // or however you identify the user
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
    }
}
