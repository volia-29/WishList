using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishList.BusinessLogic.Models;
using WishList.Services.Interfaces;

namespace WishList.App.Controller
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class WishController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IWishService wishService;

        public WishController(IUserService userService, IWishService wishService)
        {
            this.userService = userService;
            this.wishService = wishService;
        }

        [HttpPost]
        public async Task<ActionResult> ChoiceOfWishAsync(ChoseWishDto choseWish)
        {
            var user = await userService.GetByIdAsync(choseWish.UserId);
            await wishService.ChooseWishAsync(user, choseWish);
            return Ok();
        }
    }
}
