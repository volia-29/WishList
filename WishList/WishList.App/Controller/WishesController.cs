using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishList.BusinessLogic.Models;
using WishList.Services.Exceptions;
using WishList.Services.Interfaces;

namespace WishList.App.Controller
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class WishesController : ControllerBase
    {
        private readonly IWishService wishService;

        public WishesController(IWishService wishService)
        {
            this.wishService = wishService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateWishAsync(CreateWishDto wish)
        {
            var currentUser = (Infrastructure.Models.User)HttpContext.Items["User"];
            await wishService.AddWishAsync(currentUser, wish);
            return Ok();
        }

        [HttpGet("all-wishes")]
        public async Task<ActionResult> GetUserWishesAsync(int userId)
        {
            return Ok(await wishService.GetAllUserWishesAsync(userId));
        }

        [HttpGet("available-wishes")]
        public async Task<ActionResult> GetAllWishesAsync(int userId)
        {
            return Ok(await wishService.GetAvailableUserWishesAsync(userId));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWish(int wishId)
        {
            var wish = await wishService.GetWishById(wishId);
            var currentUser = (Infrastructure.Models.User)HttpContext.Items["User"];
            if (wish.UserId != currentUser.Id)
            {
                throw new AppException() { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "It is a wish of another user." };
            }

            return Ok(await wishService.DeleteWishAsync(wishId));
        }
    }
}