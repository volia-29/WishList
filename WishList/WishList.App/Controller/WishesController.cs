using Microsoft.AspNetCore.Mvc;
using WishList.BusinessLogic.Models;
using WishList.Services.Interfaces;

namespace WishList.App.Controller
{
    [Route("[controller]")]
    [ApiController]
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
            await wishService.AddWishAsync(wish.UserId, wish);
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
            return Ok(await wishService.DeleteWishAsync(wishId));
        }
    }
}