using System.Net;
using System.Text.Json;
using WishList.Services.Exceptions;

namespace WishList.App.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException appException)
            {
                context.Response.StatusCode = (int)appException.StatusCode;
                var response = new {message = appException.Message};
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new { message = "Something went wrong" };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
